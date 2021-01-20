using NHotkey.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WK.Libraries.SharpClipboardNS;

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
        private readonly SharpClipboard clipboard = new SharpClipboard();
        private readonly string strJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOGOS.json");

        public MainWindow()
        {
            SourceChord.FluentWPF.ResourceDictionaryEx.GlobalTheme = SourceChord.FluentWPF.ElementTheme.Dark;

            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.Fant);

            Timer timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            if (File.Exists(strJsonPath))
            {
                string strJson = File.ReadAllText(strJsonPath);
                Parameters sParams = JsonSerializer.Deserialize<Parameters>(strJson);
                displayData.Params = sParams;
            }

            MenuList.SelectedIndex = 0;

            clipboard.ClipboardChanged += ClipboardChanged;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                DateTime dateTime = DateTime.Now;
                SecondHand.Angle = dateTime.Second * 6 + 6 * dateTime.Millisecond / 1000.0;
                MinuteHand.Angle = dateTime.Minute * 6 + dateTime.Second * 0.1;
                HourHand.Angle = dateTime.Hour * 30 + dateTime.Minute * 0.5;
            }));
        }

        private void MenuItemText_Selected(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TextContent(displayData) { DataContext = this };
        }

        private void MenuItemDraw_Selected(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DrawContent(displayData) { DataContext = this };
        }

        private void MenuItemAbout_Selected(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AboutContent() { DataContext = this };
        }

        private void MenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            Control item = sender as Control;
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
            (sender as Control).Background = null;
        }

        private DisplayWindow displayWindow;
        public void Display()
        {
            if (displayWindow is null)
            {
                displayWindow = new DisplayWindow(displayData)
                {
                    DataContext = this
                };
                displayWindow.Show();
            }
            HotkeyManager.Current.AddOrReplace("TextEscape", Key.Escape, ModifierKeys.None, (o, e) => displayData.IsTextDisplay = false);
        }

        public void Undisplay()
        {
            HotkeyManager.Current.Remove("TextEscape");
            if (displayWindow is not null)
            {
                displayWindow.Close();
                displayWindow = null;
            }
        }

        private DrawWindow drawWindow;
        public void StartDraw()
        {
            WindowState = WindowState.Minimized;
            if (drawWindow is null)
            {
                drawWindow = new DrawWindow()
                {
                    DataContext = this
                };
                drawWindow.Show();
            }
            HotkeyManager.Current.AddOrReplace("DrawEscape", Key.Escape, ModifierKeys.None, (o, e) => StopDraw());
        }

        public void StopDraw()
        {
            HotkeyManager.Current.Remove("DrawEscape");
            if (drawWindow is not null)
            {
                drawWindow.Close();
                drawWindow = null;
            }
            WindowState = WindowState.Normal;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Undisplay();
            StopDraw();

            string strJson = JsonSerializer.Serialize(displayData.Params);
            File.WriteAllText(strJsonPath, strJson);
        }

        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (displayData.AutoDetect)
            {
                if (e.ContentType.Equals(SharpClipboard.ContentTypes.Text))
                {
                    string strText = clipboard.ClipboardText;
                    if (Bible.Parse(ref strText, displayData))
                    {
                        if (!string.Equals(displayData.TextString, strText))
                        {
                            displayData.TextString = strText;
                        }
                        if (!displayData.IsTextDisplay)
                        {
                            displayData.IsTextDisplay = true;
                        }
                    }
                }
            }
        }

        public void ShowDialog(string text)
        {
            DialogText.Text = text;
            Dialog.IsOpen = true;
        }
    }
}
