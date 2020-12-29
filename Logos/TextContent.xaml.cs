using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Logos
{
    /// <summary>
    /// TextContent.xaml 的互動邏輯
    /// </summary>
    public partial class TextContent : UserControl
    {
        public TextContent()
        {
            InitializeComponent();
        }

        private void FontSizeComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DisplayButton_Checked(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).Display();
        }

        private void DisplayButton_Unchecked(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).Undisplay();
        }

        private void TextContent_Loaded(object sender, RoutedEventArgs e)
        {
            StringTextBox.Focus();
        }

        private void StringTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!(DataContext as MainWindow).displayData.IsTextDisplay)
                {
                    DisplayButton.IsChecked = true;
                }
            }
        }
    }
}
