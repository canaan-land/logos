using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logos
{
    public partial class Bible
    {
        private readonly struct BookStruct
        {
            public readonly string TChinese;
            public readonly string English;
            public readonly string Alternative;
            public readonly bool SingleChapter;

            public BookStruct(string tc, string en, string alt, bool sinChap) =>
                (TChinese, English, Alternative, SingleChapter) = (tc, en, alt, sinChap);
        }

        private static readonly BookStruct[] BookList =
        [
            new("創", "Ge", null, false),
            new("出", "Ex", null, false),
            new("利", "Lev", null, false),
            new("民", "Nu", null, false),
            new("申", "Dt", null, false),
            new("書", "Jos", null, false),
            new("士", "Jdg", null, false),
            new("得", "Ru", null, false),
            new("撒上", "1Sa", null, false),
            new("撒下", "2Sa", null, false),
            new("王上", "1Ki", null, false),
            new("王下", "2Ki", null, false),
            new("代上", "1Ch", null, false),
            new("代下", "2Ch", null, false),
            new("拉", "Ezr", null, false),
            new("尼", "Ne", null, false),
            new("斯", "Est", null, false),
            new("伯", "Job", null, false),
            new("詩", "Ps", null, false),
            new("箴", "Pr", null, false),
            new("傳", "Ecc", null, false),
            new("歌", "SS", null, false),
            new("賽", "Isa", null, false),
            new("耶", "Jer", null, false),
            new("哀", "La", null, false),
            new("結", "Eze", null, false),
            new("但", "Da", null, false),
            new("何", "Hos", null, false),
            new("珥", "Joel", null, false),
            new("摩", "Am", null, false),
            new("俄", "Ob", null, true),
            new("拿", "Jnh", null, false),
            new("彌", "Mic", null, false),
            new("鴻", "Na", null, false),
            new("哈", "Hab", null, false),
            new("番", "Zep", null, false),
            new("該", "Hag", null, false),
            new("亞", "Zec", null, false),
            new("瑪", "Mal", null, false),
            new("太", "Mt", null, false),
            new("可", "Mk", null, false),
            new("路", "Lk", null, false),
            new("約", "Jn", null, false),
            new("徒", "Ac", null, false),
            new("羅", "Ro", null, false),
            new("林前", "1Co", null, false),
            new("林後", "2Co", null, false),
            new("加", "Gal", null, false),
            new("弗", "Eph", null, false),
            new("腓", "Php", null, false),
            new("西", "Col", null, false),
            new("帖前", "1Th", null, false),
            new("帖後", "2Th", null, false),
            new("提前", "1Ti", null, false),
            new("提後", "2Ti", null, false),
            new("多", "Tit", null, false),
            new("門", "Phm", null, true),
            new("來", "Heb", null, false),
            new("雅", "Jas", null, false),
            new("彼前", "1Pe", null, false),
            new("彼後", "2Pe", null, false),
            new("約一", "1Jn", "約壹", false),
            new("約二", "2Jn", "約貳", true),
            new("約三", "3Jn", "約參", true),
            new("猶", "Jude", null, true),
            new("啟", "Rev", null, false)
        ];

        public static bool Parse(ref string strText, DisplayData displayData)
        {
            string strToParse = strText.TrimStart(' ', '(');

            string bookTC = new(strToParse.TakeWhile(c => !char.IsWhiteSpace(c)).ToArray());
            BookStruct sBook = Array.Find(BookList, b => b.TChinese.Equals(bookTC));
            if (sBook.TChinese is null)
            {
                return false;
            }

            Regex regex = regexBible();
            Match match = regex.Match(strToParse);
            if (!match.Success)
            {
                return false;
            }
            strToParse = match.Value;

            if (displayData.CECompare)
            {
                strToParse = strToParse.Insert(strToParse.IndexOf(' '), ' ' + sBook.English);
            }

            if (!displayData.ShowVerse)
            {
                strToParse = strToParse[..strToParse.IndexOf(':')];
            }

            if (sBook.Alternative is not null)
            {
                strToParse = strToParse.Replace(sBook.TChinese, sBook.Alternative);
            }

            if (sBook.SingleChapter && !displayData.ShowVerse)
            {
                strToParse = strToParse.Replace(" 1", "");
            }

            strText = strToParse;

            return true;
        }

        [GeneratedRegex("^.+ \\d+:\\d+")]
        private static partial Regex regexBible();
    }
}
