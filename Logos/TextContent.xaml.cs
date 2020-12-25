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
using System.Windows.Markup;
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
        public static ObservableCollection<string> FontCollection => new ObservableCollection<string>
            (from font in new InstalledFontCollection().Families select font.Name);

        public TextContent()
        {
            InitializeComponent();
        }
    }

    public class StringToFontFamilyConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FontFamily fontFamily = new FontFamily("微軟正黑體");
            if (value != null)
            {
                fontFamily = new FontFamily(value.ToString());
            }
            return fontFamily;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
