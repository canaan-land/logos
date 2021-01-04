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

            System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(
                Convert.ToInt32(SystemParameters.PrimaryScreenWidth),
                Convert.ToInt32(SystemParameters.PrimaryScreenHeight));
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(screenshot))
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

            Line line = new Line();

            DisplayData displayData = (DataContext as MainWindow).displayData;
            line.Stroke = StringToSolidColorBrush(displayData.DrawPenColor);
            line.StrokeThickness = displayData.DrawPenWidth;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;
            line.X1 = savedPosition.Value.X;
            line.Y1 = savedPosition.Value.Y;
            line.X2 = savedPosition.Value.X;
            line.Y2 = savedPosition.Value.Y;

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
                Line line = new Line();

                DisplayData displayData = (DataContext as MainWindow).displayData;
                line.Stroke = StringToSolidColorBrush(displayData.DrawPenColor);
                line.StrokeThickness = displayData.DrawPenWidth;
                line.StrokeStartLineCap = PenLineCap.Round;
                line.StrokeEndLineCap = PenLineCap.Round;
                line.X1 = savedPosition.Value.X;
                line.Y1 = savedPosition.Value.Y;
                Point currentPosition = e.GetPosition(sender as UIElement);
                line.X2 = currentPosition.X;
                line.Y2 = currentPosition.Y;

                savedPosition = currentPosition;

                (sender as Panel).Children.Add(line);
            }
        }

        private static Brush StringToSolidColorBrush(string color)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }
    }
}
