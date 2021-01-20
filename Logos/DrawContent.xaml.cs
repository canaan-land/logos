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
                return new ActionCommand(StartDraw);
            }
        }

        private void StartDraw(object parameter)
        {
            (DataContext as MainWindow).StartDraw();
        }
    }
}
