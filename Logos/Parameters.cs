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
}
