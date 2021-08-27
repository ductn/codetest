using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClsCommon
{
    public static class StringHelper
    {
        public static string ReplaceNumber(string str)
        {
            return str.Replace(",", "#").Replace(".", ",").Replace("#", ".");
        }

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
            return str2.ToLower();
        }
        public static DateTime StringToDate(string strVal, string strFormat = "dd/mm/yyyy")
        {
            string[] ar = strVal.Split(new string[] { " " }, StringSplitOptions.None);
            string[] a;
            DateTime KQ= new DateTime();
            a = ar[0].Split(new string[] { "/" }, StringSplitOptions.None);
            if (a.Length < 3)
            {
                a = ar[0].Split(new string[] { "-" }, StringSplitOptions.None);
            }
            try
            {
                int day = Int32.Parse(a[0]);
                int month = Int32.Parse(a[1]);
                int year = Int32.Parse(a[2]);
                if (month>12)
                {
                    month = Int32.Parse(a[0]);
                    day = Int32.Parse(a[1]);
                }
                switch (strFormat.ToUpper())
                {
                     
                    case "DD/MM/YYYY":
                        KQ = new DateTime(year, month, day);
                        break;
                    case "MM/DD/YYYY":
                        KQ = new DateTime(year, day, month);
                        break;
                    case "YYYY/MM/DD":
                        KQ = new DateTime(day, month, year);
                        break;
                }
            }
            catch
            {
                KQ = new DateTime(1900, 1, 1);
            }

            return KQ;
        }
    }
}
