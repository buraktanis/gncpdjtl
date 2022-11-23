using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Core.Helpers;

namespace Core.Extensions
{
    public static class HelperExtension
    {


        public static string ValidatePassword(this string password)
        {
            var input = password;
           var ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "passwordShouldContainAtLeastOneLowerCase";
                return ErrorMessage;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "passwordShouldContainAtLeastOneUpperCase";
                return ErrorMessage;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "passwordshouldnotbelesserthan8orgreater";
                return ErrorMessage;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "passwordshouldcontainatleastonenumeric";
                return ErrorMessage;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "passwordshouldcontainatleastonespecialcase";
                return ErrorMessage;
            }
            else
            {
                return ErrorMessage;
            }
        }


        public static bool CheckURLValid(this string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }

        public static DateTime UnixTimestampToDateTime(this long unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Local);
        }
        public static string GetDescription(Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }
        public static bool IsNumericDatatype(this object obj)
        {
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static string TurkishToEnglish(this string text)
        {
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            // Match chars
            for (int i = 0; i < turkishChars.Length; i++)
                text = text.Replace(turkishChars[i], englishChars[i]);

            return text;
        }
        public static string cleartext(this string txtResponse)
        {
            txtResponse = txtResponse.Replace("'", "&#39;");
            txtResponse = txtResponse.Replace("=", "&#61;");
            txtResponse = txtResponse.Replace("-", "&minus;");
            txtResponse = txtResponse.Replace("<", "&lt;");
            txtResponse = txtResponse.Replace(">", "&gt;");

            return txtResponse;
        }
        public static string FirstUpper(this string key) { return key.Substring(0, 1).ToUpper() + key.Substring(1, key.Count() - 1).ToLower(); }
        public static string getGuid { get { return Guid.NewGuid().ToString().Replace("-", ""); } }
        public static int toint32(this object text)
        {
            return isNull(text.ToString()) ? 0 : Convert.ToInt32(text);
        }
        public static Single toSingle(this object text)
        {
            return Convert.ToSingle(text);
        }
        public static double toDouble(this object text)
        {
            return Convert.ToDouble(text);
        }
        public static decimal toDecimal(this object text)
        {
            return Convert.ToDecimal(text);
        }
        public static bool toBoolean(this object text)
        {
            return Convert.ToBoolean(text);
        }

        static string ReplaceURL(string url)
        {
            if (string.IsNullOrEmpty(url))
                url = "link";
            string u = url.ToLower().Replace(" ", "-").Replace("ü", "u").Replace("ö", "o").Replace("ı", "i").Replace('ç', 'c').Replace('ş', 's').Replace('ğ', 'g').Replace('/', '-').Replace('.', '-').Replace("%", "").Replace("$", "").Replace("&", "").Replace('>', '-').Replace('<', '-').Replace('*', '-').Replace(':', '-').Replace("-----", "-").Replace("----", "-").Replace("---", "-").Replace("--", "-");
            return u;
        }

        public static string ToLinkText(this string url)
        {
            var returnUrl = ReplaceURL(url);
            return returnUrl.Length > 50 ? returnUrl.Substring(0, 50) : returnUrl;
        }

        public static string ListToString(this IEnumerable<string> list)
        {
            if (list == null)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item);
                sb.Append("-");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static bool isNull(this string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
                return true;
            return false;
        }

        public static string ToSeoString(this string text)
        {
            var result = ReplaceURL(text);

            return result.Length > 40 ? result.Substring(0, 40) : result;
        }
        public static DateTime ToDateTime(this string text, string format = null)
        {
            CultureInfo provider = new CultureInfo(Formats.cultureKey);
            if (string.IsNullOrEmpty(format))
                format = Formats.dateFormat;

            DateTime dateTime;
            try
            {
                dateTime = DateTime.ParseExact(text, format, provider);
            }
            catch (Exception)
            {
                throw;
            }

            return dateTime;
        }


        //public static string userIP()
        //{
        //    string ip = hContext._current().Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    return ip;
        //}
    }
}
