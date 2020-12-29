using System.Text.RegularExpressions;
using System.Windows.Controls;

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

        private void FontSizeComboBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DisplayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as MainWindow).PerformTextDisplay();
        }

        private void TextContent_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            StringTextBox.Focus();
        }
    }
}
