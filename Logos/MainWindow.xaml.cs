using NHotkey;
using NHotkey.Wpf;
using System;
using System.Linq;
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
        struct BibleBookStruct
        {
            public readonly string TChinese;
            public readonly string English;
            public readonly string Alternative;
            public readonly bool SingleChapter;

            public BibleBookStruct(string tc, string en, string alt, bool sinChap) =>
                (TChinese, English, Alternative, SingleChapter) = (tc, en, alt, sinChap);
        }

        private readonly BibleBookStruct[] BibleBookList =
        {
            new BibleBookStruct("創", "Ge", null, false),
            new BibleBookStruct("出", "Ex", null, false),
            new BibleBookStruct("利", "Lev", null, false),
            new BibleBookStruct("民", "Nu", null, false),
            new BibleBookStruct("申", "Dt", null, false),
            new BibleBookStruct("書", "Jos", null, false),
            new BibleBookStruct("士", "Jdg", null, false),
            new BibleBookStruct("得", "Ru", null, false),
            new BibleBookStruct("撒上", "1Sa", null, false),
            new BibleBookStruct("撒下", "2Sa", null, false),
            new BibleBookStruct("王上", "1Ki", null, false),
            new BibleBookStruct("王下", "2Ki", null, false),
            new BibleBookStruct("代上", "1Ch", null, false),
            new BibleBookStruct("代下", "2Ch", null, false),
            new BibleBookStruct("拉", "Ezr", null, false),
            new BibleBookStruct("尼", "Ne", null, false),
            new BibleBookStruct("斯", "Est", null, false),
            new BibleBookStruct("伯", "Job", null, false),
            new BibleBookStruct("詩", "Ps", null, false),
            new BibleBookStruct("箴", "Pr", null, false),
            new BibleBookStruct("傳", "Ecc", null, false),
            new BibleBookStruct("歌", "SS", null, false),
            new BibleBookStruct("賽", "Isa", null, false),
            new BibleBookStruct("耶", "Jer", null, false),
            new BibleBookStruct("哀", "La", null, false),
            new BibleBookStruct("結", "Eze", null, false),
            new BibleBookStruct("但", "Da", null, false),
            new BibleBookStruct("何", "Hos", null, false),
            new BibleBookStruct("珥", "Joel", null, false),
            new BibleBookStruct("摩", "Am", null, false),
            new BibleBookStruct("俄", "Ob", null, true),
            new BibleBookStruct("拿", "Jnh", null, false),
            new BibleBookStruct("彌", "Mic", null, false),
            new BibleBookStruct("鴻", "Na", null, false),
            new BibleBookStruct("哈", "Hab", null, false),
            new BibleBookStruct("番", "Zep", null, false),
            new BibleBookStruct("該", "Hag", null, false),
            new BibleBookStruct("亞", "Zec", null, false),
            new BibleBookStruct("瑪", "Mal", null, false),
            new BibleBookStruct("太", "Mt", null, false),
            new BibleBookStruct("可", "Mk", null, false),
            new BibleBookStruct("路", "Lk", null, false),
            new BibleBookStruct("約", "Jn", null, false),
            new BibleBookStruct("徒", "Ac", null, false),
            new BibleBookStruct("羅", "Ro", null, false),
            new BibleBookStruct("林前", "1Co", null, false),
            new BibleBookStruct("林後", "2Co", null, false),
            new BibleBookStruct("加", "Gal", null, false),
            new BibleBookStruct("弗", "Eph", null, false),
            new BibleBookStruct("腓", "Php", null, false),
            new BibleBookStruct("西", "Col", null, false),
            new BibleBookStruct("帖前", "1Th", null, false),
            new BibleBookStruct("帖後", "2Th", null, false),
            new BibleBookStruct("提前", "1Ti", null, false),
            new BibleBookStruct("提後", "2Ti", null, false),
            new BibleBookStruct("多", "Tit", null, false),
            new BibleBookStruct("門", "Phm", null, true),
            new BibleBookStruct("來", "Heb", null, false),
            new BibleBookStruct("雅", "Jas", null, false),
            new BibleBookStruct("彼前", "1Pe", null, false),
            new BibleBookStruct("彼後", "2Pe", null, false),
            new BibleBookStruct("約一", "1Jn", "約壹", false),
            new BibleBookStruct("約二", "2Jn", "約貳", true),
            new BibleBookStruct("約三", "3Jn", "約參", true),
            new BibleBookStruct("猶", "Jude", null, true),
            new BibleBookStruct("啟", "Rev", null, false)
        };

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

        private readonly TextContent textContent;
        private readonly DrawContent drawContent;
        private readonly AboutContent aboutContent;

        public MainWindow()
        {
            InitializeComponent();

            textContent = new TextContent() { DataContext = this };
            textContent.SubPanel.DataContext = displayData;
            drawContent = new DrawContent() { DataContext = this };
            aboutContent = new AboutContent() { DataContext = this };

            MenuItemText.IsSelected = true;

            clipboard.ClipboardChanged += ClipboardChanged;
        }

        private void MenuItemText_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(textContent);
        }

        private void MenuItemDraw_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(drawContent);
        }

        private void MenuItemAbout_Selected(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(aboutContent);
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

        private DisplayWindow displayWindow;
        public void Display()
        {
            displayWindow = new DisplayWindow()
            {
                DataContext = this
            };
            displayWindow.DisplayText.DataContext = displayData;
            displayWindow.Show();
            HotkeyManager.Current.AddOrReplace("Escape", Key.Escape, ModifierKeys.None, OnEscape);
        }

        public void Undisplay()
        {
            HotkeyManager.Current.Remove("Escape");
            if (displayWindow != null)
            {
                displayWindow.Close();
                displayWindow = null;
            }
        }

        private void OnEscape(object sender, HotkeyEventArgs e)
        {
            displayData.IsTextDisplay = false;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (displayData.IsTextDisplay)
            {
                displayWindow.Close();
            }
        }

        private bool ParseBible(ref string strText)
        {
            string strToParse = strText;
            BibleBookStruct sBook;
            int nBookIdx, nColonIdx, nLastIdx;

            strToParse = strToParse.TrimStart(new char[] { ' ', '(' });

            nBookIdx = strToParse.IndexOf(' ');
            if (nBookIdx >= 0 && nBookIdx < 3)
            {
                var bookTC = strToParse.Substring(0, nBookIdx);
                sBook = Array.Find(BibleBookList, bb => bb.TChinese == bookTC);
                if (sBook.TChinese == null)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            nColonIdx = strToParse.IndexOf(':');
            if (nColonIdx < 0)
            {
                return false;
            }

            if (strToParse.Length <= nColonIdx + 1)
            {
                return false;
            }

            for (nLastIdx = nColonIdx + 1; nLastIdx < strToParse.Length && char.IsDigit(strToParse, nLastIdx); nLastIdx++) ;

            if (nLastIdx == nColonIdx + 1)
            {
                return false;
            }

            strToParse = strToParse.Substring(0, nLastIdx);

            if (!strToParse.Contains(" "))
            {
                return false;
            }

            if (displayData.CECompare)
            {
                strToParse = strToParse.Insert(strToParse.IndexOf(' '), " " + sBook.English);
            }

            if (!displayData.ShowVerse)
            {
                strToParse = strToParse.Substring(0, strToParse.IndexOf(':'));
            }

            if (sBook.Alternative != null)
            {
                strToParse = strToParse.Replace(sBook.TChinese, sBook.Alternative);
            }

            if (sBook.SingleChapter && !displayData.ShowVerse)
            {
                strToParse = strToParse.Replace(" 1", "");
            }

            strText = strToParse;

            return true;
        }

        private void ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (displayData.AutoDetect)
            {
                if (e.ContentType == SharpClipboard.ContentTypes.Text)
                {
                    string strText = clipboard.ClipboardText;
                    if (ParseBible(ref strText))
                    {
                        if (displayData.TextString != strText)
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

        public void PasteTheWord()
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    throw new Exception("不正確的剪貼簿內容型別");
                }

                string strText = Clipboard.GetText();
                if (!ParseBible(ref strText))
                {
                    throw new Exception("theWord章節格式錯誤");
                }

                displayData.TextString = strText;
            }
            catch (Exception ex)
            {
                DialogText.Text = ex.Message;
                Dialog.IsOpen = true;
            }
        }
    }
}
