using System.Windows.Controls;

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
