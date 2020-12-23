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
    }
}
