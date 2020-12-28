using System.Drawing.Text;
using System.Linq;
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
            var v = new InstalledFontCollection().Families.Select(font => font.Name);
        }

        private void FontSizeComboBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
