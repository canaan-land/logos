using System.Windows;
using System.Windows.Controls;

namespace Logos
{
    /// <summary>
    /// DrawContent.xaml 的互動邏輯
    /// </summary>
    public partial class DrawContent : UserControl
    {
        public DisplayData MainDisplayData { get; set; }

        public DrawContent(DisplayData displayData)
        {
            MainDisplayData = displayData;
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow).StartDraw();
        }
    }
}
