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
    /// Interaction logic for PasswordPrompt.xaml
    /// </summary>
    public partial class PasswordPrompt : Window
    {
        public PasswordPrompt()
        {
            InitializeComponent();
        }

        public string EnteredPassword()
        {
            return enteredPassword.Text;
        }

        private void DecryptVaultClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
