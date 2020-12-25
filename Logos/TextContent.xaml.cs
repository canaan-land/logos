using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logos
{
    /// <summary>
    /// TextContent.xaml 的互動邏輯
    /// </summary>
    public partial class TextContent : UserControl
    {
        public ObservableCollection<string> FontCollection => new ObservableCollection<string>(
            from font in new InstalledFontCollection().Families select font.Name);

        public TextContent()
        {
            InitializeComponent();
        }
    }
}
