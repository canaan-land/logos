using System.Reflection;
using System.Windows.Controls;

namespace Logos
{
    /// <summary>
    /// AboutContent.xaml 的互動邏輯
    /// </summary>
    public partial class AboutContent : UserControl
    {
        public static readonly string copyright = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

        public AboutContent()
        {
            InitializeComponent();
        }
    }
}
