using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Controls;

namespace Logos
{
    /// <summary>
    /// TextContent.xaml 的互動邏輯
    /// </summary>
    public partial class TextContent : UserControl
    {
        public static ObservableCollection<string> FontCollection => new ObservableCollection<string>
            (from font in new InstalledFontCollection().Families select font.Name);

        public TextContent()
        {
            InitializeComponent();
        }
    }
}
