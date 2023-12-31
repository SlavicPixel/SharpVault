﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Ookii.Dialogs.Wpf;

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateVault : Window
    {
        private DataContainer _dataContainer = new DataContainer();
        public string path;
        public CreateVault(DataContainer dataContainer)
        {
            InitializeComponent();

            _dataContainer = dataContainer;
        }

       
        private void SelectDirectoryClick(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();

            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog(this))
            {
                path = $"{Environment.NewLine}{dialog.SelectedPath}";
                path = path.Trim();
                selectedDirectory.SelectedText = path;
            }

        }

        private void CreateVaultClick(object sender, RoutedEventArgs e)
        {
            if (pass1.Text == pass2.Text)
            {
                if ((!string.IsNullOrEmpty(newVaultName.Text) && !string.IsNullOrEmpty(pass1.Text) && !string.IsNullOrEmpty(pass2.Text) && !string.IsNullOrEmpty(path)))
                {
                    string vaultPath = $@"{path}\" + $"{newVaultName.Text}.svdb";

                    VaultManagement newVault = new VaultManagement(newVaultName.Text, pass1.Text);
                    newVault.NewVault(vaultPath);

                    _dataContainer.Vault = newVault;
                    _dataContainer.vaultPath = vaultPath;

                    DialogResult = true;
                    MessageBox.Show(this, $"Vault {newVaultName.Text} has been successfully created.");

                    Close();
                }
                else MessageBox.Show("All fields are required and must be filled");
            }
            else MessageBox.Show("Passwords do not match!");
        }
    }
}
