using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Winsell.Hopi.API.Models;
using System.Configuration;
using System.Drawing;

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
            cmd.CommandText = "SELECT DISTINCT RC.MASANO, RC.MASANOSTR, (SELECT SUM((ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI, CAST(0 AS FLOAT))) - ISNULL(ALACAK, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO) AS Tutar, " +
                              "CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND LTRIM(RTRIM(ISNULL(BIRDID, CAST(0 AS INT)))) > 0) > 0 THEN 0 ELSE 1 END AS Kullanilabilir " +
                              "FROM RESCEK AS RC " +
                              "ORDER BY RC.MASANOSTR";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Masa masa = new Masa()
                {
                    no = reader["MASANO"].TOINT(),
                    noStr = reader["MASANOSTR"].TOSTRING(),
                    tutar = reader["Tutar"].TODECIMAL(),
                    color = (reader["Kullanilabilir"].TOINT() == 1 ? Color.Salmon : Color.Silver),
                    height = 100,
                    width = 100
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
            cmd.CommandText = "SELECT RC.STOKKODU, RC.STOKADI, RC.MIKTAR, RC.MIKTAR_ACK, RC.X, (ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.KOD = 'B'";
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
            cmd.CommandText = "SELECT RC.STOKKODU, RC.STOKADI, RC.MIKTAR, RC.MIKTAR_ACK, RC.X, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT)) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO AND RC.KOD = 'B'";
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
            cmd.CommandText = "SELECT RC.X, RC.STOKKODU, RC.STOKADI, RC.MIKTAR, (ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) + (SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KOD = RC.KOD AND X = RC.X) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO AND RC.KOD = 'B' AND LTRIM(RTRIM(ISNULL(RC.GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = ''";
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

        [HttpGet("masadakiAdisyonlar")]
        public List<Adisyon> GetMasadakiAdisyonlar(int masaNo)
        {
            List<Adisyon> listAdisyon = new List<Adisyon>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.CEKNO, SUM((ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO GROUP BY RC.CEKNO";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Adisyon adisyon = new Adisyon()
                {
                    no = reader["CEKNO"].TOINT(),
                    tutar = reader["Tutar"].TODECIMAL(),
                    stoklar = GetAdisyondakiStoklar(masaNo, reader["CEKNO"].TOINT())
                };

                listAdisyon.Add(adisyon);
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();

            return listAdisyon;
        }

        [HttpGet("hesapBol")]
        public DurumResponse GetHesapBol(HesapBolmeRequest hesapBolmeRequest)
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
                                          "DVZ_KODU, DVZ_TUTARI, SIRKET_KODU, ONCELIK, SANDELYE, CEK_YAZDIR_NO, GARNITUR_MASTER_STOKKODU, X + (SELECT COUNT(DISTINCT X) + 1 FROM RESCEK WHERE X > RC.X), KISMI_ODEME_ISARET, MASANOSTR, GITTI, BARKOD, TIP, GUN_SIP_NO, ZAYI_SEBEBI, " +
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
        public List<Kampanya> GetKampanyalar(KampanyaRequest kampanyaRequest)
        {
            List<Kampanya> listKampanya = new List<Kampanya>();

            SqlConnection cnn = Genel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT KOD, ACIKLAMA, CASE WHEN Kazanc > MAX_KAZAN_PARA AND MAX_KAZAN_PARA <> CAST(0 AS FLOAT) THEN MAX_KAZAN_PARA ELSE Kazanc END AS Kazanc, Indirim, (CASE WHEN Tutar > 0 THEN Indirim / Tutar * 100 ELSE CAST(0 AS FLOAT) END) IndirimOrani, " +
                              "CASE WHEN IndirimOdeme > Tutar THEN CAST(0 AS INT) ELSE CAST(1 AS INT) END AS Kullanilabilir, " +
                              "CASE WHEN IndirimOdeme > Tutar THEN 'Kullanılan paracık bu kampanyada ödeme tutarının aşılmasına sebep oluyor. Paracık kullanımı maksimum ' + CAST(CONVERT(Decimal(8,2), Tutar / ISNULL(HOPI_KAT, CAST(1 AS FLOAT))) AS VARCHAR) + ' olmalı.' ELSE CAST('' AS VARCHAR) END AS Dikkat " +
                              "FROM " +
                              "(SELECT KOD, ACIKLAMA, Tutar, HOPI_KAT, MAX_KAZAN_PARA, " +

                              "(CASE WHEN ISNULL(HOPI_KAZAN_ORANI, CAST(0 AS FLOAT)) > 0 THEN Tutar * ISNULL(HOPI_KAZAN_ORANI, CAST(0 AS FLOAT)) / 100 " +
                              "WHEN ISNULL(HOPI_KAZAN_PARA, CAST(0 AS FLOAT)) > 0 THEN ISNULL(HOPI_KAZAN_PARA, CAST(0 AS FLOAT)) " +
                              "ELSE CAST(0 AS FLOAT) END) * (CASE WHEN ISNULL(ADET, CAST(0 AS FLOAT)) > 0 THEN FLOOR(Miktar / ISNULL(ADET, CAST(1 AS FLOAT))) ELSE CAST(1 AS FLOAT) END) AS Kazanc, " +

                              "(CASE WHEN ISNULL(HOPI_KAT, CAST(0 AS FLOAT)) > 0 THEN (CASE WHEN @Paracik > MAX_HOPI_KAT AND MAX_HOPI_KAT <> CAST(0 AS FLOAT) THEN MAX_HOPI_KAT ELSE @Paracik END * ISNULL(HOPI_KAT, CAST(0 AS FLOAT)) - CASE WHEN @Paracik > MAX_HOPI_KAT AND MAX_HOPI_KAT <> CAST(0 AS FLOAT) THEN MAX_HOPI_KAT ELSE @Paracik END) " +
                              "WHEN ISNULL(INDIRIM_ORANI, CAST(0 AS FLOAT)) > 0 THEN Tutar * ISNULL(INDIRIM_ORANI, CAST(0 AS FLOAT)) / 100 " +
                              "ELSE CAST(0 AS FLOAT) END) * (CASE WHEN ISNULL(ADET, CAST(0 AS FLOAT)) > 0 THEN FLOOR(Miktar / ISNULL(ADET, CAST(1 AS FLOAT))) ELSE CAST(1 AS FLOAT) END) AS Indirim, " +

                              "(CASE WHEN ISNULL(HOPI_KAT, CAST(0 AS FLOAT)) > 0 THEN (CASE WHEN @Paracik > MAX_HOPI_KAT AND MAX_HOPI_KAT <> CAST(0 AS FLOAT) THEN MAX_HOPI_KAT ELSE @Paracik END * ISNULL(HOPI_KAT, CAST(0 AS FLOAT))) " +
                              "WHEN ISNULL(INDIRIM_ORANI, CAST(0 AS FLOAT)) > 0 THEN Tutar * ISNULL(INDIRIM_ORANI, CAST(0 AS FLOAT)) / 100 " +
                              "ELSE CAST(0 AS FLOAT) END) * (CASE WHEN ISNULL(ADET, CAST(0 AS FLOAT)) > 0 THEN FLOOR(Miktar / ISNULL(ADET, CAST(1 AS FLOAT))) ELSE CAST(1 AS FLOAT) END) AS IndirimOdeme " +

                              "FROM " +
                              "(SELECT H.KOD, H.ACIKLAMA, H.ADET, H.HOPI_KAZAN_ORANI, H.HOPI_KAZAN_PARA, H.HOPI_KAT, H.INDIRIM_ORANI, ISNULL(H.HOPI_KAT_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_HOPI_KAT, ISNULL(H.KAZANILACAK_PARACIK_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_KAZAN_PARA, " +

                              "CASE WHEN ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' THEN A.StokMiktar " +
                              "WHEN ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' THEN A.GrupMiktar " +
                              "ELSE CAST(0 AS FLOAT) END AS Miktar, " +

                              "CASE WHEN ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' THEN A.StokTutar " +
                              "WHEN ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' THEN A.GrupTutar " +
                              "ELSE A.AdisyonTutar END AS Tutar " +

                              "FROM " +
                              "(SELECT DISTINCT H.KOD, H.ACIKLAMA, " +
                              "ISNULL((SELECT SUM(ISNULL((MIKTAR), CAST(0 AS FLOAT))) AS Miktar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND STOKKODU = H.STOKKODU), CAST(0 AS FLOAT)) AS StokMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON RC1.STOKKODU = SM.STOKKODU WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.YEMEK_KODU1 = H.ANAGRUP), CAST(0 AS FLOAT)) AS GrupMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND H.STOKKODU = ISNULL(GARNITUR_MASTER_STOKKODU, STOKKODU)), CAST(0 AS FLOAT)) AS StokTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.SFIY * RC1.MIKTAR, CAST(0 AS FLOAT)) - ISNULL(RC1.ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(RC1.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.YEMEK_KODU1 = H.ANAGRUP), CAST(0 AS FLOAT)) AS GrupTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS AdisyonTutar " +
                              "FROM HOPI AS H " +
                              "INNER JOIN RESCEK AS RC ON RC.TARIH >= H.BAS_TARIHI AND RC.TARIH <= H.BIT_TARIHI AND RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO " +
                              "INNER JOIN STKMAS AS SM ON RC.STOKKODU = SM.STOKKODU " +
                              "WHERE H.AKTIF = 1 " +
                              (!kampanyaRequest.serverVar ? "AND (SELECT COUNT(KOD) FROM HOPI_SIRKET WHERE KOD = H.KOD AND SIRKET_KODU = @SIRKET_KODU) > 0" : "") +
                              ") AS A INNER JOIN HOPI H ON A.KOD = H.KOD " +
                              "WHERE " +
                              "(ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(ADET, CAST(0 AS FLOAT)) <= StokMiktar AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= StokTutar) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0))) " +
                              "OR (ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(ADET, CAST(0 AS FLOAT)) <= GrupMiktar AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= GrupTutar) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0))) " +
                              "OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '' AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= AdisyonTutar)) AS B WHERE Tutar <> 0) AS C " +
                              "WHERE (Kazanc <> 0 OR Indirim <> 0)";

            cmd.Parameters.AddWithValue("@Paracik", kampanyaRequest.paracik);
            cmd.Parameters.AddWithValue("@MASANO", kampanyaRequest.masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", kampanyaRequest.adisyonNo);
            cmd.Parameters.AddWithValue("@SIRKET_KODU", kampanyaRequest.sirketKodu);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kampanya kampanya = new Kampanya()
                {
                    kodu = reader["KOD"].TOSTRING(),
                    aciklama = reader["ACIKLAMA"].TOSTRING(),
                    kazanc = reader["Kazanc"].TODECIMAL(),
                    indirim = reader["Indirim"].TODECIMAL(),
                    dikkat = reader["Dikkat"].TOSTRING(),
                    kullanilabilir = reader["Kullanilabilir"].TOINT(),
                    indirimOrani = reader["IndirimOrani"].TODECIMAL()
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
    }
}
