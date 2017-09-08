using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winsell.YK.Ingenico
{
    public static class IngenicoExtensions
    {
        public static double TODOUBLE(this object o)
        {
            double dblReturn = 0;
            if (o != null && o is DBNull == false)
            {
                double.TryParse(o.ToString().Trim(), out dblReturn);
            }
            return dblReturn;
        }

        public static bool TOBOOLEAN(this object o)
        {
            bool dblReturn = false;
            if (o != null && o is DBNull == false)
            {
                bool.TryParse(o.ToString().Trim(), out dblReturn);
            }
            return dblReturn;
        }

        public static decimal TODECIMAL(this object o)
        {
            decimal dReturn = 0;
            if (o != null && o is DBNull == false)
            {
                decimal.TryParse(o.ToString().Trim(), out dReturn);
            }
            return dReturn;
        }

        public static int TOINTEGER(this object o)
        {
            int intReturn = 0;
            if (o != null && o is DBNull == false)
            {
                int.TryParse(o.ToString().Trim(), out intReturn);
            }
            return intReturn;
        }

        public static UInt32 TOUINT32(this object o)
        {
            UInt32 intReturn = 0;
            if (o != null && o is DBNull == false)
            {
                UInt32.TryParse(o.ToString().Trim(), out intReturn);
            }
            return intReturn;
        }

        public static ushort TOUSHORT(this object o)
        {
            ushort intReturn = 0;
            if (o != null && o is DBNull == false)
            {
                ushort.TryParse(o.ToString().Trim(), out intReturn);
            }
            return intReturn;
        }

        public static byte TOBYTE(this object o)
        {
            byte bytReturn = 0;
            if (o != null && o is DBNull == false)
            {
                byte.TryParse(o.ToString().Trim(), out bytReturn);
            }
            return bytReturn;
        }

        public static bool ISNULLOREMPTY(this string str)
        {
            return String.IsNullOrEmpty(str.Trim());
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
