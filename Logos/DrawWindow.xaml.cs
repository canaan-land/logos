using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Logos
{
    /// <summary>
    /// DrawWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DrawWindow : Window
    {
        public DrawWindow()
        {
            InitializeComponent();

            Bitmap screenshot = new Bitmap(
                Convert.ToInt32(SystemParameters.PrimaryScreenWidth),
                Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, screenshot.Size);
            }
            DrawGrid.Background = CreateBrushFromBitmap(screenshot);
        }

        private static System.Windows.Media.Brush CreateBrushFromBitmap(Bitmap bitmap)
        {
            var source = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return new ImageBrush(source);
        }

        private void DrawGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindow).StopDraw();
        }
    }
}
