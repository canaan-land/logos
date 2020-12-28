using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;

namespace Logos
{
    public static class InstalledFonts
    {
        public static List<string> GetList()
        {
            return new InstalledFontCollection().Families.Select(font => font.Name).ToList();
        }
    }
}
