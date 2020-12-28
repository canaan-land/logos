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

        private void SendButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DisplayWindow displayWindow = new DisplayWindow()
            {
                DataContext = DataContext
            };
            displayWindow.Show();
        }
    }
}
