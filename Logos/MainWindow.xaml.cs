using SourceChord.FluentWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string ProductName => Application.ResourceAssembly.GetName().Name;

        public MainWindow()
        {
            InitializeComponent();

            MenuItemText.IsSelected = true;
        }

        private void MenuItemText_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new TextContent());
        }

        private void MenuItemDraw_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new DrawContent());
        }

        private void MenuItemAbout_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new AboutContent());
        }

        private void MenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            Point point = Mouse.GetPosition(item);
            RadialGradientBrush radialGradient = new RadialGradientBrush()
            {
                GradientOrigin = new Point(point.X / MenuItemAbout.ActualWidth, point.Y / MenuItemAbout.ActualHeight),
                RadiusX = 1.0,
                RadiusY = 1.0
            };
            radialGradient.Center = radialGradient.GradientOrigin;
            radialGradient.GradientStops.Add(new GradientStop(Color.FromArgb(0xC0, 0x6D, 0x6D, 0x6D), 0.0));
            radialGradient.GradientStops.Add(new GradientStop(Color.FromArgb(0xC0, 0x42, 0x42, 0x42), 1.0));
            item.Background = radialGradient;
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as ListViewItem).Background = null;
        }
    }
}
