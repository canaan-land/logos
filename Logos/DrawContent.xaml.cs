using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;

namespace Logos
{
    /// <summary>
    /// DrawContent.xaml 的互動邏輯
    /// </summary>
    public partial class DrawContent : UserControl
    {
        public DisplayData MainDisplayData { get; set; }

        public DrawContent(DisplayData displayData)
        {
            MainDisplayData = displayData;
            InitializeComponent();
        }

        public ICommand DrawCommand
        {
            get
            {
                return new RelayCommand(StartDraw);
            }
        }

        private void StartDraw()
        {
            (DataContext as MainWindow).StartDraw();
        }
    }
}
