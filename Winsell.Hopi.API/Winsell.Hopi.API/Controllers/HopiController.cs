using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Winsell.Hopi.API.Models;
using System.Configuration;
using System.Drawing;
using Winsell.Hopi.Data;
using System.Data;
using Newtonsoft.Json;

namespace Winsell.Hopi.API.Controllers
{
    [Route("api/[controller]")]
    public class HopiController : Controller
    {
        [HttpGet("doluMasalar")]
        public List<Masa> GetDoluMasalar()
        {
            List<Masa> listMasa = new List<Masa>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT RC.MASANO, RC.MASANOSTR, (SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO) - " +
                              "(ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS FLOAT)) FROM ODEME_TEMP WHERE CEKNO IN (SELECT DISTINCT CEKNO FROM RESCEK WHERE MASANO = RC.MASANO)), CAST(0 AS FLOAT)) / 100) " +
                              "AS Tutar, " +
                              "CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK AS RC1 WHERE MASANO = RC.MASANO AND (SELECT SUM(MIKTAR) AS Sayi FROM RESCEK WHERE MASANO = RC1.MASANO AND CEKNO = RC1.CEKNO AND KOD = 'B' AND LTRIM(RTRIM(ISNULL(GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = '' AND (SELECT COUNT(SIRANO) FROM HOPIHRY WHERE MASANO = RC1.MASANO AND CEKNO = RC1.CEKNO) = 0) <> 1) > 0 THEN 1 ELSE 0 END AS KullanilabilirBolme, " +
                              "CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK AS RC1 WHERE MASANO = RC.MASANO AND (SELECT COUNT(SIRANO) FROM HOPIHRY WHERE MASANO = RC1.MASANO AND CEKNO = RC1.CEKNO) = 0) > 0 THEN 1 ELSE 0 END AS KullanilabilirOdeme, " +
                              "CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK AS RC1 WHERE MASANO = RC.MASANO AND (SELECT COUNT(SIRANO) FROM HOPIHRY WHERE MASANO = RC1.MASANO AND CEKNO = RC1.CEKNO) <> 0) > 0 THEN 1 ELSE 0 END AS KullanilabilirIade " +
                              "FROM RESCEK AS RC " +
                              "ORDER BY RC.MASANO";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Masa masa = new Masa()
                {
                    no = reader["MASANO"].TOINT(),
                    noStr = reader["MASANOSTR"].TOSTRING(),
                    tutar = reader["Tutar"].TODECIMAL(),
                    yetki = reader["KullanilabilirBolme"].TOSTRING() + reader["KullanilabilirOdeme"].TOSTRING() + reader["KullanilabilirIade"].TOSTRING(),
                    color = (reader["KullanilabilirOdeme"].TOINT() == 1 ? Color.Salmon : Color.Silver),
                    height = 100,
                    width = 100,
                    stoklar = GetMasadakiStoklar(reader["MASANO"].TOINT())
                };

                listMasa.Add(masa);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listMasa;
        }

        [HttpGet("masadakiStoklar")]
        public List<Stok> GetMasadakiStoklar(int masaNo)
        {
            List<Stok> listStok = new List<Stok>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.X, RC.STOKKODU, RC.STOKADI, RC.MIKTAR, RC.MIKTAR_ACK, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) - " +
                              "ROUND(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) * ((ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS MONEY)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS MONEY)) / CAST(100 AS MONEY)) / ISNULL((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS MONEY))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS MONEY))), 2) " +
                              "AS TUTAR FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.KOD = 'B'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stok stok = new Stok()
                {
                    adi = reader["STOKADI"].TOSTRING(),
                    miktariAck = reader["MIKTAR_ACK"].TOSTRING(),
                    tutari = reader["Tutar"].TODECIMAL(),
                    miktari = reader["MIKTAR"].TODECIMAL(),
                    miktariDeg = reader["MIKTAR"].TODECIMAL(),
                    kodu = reader["STOKKODU"].TOSTRING(),
                    x = reader["X"].TOINT()
                };

                listStok.Add(stok);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listStok;
        }

        [HttpGet("adisyondakiStoklar")]
        public List<Stok> GetAdisyondakiStoklar(int masaNo, int adisyonNo)
        {
            List<Stok> listStok = new List<Stok>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.X, RC.STOKKODU, RC.STOKADI, MIKTAR_ACK, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) - " +
                              "ROUND(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) * ((ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS MONEY)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS MONEY)) / CAST(100 AS MONEY)) / ISNULL((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS MONEY))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS MONEY))), 2) " +
                              "AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO AND RC.KOD = 'B'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stok stok = new Stok()
                {
                    adi = reader["STOKADI"].TOSTRING(),
                    miktariAck = reader["MIKTAR_ACK"].TOSTRING(),
                    tutari = reader["Tutar"].TODECIMAL(),
                    miktari = reader["MIKTAR"].TODECIMAL(),
                    miktariDeg = reader["MIKTAR"].TODECIMAL(),
                    kodu = reader["STOKKODU"].TOSTRING(),
                    x = reader["X"].TOINT()
                };

