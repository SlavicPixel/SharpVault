using System;
using System.Collections.Generic;
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

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        private DataContainer _dataContainer;
        public NewEntry(DataContainer dataContainer)
        {
            InitializeComponent();

            _dataContainer = dataContainer;



        }

        private void newEntryClick(object sender, RoutedEventArgs e)
        {
            EntryModel newEntry = new EntryModel(_dataContainer.lastID, newTitle.Text, newUsername.Text, newPassword.Text, newURL.Text, newNotes.Text);

            _dataContainer.Entry = newEntry;

            DialogResult = true;

            Close();
        }
    }
}
