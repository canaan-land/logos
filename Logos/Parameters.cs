using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace Logos
{
    class Parameters
    {
        [JsonPropertyName("font")]
        public string Font { get; set; }
        [JsonPropertyName("fontSize")]
        public double FontSize { get; set; }
        [JsonPropertyName("fontBold")]
        public bool FontBold { get; set; }
        [JsonPropertyName("fontItalic")]
        public bool FontItalic { get; set; }
        [JsonPropertyName("fontUnderline")]
        public bool FontUnderline { get; set; }
        [JsonPropertyName("fontColor")]
        public string FontColor { get; set; }
        [JsonPropertyName("penWidth")]
        public int PenWidth { get; set; }
        [JsonPropertyName("outline")]
        public bool Outline { get; set; }
        [JsonPropertyName("outlineColor")]
        public string OutlineColor { get; set; }
        [JsonPropertyName("outlineWidth")]
        public decimal OutlineWidth { get; set; }
        [JsonPropertyName("autoDetect")]
        public bool AutoDetect { get; set; }
        [JsonPropertyName("ceCompare")]
        public bool CECompare { get; set; }
        [JsonPropertyName("showVerse")]
        public bool ShowVerse { get; set; }
    }

    public class DisplayData : INotifyPropertyChanged
    {
        private string textString;
        private bool isTextDisplay;
        private readonly Parameters parameters = new Parameters();

        public event PropertyChangedEventHandler PropertyChanged;

        public DisplayData()
        {
            TextFont = "微軟正黑體";
            TextFontSize = 96;
            TextFontBold = true;
            TextFontItalic = TextFontUnderline = false;
            TextFontColor = GetColorName(Colors.Red);
            TextOutline = true;
            TextOutlineColor = GetColorName(Colors.White);
            TextOutlineWidth = 5;
            AutoDetect = true;
            CECompare = true;
            ShowVerse = false;
        }

        public string TextString
        {
            get => textString;
            set
            {
                textString = value;
                OnPropertyChanged();
            }
        }

        public bool IsTextDisplay
        {
            get => isTextDisplay;
            set
            {
                isTextDisplay = value;
                OnPropertyChanged();
            }
        }

        public string TextFont
        {
            get => parameters.Font;
            set
            {
                parameters.Font = value;
                OnPropertyChanged();
            }
        }

        public double TextFontSize
        {
            get => parameters.FontSize;
            set
            {
                parameters.FontSize = value;
                OnPropertyChanged();
            }
        }

        public bool TextFontBold
        {
            get => parameters.FontBold;
            set
            {
                parameters.FontBold = value;
                OnPropertyChanged();
            }
        }

        public bool TextFontItalic
        {
            get => parameters.FontItalic;
            set
            {
                parameters.FontItalic = value;
                OnPropertyChanged();
            }
        }

        public bool TextFontUnderline
        {
            get => parameters.FontUnderline;
            set
            {
                parameters.FontUnderline = value;
                OnPropertyChanged();
            }
        }

        public string TextFontColor
        {
            get => parameters.FontColor;
            set
            {
                parameters.FontColor = value;
                OnPropertyChanged();
            }
        }

        public bool TextOutline
        {
            get => parameters.Outline;
            set
            {
                parameters.Outline = value;
                OnPropertyChanged();
            }
        }

        public string TextOutlineColor
        {
            get => parameters.OutlineColor;
            set
            {
                parameters.OutlineColor = value;
                OnPropertyChanged();
            }
        }

        public decimal TextOutlineWidth
        {
            get => parameters.OutlineWidth;
            set
            {
                parameters.OutlineWidth = value;
                OnPropertyChanged();
            }
        }

        public bool AutoDetect
        {
            get => parameters.AutoDetect;
            set
            {
                parameters.AutoDetect = value;
                OnPropertyChanged();
            }
        }

        public bool CECompare
        {
            get => parameters.CECompare;
            set
            {
                parameters.CECompare = value;
                OnPropertyChanged();
            }
        }

        public bool ShowVerse
        {
            get => parameters.ShowVerse;
            set
            {
                parameters.ShowVerse = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private static string GetColorName(Color color)
        {
            return typeof(Colors).GetProperties()
                .FirstOrDefault(prop => color.Equals(prop.GetValue(null))).Name;
        }
    }
}
