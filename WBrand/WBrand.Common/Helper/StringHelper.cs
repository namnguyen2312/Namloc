using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WBrand.Common.Helper
{
    public class StringHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }
        public static string ToUrlFriendly(string title)
        {
            return ToUrlFriendly(true, title);
        }

        public static string ToUrlFriendlyWithDateTime(string title)
        {
            return ToUrlFriendly(true, title) + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss");
        }
        public static string ToUrlFriendlyWithDateTime(string title, DateTime dateTime)
        {
            return ToUrlFriendly(true, title) + "-" + dateTime.ToString("ddMMyyyyHHmmss");
        }
        public static string ToUrlFriendlyWithDate(string title)
        {
            return ToUrlFriendly(true, title) + "-" + DateTime.Now.ToString("ddMMyyyy");
        }
        public static string ToUrlFriendlyWithDate(string title, DateTime dateTime)
        {
            return ToUrlFriendly(true, title) + "-" + dateTime.ToString("ddMMyyyy");
        }
        /// <summary>
        /// Creates a slug.
        /// Author: Daniel Harman, based on original code by Jeff Atwood
        /// References:
        /// http://www.unicode.org/reports/tr15/tr15-34.html
        /// http://meta.stackoverflow.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696
        /// http://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486
        /// http://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string
        /// </summary>
        public static string ToUrlFriendly(bool toLower, string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var normalised = value.Normalize(NormalizationForm.FormKD);

            const int Maxlen = 80;
            int len = normalised.Length;
            bool prevDash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = normalised[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }

                    sb.Append(c);
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }

                    // tricky way to convert to lowercase
                    if (toLower)
                    {
                        sb.Append((char)(c | 32));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevDash && sb.Length > 0)
                    {
                        prevDash = true;
                    }
                }
                else
                {
                    string swap = ConvertEdgeCases(c, toLower);

                    if (swap != null)
                    {
                        if (prevDash)
                        {
                            sb.Append('-');
                            prevDash = false;
                        }

                        sb.Append(swap);
                    }
                }

                if (sb.Length == Maxlen)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        private static string ConvertEdgeCases(char c, bool toLower)
        {
            string swap = null;
            switch (c)
            {
                case 'ı':
                    swap = "i";
                    break;
                case 'ł':
                    swap = "l";
                    break;
                case 'Ł':
                    swap = toLower ? "l" : "L";
                    break;
                case 'đ':
                    swap = "d";
                    break;
                case 'ß':
                    swap = "ss";
                    break;
                case 'ø':
                    swap = "o";
                    break;
                case 'Þ':
                    swap = "th";
                    break;
            }

            return swap;
        }
    }
}
