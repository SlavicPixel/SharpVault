using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SharpVault.models;
using System.ComponentModel;

namespace SharpVault
{
    /// <summary>
    /// Interaction logic for PasswordGeneratorWindow.xaml
    /// </summary>
    public partial class PasswordGeneratorWindow : Window
    {
        public string generatedPassword = string.Empty;
        int passwordLength = 12;
        private bool dragStarted = false;
        public PasswordGeneratorWindow()
        {
            InitializeComponent();

            setPassword(passwordLength);
        }

        private void setPassword(int passwordLength)
        {
            PasswordGenerator.Clear();

            if (uppercaseBox != null && (bool)uppercaseBox.IsChecked) PasswordGenerator.Add(new UpperCaseGenerator());
            if (lowercaseBox != null && (bool)lowercaseBox.IsChecked) PasswordGenerator.Add(new LowerCaseGenerator());
            if (numbersBox != null && (bool)numbersBox.IsChecked) PasswordGenerator.Add(new NumericGenerator());
            if (specialCharsBox != null && (bool)specialCharsBox.IsChecked) PasswordGenerator.Add(new SpecialCharGenerator());

            if (PasswordGenerator.IsEmpty())
            {
                lowercaseBox.IsChecked = true;
                PasswordGenerator.Add(new LowerCaseGenerator());
            }

            if (passwordLengthSlider != null) passwordLength = Convert.ToInt32(passwordLengthSlider.Value);
            generatedPassword = PasswordGenerator.GeneratePassword(passwordLength);
            generatedPasswordTextBox.Text = generatedPassword;
        }

        private void generatePasswordClick(object sender, RoutedEventArgs e)
        {
            setPassword(passwordLength);
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            HandleSliderValueChanged(((Slider)sender).Value);
            this.dragStarted = false;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.dragStarted = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!dragStarted)
                HandleSliderValueChanged(e.NewValue);
        }

        private void HandleSliderValueChanged(double newValue)
        {
            newValue = Convert.ToInt32(newValue);
            passwordLengthLabel.Content = $"Lenght: {newValue}";
            passwordLength = (int)newValue;
            setPassword(passwordLength);
        }

        private void uppercaseBoxChanged(object sender, RoutedEventArgs e)
        {
            setPassword(passwordLength);
        }

        private void lowercaseBoxChanged(object sender, RoutedEventArgs e)
        {
            setPassword(passwordLength);
        }

        private void numbersBoxChanged(object sender, RoutedEventArgs e)
        {
            setPassword(passwordLength);
        }

        private void specialCharsBoxChanged(object sender, RoutedEventArgs e)
        {
            setPassword(passwordLength);
        }

        private void savePasswordClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
