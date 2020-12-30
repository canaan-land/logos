using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Logos
{
    /// <summary>
    /// DisplayWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DisplayWindow : Window
    {
        public DisplayWindow()
        {
            InitializeComponent();

            DisplayText.RenderTransform = new TranslateTransform(prevX, prevY);
        }

        private bool isDragging;
        private Point savedPosition;
        private static double prevX, prevY;

        private void DisplayText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var control = sender as UIElement;
            savedPosition = e.GetPosition(Parent as UIElement);
            control.CaptureMouse();
        }

        private void DisplayText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var control = sender as UIElement;
            var transform = control.RenderTransform as TranslateTransform;
            prevX = transform.X;
            prevY = transform.Y;
            control.ReleaseMouseCapture();
        }        

        private void DisplayText_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(Parent as UIElement);
                var control = sender as UIElement;
                var transform = control.RenderTransform as TranslateTransform;
                transform.X += currentPosition.X - savedPosition.X;
                transform.Y += currentPosition.Y - savedPosition.Y;
                savedPosition = currentPosition;
            }
        }
        
        private void DisplayText_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var control = sender as FrameworkElement;
            var displayData = control.DataContext as DisplayData;
            if (e.Delta > 0 || (e.Delta < 0 && displayData.TextFontSize > 10))
            {
                Point pointOld = e.GetPosition(control);
                double ratioX = pointOld.X / control.ActualWidth;
                double ratioY = pointOld.Y / control.ActualHeight;
                displayData.TextFontSize += e.Delta / 120 * 6;
                control.UpdateLayout();
                Point pointNew = new Point(
                        control.ActualWidth * ratioX, control.ActualHeight * ratioY);
                Vector pointDiff = Point.Subtract(pointNew, pointOld);
                var transform = control.RenderTransform as TranslateTransform;
                transform.X -= pointDiff.X;
                transform.Y -= pointDiff.Y;
                prevX = transform.X;
                prevY = transform.Y;
            }
        }
    }
}
