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

        private Point? savedPosition;
        private static double prevX, prevY;

        private void DisplayText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as UIElement;
            savedPosition = e.GetPosition(Parent as UIElement);
            control.CaptureMouse();
        }

        private void DisplayText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            savedPosition = null;
            var control = sender as UIElement;
            var transform = control.RenderTransform as TranslateTransform;
            prevX = transform.X;
            prevY = transform.Y;
            control.ReleaseMouseCapture();
        }

        private void DisplayText_MouseMove(object sender, MouseEventArgs e)
        {
            if (savedPosition.HasValue)
            {
                Point currentPosition = e.GetPosition(Parent as UIElement);
                if (currentPosition.Equals(savedPosition.Value))
                {
                    return;
                }
                var control = sender as UIElement;
                var transform = control.RenderTransform as TranslateTransform;
                transform.X += currentPosition.X - savedPosition.Value.X;
                transform.Y += currentPosition.Y - savedPosition.Value.Y;
                savedPosition = currentPosition;
            }
        }

        private void DisplayText_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindow).MainDisplayData.IsTextDisplay = false;
        }

        private void DisplayText_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var control = sender as FrameworkElement;
            var displayData = (DataContext as MainWindow).MainDisplayData;
            if (e.Delta > 0 || (e.Delta < 0 && displayData.TextFontSize > 10))
            {
                Point pointOld = e.GetPosition(control);
                double ratioX = pointOld.X / control.ActualWidth;
                double ratioY = pointOld.Y / control.ActualHeight;
                displayData.TextFontSize += e.Delta / 120 * 6;
                control.UpdateLayout();
                Point pointNew = new(
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
