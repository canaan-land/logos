using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Logos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string ProductName => Application.ResourceAssembly.GetName().Name;
        public static string ProductVersion => Application.ResourceAssembly.GetName().Version.ToString(3);
        public string MenuItemTextName
        {
            get
            {
                StackPanel sp = MenuItemText.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }
        public string MenuItemDrawName
        {
            get
            {
                StackPanel sp = MenuItemDraw.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }
        public string MenuItemAboutName
        {
            get
            {
                StackPanel sp = MenuItemAbout.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }

        public readonly DisplayData displayData = new DisplayData();

        public MainWindow()
        {
            InitializeComponent();

            MenuItemText.IsSelected = true;
        }

        private void MenuItemText_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            TextContent textContent = new TextContent()
            {
                DataContext = this
            };
            textContent.StringTextBox.DataContext = displayData;
            textContent.FontComboBox.DataContext = displayData;
            textContent.FontSizeComboBox.DataContext = displayData;
            textContent.BoldCheckbox.DataContext = displayData;
            textContent.ItalicCheckbox.DataContext = displayData;
            textContent.UnderlineCheckbox.DataContext = displayData;
            textContent.FontColorComboBox.DataContext = displayData;
            textContent.OutlineCheckBox.DataContext = displayData;
            textContent.OutlineColorComboBox.DataContext = displayData;
            textContent.OutlineWidthComboBox.DataContext = displayData;
            ContentGrid.Children.Add(textContent);
        }

        private void MenuItemDraw_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new DrawContent() { DataContext = this });
        }

        private void MenuItemAbout_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new AboutContent() { DataContext = this });
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

        public bool IsTextDisplay { get; set; }
        private DisplayWindow displayWindow;
        public void PerformTextDisplay()
        {
            if (IsTextDisplay)
            {
                displayWindow = new DisplayWindow()
                {
                    DataContext = this
                };
                displayWindow.DisplayText.DataContext = displayData;
                displayWindow.Show();
            }
            else
            {
                displayWindow.Close();
            }
        }
    }
}
