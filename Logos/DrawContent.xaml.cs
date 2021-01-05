using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Logos
{
    /// <summary>
    /// DrawContent.xaml 的互動邏輯
    /// </summary>
    public partial class DrawContent : UserControl
    {
        public DrawContent()
        {
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as MainWindow).StartDraw();
        }
    }
}
