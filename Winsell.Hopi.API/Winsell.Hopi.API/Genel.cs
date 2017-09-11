using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API
{
    public static class Genel
    {
        public enum OdemeTipi
        {
            HopiTahsilat = 0,
            HopiIndirim = 1
        }

        public static string connectionString = "";
        
        public static string kampanyaServerName = "";
        public static string kampanyaDatabaseName = "";
        public static string kampanyaUserName = "";
        public static string kampanyaPassword = "";

        public static string hopiIndirimOdemeKodu = "";
        public static string hopiOdemeKodu = "";
        public static string magazaKodu = "";
        public static string storeCode = "";

        public static string kullaniciKoduWS = "";
        public static string sifreWS = "";

        public static SqlConnection createDBConnection()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            return cnn;
        }
        
        public static SqlConnection createDBConnectionKampanya()
        {
            SqlConnection cnn = new SqlConnection("Data Source=" + kampanyaServerName + ";Persist Security Info=true;User Id=" + kampanyaUserName + ";Password=" + kampanyaPassword + ";Initial Catalog=" + kampanyaDatabaseName);
            cnn.Open();
            return cnn;
        }
        
        public static object parametreOku(string strAlan, object oDefault = null)
        {
            object oReturn = oDefault;

            SqlConnection cnn = createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 " + strAlan + " FROM RESPRM";
            object o = cmd.ExecuteScalar();
            if (o != null && o is DBNull == false)
                oReturn = o;
            cmd.Dispose();
            cnn.Close();

            return oReturn;
        }
        public static string odemeTipiGetir(OdemeTipi odemeTipi)
        {
            string strReturn = "";

            string strBankaKarti = "";

            switch (odemeTipi)
            {
                case OdemeTipi.HopiIndirim:
                    strBankaKarti = "M";
                    break;
                case OdemeTipi.HopiTahsilat:
                    strBankaKarti = "H";
                    break;
                default:
                    break;
            }

            SqlConnection cnn = createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 KOD FROM KRDKRT WHERE BANKA_KARTI = @BANKA_KARTI";
            cmd.Parameters.AddWithValue("@BANKA_KARTI", strBankaKarti);
            strReturn = cmd.ExecuteScalar().TOSTRING();
            cmd.Dispose();
            cnn.Close();

            return strReturn;
        }
    }
}
