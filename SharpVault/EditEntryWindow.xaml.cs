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

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for EditEntryWindow.xaml
    /// </summary>
    public partial class EditEntryWindow : Window
    {
        private DataContainer _dataContainer;
        public EditEntryWindow(DataContainer dataContainer)
        {
            InitializeComponent();

            _dataContainer = dataContainer;

            updatedTitle.Text = _dataContainer.Entry.Title;
            updatedUsername.Text = _dataContainer.Entry.Username;
            updatedPassword.Text = _dataContainer.Entry.Password;
            updatedURL.Text = _dataContainer.Entry.Url;
            updatedNotes.Text = _dataContainer.Entry.Notes;
        }

        private void updateEntryClick(object sender, RoutedEventArgs e)
        {
            EntryModel updatedEntry = _dataContainer.Entry;

            updatedEntry.Title = updatedTitle.Text;
            updatedEntry.Username = updatedUsername.Text;
            updatedEntry.Password = updatedPassword.Text;
            updatedEntry.Url = updatedPassword.Text;
            updatedEntry.Notes = updatedNotes.Text;

            _dataContainer.Entry = updatedEntry;

            DialogResult = true;

            Close();
        }

        private void generatePasswordClick(object sender, RoutedEventArgs e)
        {
            PasswordGeneratorWindow passwordGeneratorWindow = new PasswordGeneratorWindow();
            passwordGeneratorWindow.savePasswordBtn.Visibility = Visibility.Visible;

            bool? result = passwordGeneratorWindow.ShowDialog();

            if ((bool)result && (bool)passwordGeneratorWindow.DialogResult)
            {
                updatedPassword.Text = passwordGeneratorWindow.generatedPassword;
            }
        }
    }
}
