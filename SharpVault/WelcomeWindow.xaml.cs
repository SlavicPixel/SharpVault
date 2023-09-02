using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void newVaultClick(object sender, RoutedEventArgs e)
        {
            DataContainer dataContainer = new DataContainer();

            CreateVault createVault = new CreateVault(dataContainer);

            bool? result = createVault.ShowDialog();

            if ((bool)result)
            {
                dataContainer.decryptedVault = dataContainer.Vault.DecryptVault(dataContainer.vaultPath, dataContainer.Vault.GetMasterKey());
                MainWindow mainWindow = new MainWindow(dataContainer);

                if (mainWindow.Visibility == Visibility.Hidden) mainWindow.Visibility = Visibility.Visible;
                else mainWindow.Show();

                this.Visibility = Visibility.Hidden;
            }
        }
        
        private void openVaultClick(object sender, RoutedEventArgs e)
        {
            PasswordPrompt passwordprompt = new PasswordPrompt();
            DataContainer dataContainer = new DataContainer();
            VaultManagement vaultManagement = new VaultManagement();

            VistaOpenFileDialog dialog = new VistaOpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";

            if (!VistaFileDialog.IsVistaFileDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular open file dialog will be used. Please use Windows Vista to see the new dialog.", "Sample open file dialog");
            if ((bool)dialog.ShowDialog(this))
            {
                string vaultPath = dialog.FileName;
                string extension = System.IO.Path.GetExtension(vaultPath);

                if (extension != ".svdb") MessageBox.Show("Error while reading the database: Not a SharpVault database.");
                else if (passwordprompt.ShowDialog() == true)
                {
                    string password = passwordprompt.EnteredPassword();

                    vaultManagement.SetVaultName(System.IO.Path.GetFileNameWithoutExtension(vaultPath));
                    vaultManagement.SetMasterKey(password);

                    dataContainer.Vault = vaultManagement;
                    dataContainer.decryptedVault = vaultManagement.DecryptVault(vaultPath, password);
                    dataContainer.vaultPath = vaultPath;


                    if (dataContainer.decryptedVault != "Error")
                    {
                        MainWindow mainWindow = new MainWindow(dataContainer);
                        
                        if (mainWindow.Visibility == Visibility.Hidden) mainWindow.Visibility = Visibility.Visible;
                        else mainWindow.Show();
                        
                        this.Visibility = Visibility.Hidden;
                    }
                    else MessageBox.Show("Error while reading the database: Invalid credentials were provided or the database is corrupted, please try again.");

                }
            }
        }
    }
}
