﻿using CommunityToolkit.Mvvm.Input;
using NHotkey.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        public static string ProductCopyright => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        public string MenuItemTextName
        {
            get
            {
                var sp = MenuItemText.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }
        public string MenuItemDrawName
        {
            get
            {
                var sp = MenuItemDraw.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }
        public string MenuItemAboutName
        {
            get
            {
                var sp = MenuItemAbout.Content as StackPanel;
                return sp.Children.OfType<TextBlock>().First().Text;
            }
        }

        private readonly DisplayData displayData = new();
        private readonly SharpClipboard clipboard = new();
        private readonly string strJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOGOS.json");

        public DisplayData MainDisplayData => displayData;

        public MainWindow()
        {
            SourceChord.FluentWPF.ResourceDictionaryEx.GlobalTheme = SourceChord.FluentWPF.ElementTheme.Dark;

            InitializeComponent();

            if (File.Exists(strJsonPath))
            {
                string strJson = File.ReadAllText(strJsonPath);
                Parameters sParams = JsonSerializer.Deserialize<Parameters>(strJson);
                MainDisplayData.Params = sParams;
            }

            MenuList.SelectedIndex = 0;

            clipboard.ClipboardChanged += ClipboardChanged;
        }

        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListView;

            ContentArea.Content =
                list.SelectedIndex switch
                {
                    0 => new TextContent() { DataContext = this },
                    1 => new DrawContent() { DataContext = this },
                    2 => new AboutContent() { DataContext = this },
                    _ => null
                };
        }

        private void MenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            Control item = sender as Control;
            Point point = Mouse.GetPosition(item);
            RadialGradientBrush radialGradient = new()
            {
                GradientOrigin = new Point(point.X / item.ActualWidth, point.Y / item.ActualHeight),
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
                displayWindow = new DisplayWindow()
                {
                    DataContext = this
                };
                displayWindow.Show();
            }
            HotkeyManager.Current.AddOrReplace("TextEscape", Key.Escape, ModifierKeys.None, (o, e) => MainDisplayData.IsTextDisplay = false);
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

        public ICommand PasteCommand => new RelayCommand(PasteTheWord);

        private void PasteTheWord()
        {
            do
            {
                if (!Clipboard.ContainsText())
                {
                    ShowDialog("不正確的剪貼簿內容型別");
                    break;
                }

                string strText = Clipboard.GetText();
                if (!Bible.Parse(ref strText, MainDisplayData))
                {
                    ShowDialog("theWord章節格式錯誤");
                    break;
                }

                MainDisplayData.TextString = strText;
            } while (false);
        }

        public ICommand DrawCommand => new RelayCommand(StartDraw);

        private DrawWindow drawWindow;
        private void StartDraw()
        {
            WindowState = WindowState.Minimized;
            Thread.Sleep(500);
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

            string strJson = JsonSerializer.Serialize(MainDisplayData.Params);
            File.WriteAllText(strJsonPath, strJson);
        }

        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (MainDisplayData.AutoDetect && e.ContentType.Equals(SharpClipboard.ContentTypes.Text))
            {
                string strText = clipboard.ClipboardText;
                if (Bible.Parse(ref strText, MainDisplayData))
                {
                    if (!string.Equals(MainDisplayData.TextString, strText))
                    {
                        MainDisplayData.TextString = strText;
                    }
                    if (!MainDisplayData.IsTextDisplay)
                    {
                        MainDisplayData.IsTextDisplay = true;
                    }
                    Clipboard.Clear();
                }
            }
        }

        private void ShowDialog(string text)
        {
            DialogText.Text = text;
            Dialog.IsOpen = true;
        }
    }
}
