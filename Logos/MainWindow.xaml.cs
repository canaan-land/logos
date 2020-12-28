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

        private readonly Parameters parameters = new Parameters();
        public string TextString { get; set; }
        public string TextFont
        {
            get => parameters.Font;
            set => parameters.Font = value;
        }
        public string TextFontSize
        {
            get => parameters.FontSize;
            set => parameters.FontSize = value;
        }
        public bool TextFontBold
        {
            get => parameters.FontBold;
            set => parameters.FontBold = value;
        }
        public bool TextFontItalic
        {
            get => parameters.FontItalic;
            set => parameters.FontItalic = value;
        }
        public bool TextFontUnderline
        {
            get => parameters.FontUnderline;
            set => parameters.FontUnderline = value;
        }
        public string TextFontColor
        {
            get => parameters.FontColor;
            set => parameters.FontColor = value;
        }
        public bool Outline
        {
            get => parameters.Outline;
            set => parameters.Outline = value;
        }
        public string OutlineColor
        {
            get => parameters.OutlineColor;
            set => parameters.OutlineColor = value;
        }

        public MainWindow()
        {
            InitializeComponent();

            TextFont = "微軟正黑體";
            TextFontSize = "72";
            TextFontBold = TextFontItalic = TextFontUnderline = false;
            TextFontColor = GetColorName(Colors.Red);
            Outline = true;
            OutlineColor = GetColorName(Colors.White);

            MenuItemText.IsSelected = true;
        }

        private void MenuItemText_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new TextContent() { DataContext = this });
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

        private string GetColorName(Color color)
        {
            return typeof(Colors).GetProperties()
                .FirstOrDefault(prop => color.Equals(prop.GetValue(null))).Name;
        }
    }
}
