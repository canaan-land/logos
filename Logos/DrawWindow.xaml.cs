using System;
using System.Windows;
using System.Windows.Controls;
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

            float dpiX, dpiY;
            using (var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(
                Convert.ToInt32(SystemParameters.PrimaryScreenWidth * dpiX / 96),
                Convert.ToInt32(SystemParameters.PrimaryScreenHeight * dpiY / 96));
            using (var g = System.Drawing.Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, screenshot.Size);
            }
            DrawPanel.Background = CreateBrushFromBitmap(screenshot);
        }

        private static Brush CreateBrushFromBitmap(System.Drawing.Bitmap bitmap)
        {
            var source = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return new ImageBrush(source);
        }

        private void DrawPanel_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindow).StopDraw();
        }

        private Point? savedPosition;

        private void DrawPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            savedPosition = e.GetPosition(sender as UIElement);

            Line line = GetLine(savedPosition.Value, savedPosition.Value);

            (sender as Panel).Children.Add(line);
        }

        private void DrawPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            savedPosition = null;
        }

        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (savedPosition.HasValue)
            {
                Point currentPosition = e.GetPosition(sender as UIElement);
                Line line = GetLine(savedPosition.Value, currentPosition);

                savedPosition = currentPosition;

                (sender as Panel).Children.Add(line);
            }
        }

        private static Brush StringToSolidColorBrush(string color)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }

        private Line GetLine(Point point1, Point point2)
        {
            Line line = new Line();

            DisplayData displayData = (DataContext as MainWindow).displayData;
            line.Stroke = StringToSolidColorBrush(displayData.DrawPenColor);
            line.StrokeThickness = displayData.DrawPenWidth;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;
            line.X1 = point1.X;
            line.Y1 = point1.Y;
            line.X2 = point2.X;
            line.Y2 = point2.Y;

            return line;
        }
    }
}
