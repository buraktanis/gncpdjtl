using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class Formats
    {
        public static string dateFormatWithDay { get { return "dd.MM.yyyy dddd"; } }
        public static string dateFormatsql { get { return "yyyy-MM-dd"; } }
        public static string cultureKey { get { return "tr-TR"; } }
        public static string dateFormat { get { return "dd.MM.yyyy"; } }
        public static string dateFormatlogo { get { return "MM.dd.yyyy"; } }
        public static string dateTimeFormat { get { return "dd.MM.yyyy HH:mm"; } }
        public static string dateTimeWithSecondsFormat { get { return "dd.MM.yyyy HH:mm:ss"; } }

    }
}
