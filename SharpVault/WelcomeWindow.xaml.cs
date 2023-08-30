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
                dataContainer.encrypted = true;
                MainWindow mainWindow = new MainWindow(dataContainer, dataContainer.vaultPath);
                mainWindow.Show();
                Close();
            }
        }
        
        private void openVaultClick(object sender, RoutedEventArgs e)
        {
            VaultManagement vaultManagement = new VaultManagement();
            PasswordPrompt passwordprompt = new PasswordPrompt();
            DataContainer dataContainer = new DataContainer();

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
                    string decryptedVault = vaultManagement.OpenVault(dialog.FileName, password);

                    if (decryptedVault != "Error")
                    {
                        dataContainer.decryptedVault = decryptedVault;
                        dataContainer.encrypted = false;

                        MainWindow mainWindow = new MainWindow(dataContainer, vaultPath);
                        mainWindow.Show();
                        Close();
                    }
                    else MessageBox.Show("Error while reading the database: Invalid credentials were provided, please try again.");

                }
            }
        }
    }
}
