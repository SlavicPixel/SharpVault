using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<EntryModel> entries = new ObservableCollection<EntryModel>();
        public string vaultPath = string.Empty;
        string password = string.Empty;
        private DataContainer _dataContainer;
        public MainWindow(DataContainer dataContainer, string vaultPath)
        {
            InitializeComponent();

            _dataContainer = dataContainer;
            VaultManagement vaultManagement = new VaultManagement();
            this.vaultPath = vaultPath;

            VaultManagement encryptedVault = _dataContainer.Vault;
            string decryptedVault;

            if (_dataContainer.encrypted)
            {
                decryptedVault = vaultManagement.OpenVault(vaultPath, encryptedVault.GetMasterKey());
                password = encryptedVault.GetMasterKey();
            } 
            else
            {
                decryptedVault = dataContainer.decryptedVault;
            }
            
            entries = vaultManagement.InitializeVault(decryptedVault);
            entriesView.ItemsSource = entries;
        } 

        private void createVaultClick(object sender, RoutedEventArgs e) 
        { 
            DataContainer dataContainer = new DataContainer();
            CreateVault createVault = new CreateVault(dataContainer);
            createVault.ShowDialog();

            vaultPath = dataContainer.vaultPath;
            VaultManagement encryptedVault = dataContainer.Vault;
            password = encryptedVault.GetMasterKey();
            string decryptedVault = encryptedVault.OpenVault(vaultPath, encryptedVault.GetMasterKey());


            entries = encryptedVault.InitializeVault(decryptedVault);
            entriesView.ItemsSource = entries;
        }

        private void openVaultClick(object sender, RoutedEventArgs e)
        {
            VaultManagement vaultManagement = new VaultManagement();
            PasswordPrompt passwordprompt = new PasswordPrompt();

            VistaOpenFileDialog dialog = new VistaOpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";

            if (!VistaFileDialog.IsVistaFileDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular open file dialog will be used. Please use Windows Vista to see the new dialog.", "Sample open file dialog");
            if ((bool)dialog.ShowDialog(this))
            {
                vaultPath = dialog.FileName;
                string extension = System.IO.Path.GetExtension(vaultPath);

                if (extension != ".svdb") MessageBox.Show("Error while reading the database: Not a SharpVault database.");
                else if (passwordprompt.ShowDialog() == true)
                {
                    password = passwordprompt.EnteredPassword();
                    string decryptedVault = vaultManagement.OpenVault(dialog.FileName, password);

                    if (decryptedVault != "Error")
                    {
                        entries = vaultManagement.InitializeVault(decryptedVault);
                        entriesView.ItemsSource = entries;
                    }
                    else MessageBox.Show("Error while reading the database: Invalid credentials were provided, please try again.");

                }
            }
        }

        private void addAccountClick(object sender, RoutedEventArgs e)
        {
            DataContainer dataContainer = new DataContainer();

            if (entries.Count != 0) dataContainer.lastID = entries[entries.Count - 1].ID + 1;

            NewEntry newEntryWindow = new NewEntry(dataContainer);

            bool? result = newEntryWindow.ShowDialog();

            if (result == true)
            {
                VaultManagement vaultManagement = new VaultManagement();

                EntryModel newEntry = dataContainer.Entry;
                entries.Add(newEntry);

                Console.WriteLine(vaultPath);

                vaultManagement.UpdateVault(entries, password, vaultPath);
            }
        }

        private void editAccountClick(object sender, RoutedEventArgs e)
        {
            if (entriesView.SelectedIndex != -1)
            {
                DataContainer dataContainer = new DataContainer();
                dataContainer.Entry = entriesView.SelectedItem as EntryModel;
                EditEntryWindow editEntryWindow = new EditEntryWindow(dataContainer);

                bool? result = editEntryWindow.ShowDialog();

                if (result == true)
                {
                    EntryModel updatedEntry = dataContainer.Entry;

                    // Update properties of the existing EntryModel object
                    foreach (EntryModel entry in entries)
                    {
                        if (entry.ID == updatedEntry.ID)
                        {
                            VaultManagement vaultManagement = new VaultManagement();

                            entry.Title = updatedEntry.Title;
                            entry.Username = updatedEntry.Username;
                            entry.Password = updatedEntry.Password;
                            entry.Url = updatedEntry.Url;
                            entry.Notes = updatedEntry.Notes;

                            vaultManagement.UpdateVault(entries, password, vaultPath);

                            break;
                        }
                    }
                }
            }

        }

        private void deleteAccountClick(object sender, RoutedEventArgs e)
        {
            if (entriesView.SelectedIndex != -1)
            {
                EntryModel entryToBeDelted = entriesView.SelectedItem as EntryModel;
                foreach (EntryModel entry in entries)
                {
                    if (entry.ID == entryToBeDelted.ID)
                    {
                        VaultManagement vaultManagement = new VaultManagement();

                        entries.Remove(entry);
                        vaultManagement.UpdateVault(entries, password, vaultPath);

                        break;
                    }
                }
            }
        }

    }



    
}
