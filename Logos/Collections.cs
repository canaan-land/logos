using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Media;

namespace Logos
{
    public static class InstalledFonts
    {
        public static List<string> GetList()
        {
            return new InstalledFontCollection().Families.Select(font => font.Name).ToList();
        }
    }

    public static class ColorHelper
    {
        public static List<string> GetList()
        {
            return typeof(Colors)
                .GetProperties()
                .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => prop.Name)
                .ToList();
        }
    }
}
