using GalaSoft.MvvmLight;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace Logos
{
    public class Parameters
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
        [JsonPropertyName("outline")]
        public bool Outline { get; set; }
        [JsonPropertyName("outlineColor")]
        public string OutlineColor { get; set; }
        [JsonPropertyName("outlineWidth")]
        public double OutlineWidth { get; set; }
        [JsonPropertyName("autoDetect")]
        public bool AutoDetect { get; set; }
        [JsonPropertyName("ceCompare")]
        public bool CECompare { get; set; }
        [JsonPropertyName("showVerse")]
        public bool ShowVerse { get; set; }
        [JsonPropertyName("penColor")]
        public string PenColor { get; set; }
        [JsonPropertyName("penWidth")]
        public int PenWidth { get; set; }
    }

    public class DisplayData : ViewModelBase
    {
        private string textString;
        private bool isTextDisplay;
        private readonly Parameters parameters = new();

        public DisplayData()
        {
            TextFont = "微軟正黑體";
            TextFontSize = 96;
            TextFontBold = true;
            TextFontItalic = false;
            TextFontUnderline = false;
            TextFontColor = GetColorName(Colors.Red);
            TextOutline = true;
            TextOutlineColor = GetColorName(Colors.White);
            TextOutlineWidth = 5;
            AutoDetect = true;
            CECompare = true;
            ShowVerse = false;
            DrawPenColor = GetColorName(Colors.Red);
            DrawPenWidth = 5;
        }

        public string TextString
        {
            get => textString;
            set
            {
                textString = value;
                RaisePropertyChanged();
            }
        }

        public bool IsTextDisplay
        {
            get => isTextDisplay;
            set
            {
                isTextDisplay = value;
                RaisePropertyChanged();
            }
        }

        public string TextFont
        {
            get => parameters.Font;
            set
            {
                parameters.Font = value;
                RaisePropertyChanged();
            }
        }

        public double TextFontSize
        {
            get => parameters.FontSize;
            set
            {
                parameters.FontSize = value;
                RaisePropertyChanged();
            }
        }

        public bool TextFontBold
        {
            get => parameters.FontBold;
            set
            {
                parameters.FontBold = value;
                RaisePropertyChanged();
            }
        }

        public bool TextFontItalic
        {
            get => parameters.FontItalic;
            set
            {
                parameters.FontItalic = value;
                RaisePropertyChanged();
            }
        }

        public bool TextFontUnderline
        {
            get => parameters.FontUnderline;
            set
            {
                parameters.FontUnderline = value;
                RaisePropertyChanged();
            }
        }

        public string TextFontColor
        {
            get => parameters.FontColor;
            set
            {
                parameters.FontColor = value;
                RaisePropertyChanged();
            }
        }

        public bool TextOutline
        {
            get => parameters.Outline;
            set
            {
                parameters.Outline = value;
                RaisePropertyChanged();
            }
        }

        public string TextOutlineColor
        {
            get => parameters.OutlineColor;
            set
            {
                parameters.OutlineColor = value;
                RaisePropertyChanged();
            }
        }

        public double TextOutlineWidth
        {
            get => parameters.OutlineWidth;
            set
            {
                parameters.OutlineWidth = value;
                RaisePropertyChanged();
            }
        }

        public bool AutoDetect
        {
            get => parameters.AutoDetect;
            set
            {
                parameters.AutoDetect = value;
                RaisePropertyChanged();
            }
        }

        public bool CECompare
        {
            get => parameters.CECompare;
            set
            {
                parameters.CECompare = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowVerse
        {
            get => parameters.ShowVerse;
            set
            {
                parameters.ShowVerse = value;
                RaisePropertyChanged();
            }
        }

        public string DrawPenColor
        {
            get => parameters.PenColor;
            set
            {
                parameters.PenColor = value;
                RaisePropertyChanged();
            }
        }

        public int DrawPenWidth
        {
            get => parameters.PenWidth;
            set
            {
                parameters.PenWidth = value;
                RaisePropertyChanged();
            }
        }

        public Parameters Params
        {
            get => parameters;
            set
            {
                TextFont = value.Font;
                TextFontSize = value.FontSize;
                TextFontBold = value.FontBold;
                TextFontItalic = value.FontItalic;
                TextFontUnderline = value.FontUnderline;
                TextFontColor = value.FontColor;
                TextOutline = value.Outline;
                TextOutlineColor = value.OutlineColor;
                TextOutlineWidth = value.OutlineWidth;
                AutoDetect = value.AutoDetect;
                CECompare = value.CECompare;
                ShowVerse = value.ShowVerse;
                DrawPenColor = value.PenColor;
                DrawPenWidth = value.PenWidth;
            }
        }

        private static string GetColorName(Color color)
        {
            return typeof(Colors).GetProperties()
                .FirstOrDefault(prop => color.Equals(prop.GetValue(null))).Name;
        }
    }
}
