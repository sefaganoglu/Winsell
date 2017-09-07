using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsell.Hopi
{
    public static class clsExtensions
    {
        public static long TOLONG(this object o, long lDefault = 0)
        {
            long lResult = lDefault;
            if (o != null && o is DBNull == false)
            {
                long.TryParse(o.ToString().Trim(), out lResult);
            }
            return lResult;
        }

        public static ulong TOULONG(this object o, ulong lDefault = 0)
        {
            ulong lResult = lDefault;
            if (o != null && o is DBNull == false)
            {
                ulong.TryParse(o.ToString().Trim(), out lResult);
            }
            return lResult;
        }

        public static decimal TODECIMAL(this object o, decimal dDefault = 0)
        {
            decimal dResult = dDefault;
            if (o != null && o is DBNull == false)
            {
                decimal.TryParse(o.ToString().Trim(), out dResult);
            }
            return dResult;
        }

        public static decimal TODOUBLE(this object o, decimal dDefault = 0)
        {
            decimal dResult = dDefault;
            if (o != null && o is DBNull == false)
            {
                decimal.TryParse(o.ToString().Trim(), out dResult);
            }
            return dResult;
        }

        public static int TOINT(this object o, int intDefault = 0)
        {
            int intResult = intDefault;
            if (o != null && o is DBNull == false)
            {
                if (o is Decimal)
                {
                    int.TryParse(Math.Round(((Decimal)o), 0).ToString().Trim(), out intResult);
                }
                else
                {
                    int.TryParse(o.ToString().Trim(), out intResult);
                }
            }
            return intResult;
        }

        public static string TOSTRING(this object o, string strDefault = "")
        {
            string strResult = strDefault;
            if (o != null && o is DBNull == false)
            {
                strResult = o.ToString().Trim();
            }
            return strResult;
        }

        public static decimal ROUNDTWO(this decimal o)
        {
            decimal dReturn = Math.Round(o, 2, MidpointRounding.AwayFromZero);
            return dReturn;
        }

        public static string TOLOCALDECIMAL(this string str)
        {
            string strSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return str.Replace(",", strSeparator).Replace(".", strSeparator);
        }
    }
}
