using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Helpertype
    {
        public static Dictionary<int, string> Aylar
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(1, "Ocak");
                result.Add(2, "Şubat");
                result.Add(3, "Mart");
                result.Add(4, "Nisan");
                result.Add(5, "Mayıs");
                result.Add(6, "Haziran");
                result.Add(7, "Temmuz");
                result.Add(8, "Ağustos");
                result.Add(9, "Eylül");
                result.Add(10, "Ekim");
                result.Add(11, "Kasım");
                result.Add(12, "Aralık");
                return result;
            }
        }

        public static int currentmonth
        {
            get
            {
                int ay = 0;
                if (DateTime.Now.ToString("MM").Substring(0, 1) == "0")
                {
                    ay = DateTime.Now.ToString("MM").toint32();
                }
                else
                {
                    ay = DateTime.Now.ToString("MM").toint32();
                }

                return ay;

            }
        }

        public static string currentyaer
        {
            get
            {

                return DateTime.Now.ToString("yyyy");

            }
        }


        public static string Aylarlistwihtcomma
        {
            get
            {
                int ay = 0;
                if (DateTime.Now.ToString("MM").Substring(0, 1) == "0")
                {
                    ay = DateTime.Now.ToString("MM").toint32();
                }
                else
                {
                    ay = DateTime.Now.ToString("MM").toint32();
                }

                return string.Join(",", Aylar.Keys.Where(x => x <= ay).ToList());

            }
        }
        public static double Yuzdelik(double a, double b)
        {
            return (a * 100 / b) - 100;
        }
        public static double YuzdelikChart(double a, double b)
        {
            double dd = (a * 100 / b);
            if (dd < 1)
            {
                dd = 1;
                return dd;
            }
            return dd;
        }


    }
}
