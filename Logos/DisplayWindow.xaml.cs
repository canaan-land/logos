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
        private Point clickPosition;
        private static double prevX, prevY;

        private void DisplayText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var control = sender as OutlinedTextBlock;
            clickPosition = e.GetPosition(this);
            control.CaptureMouse();
        }

        private void DisplayText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var control = sender as OutlinedTextBlock;
            var transform = (control.RenderTransform as TranslateTransform);
            prevX = transform.X;
            prevY = transform.Y;
            control.ReleaseMouseCapture();
        }

        private void DisplayText_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && sender is OutlinedTextBlock control)
            {
                Point currentPosition = e.GetPosition(Parent as UIElement);

                var transform = (control.RenderTransform as TranslateTransform);
                transform.X = currentPosition.X - clickPosition.X + prevX;
                transform.Y = currentPosition.Y - clickPosition.Y + prevY;
            }
        }
    }
}
