using CommunityToolkit.Mvvm.ComponentModel;
using System;
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

    public partial class DisplayData : ObservableObject
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

        public double TextOutlineWidth
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

        public string DrawPenColor
        {
            get => parameters.PenColor;
            set
            {
                parameters.PenColor = value;
                OnPropertyChanged();
            }
        }

        public int DrawPenWidth
        {
            get => parameters.PenWidth;
            set
            {
                parameters.PenWidth = value;
                OnPropertyChanged();
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
            var prop = Array.Find(typeof(Colors).GetProperties(),
                prop => color.Equals(prop.GetValue(null)));
            return prop?.Name;
        }
    }
}
