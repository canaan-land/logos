using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logos
{
    public class Bible
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
        {
            new BookStruct("創", "Ge", null, false),
            new BookStruct("出", "Ex", null, false),
            new BookStruct("利", "Lev", null, false),
            new BookStruct("民", "Nu", null, false),
            new BookStruct("申", "Dt", null, false),
            new BookStruct("書", "Jos", null, false),
            new BookStruct("士", "Jdg", null, false),
            new BookStruct("得", "Ru", null, false),
            new BookStruct("撒上", "1Sa", null, false),
            new BookStruct("撒下", "2Sa", null, false),
            new BookStruct("王上", "1Ki", null, false),
            new BookStruct("王下", "2Ki", null, false),
            new BookStruct("代上", "1Ch", null, false),
            new BookStruct("代下", "2Ch", null, false),
            new BookStruct("拉", "Ezr", null, false),
            new BookStruct("尼", "Ne", null, false),
            new BookStruct("斯", "Est", null, false),
            new BookStruct("伯", "Job", null, false),
            new BookStruct("詩", "Ps", null, false),
            new BookStruct("箴", "Pr", null, false),
            new BookStruct("傳", "Ecc", null, false),
            new BookStruct("歌", "SS", null, false),
            new BookStruct("賽", "Isa", null, false),
            new BookStruct("耶", "Jer", null, false),
            new BookStruct("哀", "La", null, false),
            new BookStruct("結", "Eze", null, false),
            new BookStruct("但", "Da", null, false),
            new BookStruct("何", "Hos", null, false),
            new BookStruct("珥", "Joel", null, false),
            new BookStruct("摩", "Am", null, false),
            new BookStruct("俄", "Ob", null, true),
            new BookStruct("拿", "Jnh", null, false),
            new BookStruct("彌", "Mic", null, false),
            new BookStruct("鴻", "Na", null, false),
            new BookStruct("哈", "Hab", null, false),
            new BookStruct("番", "Zep", null, false),
            new BookStruct("該", "Hag", null, false),
            new BookStruct("亞", "Zec", null, false),
            new BookStruct("瑪", "Mal", null, false),
            new BookStruct("太", "Mt", null, false),
            new BookStruct("可", "Mk", null, false),
            new BookStruct("路", "Lk", null, false),
            new BookStruct("約", "Jn", null, false),
            new BookStruct("徒", "Ac", null, false),
            new BookStruct("羅", "Ro", null, false),
            new BookStruct("林前", "1Co", null, false),
            new BookStruct("林後", "2Co", null, false),
            new BookStruct("加", "Gal", null, false),
            new BookStruct("弗", "Eph", null, false),
            new BookStruct("腓", "Php", null, false),
            new BookStruct("西", "Col", null, false),
            new BookStruct("帖前", "1Th", null, false),
            new BookStruct("帖後", "2Th", null, false),
            new BookStruct("提前", "1Ti", null, false),
            new BookStruct("提後", "2Ti", null, false),
            new BookStruct("多", "Tit", null, false),
            new BookStruct("門", "Phm", null, true),
            new BookStruct("來", "Heb", null, false),
            new BookStruct("雅", "Jas", null, false),
            new BookStruct("彼前", "1Pe", null, false),
            new BookStruct("彼後", "2Pe", null, false),
            new BookStruct("約一", "1Jn", "約壹", false),
            new BookStruct("約二", "2Jn", "約貳", true),
            new BookStruct("約三", "3Jn", "約參", true),
            new BookStruct("猶", "Jude", null, true),
            new BookStruct("啟", "Rev", null, false)
        };

        public static bool Parse(ref string strText, DisplayData displayData)
        {
            string strToParse = strText.TrimStart(' ', '(');

            string bookTC = new string(strToParse.TakeWhile(c => !char.IsWhiteSpace(c)).ToArray());
            BookStruct sBook = Array.Find(BookList, b => b.TChinese.Equals(bookTC));
            if (sBook.TChinese is null)
            {
                return false;
            }

            Regex regex = new Regex(@"^.+ \d+:\d+");
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
                strToParse = strToParse.Substring(0, strToParse.IndexOf(':'));
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
    }
}
