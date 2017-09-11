using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API
{
    public static class Extensions
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

        public static byte TOBYTE(this object o, byte bDefault = 0)
        {
            byte bResult = bDefault;
            if (o != null && o is DBNull == false)
            {
                if (o is Decimal)
                {
                    byte.TryParse(Math.Round(((Decimal)o), 0).ToString().Trim(), out bResult);
                }
                else
                {
                    byte.TryParse(o.ToString().Trim(), out bResult);
                }
            }
            return bResult;
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

        public static object ISNULL(this object o, object oDefault = null)
        {
            object oResult = oDefault;
            if (o != null && o is DBNull == false)
                oResult = o;
            return oResult;
        }

        public static decimal ROUNDTWO(this decimal o)
        {
            decimal dReturn = Math.Round(o, 2);
            return dReturn;
        }
    }
}
