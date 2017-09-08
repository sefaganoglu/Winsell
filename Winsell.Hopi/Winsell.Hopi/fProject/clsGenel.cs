using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Winsell.Hopi.fProject
{
    public static class clsGenel
    {
        public enum OdemeTipi
        {
            HopiTahsilat = 0,
            HopiIndirim = 1
        }

        public static string connectionString = ConfigurationManager.AppSettings["ConnectionString"].TOSTRING();
        public static string kullaniciKodu = "";
        public static string kullaniciAdi = "";
        public static string numberSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static string hopiIndirimOdemeKodu = "";
        public static string hopiOdemeKodu = "";
        public static string magazaKodu = "";
        public static string storeCode = "";

        public static string kampanyaServerName = "";
        public static string kampanyaDatabaseName = "";
        public static string kampanyaUserName = "";
        public static string kampanyaPassword = "";

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

        public static Button masaOlustur(clsSiniflar.Masa masa)
        {
            Button butonMasa = new Button();
            butonMasa.AccessibleDescription = masa.masaNo.TOSTRING();
            butonMasa.Tag = masa.tutar;
            butonMasa.Text = "Masa No: " + masa.masaNoStr.TOSTRING() + Environment.NewLine + "Tutar: " + masa.tutar.ToString("N2");
            butonMasa.AccessibleName = masa.masaNoStr.TOSTRING();
            butonMasa.AccessibleDefaultActionDescription = masa.yetki;
            butonMasa.Width = masa.width;
            butonMasa.Height = masa.height;
            butonMasa.Cursor = System.Windows.Forms.Cursors.Hand;
            butonMasa.BackColor = masa.color;
            return butonMasa;
        }

        public static bool kismiOdemeVar(int masaNo, int adisyonNo)
        {
            bool blnReturn = false;

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(RC.CEKNO) AS Sayi FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KOD = 'A'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            blnReturn = cmd.ExecuteScalar().TOINT() > 0;
            cmd.Dispose();
            cnn.Close();

            return blnReturn;
        }

        public static bool tekStokVar(int masaNo, int adisyonNo)
        {
            bool blnReturn = false;

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT SUM(RC.MIKTAR) AS Sayi FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KOD = 'B' AND LTRIM(RTRIM(ISNULL(RC.GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = ''";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            blnReturn = cmd.ExecuteScalar().TODECIMAL() == 1;
            cmd.Dispose();
            cnn.Close();

            return blnReturn;
        }

        public static clsSiniflar.AdisyonBilgisi secAdisyon(int masaNo, bool hopiOlacak = false)
        {
            clsSiniflar.AdisyonBilgisi adisyonBilgisi = new clsSiniflar.AdisyonBilgisi();

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(DISTINCT RC.CEKNO) AS Sayi FROM RESCEK AS RC WHERE MASANO = @MASANO AND KOD = 'B'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            if (cmd.ExecuteScalar().TOINT() == 1)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT DISTINCT RC.CEKNO, SUM(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC WHERE MASANO = @MASANO AND KOD = 'B' GROUP BY RC.CEKNO";
                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    adisyonBilgisi.adisyonNo = reader["CEKNO"].TOINT();
                    adisyonBilgisi.adisyonTutar = reader["Tutar"].TODECIMAL();
                }
                reader.Close();
            }
            else
            {
                frmAdisyonListesi falAdisyonListesi = new frmAdisyonListesi(masaNo, hopiOlacak);
                if (falAdisyonListesi.ShowDialog() == DialogResult.OK)
                    adisyonBilgisi = falAdisyonListesi.adisyonBilgisi;
                falAdisyonListesi.Dispose();
            }
            cmd.Dispose();
            cnn.Close();

            return adisyonBilgisi;
        }

        public static void hesapBol(int masaNo, int adisyonNo)
        {
            frmHesapBol falHesapBol = new frmHesapBol(masaNo, adisyonNo);
            falHesapBol.ShowDialog();
            falHesapBol.Dispose();
        }

        public static bool kullaniciGirisi()
        {
            bool blnReturn = false;
            kullaniciKodu = "";
            kullaniciAdi = "";
            frmKullaniciGirisi fkgKullaniciGirisi = new frmKullaniciGirisi();
            if (fkgKullaniciGirisi.ShowDialog() == DialogResult.OK)
            {
                clsGenel.kullaniciKodu = fkgKullaniciGirisi.kullaniciKodu;
                clsGenel.kullaniciAdi = fkgKullaniciGirisi.kullaniciAdi;
                blnReturn = true;
            }
            return blnReturn;
        }

        public static clsSiniflar.KimlikBilgisi kimlikKontrol()
        {
            clsSiniflar.KimlikBilgisi kimlikBilgisi = null;
            frmKimlikOkuma fkoKimlikOkuma = new frmKimlikOkuma();
            if (fkoKimlikOkuma.ShowDialog() == DialogResult.OK)
                kimlikBilgisi = fkoKimlikOkuma.kimlikBilgisi;
            fkoKimlikOkuma.Dispose();
            return kimlikBilgisi;
        }

        public static clsSiniflar.KampanyaBilgisi secKampanya(int masaNo, int adisyonNo, decimal paracik, decimal adisyonTutari)
        {
            clsSiniflar.KampanyaBilgisi kampanyaBilgisi = null;
            frmKampanyaListesi fklKampanyaListesi = new frmKampanyaListesi(masaNo, adisyonNo, paracik, adisyonTutari);
            if (fklKampanyaListesi.ShowDialog() == DialogResult.OK)
            {
                kampanyaBilgisi = fklKampanyaListesi.kampanyaBilgisi;
            }
            fklKampanyaListesi.Dispose();
            return kampanyaBilgisi;
        }


        public static string getExePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", "");
        }
        public static void directoryControl(string strPath)
        {
            string[] arrSplit = strPath.Split('\\');
            string strPathConfig = "";
            for (int i = 0; i < arrSplit.Length; i++)
            {
                strPathConfig += arrSplit[i] + "\\";
                if (!Directory.Exists(strPathConfig))
                {
                    Directory.CreateDirectory(strPathConfig);
                }
            }
        }

        public static object parametreOku(string strAlan, object oDefault = null)
        {
            object oReturn = oDefault;

            SqlConnection cnn = clsGenel.createDBConnection();
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

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 KOD FROM KRDKRT WHERE BANKA_KARTI = @BANKA_KARTI";
            cmd.Parameters.AddWithValue("@BANKA_KARTI", strBankaKarti);
            strReturn = cmd.ExecuteScalar().TOSTRING();
            cmd.Dispose();
            cnn.Close();

            return strReturn;
        }

        public static bool kampanyaGuncelle()
        {
            bool blnReturn = false;

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.Transaction = cnn.BeginTransaction();

            SqlConnection cnnKampanya = clsGenel.createDBConnectionKampanya();
            SqlCommand cmdKampanya = cnnKampanya.CreateCommand();

            try
            {
                DataTable dtHopi;


                List<string> lstTablolar = new List<string>();
                lstTablolar.Add("HOPI");
                lstTablolar.Add("HOPI_STOK");
                lstTablolar.Add("HOPI_ANAGRUP");
                lstTablolar.Add("HOPI_SINIF");

                foreach (string strTablo in lstTablolar)
                {
                    dtHopi = new DataTable();

                    cmd.CommandText = "DELETE FROM " + strTablo;
                    cmd.ExecuteNonQuery();

                    cmdKampanya.CommandText = "SELECT * FROM " + strTablo + " AS H WHERE (SELECT COUNT(KOD) FROM HOPI_SIRKET WHERE KOD = H.KOD AND SIRKET_KODU = @SIRKET_KODU) > 0";
                    cmdKampanya.Parameters.AddWithValue("@SIRKET_KODU", magazaKodu);
                    SqlDataAdapter sdaKampanya = new SqlDataAdapter(cmdKampanya);
                    sdaKampanya.Fill(dtHopi);
                    sdaKampanya.Dispose();
                    cmdKampanya.Parameters.Clear();


                    string strHopiAlan = "";
                    string strHopiParametre = "";

                    foreach (DataColumn DC in dtHopi.Columns)
                    {
                        if (DC.ColumnName != "ID" && DC.ColumnName != "SIRANO")
                        {
                            strHopiAlan += !string.IsNullOrEmpty(strHopiAlan) ? ", " : "";
                            strHopiParametre += !string.IsNullOrEmpty(strHopiParametre) ? ", " : "";
                            strHopiAlan += DC.ColumnName;
                            strHopiParametre += "@" + DC.ColumnName;
                        }
                    }

                    cmd.CommandText = "INSERT INTO " + strTablo + " (" + strHopiAlan + ") " +
                                      "VALUES (" + strHopiParametre + ")";
                    foreach (DataRow DR in dtHopi.Rows)
                    {
                        foreach (DataColumn DC in dtHopi.Columns)
                        {
                            cmd.Parameters.AddWithValue("@" + DC.ColumnName, DR[DC.ColumnName]);
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                cmd.Transaction.Commit();

                blnReturn = true;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                MessageBox.Show("Kampanyalar güncellenirken bir hata ile karşılaşıldı." + Environment.NewLine + ex.Message,
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            cmdKampanya.Dispose();
            cnnKampanya.Close();
            cmd.Dispose();
            cnn.Close();

            return blnReturn;
        }
    }
}
