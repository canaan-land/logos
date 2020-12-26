using System.Text.Json.Serialization;
using System.Windows.Media;

namespace Logos
{
    class Parameters
    {
        [JsonPropertyName("font")]
        public string Font { get; set; } = "微軟正黑體";
        [JsonPropertyName("fontSize")]
        public string FontSize { get; set; } = "72";
        [JsonPropertyName("fontBold")]
        public bool FontBold { get; set; } = false;
        [JsonPropertyName("fontItalic")]
        public bool FontItalic { get; set; } = false;
        [JsonPropertyName("fontUnderline")]
        public bool FontUnderline { get; set; } = false;
        [JsonPropertyName("fontColor")]
        public string FontColor { get; set; } = Colors.Red.ToString();
        [JsonPropertyName("penWidth")]
        public int PenWidth { get; set; } = 2;
        [JsonPropertyName("outline")]
        public bool Outline { get; set; } = true;
        [JsonPropertyName("outlineColor")]
        public string OutlineColor { get; set; } = Colors.White.ToString();
        [JsonPropertyName("outlineWidth")]
        public decimal OutlineWidth { get; set; } = 2;
        [JsonPropertyName("autoDetect")]
        public bool AutoDetect { get; set; } = true;
        [JsonPropertyName("ceCompare")]
        public bool CECompare { get; set; } = true;
        [JsonPropertyName("showVerse")]
        public bool ShowVerse { get; set; } = false;
    }
}