                listStok.Add(stok);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listStok;
        }

        [HttpGet("adisyondakiStoklarOzet")]
        public List<Stok> GetAdisyondakiStoklarOzet(int masaNo, int adisyonNo)
        {
            List<Stok> listStok = new List<Stok>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.X, RC.STOKKODU, RC.STOKADI, RC.MIKTAR, RC.MIKTAR_ACK, (SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KOD = RC.KOD AND X = RC.X) - " +
                              "ROUND((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KOD = RC.KOD AND X = RC.X) * ((ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS MONEY)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS MONEY)) / CAST(100 AS MONEY)) / ISNULL((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS MONEY))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS MONEY))), 2) " +
                              "AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO AND RC.KOD = 'B' AND LTRIM(RTRIM(ISNULL(RC.GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = ''";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stok stok = new Stok()
                {
                    adi = reader["STOKADI"].TOSTRING(),
                    miktariAck = reader["MIKTAR_ACK"].TOSTRING(),
                    tutari = reader["Tutar"].TODECIMAL(),
                    miktari = reader["MIKTAR"].TODECIMAL(),
                    miktariDeg = reader["MIKTAR"].TODECIMAL(),
                    kodu = reader["STOKKODU"].TOSTRING(),
                    x = reader["X"].TOINT(),
                };

                listStok.Add(stok);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listStok;
        }

        [HttpGet("masadakiAdisyonlar")]
        public List<Adisyon> GetMasadakiAdisyonlar(int masaNo)
        {
            List<Adisyon> listAdisyon = new List<Adisyon>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.CEKNO, SUM(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) - " +
                              "(ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS FLOAT)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) / 100) " +
                              "AS Tutar, CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND ISNULL(BIRDID, CAST(0 AS INT)) > 0) = 0 THEN CAST(0 AS INT) ELSE CAST(1 AS INT) END AS Hopi_Var FROM RESCEK AS RC WHERE RC.MASANO = @MASANO GROUP BY RC.MASANO, RC.CEKNO";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Adisyon adisyon = new Adisyon()
                {
                    no = reader["CEKNO"].TOINT(),
                    tutar = reader["Tutar"].TODECIMAL(),
                    hopi = reader["Hopi_Var"].TOBYTE(),
                    stoklar = GetAdisyondakiStoklar(masaNo, reader["CEKNO"].TOINT())
                };

                listAdisyon.Add(adisyon);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listAdisyon;
        }

        [HttpPost("hesapBol")]
        public DurumResponse SetHesapBol(HesapBolmeRequest hesapBolmeRequest)
        {
            DurumResponse hesapBolmeResponse = new DurumResponse();

            hesapBolmeResponse.basarili = false;

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.Transaction = cnn.BeginTransaction();

            try
            {
                int islemSayisi = 0;
                foreach (Stok stok in hesapBolmeRequest.stoklar)
                {
                    if (stok.miktariDeg.TODECIMAL() == stok.miktari.TODECIMAL())
                    {
                        cmd.CommandText = "UPDATE RESCEK SET CEKNO = ISNULL((SELECT TOP 1 ADISYONNO FROM SIRANO), CAST(0 AS INT)) + 1 WHERE MASANO = @MASANO AND X = @X AND KOD = 'B'";
                        cmd.Parameters.AddWithValue("@MASANO", hesapBolmeRequest.masaNo);
                        cmd.Parameters.AddWithValue("@X", stok.x);
                        islemSayisi += cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    else
                    {

                        cmd.CommandText = "INSERT INTO RESCEK (STOKKODU, AFIY, SFIY, MIKTAR, KDV, MASANO, TARIH, ISKONTOORANI, ISKONTOTUTARI1, ISKONTOTUTARI, IPTMIKTAR, IPTTUTAR, CEKNO, STOKADI, KARTKODU, SATICIKODU, YAZNO, " +
                                          "KOD, YAZICI, YAZSIRA, ALACAK, FATURANO, HESAPNO, ADISYON_TIPI, CHSMAS_SIRANO, HESAPADI, R_A, KISI_SAYISI, SAAT, MIKTAR_ACK, PORSIYON_MIK, ADET, KURYE_KODU, STOKADI_D1, STOKADI_D2, " +
                                          "DVZ_KODU, DVZ_TUTARI, SIRKET_KODU, ONCELIK, SANDELYE, CEK_YAZDIR_NO, GARNITUR_MASTER_STOKKODU, X, KISMI_ODEME_ISARET, MASANOSTR, GITTI, BARKOD, TIP, GUN_SIP_NO, ZAYI_SEBEBI, " +
                                          "SATIS_TURU, SIPARISNO, ORJ_FIYAT, SATICIKODU1, INDIRIM_CARISI, KAYNAK, KAYNAK_ZAMANI, DVZ_KUR, DVZ_TUTARI_ALINACAK, ODENMEZ_KOD, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_KOD, " +
                                          "ODENMEZ_DETAY_ACIKLAMA, YSEPETI_MessageId, WINDOWS_USER_NAME, HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, ALINDI, MASTER_CEKNO, TERAZI_USER, TERAZI_ID, ILK_FIYAT) " +

                                          "SELECT STOKKODU, AFIY, SFIY, @MIKTAR, KDV, MASANO, TARIH, ISKONTOORANI, ISKONTOTUTARI1, ISKONTOTUTARI, IPTMIKTAR, IPTTUTAR, ISNULL((SELECT TOP 1 ADISYONNO FROM SIRANO), CAST(0 AS INT)) + 1 AS CEKNO, STOKADI, KARTKODU, SATICIKODU, YAZNO, " +
                                          "KOD, YAZICI, YAZSIRA, ALACAK, FATURANO, HESAPNO, ADISYON_TIPI, CHSMAS_SIRANO, HESAPADI, R_A, KISI_SAYISI, SAAT, CAST(CONVERT(Decimal(8,0), @MIKTAR_ACK) AS VARCHAR) + '*' + CAST(CONVERT(Decimal(8,0), PORSIYON_MIK) AS VARCHAR) AS MIKTAR_ACK, PORSIYON_MIK, @ADET, KURYE_KODU, STOKADI_D1, STOKADI_D2, " +
                                          "DVZ_KODU, DVZ_TUTARI, SIRKET_KODU, ONCELIK, SANDELYE, CEK_YAZDIR_NO, GARNITUR_MASTER_STOKKODU, X + (SELECT COUNT(DISTINCT X) + 1 FROM RESCEK WHERE X > RC.X), KISMI_ODEME_ISARET, MASANOSTR, GITTI, BARKOD, TIP, GUN_SIP_NO, ZAYI_SEBEBI, " + //ISNULL((SELECT MAX(SIRANO) FROM RESCEK), CAST(0 AS INT)) + 1 AS X
                                          "SATIS_TURU, SIPARISNO, ORJ_FIYAT, SATICIKODU1, INDIRIM_CARISI, KAYNAK, KAYNAK_ZAMANI, DVZ_KUR, DVZ_TUTARI_ALINACAK, ODENMEZ_KOD, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_KOD, " +
                                          "ODENMEZ_DETAY_ACIKLAMA, YSEPETI_MessageId, WINDOWS_USER_NAME, HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, ALINDI, MASTER_CEKNO, TERAZI_USER, TERAZI_ID, ILK_FIYAT " +
                                          "FROM RESCEK AS RC WHERE MASANO = @MASANO AND X = @X AND KOD = 'B'";
                        cmd.Parameters.AddWithValue("@MIKTAR", stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@MIKTAR_ACK", stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@ADET", stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@MASANO", hesapBolmeRequest.masaNo);
                        cmd.Parameters.AddWithValue("@X", stok.x);
                        islemSayisi += cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE RESCEK SET MIKTAR = @MIKTAR, ADET = @ADET, MIKTAR_ACK = CAST(CONVERT(Decimal(8,0), @MIKTAR_ACK) AS VARCHAR) + '*' + CAST(CONVERT(Decimal(8,0), PORSIYON_MIK) AS VARCHAR) WHERE MASANO = @MASANO AND X = @X AND KOD = 'B'";
                        cmd.Parameters.AddWithValue("@MIKTAR", stok.miktari.TODECIMAL() - stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@ADET", stok.miktari.TODECIMAL() - stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@MIKTAR_ACK", stok.miktari.TODECIMAL() - stok.miktariDeg.TODECIMAL());
                        cmd.Parameters.AddWithValue("@MASANO", hesapBolmeRequest.masaNo);
                        cmd.Parameters.AddWithValue("@X", stok.x);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (islemSayisi > 0)
                {
                    cmd.CommandText = "UPDATE SIRANO SET ADISYONNO = ADISYONNO + 1";
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                cmd.Transaction.Commit();
                hesapBolmeResponse.basarili = true;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                hesapBolmeResponse.hataMesaji = "Kayıt aşamasında hata oluştu:" + Environment.NewLine + ex.Message;
            }
            cmd.Dispose();
            cnn.Close();

            return hesapBolmeResponse;
        }

        [HttpGet("kampanyalar")]
        public List<Kampanya> GetKampanyalar(int masaNo, int adisyonNo)
        {
            List<Kampanya> listKampanya = new List<Kampanya>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT CAST(0 AS INT) AS Sec, H.KOD, H.ACIKLAMA, ISNULL(H.FIYATSAL_SINIR, CAST(0 AS INT)) AS FIYATSAL_SINIR, ISNULL(H.ADET, CAST(0 AS INT)) AS ADET, ISNULL(H.HOPI_KAZAN_ORANI, CAST(0 AS INT)) AS HOPI_KAZAN_ORANI, ISNULL(H.HOPI_KAZAN_PARA, CAST(0 AS money)) AS HOPI_KAZAN_PARA, ISNULL(H.HOPI_KAT, CAST(0 AS money)) AS HOPI_KAT, ISNULL(H.INDIRIM_ORANI, CAST(0 AS INT)) AS INDIRIM_ORANI, ISNULL(H.HOPI_KAT_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_HOPI_KAT, ISNULL(H.KAZANILACAK_PARACIK_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_KAZAN_PARA, " +

                              "CASE WHEN ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' THEN A.StokMiktar " +
                              "WHEN ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' THEN A.GrupMiktar " +
                              "WHEN ISNULL(LTRIM(RTRIM(SINIF_KODU)), CAST('' AS VARCHAR)) <> '' THEN A.SinifMiktar " +
                              "ELSE CAST(0 AS FLOAT) END AS Miktar " +

                              "FROM " +
                              "(SELECT DISTINCT H.KOD, H.ACIKLAMA, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN HOPI_STOK AS HS ON HS.KOD = H.KOD AND HS.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS StokMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON RC1.STOKKODU = SM.STOKKODU INNER JOIN HOPI_ANAGRUP AS HS ON HS.KOD = H.KOD AND HS.ANAGRUP = SM.YEMEK_KODU1 WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS GrupMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON RC1.STOKKODU = SM.STOKKODU INNER JOIN HOPI_SINIF AS HS ON HS.KOD = H.KOD AND HS.SINIFKODU = SM.SINIF_KODU WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS SinifMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN HOPI_STOK AS HS ON HS.KOD = H.KOD AND HS.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS StokTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.SFIY * RC1.MIKTAR, CAST(0 AS FLOAT)) - ISNULL(RC1.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) INNER JOIN HOPI_ANAGRUP AS HS ON HS.KOD = H.KOD AND HS.ANAGRUP = SM.YEMEK_KODU1 WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS GrupTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.SFIY * RC1.MIKTAR, CAST(0 AS FLOAT)) - ISNULL(RC1.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) INNER JOIN HOPI_SINIF AS HS ON HS.KOD = H.KOD AND HS.SINIFKODU = SM.SINIF_KODU WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS SinifTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS AdisyonTutar " +
                              "FROM HOPI AS H " +
                              "INNER JOIN RESCEK AS RC ON RC.TARIH >= H.BAS_TARIHI AND RC.TARIH <= H.BIT_TARIHI AND RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO " +
                              "WHERE H.AKTIF = 1 " +
                              (string.IsNullOrWhiteSpace(Genel.kampanyaServerName.Trim()) ? "AND (SELECT COUNT(KOD) FROM HOPI_SIRKET WHERE KOD = H.KOD AND SIRKET_KODU = @SIRKET_KODU) > 0" : "") +
                              ") AS A INNER JOIN HOPI H ON A.KOD = H.KOD AND H.ID = (SELECT MIN(ID) FROM HOPI WHERE KOD = H.KOD) " +
                              "WHERE " +
                              "   ((SELECT COUNT(STOKKODU  ) FROM HOPI_STOK    WHERE KOD = H.KOD) > 0 AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= StokMiktar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= StokTutar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (StokMiktar <> 0 OR StokTutar <> 0)))) " +
                              "OR ((SELECT COUNT(ANAGRUP   ) FROM HOPI_ANAGRUP WHERE KOD = H.KOD) > 0 AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= GrupMiktar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= GrupTutar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (GrupMiktar <> 0 OR GrupTutar <> 0)))) " +
                              "OR ((SELECT COUNT(SINIFKODU ) FROM HOPI_SINIF   WHERE KOD = H.KOD) > 0 AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= SinifMiktar) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= SinifTutar) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (SinifMiktar <> 0 OR SinifTutar <> 0)))) " +
                              "OR ((SELECT COUNT(STOKKODU  ) FROM HOPI_STOK    WHERE KOD = H.KOD) = 0 AND (SELECT COUNT(ANAGRUP   ) FROM HOPI_ANAGRUP WHERE KOD = H.KOD) = 0 AND (SELECT COUNT(SINIFKODU ) FROM HOPI_SINIF   WHERE KOD = H.KOD) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= AdisyonTutar) " +
                              "ORDER BY HOPI_KAT DESC, INDIRIM_ORANI DESC, HOPI_KAZAN_ORANI DESC, HOPI_KAZAN_PARA DESC";

            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            if (string.IsNullOrWhiteSpace(Genel.kampanyaServerName.Trim()))
                cmd.Parameters.AddWithValue("@SIRKET_KODU", Genel.magazaKodu);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kampanya kampanya = new Kampanya()
                {
                    kodu = reader["KOD"].TOSTRING(),
                    aciklama = reader["ACIKLAMA"].TOSTRING(),
                    adet = reader["ADET"].TOINT(),
                    indirimKat = reader["HOPI_KAT"].TODECIMAL().ROUNDTWO(),
                    indirimOrani = reader["INDIRIM_ORANI"].TOINT(),
                    kazancOrani = reader["HOPI_KAZAN_ORANI"].TOINT(),
                    kazancParacik = reader["HOPI_KAZAN_PARA"].TODECIMAL().ROUNDTWO(),
                    maksimumKatParacik = reader["MAX_HOPI_KAT"].TODECIMAL().ROUNDTWO(),
                    maksimumKazanc = reader["MAX_KAZAN_PARA"].TODECIMAL().ROUNDTWO(),
                    miktar = reader["Miktar"].TODECIMAL().ROUNDTWO(),
                    fiyatsalSinir = reader["FIYATSAL_SINIR"].TODECIMAL().ROUNDTWO(),
                    indirimler = new Dictionary<int, decimal>(),
                    tutarlar = new Dictionary<int, decimal>()
                };

                listKampanya.Add(kampanya);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listKampanya;
        }

        [HttpGet("kullaniciGirisi")]
        public Kullanici GetKullaniciGirisi(string sifre)
        {
            Kullanici kullanici = new Kullanici();

            kullanici.durum = new DurumResponse();
            kullanici.durum.basarili = false;
            kullanici.durum.hataMesaji = "Şifre yanlış.";

            try
            {
                SqlConnection cnn = Genel.createDBConnection();
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT K_KODU, KULLANICI_ADI FROM KRDRMZ WHERE MOBIL_SIFRE = @MOBIL_SIFRE";
                cmd.Parameters.AddWithValue("@MOBIL_SIFRE", sifre);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    kullanici.kullaniciKodu = reader["K_KODU"].TOSTRING();
                    kullanici.kullaniciAdi = reader["KULLANICI_ADI"].TOSTRING();
                    kullanici.durum.basarili = true;
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                kullanici.durum.hataMesaji = ex.Message;
            }

            return kullanici;
        }

        [HttpGet("tekStokKontrol")]
        public bool GetTekStokKontrol(int masaNo, int adisyonNo)
        {
            bool blnReturn = false;

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT SUM(RC.MIKTAR) AS Sayi FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KOD = 'B' AND LTRIM(RTRIM(ISNULL(RC.GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = ''";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            blnReturn = cmd.ExecuteScalar().TODECIMAL() == 1;
            cmd.Dispose();
            cnn.Close();

            return blnReturn;
        }

        [HttpGet("kismiOdemeKontrol")]
        public bool GetKismiOdemeKontrol(int masaNo, int adisyonNo)
        {
            bool blnReturn = false;

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(RC.CEKNO) AS Sayi FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KOD = 'A'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            blnReturn = cmd.ExecuteScalar().TOINT() > 0;
            cmd.Dispose();
            cnn.Close();

            return blnReturn;
        }

        [HttpGet("parametreDegeri")]
        public List<Parametre> GetParametreDegeri(List<ParametreRequest> parametreListesi)
        {
            List<Parametre> listParametre = new List<Parametre>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM RESPRM";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach (ParametreRequest prItem in parametreListesi)
                {
                    Parametre prSend = new Parametre()
                    {
                        adi = prItem.adi,
                        degeri = reader[prItem.adi].ISNULL(prItem.varsayilanDegeri)
                    };
                    listParametre.Add(prSend);
                }
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listParametre;
        }

        [HttpPost("kampanyaGuncelle")]
        public static DurumResponse SetKampanyaGuncelle()
        {
            DurumResponse drReturn = new DurumResponse();
            drReturn.basarili = false;

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.Transaction = cnn.BeginTransaction();

            SqlConnection cnnKampanya = Genel.createDBConnectionKampanya();
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
                    cmdKampanya.Parameters.AddWithValue("@SIRKET_KODU", Genel.magazaKodu);
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

                drReturn.basarili = true;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                drReturn.hataMesaji = "Kampanyalar güncellenirken bir hata ile karşılaşıldı." + Environment.NewLine + ex.Message;
            }
            cmdKampanya.Dispose();
            cnnKampanya.Close();
            cmd.Dispose();
            cnn.Close();

            return drReturn;
        }

        #region WebServiceIleIlgili
        [HttpPost("alisverisBilgisi")]
        public static DurumResponse SetAlisverisBilgisi(AlisverisBilgisi alisverisBilgisi)
        {
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = false;

            List<HopiWS.HopiRequestClasses.Urun> arrUrun = new List<HopiWS.HopiRequestClasses.Urun>();

            string strKampanyalar = "";
            if (alisverisBilgisi.kampanyalar.Count > 0)
            {
                foreach (Kampanya kampanya in alisverisBilgisi.kampanyalar)
                {
                    if (!string.IsNullOrWhiteSpace(strKampanyalar)) strKampanyalar += ", ";
                    strKampanyalar += "'" + kampanya.kodu + "'";
                }
            }
            else
            {
                strKampanyalar = "''";
            }

            string strSaticiKodu = "";

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT RC.STOKKODU, RC.SATICIKODU, RC.SIRANO, RC.X, RC.KDV, RC.MIKTAR, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) - " +
                              "ROUND(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) * ((ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS MONEY)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS MONEY)) / CAST(100 AS MONEY)) / ISNULL((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS MONEY))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS MONEY))), 2) " +
                              "AS TUTAR, STUFF(ISNULL((SELECT ',' + KOD FROM HOPI WHERE KOD IN (" + strKampanyalar + ") AND AKTIF = 1 AND ((STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU)) OR (ANAGRUP = SM.YEMEK_KODU1) OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '')) FOR XML PATH('')), CAST('' AS VARCHAR)), 1, 1, '') AS KAMPANYA " +
                              "FROM RESCEK AS RC " +
                              "INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU) " +
                              "WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO "; // AND SFIY > 0
            cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (string.IsNullOrWhiteSpace(strSaticiKodu.Trim()))
                    strSaticiKodu = reader["SATICIKODU"].TOSTRING();

                HopiWS.HopiRequestClasses.Urun urun = new HopiWS.HopiRequestClasses.Urun();
                urun.barkod = reader["STOKKODU"].TOSTRING();
                if (!string.IsNullOrWhiteSpace(reader["KAMPANYA"].TOSTRING()))
                    urun.kampanyaKodlari = reader["KAMPANYA"].TOSTRING().Split(',').ToList();
                urun.kdv = reader["KDV"].TOINT();
                urun.miktar = reader["MIKTAR"].TODECIMAL().ROUNDTWO();
                urun.tutar = reader["TUTAR"].TODECIMAL().ROUNDTWO();
                urun.x = reader["X"].TOINT();
                urun.siraNo = reader["SIRANO"].TOINT();

                arrUrun.Add(urun);
            }
            reader.Close();
            cmd.Parameters.Clear();

            //KATLI İNDİRİM HESAPLAMA
            foreach (Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                decimal adisyondanDusulecekIndirim = 0;

                decimal dblKampanyayaDahilTutar = arrUrun.Where(p => p.kampanyaKodlari.Contains(kampanya.kodu)).Sum(p => p.tutar);
                if (kampanya.indirimKat > 0)
                {
                    foreach (HopiWS.HopiRequestClasses.Urun urun in arrUrun)
                    {
                        if (urun.kampanyaKodlari.Contains(kampanya.kodu))
                        {
                            if (kampanya.tutarlar.ContainsKey(urun.kdv))
                                kampanya.tutarlar[urun.kdv] += urun.tutar - urun.indirimTutari;
                            else
                                kampanya.tutarlar.Add(urun.kdv, urun.tutar - urun.indirimTutari);

                            decimal gercekParacik = 0;
                            if (kampanya.maksimumKatParacik != 0)
                                gercekParacik = (alisverisBilgisi.harcanacakParacik > kampanya.maksimumKatParacik ? kampanya.maksimumKatParacik : alisverisBilgisi.harcanacakParacik);
                            else
                                gercekParacik = alisverisBilgisi.harcanacakParacik;

                            if (gercekParacik * kampanya.indirimKat > alisverisBilgisi.adisyonTutari)
                                gercekParacik = alisverisBilgisi.adisyonTutari / kampanya.indirimKat;
                            decimal indirimOrani = ((gercekParacik * kampanya.indirimKat) - gercekParacik) / (dblKampanyayaDahilTutar) * 100.TODECIMAL();

                            //indirimOrani = indirimOrani > 100 ? 100 : indirimOrani;

                            decimal indirimTutari = ((urun.tutar - urun.indirimTutari) * indirimOrani.TODECIMAL() / 100);
                            urun.indirimTutari += indirimTutari;
                            adisyondanDusulecekIndirim += indirimTutari;

                            if (kampanya.indirimler.ContainsKey(urun.kdv))
                                kampanya.indirimler[urun.kdv] += indirimTutari;
                            else
                                kampanya.indirimler.Add(urun.kdv, indirimTutari);
                        }
                    }
                }
                alisverisBilgisi.adisyonTutari -= adisyondanDusulecekIndirim;
            }
            ////////////////////////////
            alisverisBilgisi.adisyonTutari = alisverisBilgisi.adisyonTutari.ROUNDTWO();
            if (alisverisBilgisi.harcanacakParacik > alisverisBilgisi.adisyonTutari) alisverisBilgisi.harcanacakParacik = alisverisBilgisi.adisyonTutari.ROUNDTWO();

            //YÜZDELİ İNDİRİM HESAPLAMA (KATLI İNDİRİM DÜŞÜLDÜKTEN SONRA)
            foreach (Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                decimal adisyondanDusulecekIndirim = 0;

                decimal dblKampanyayaDahilTutar = arrUrun.Where(p => p.kampanyaKodlari.Contains(kampanya.kodu)).Sum(p => p.tutar);
                if (kampanya.indirimOrani > 0)
                {
                    foreach (HopiWS.HopiRequestClasses.Urun urun in arrUrun)
                    {
                        if (urun.kampanyaKodlari.Contains(kampanya.kodu))
                        {
                            if (kampanya.tutarlar.ContainsKey(urun.kdv))
                                kampanya.tutarlar[urun.kdv] += urun.tutar - urun.indirimTutari;
                            else
                                kampanya.tutarlar.Add(urun.kdv, urun.tutar - urun.indirimTutari);

                            var indirimOrani = kampanya.indirimOrani;
                            decimal indirimTutari = ((urun.tutar - urun.indirimTutari) * indirimOrani.TODECIMAL() / 100);
                            urun.indirimTutari += indirimTutari;
                            alisverisBilgisi.adisyonTutari -= urun.indirimTutari;

                            if (kampanya.indirimler.ContainsKey(urun.kdv))
                                kampanya.indirimler[urun.kdv] += indirimTutari;
                            else
                                kampanya.indirimler.Add(urun.kdv, indirimTutari);
                        }
                    }
                }
                alisverisBilgisi.adisyonTutari -= adisyondanDusulecekIndirim;
            }
            ////////////////////////////
            alisverisBilgisi.adisyonTutari = alisverisBilgisi.adisyonTutari.ROUNDTWO();

            HopiWS.HopiRequestClasses.AlisverisBilgisi alisverisBilgisiHopi = new HopiWS.HopiRequestClasses.AlisverisBilgisi();
            alisverisBilgisiHopi.birdId = alisverisBilgisi.birdId;
            alisverisBilgisiHopi.dateTime = DateTime.Now;
            alisverisBilgisiHopi.hopiPayId = 0;
            alisverisBilgisiHopi.kasaNo = strSaticiKodu;
            alisverisBilgisiHopi.kullanilacakParacik = alisverisBilgisi.harcanacakParacik;
            alisverisBilgisiHopi.storeCode = Genel.storeCode;
            alisverisBilgisiHopi.transactionId = alisverisBilgisiHopi.dateTime.ToString("yyyyMMddHHmmssfff") + "_" + Genel.storeCode + "_" + alisverisBilgisi.adisyonNo;

            foreach (Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                HopiWS.HopiRequestClasses.Kampanya kampanyaHopi = new HopiWS.HopiRequestClasses.Kampanya();
                kampanyaHopi.kampanyaKodu = kampanya.kodu;
                if (kampanya.kazancOrani != 0 || kampanya.kazancParacik != 0)
                {
                    var vKampanyaliKdvGrupluTutarlar = from tab in arrUrun.AsEnumerable()
                                                       where tab.kampanyaKodlari.Contains(kampanya.kodu)
                                                       group tab by tab.kdv
                                                       into groupDt
                                                       select new
                                                       {
                                                           Kdv = groupDt.Key,
                                                           SumTutar = groupDt.Sum((r) => (r.tutar - r.indirimTutari))
                                                       };

                    foreach (var item in vKampanyaliKdvGrupluTutarlar)
                    {
                        if (kampanya.tutarlar.ContainsKey(item.Kdv))
                            kampanya.tutarlar[item.Kdv] += item.SumTutar;
                        else
                            kampanya.tutarlar.Add(item.Kdv, item.SumTutar);
                    }


                    decimal kampanyaTutari = arrUrun.Where(p => p.kampanyaKodlari.Contains(kampanya.kodu)).Sum(p => p.tutar - p.indirimTutari);



                    if (kampanya.fiyatsalSinir == 0 || kampanyaTutari >= kampanya.fiyatsalSinir)
                    {
                        if (kampanya.kazancOrani > 0)
                            kampanyaHopi.paracikKazanc = kampanyaTutari * kampanya.kazancOrani / 100;
                        else
                            kampanyaHopi.paracikKazanc = kampanya.kazancParacik;

                        if (kampanyaHopi.paracikKazanc > kampanya.maksimumKazanc)
                            kampanyaHopi.paracikKazanc = kampanya.maksimumKazanc;

                        kampanyaHopi.paracikKazanc = kampanyaHopi.paracikKazanc * (kampanya.adet > 0 ? kampanya.miktar / kampanya.adet : 1);
                    }
                }
                kampanyaHopi.tutarlar = kampanya.tutarlar;
                kampanyaHopi.indirimler = kampanya.indirimler;

                alisverisBilgisiHopi.kampanyalar.Add(kampanyaHopi);
            }

            alisverisBilgisiHopi.urunler.AddRange(arrUrun);

            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetAlisverisBilgisi(alisverisBilgisiHopi);
            if (webServiceResult.IsSuccess)
            {
                cmd.Transaction = cnn.BeginTransaction();

                try
                {
                    decimal dIskontoToplami = 0;
                    cmd.CommandText = "UPDATE RESCEK SET BIRDID = @BIRDID, KAMPANYA_KODU = @KAMPANYA_KODU, HOPI_INDIRIM = @HOPI_INDIRIM WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND X = @X AND STOKKODU = @STOKKODU AND SIRANO = @SIRANO";
                    foreach (HopiWS.HopiRequestClasses.Urun urun in alisverisBilgisiHopi.urunler)
                    {
                        if (urun.kampanyaKodlari.Count > 0)
                        {
                            string strUrunKampanyalar = urun.kampanyaKodlari.Aggregate((i, j) => i + ',' + j);

                            cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisiHopi.birdId);
                            cmd.Parameters.AddWithValue("@KAMPANYA_KODU", strUrunKampanyalar);
                            cmd.Parameters.AddWithValue("@HOPI_INDIRIM", urun.indirimTutari);
                            cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                            cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                            cmd.Parameters.AddWithValue("@X", urun.x);
                            cmd.Parameters.AddWithValue("@STOKKODU", urun.barkod);
                            cmd.Parameters.AddWithValue("@SIRANO", urun.siraNo);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            dIskontoToplami += urun.indirimTutari;
                        }
                    }
                    dIskontoToplami = dIskontoToplami.ROUNDTWO();

                    if (dIskontoToplami != 0)
                    {
                        cmd.CommandText = "INSERT INTO ODEME_TEMP (CEKNO, LST1, LST2, ISKONTO, LST3, KARTKODU, ISK) " +
                                          "SELECT @CEKNO, ACIKLAMA, @SEPARATOR + '00', @ISKONTO, '0', KOD, @ISK FROM KRDKRT WHERE KOD = @KOD";
                        cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                        cmd.Parameters.AddWithValue("@SEPARATOR", Genel.numberSeparator);
                        cmd.Parameters.AddWithValue("@ISKONTO", dIskontoToplami.ROUNDTWO().TOSTRING().Replace(",", Genel.numberSeparator).Replace(".", Genel.numberSeparator));
                        cmd.Parameters.AddWithValue("@ISK", dIskontoToplami.ROUNDTWO().TOSTRING().Replace(",", Genel.numberSeparator).Replace(".", Genel.numberSeparator));
                        cmd.Parameters.AddWithValue("@KOD", Genel.hopiIndirimOdemeKodu);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //HOPI ODEME KAYDI
                    if (alisverisBilgisiHopi.kullanilacakParacik != 0)
                    {
                        cmd.CommandText = "INSERT INTO RESCEK (MASANO, TARIH, CEKNO, KARTKODU, SATICIKODU, KOD, ALACAK, MASANOSTR, KAYNAK, HESAPADI, INDIRIM_CARISI, BIRDID) " +
                                          "SELECT @MASANO, @TARIH, @CEKNO, KOD, @SATICIKODU, 'A', @ALACAK, @MASANOSTR, @KAYNAK, CAST('' AS VARCHAR), CAST('' AS VARCHAR), @BIRDID  FROM KRDKRT WHERE KOD = @KOD";
                        cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                        cmd.Parameters.AddWithValue("@TARIH", alisverisBilgisiHopi.dateTime.Date);
                        cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                        cmd.Parameters.AddWithValue("@SATICIKODU", alisverisBilgisi.kullaniciKodu);
                        cmd.Parameters.AddWithValue("@ALACAK", alisverisBilgisiHopi.kullanilacakParacik);
                        cmd.Parameters.AddWithValue("@MASANOSTR", alisverisBilgisi.masaNoStr);
                        cmd.Parameters.AddWithValue("@KAYNAK", 9);
                        cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisiHopi.birdId);
                        cmd.Parameters.AddWithValue("@KOD", Genel.hopiOdemeKodu);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //HOPIHRY KAYDI
                    cmd.CommandText = "INSERT INTO HOPIHRY (MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID) VALUES (@MASANO, @CEKNO, @KAMPANYA_KODU, @TUTAR, @INDIRIM, @KAZANILAN_PARACIK, @HARCANAN_PARACIK, @TRANSACTION_ID, @BIRDID, @ODEME_TRANSACTION_ID)";
                    cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                    cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                    cmd.Parameters.AddWithValue("@KAMPANYA_KODU", "");
                    cmd.Parameters.AddWithValue("@TUTAR", alisverisBilgisiHopi.urunler.AsEnumerable().Where(p => p.kampanyaKodlari.Count == 0).Sum(p => p.tutar).ROUNDTWO());
                    cmd.Parameters.AddWithValue("@INDIRIM", alisverisBilgisiHopi.urunler.AsEnumerable().Where(p => p.kampanyaKodlari.Count == 0).Sum(p => p.indirimTutari).ROUNDTWO());
                    cmd.Parameters.AddWithValue("@KAZANILAN_PARACIK", 0);
                    cmd.Parameters.AddWithValue("@HARCANAN_PARACIK", alisverisBilgisiHopi.kullanilacakParacik.ROUNDTWO());
                    cmd.Parameters.AddWithValue("@TRANSACTION_ID", alisverisBilgisiHopi.transactionId);
                    cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisiHopi.birdId);
                    cmd.Parameters.AddWithValue("@ODEME_TRANSACTION_ID", webServiceResult.ResultAttachment.TOULONG());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    foreach (HopiWS.HopiRequestClasses.Kampanya kampanya in alisverisBilgisiHopi.kampanyalar)
                    {
                        cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                        cmd.Parameters.AddWithValue("@KAMPANYA_KODU", kampanya.kampanyaKodu);
                        cmd.Parameters.AddWithValue("@TUTAR", kampanya.tutarlar.Sum(p => p.Value).ROUNDTWO());
                        cmd.Parameters.AddWithValue("@INDIRIM", kampanya.indirimler.Sum(p => p.Value).ROUNDTWO());
                        cmd.Parameters.AddWithValue("@KAZANILAN_PARACIK", kampanya.paracikKazanc.ROUNDTWO());
                        cmd.Parameters.AddWithValue("@HARCANAN_PARACIK", alisverisBilgisiHopi.kullanilacakParacik.ROUNDTWO());
                        cmd.Parameters.AddWithValue("@TRANSACTION_ID", alisverisBilgisiHopi.transactionId);
                        cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisiHopi.birdId);
                        cmd.Parameters.AddWithValue("@ODEME_TRANSACTION_ID", webServiceResult.ResultAttachment.TOULONG());
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //RESHRY KAYDI
                    cmd.CommandText = "SELECT SUM(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) AS Bakiye FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                    cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                    cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                    if (cmd.ExecuteScalar().TODECIMAL() <= 0)
                    {
                        cmd.Parameters.Clear();

                        cmd.CommandText = "INSERT INTO RESHRY (ADISYON_TIPI, AFIY, ALACAK, BARKOD, BIRDID, CEKNO, CHSMAS_SIRANO, DVZ_KODU, DVZ_KUR, DVZ_TUTARI, DVZ_TUTARI_ALINACAK, FATURANO, GARNITUR_MASTER_STOKKODU, GITTI, GUN_SIP_NO, HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, HESAPADI, HESAPNO, HOPI_INDIRIM, ILK_FIYAT, INDIRIM_CARISI, IPTMIKTAR, IPTTUTAR, ISKONTOORANI, ISKONTOTUTARI, ISKONTOTUTARI1, KAMPANYA_KODU, KARTKODU, KDV, KISI_SAYISI, KOD, KURYE_KODU, MASANO, MASANOSTR, MASTER_CEKNO, MIKTAR, MIKTAR_ACK, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_ACIKLAMA, ODENMEZ_DETAY_KOD, ODENMEZ_KOD, ORJ_FIYAT, R_A, SAAT, SATICIKODU, SATIS_TURU, SFIY, SIPARISNO, SIPARIS_YERI, SIRKET_KODU, STOKADI, STOKKODU, TARIH, TERAZI_ID, TERAZI_USER, TIP, X, X_S, YAZICI, YAZNO, YAZSIRA, YSEPETI_MessageId, ZAYI_SEBEBI) " +
                                          "SELECT ADISYON_TIPI, AFIY, ALACAK, BARKOD, BIRDID, CEKNO, CHSMAS_SIRANO, DVZ_KODU, DVZ_KUR, DVZ_TUTARI, DVZ_TUTARI_ALINACAK, FATURANO, GARNITUR_MASTER_STOKKODU, GITTI, ISNULL(GUN_SIP_NO, (SELECT TOP 1 GUN_SIP_NO FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND GUN_SIP_NO IS NOT NULL)), HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, HESAPADI, HESAPNO, HOPI_INDIRIM, ILK_FIYAT, INDIRIM_CARISI, IPTMIKTAR, IPTTUTAR, ISKONTOORANI, ISKONTOTUTARI, ISKONTOTUTARI1, KAMPANYA_KODU, KARTKODU, KDV, ISNULL(KISI_SAYISI, (SELECT TOP 1 KISI_SAYISI FROM RESYAZ WHERE CEKNO = RC.CEKNO)), KOD, KURYE_KODU, MASANO, MASANOSTR, MASTER_CEKNO, MIKTAR, MIKTAR_ACK, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_ACIKLAMA, ODENMEZ_DETAY_KOD, ODENMEZ_KOD, ORJ_FIYAT, @R_A, @SAAT, SATICIKODU, ISNULL(SATIS_TURU, (SELECT SATIS_TURU FROM KRDKRT WHERE KOD = (SELECT TOP 1 KARTKODU FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KARTKODU IS NOT NULL))), SFIY, SIPARISNO, @SIPARIS_YERI, ISNULL(SIRKET_KODU, CAST('' AS VARCHAR)) AS SIRKET_KODU, STOKADI, STOKKODU, TARIH, TERAZI_ID, TERAZI_USER, TIP, X, ISNULL(X, SIRANO) AS X_S, YAZICI, YAZNO, YAZSIRA, YSEPETI_MessageId, ZAYI_SEBEBI FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@R_A", "R");
                        cmd.Parameters.AddWithValue("@SAAT", alisverisBilgisiHopi.dateTime.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@SIPARIS_YERI", "R");
                        cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "DELETE FROM RESCEK WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@MASANO", alisverisBilgisi.masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", alisverisBilgisi.adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    cmd.Transaction.Commit();

                    durumResponse.basarili = true;
                }
                catch (Exception ex)
                {
                    cmd.Transaction.Rollback();
                    durumResponse.hataMesaji = "Ödeme/kazanma işlemi başarılı. Ancak veritabanına kaydederken hata oluştu." + Environment.NewLine + ex.Message;
                }
            }
            else
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            cmd.Dispose();
            cnn.Close();

            return durumResponse;
        }

        [HttpPost("alisverisIadeBilgisi")]
        public static DurumResponse SetAlisverisIadeBilgisi(int masaNo, int adisyonNo)
        {
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = false;

            HopiWS.HopiRequestClasses.AlisverisIadeBilgisi alisverisIadeBilgisi = new HopiWS.HopiRequestClasses.AlisverisIadeBilgisi();

            alisverisIadeBilgisi.storeCode = Genel.storeCode;

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader["KAMPANYA_KODU"].TOSTRING()))
                {
                    HopiWS.HopiRequestClasses.KampanyaIade kampanyaIade = new HopiWS.HopiRequestClasses.KampanyaIade();
                    kampanyaIade.kampanyaKodu = reader["KAMPANYA_KODU"].TOSTRING();
                    kampanyaIade.tutar = (reader["TUTAR"].TODECIMAL() - reader["INDIRIM"].TODECIMAL()).ROUNDTWO();
                    kampanyaIade.iadeParacik = reader["KAZANILAN_PARACIK"].TODECIMAL().ROUNDTWO();

                    alisverisIadeBilgisi.kampanyalar.Add(kampanyaIade);
                }
                else
                {
                    alisverisIadeBilgisi.transactionId = reader["TRANSACTION_ID"].TOSTRING();
                    alisverisIadeBilgisi.kampanyasizTutar = reader["TUTAR"].TODECIMAL().ROUNDTWO();
                    alisverisIadeBilgisi.kazanilanParacik = reader["HARCANAN_PARACIK"].TODECIMAL().ROUNDTWO();
                    alisverisIadeBilgisi.odemeTransactionId = reader["ODEME_TRANSACTION_ID"].TOSTRING();
                }
            }
            reader.Close();
            cmd.Parameters.Clear();

            string strKampanyalar = "";
            if (alisverisIadeBilgisi.kampanyalar.Count > 0)
            {
                foreach (HopiWS.HopiRequestClasses.KampanyaIade kampanya in alisverisIadeBilgisi.kampanyalar)
                {
                    if (!string.IsNullOrWhiteSpace(strKampanyalar)) strKampanyalar += ", ";
                    strKampanyalar += "'" + kampanya.kampanyaKodu + "'";
                }
            }
            else
            {
                strKampanyalar = "''";
            }


            cmd.CommandText = "SELECT DISTINCT RC.STOKKODU, RC.SATICIKODU, RC.SIRANO, RC.X, RC.KDV, RC.MIKTAR, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) AS TUTAR, " + //+ ISNULL(RC.HOPI_INDIRIM, CAST(0 AS FLOAT))
                                      "STUFF(ISNULL((SELECT ',' + KOD FROM HOPI WHERE KOD IN (" + strKampanyalar + ") AND AKTIF = 1 AND ((STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU)) OR (ANAGRUP = SM.YEMEK_KODU1) OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '')) FOR XML PATH('')), CAST('' AS VARCHAR)), 1, 1, '') AS KAMPANYA " +
                                      "FROM RESCEK AS RC " +
                                      "INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU) " +
                                      "WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO ";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                HopiWS.HopiRequestClasses.Urun urun = new HopiWS.HopiRequestClasses.Urun();
                urun.barkod = reader["STOKKODU"].TOSTRING();
                if (!string.IsNullOrWhiteSpace(reader["KAMPANYA"].TOSTRING()))
                    urun.kampanyaKodlari = reader["KAMPANYA"].TOSTRING().Split(',').ToList();
                urun.kdv = reader["KDV"].TOINT();
                urun.miktar = reader["MIKTAR"].TODECIMAL().ROUNDTWO();
                urun.tutar = reader["TUTAR"].TODECIMAL().ROUNDTWO();
                urun.x = reader["X"].TOINT();
                urun.siraNo = reader["SIRANO"].TOINT();

                alisverisIadeBilgisi.urunler.Add(urun);
            }
            reader.Close();
            cmd.Parameters.Clear();

            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetAlisverisIadeBilgisi(alisverisIadeBilgisi);
            if (webServiceResult.IsSuccess)
            {
                HopiWS.HopiResponseClasses.ReturnInfo returnInfo = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.ReturnInfo>(webServiceResult.ResultString);
                webServiceResult = HopiWS.HopiWSProvider.SetAlisverisIadeBilgisiIslemiBitir(returnInfo.returnTrxId);
                if (webServiceResult.IsSuccess)
                {
                    if (alisverisIadeBilgisi.kazanilanParacik != 0)
                        webServiceResult = HopiWS.HopiWSProvider.SetIadeEdilecekParacikBilgisi(alisverisIadeBilgisi.odemeTransactionId.TOULONG(), alisverisIadeBilgisi.kazanilanParacik);

                    cmd.Transaction = cnn.BeginTransaction();

                    try
                    {
                        cmd.CommandText = "INSERT INTO HOPIIPT (MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID) " +
                                          "SELECT MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "DELETE FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE RESCEK SET BIRDID = 0, KAMPANYA_KODU = '' WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE RESCEK SET HOPI_INDIRIM = 0 WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "DELETE FROM ODEME_TEMP WHERE CEKNO = @CEKNO AND KARTKODU = @KARTKODU";
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.Parameters.AddWithValue("@KARTKODU", Genel.hopiIndirimOdemeKodu);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "DELETE FROM RESCEK WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KARTKODU = @KARTKODU";
                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                        cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
                        cmd.Parameters.AddWithValue("@KARTKODU", Genel.hopiOdemeKodu);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.Transaction.Commit();

                        durumResponse.basarili = true;
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        durumResponse.hataMesaji = "İade işlemi başarılı. Ancak veritabanına kaydederken hata oluştu." + Environment.NewLine + ex.Message;
                    }
                }
                else
                {
                    HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                    durumResponse.hataMesaji = faultError.detail.ServiceError.description;
                }
            }
            else
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }

            cmd.Dispose();
            cnn.Close();

            return durumResponse;
        }

        [HttpGet("kimlikKontrol")]
        public KimlikKontrolResponse GetKimlikKontrol(string token)
        {
            KimlikKontrolResponse kimlikKontrolResponse = new KimlikKontrolResponse();
            kimlikKontrolResponse.durum = new DurumResponse();
            kimlikKontrolResponse.durum.basarili = false;
            

            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.GetKullaniciBilgi(token);
            if (webServiceResult.IsSuccess)
            {
                HopiWS.HopiResponseClasses.UserInfo userInfo = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.UserInfo>(webServiceResult.ResultString);
                kimlikKontrolResponse.durum.basarili = true;
                kimlikKontrolResponse.birdId = userInfo.birdId;
                kimlikKontrolResponse.bakiye = userInfo.coinBalance;
            }
            else
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                kimlikKontrolResponse.durum.hataMesaji = faultError.detail.ServiceError.description;
            }
            return kimlikKontrolResponse;
        }

        [HttpGet("kullanilacakParacikBilgisi")]
        public DurumResponse SetKullanilacakParacikBilgisi(long birdId, decimal kullanilacakParacik)
        {
            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetKullanilacakParacikBilgisi(birdId, kullanilacakParacik);
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = webServiceResult.IsSuccess;
            if (!durumResponse.basarili)
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            return durumResponse;
        }

        [HttpGet("kullanilacakParacikBilgisiIslemBitir")]
        public DurumResponse SetKullanilacakParacikBilgisi(ulong provisionId)
        {
            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetKullanilacakParacikBilgisiIslemiBitir(provisionId);
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili =  webServiceResult.IsSuccess;
            if (!durumResponse.basarili)
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            return durumResponse;
        }

        [HttpGet("iptalEdilecekParacikBilgisi")]
        public DurumResponse SetIptalEdilecekParacikBilgisi(ulong provisionId)
        {
            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetIptalEdilecekParacikBilgisi(provisionId);
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = webServiceResult.IsSuccess;
            if (!durumResponse.basarili)
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            return durumResponse;
        }

        [HttpGet("iadeEdilecekParacikBilgisi")]
        public DurumResponse SetIadeEdilecekParacikBilgisi(ulong provisionId, decimal iadeParacikTutari)
        {
            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetIadeEdilecekParacikBilgisi(provisionId, iadeParacikTutari);
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = webServiceResult.IsSuccess;
            if (!durumResponse.basarili)
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            return durumResponse;
        }

        [HttpGet("iadeAlisverisBilgisiIslemiBitir")]
        public DurumResponse SetIadeAlisverisBilgisiIslemiBitir(ulong returnTrxId)
        {
            HopiWS.WebServiceResult webServiceResult = HopiWS.HopiWSProvider.SetAlisverisIadeBilgisiIslemiBitir(returnTrxId);
            DurumResponse durumResponse = new DurumResponse();
            durumResponse.basarili = webServiceResult.IsSuccess;
            if (!durumResponse.basarili)
            {
                HopiWS.HopiResponseClasses.FaultError faultError = JsonConvert.DeserializeObject<HopiWS.HopiResponseClasses.FaultError>(webServiceResult.ResultString);
                durumResponse.hataMesaji = faultError.detail.ServiceError.description;
            }
            return durumResponse;
        }
        #endregion
    }
}
