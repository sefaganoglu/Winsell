using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Winsell.Hopi.fProject;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Winsell.Hopi
{
    public partial class frmAnaMenu : Form
    {
        public frmAnaMenu()
        {
            InitializeComponent();
        }

        private DataTable dtStoklar = new DataTable();

        private void btnMasa_Click(object sender, EventArgs e)
        {
            pnlIslemler.Visible = false;
            pnlIslemler.Tag = ((Button)sender).Tag;
            pnlIslemler.AccessibleDescription = ((Button)sender).AccessibleDescription;
            pnlIslemler.AccessibleName = ((Button)sender).AccessibleName;
            lblMasaNo.Text = ((Button)sender).AccessibleName;
            tsbHesapBol.Enabled = ((Button)sender).BackColor != Color.Silver;
            tsbOdemeAl.Enabled = tsbHesapBol.Enabled;
            pnlIslemler.Visible = true;
            urunListesiGetir(pnlIslemler.AccessibleDescription.TOINT());
        }

        private void urunListesiGetir(int masaNo)
        {
            dtStoklar.Rows.Clear();

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.STOKADI, RC.MIKTAR_ACK, (ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.KOD = 'B'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtStoklar);
            DA.Dispose();
            cmd.Dispose();
            cnn.Close();

            dgvStoklar.DataSource = dtStoklar;
        }

        private void tsbYenile_Click(object sender, EventArgs e)
        {
            tsbKapatPanel.PerformClick();
            for (int i = flpMasalar.Controls.Count - 1; i >= 0; i--)
            {
                if (flpMasalar.Controls[i] is Button)
                    flpMasalar.Controls[i].Dispose();
            }

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT RC.MASANO, RC.MASANOSTR, (SELECT SUM((ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI, CAST(0 AS FLOAT))) - ISNULL(ALACAK, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO) AS Tutar, " +
                              "CASE WHEN (SELECT COUNT(CEKNO) FROM (SELECT DISTINCT CEKNO, CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK WHERE MASANO = A.MASANO AND CEKNO = A.CEKNO AND ISNULL(BIRDID, CAST(0 AS INT)) > 0) > 0 THEN CAST(0 AS INT) ELSE CAST(1 AS INT) END AS Kullanilabilir FROM RESCEK AS A WHERE MASANO = RC.MASANO) AS A WHERE Kullanilabilir = 1) > 0 THEN 1 ELSE 0 END AS Kullanilabilir " +
                              "FROM RESCEK AS RC " +
                              "ORDER BY RC.MASANOSTR";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Button btnMasa = clsGenel.masaOlustur(new clsSiniflar.Masa() { masaNo = reader["MASANO"].TOINT(), masaNoStr = reader["MASANOSTR"].TOSTRING(), tutar = reader["Tutar"].TODECIMAL(), color = (reader["Kullanilabilir"].TOINT() == 1 ? Color.Salmon : Color.Silver) });
                btnMasa.Click += btnMasa_Click;
                btnMasa.Parent = flpMasalar;
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();
        }

        private void tsbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAnaMenu_Load(object sender, EventArgs e)
        {
            tsbKullanici.PerformClick();
        }

        private void tsbHesapBol_Click(object sender, EventArgs e)
        {
            int masaNo = pnlIslemler.AccessibleDescription.TOINT();
            clsSiniflar.AdisyonBilgisi adisyonBilgisi = clsGenel.secAdisyon(masaNo);
            if (adisyonBilgisi.adisyonNo > 0)
            {
                if (!clsGenel.kismiOdemeVar(masaNo, adisyonBilgisi.adisyonNo))
                {
                    if (!clsGenel.tekStokVar(masaNo, adisyonBilgisi.adisyonNo))
                    {
                        clsGenel.hesapBol(masaNo, adisyonBilgisi.adisyonNo);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Bu adisyonda tek ürün var ve tek ürün bölünemez.",
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Bu adisyonda kısmi ödeme mevcut. Bu nedenle hesap bölme işlemi yapmadan önce bu ödemeyi iptal ettirmelisiniz.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void tsbOdemeAl_Click(object sender, EventArgs e)
        {
            int masaNo = pnlIslemler.AccessibleDescription.TOINT();
            clsSiniflar.AdisyonBilgisi adisyonBilgisi = clsGenel.secAdisyon(masaNo);
            if (adisyonBilgisi.adisyonNo > 0)
            {
                SqlConnection cnn = clsGenel.createDBConnection();
                SqlCommand cmd = cnn.CreateCommand();

                cmd.CommandText = "SELECT COUNT(CEKNO) FROM RESCEK WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND ISNULL(BIRDID, CAST(0 AS INT)) > 0";
                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                if (cmd.ExecuteScalar().TOINT() == 0)
                {
                    cmd.Parameters.Clear();

                    clsSiniflar.KimlikBilgisi kimlikBilgisi = clsGenel.kimlikKontrol();
                    if (kimlikBilgisi != null)
                    {
                        clsSiniflar.KampanyaBilgisi kampanyaBilgisi = clsGenel.secKampanya(masaNo, adisyonBilgisi.adisyonNo, kimlikBilgisi.paracik, adisyonBilgisi.adisyonTutar);
                        if (kampanyaBilgisi != null)
                        {
                            List<clsHopi.Urun> arrUrun = new List<clsHopi.Urun>();

                            string strKampanyalar = "";
                            if (kampanyaBilgisi.kampanyalar.Count > 0)
                            {
                                foreach (clsSiniflar.Kampanya kampanya in kampanyaBilgisi.kampanyalar)
                                {
                                    if (!string.IsNullOrWhiteSpace(strKampanyalar)) strKampanyalar += ", ";
                                    strKampanyalar += "'" + kampanya.kampanyaKodu + "'";
                                }
                            }
                            else
                            {
                                strKampanyalar = "''";
                            }

                            string strSaticiKodu = "";
                            //decimal dIskontoTutari = 0;
                            cmd.CommandText = "SELECT DISTINCT RC.STOKKODU, RC.SATICIKODU, RC.SIRANO, RC.X, RC.KDV, RC.MIKTAR, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI, CAST(0 AS FLOAT)) AS TUTAR, " + //"ISNULL(RC.ISKONTOTUTARI, CAST(0 AS FLOAT)) AS ISKONTOTUTARI, " +
                                              "STUFF(ISNULL((SELECT ',' + KOD FROM HOPI WHERE KOD IN (" + strKampanyalar + ") AND AKTIF = 1 AND ((STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU)) OR (ANAGRUP = SM.YEMEK_KODU1) OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '')) FOR XML PATH('')), CAST('' AS VARCHAR)), 1, 1, '') AS KAMPANYA " +
                                              "FROM RESCEK AS RC " +
                                              "INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU) " +
                                              "WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO "; // AND SFIY > 0
                            cmd.Parameters.AddWithValue("@MASANO", masaNo);
                            cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                if (string.IsNullOrWhiteSpace(strSaticiKodu.Trim()))
                                    strSaticiKodu = reader["SATICIKODU"].TOSTRING();

                                clsHopi.Urun urun = new clsHopi.Urun();
                                urun.barkod = reader["STOKKODU"].TOSTRING();
                                if (!string.IsNullOrWhiteSpace(reader["KAMPANYA"].TOSTRING()))
                                    urun.kampanyaKodlari = reader["KAMPANYA"].TOSTRING().Split(',').ToList();
                                urun.kdv = reader["KDV"].TOINT();
                                urun.miktar = reader["MIKTAR"].TODECIMAL().ROUNDTWO();
                                urun.tutar = reader["TUTAR"].TODECIMAL().ROUNDTWO();
                                urun.x = reader["X"].TOINT();
                                urun.siraNo = reader["SIRANO"].TOINT();

                                //dIskontoTutari += reader["ISKONTOTUTARI"].TODECIMAL();

                                arrUrun.Add(urun);
                            }
                            reader.Close();
                            cmd.Parameters.Clear();

                            //KATLI İNDİRİM HESAPLAMA (YÜZDELİ DÜŞÜLDÜKTEN SONRA)
                            foreach (clsSiniflar.Kampanya kampanya in kampanyaBilgisi.kampanyalar)
                            {
                                decimal adisyondanDusulecekIndirim = 0;
                                if (kampanya.indirimKat > 0)
                                {
                                    foreach (clsHopi.Urun urun in arrUrun)
                                    {
                                        if (urun.kampanyaKodlari.Contains(kampanya.kampanyaKodu))
                                        {
                                            if (kampanya.tutarlar.ContainsKey(urun.kdv))
                                                kampanya.tutarlar[urun.kdv] += urun.tutar - urun.indirimTutari;
                                            else
                                                kampanya.tutarlar.Add(urun.kdv, urun.tutar - urun.indirimTutari);

                                            decimal gercekParacik = (kampanyaBilgisi.paracik > kampanya.maksimumKatParacik ? kampanya.maksimumKatParacik : kampanyaBilgisi.paracik);
                                            if (gercekParacik * kampanya.indirimKat > adisyonBilgisi.adisyonTutar)
                                                gercekParacik = adisyonBilgisi.adisyonTutar / kampanya.indirimKat;
                                            decimal indirimOrani = ((gercekParacik * kampanya.indirimKat) - gercekParacik) / (adisyonBilgisi.adisyonTutar) * 100.TODECIMAL();

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
                                adisyonBilgisi.adisyonTutar -= adisyondanDusulecekIndirim;
                            }
                            ////////////////////////////

                            if (kampanyaBilgisi.paracik > adisyonBilgisi.adisyonTutar) kampanyaBilgisi.paracik = adisyonBilgisi.adisyonTutar.ROUNDTWO();

                            //YÜZDELİ İNDİRİM HESAPLAMA
                            foreach (clsSiniflar.Kampanya kampanya in kampanyaBilgisi.kampanyalar)
                            {
                                decimal adisyondanDusulecekIndirim = 0;
                                if (kampanya.indirimOrani > 0)
                                {
                                    foreach (clsHopi.Urun urun in arrUrun)
                                    {
                                        if (urun.kampanyaKodlari.Contains(kampanya.kampanyaKodu))
                                        {
                                            if (kampanya.tutarlar.ContainsKey(urun.kdv))
                                                kampanya.tutarlar[urun.kdv] += urun.tutar - urun.indirimTutari;
                                            else
                                                kampanya.tutarlar.Add(urun.kdv, urun.tutar - urun.indirimTutari);

                                            var indirimOrani = kampanya.indirimOrani;
                                            decimal indirimTutari = ((urun.tutar - urun.indirimTutari) * indirimOrani.TODECIMAL() / 100);
                                            urun.indirimTutari += indirimTutari;
                                            adisyonBilgisi.adisyonTutar -= urun.indirimTutari;

                                            if (kampanya.indirimler.ContainsKey(urun.kdv))
                                                kampanya.indirimler[urun.kdv] += indirimTutari;
                                            else
                                                kampanya.indirimler.Add(urun.kdv, indirimTutari);
                                        }
                                    }
                                }
                                adisyonBilgisi.adisyonTutar -= adisyondanDusulecekIndirim;
                            }
                            ////////////////////////////

                            //if (kampanyaBilgisi.indirimOrani > 0 && dIskontoTutari > 0)
                            //{
                            //    MessageBox.Show("Bu adisyonda indirim kampanyası kullanabilmek için önceki indirimi iptal etmeniz gerekmektedir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            //}
                            //else
                            //{
                            clsHopi.AlisverisBilgisi alisverisBilgisi = new clsHopi.AlisverisBilgisi();
                            alisverisBilgisi.birdId = kimlikBilgisi.birdId;
                            alisverisBilgisi.dateTime = DateTime.Now;
                            alisverisBilgisi.hopiPayId = 0;
                            alisverisBilgisi.kasaNo = strSaticiKodu;
                            alisverisBilgisi.kullanilacakParacik = kampanyaBilgisi.paracik;
                            alisverisBilgisi.storeCode = clsGenel.storeCode;
                            alisverisBilgisi.transactionId = alisverisBilgisi.dateTime.ToString("yyyyMMddHHmmssfff") + "_" + clsGenel.storeCode + "_" + adisyonBilgisi.adisyonNo;

                            foreach (clsSiniflar.Kampanya kampanya in kampanyaBilgisi.kampanyalar)
                            {
                                clsHopi.Kampanya kampanyaHopi = new clsHopi.Kampanya();
                                kampanyaHopi.kampanyaKodu = kampanya.kampanyaKodu;
                                if (kampanya.kazancOrani != 0 || kampanya.kazancParacik != 0)
                                {
                                    var vKampanyaliKdvGrupluTutarlar = from tab in arrUrun.AsEnumerable()
                                                                       where tab.kampanyaKodlari.Contains(kampanya.kampanyaKodu)
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


                                    decimal kampanyaTutari = arrUrun.Where(p => p.kampanyaKodlari.Contains(kampanya.kampanyaKodu)).Sum(p => p.tutar - p.indirimTutari);



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

                                alisverisBilgisi.kampanyalar.Add(kampanyaHopi);
                            }

                            alisverisBilgisi.urunler.AddRange(arrUrun);

                            clsHopi.AlisverisResponse alisverisResponse = clsHopiIslem.setAlisverisBilgisi(clsGenel.kullaniciKoduWS, clsGenel.sifreWS, alisverisBilgisi);
                            if (alisverisResponse.basarili)
                            {
                                cmd.Transaction = cnn.BeginTransaction();

                                try
                                {
                                    decimal dIskontoToplami = 0;
                                    cmd.CommandText = "UPDATE RESCEK SET ISKONTOTUTARI = ISNULL(ISKONTOTUTARI, CAST(0 AS FLOAT)) + @ISKONTOTUTARI, BIRDID = @BIRDID, KAMPANYA_KODU = @KAMPANYA_KODU WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND X = @X AND STOKKODU = @STOKKODU AND SIRANO = @SIRANO";
                                    foreach (clsHopi.Urun urun in alisverisBilgisi.urunler)
                                    {
                                        if (urun.kampanyaKodlari.Count > 0)
                                        {
                                            string strUrunKampanyalar = urun.kampanyaKodlari.Aggregate((i, j) => i + ',' + j);

                                            cmd.Parameters.AddWithValue("@ISKONTOTUTARI", Math.Round(urun.indirimTutari, 2));
                                            cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisi.birdId);
                                            cmd.Parameters.AddWithValue("@KAMPANYA_KODU", strUrunKampanyalar);
                                            cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                            cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                            cmd.Parameters.AddWithValue("@X", urun.x);
                                            cmd.Parameters.AddWithValue("@STOKKODU", urun.barkod);
                                            cmd.Parameters.AddWithValue("@SIRANO", urun.siraNo);
                                            cmd.ExecuteNonQuery();
                                            cmd.Parameters.Clear();

                                            dIskontoToplami += Math.Round(urun.indirimTutari, 2);
                                        }
                                    }

                                    if (dIskontoToplami != 0)
                                    {
                                        cmd.CommandText = "INSERT INTO ODEME_TEMP (CEKNO, LST1, LST2, ISKONTO, LST3, KARTKODU, ISK) " +
                                                          "SELECT @CEKNO, ACIKLAMA, @SEPARATOR + '00', @ISKONTO, '0', KOD, @ISK FROM KRDKRT WHERE KOD = @KOD";
                                        cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                        cmd.Parameters.AddWithValue("@SEPARATOR", clsGenel.numberSeparator);
                                        cmd.Parameters.AddWithValue("@ISKONTO", dIskontoToplami.TOSTRING().Replace(",", clsGenel.numberSeparator).Replace(".", clsGenel.numberSeparator));
                                        cmd.Parameters.AddWithValue("@ISK", dIskontoToplami.TOSTRING().Replace(",", clsGenel.numberSeparator).Replace(".", clsGenel.numberSeparator));
                                        cmd.Parameters.AddWithValue("@KOD", clsGenel.hopiIndirimOdemeKodu);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }

                                    //HOPI ODEME KAYDI
                                    if (alisverisBilgisi.kullanilacakParacik != 0)
                                    {
                                        cmd.CommandText = "INSERT INTO RESCEK (MASANO, TARIH, ISKONTOTUTARI1, CEKNO, KARTKODU, SATICIKODU, KOD, ALACAK, MASANOSTR, KAYNAK, HESAPADI, INDIRIM_CARISI, BIRDID) " +
                                                          "SELECT @MASANO, @TARIH, @ISKONTOTUTARI1, @CEKNO, KOD, @SATICIKODU, 'A', @ALACAK, @MASANOSTR, @KAYNAK, CAST('' AS VARCHAR), CAST('' AS VARCHAR), @BIRDID  FROM KRDKRT WHERE KOD = @KOD";
                                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                        cmd.Parameters.AddWithValue("@TARIH", alisverisBilgisi.dateTime);
                                        cmd.Parameters.AddWithValue("@ISKONTOTUTARI1", (0).TODECIMAL());
                                        cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                        cmd.Parameters.AddWithValue("@SATICIKODU", clsGenel.kullaniciKodu);
                                        cmd.Parameters.AddWithValue("@ALACAK", alisverisBilgisi.kullanilacakParacik);
                                        cmd.Parameters.AddWithValue("@MASANOSTR", pnlIslemler.AccessibleName);
                                        cmd.Parameters.AddWithValue("@KAYNAK", 9);
                                        cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisi.birdId);
                                        cmd.Parameters.AddWithValue("@KOD", clsGenel.hopiOdemeKodu);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }

                                    //HOPIHRY KAYDI
                                    cmd.CommandText = "INSERT INTO HOPIHRY (MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID) VALUES (@MASANO, @CEKNO, @KAMPANYA_KODU, @TUTAR, @INDIRIM, @KAZANILAN_PARACIK, @HARCANAN_PARACIK, @TRANSACTION_ID, @BIRDID, @ODEME_TRANSACTION_ID)";
                                    cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                    cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                    cmd.Parameters.AddWithValue("@KAMPANYA_KODU", "");
                                    cmd.Parameters.AddWithValue("@TUTAR", alisverisBilgisi.urunler.AsEnumerable().Where(p => p.kampanyaKodlari.Count == 0).Sum(p => p.tutar).ROUNDTWO());
                                    cmd.Parameters.AddWithValue("@INDIRIM", alisverisBilgisi.urunler.AsEnumerable().Where(p => p.kampanyaKodlari.Count == 0).Sum(p => p.indirimTutari).ROUNDTWO());
                                    cmd.Parameters.AddWithValue("@KAZANILAN_PARACIK", 0);
                                    cmd.Parameters.AddWithValue("@HARCANAN_PARACIK", alisverisBilgisi.kullanilacakParacik.ROUNDTWO());
                                    cmd.Parameters.AddWithValue("@TRANSACTION_ID", alisverisBilgisi.transactionId);
                                    cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisi.birdId);
                                    cmd.Parameters.AddWithValue("@ODEME_TRANSACTION_ID", alisverisResponse.odemeTransactionId);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    foreach (clsHopi.Kampanya kampanya in alisverisBilgisi.kampanyalar)
                                    {
                                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                        cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                        cmd.Parameters.AddWithValue("@KAMPANYA_KODU", kampanya.kampanyaKodu);
                                        cmd.Parameters.AddWithValue("@TUTAR", kampanya.tutarlar.Sum(p => p.Value).ROUNDTWO());
                                        cmd.Parameters.AddWithValue("@INDIRIM", kampanya.indirimler.Sum(p => p.Value).ROUNDTWO());
                                        cmd.Parameters.AddWithValue("@KAZANILAN_PARACIK", kampanya.paracikKazanc.ROUNDTWO());
                                        cmd.Parameters.AddWithValue("@HARCANAN_PARACIK", alisverisBilgisi.kullanilacakParacik.ROUNDTWO());
                                        cmd.Parameters.AddWithValue("@TRANSACTION_ID", alisverisBilgisi.transactionId);
                                        cmd.Parameters.AddWithValue("@BIRDID", alisverisBilgisi.birdId);
                                        cmd.Parameters.AddWithValue("@ODEME_TRANSACTION_ID", alisverisResponse.odemeTransactionId);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }

                                    

                                    //RESHRY KAYDI
                                    cmd.CommandText = "SELECT SUM(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI, CAST(0 AS FLOAT)) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) AS Bakiye FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                                    cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                    cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                    if (cmd.ExecuteScalar().TODECIMAL() <= 0)
                                    {
                                        cmd.Parameters.Clear();

                                        cmd.CommandText = "INSERT INTO RESHRY (ADISYON_TIPI, AFIY, ALACAK, BARKOD, BIRDID, CEKNO, CHSMAS_SIRANO, DVZ_KODU, DVZ_KUR, DVZ_TUTARI, DVZ_TUTARI_ALINACAK, FATURANO, GARNITUR_MASTER_STOKKODU, GITTI, GUN_SIP_NO, HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, HESAPADI, HESAPNO, ILK_FIYAT, INDIRIM_CARISI, IPTMIKTAR, IPTTUTAR, ISKONTOORANI, ISKONTOTUTARI, ISKONTOTUTARI1, KAMPANYA_KODU, KARTKODU, KDV, KISI_SAYISI, KOD, KURYE_KODU, MASANO, MASANOSTR, MASTER_CEKNO, MIKTAR, MIKTAR_ACK, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_ACIKLAMA, ODENMEZ_DETAY_KOD, ODENMEZ_KOD, ORJ_FIYAT, R_A, SAAT, SATICIKODU, SATIS_TURU, SFIY, SIPARISNO, SIPARIS_YERI, SIRKET_KODU, STOKADI, STOKKODU, TARIH, TERAZI_ID, TERAZI_USER, TIP, X, X_S, YAZICI, YAZNO, YAZSIRA, YSEPETI_MessageId, ZAYI_SEBEBI) " +
                                                          "SELECT ADISYON_TIPI, AFIY, ALACAK, BARKOD, BIRDID, CEKNO, CHSMAS_SIRANO, DVZ_KODU, DVZ_KUR, DVZ_TUTARI, DVZ_TUTARI_ALINACAK, FATURANO, GARNITUR_MASTER_STOKKODU, GITTI, ISNULL(GUN_SIP_NO, (SELECT TOP 1 GUN_SIP_NO FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND GUN_SIP_NO IS NOT NULL)), HAZIRLANDI, HAZIRLANMA_TARIHI, HAZIRLAYAN, HESAPADI, HESAPNO, ILK_FIYAT, INDIRIM_CARISI, IPTMIKTAR, IPTTUTAR, ISKONTOORANI, ISKONTOTUTARI, ISKONTOTUTARI1, KAMPANYA_KODU, KARTKODU, KDV, ISNULL(KISI_SAYISI, (SELECT TOP 1 KISI_SAYISI FROM RESYAZ WHERE CEKNO = RC.CEKNO)), KOD, KURYE_KODU, MASANO, MASANOSTR, MASTER_CEKNO, MIKTAR, MIKTAR_ACK, ODENMEZ_ACIKLAMA, ODENMEZ_DETAY_ACIKLAMA, ODENMEZ_DETAY_KOD, ODENMEZ_KOD, ORJ_FIYAT, @R_A, @SAAT, SATICIKODU, ISNULL(SATIS_TURU, (SELECT SATIS_TURU FROM KRDKRT WHERE KOD = (SELECT TOP 1 KARTKODU FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KARTKODU IS NOT NULL))), SFIY, SIPARISNO, @SIPARIS_YERI, ISNULL(SIRKET_KODU, CAST('' AS VARCHAR)) AS SIRKET_KODU, STOKADI, STOKKODU, TARIH, TERAZI_ID, TERAZI_USER, TIP, X, ISNULL(X, SIRANO) AS X_S, YAZICI, YAZNO, YAZSIRA, YSEPETI_MessageId, ZAYI_SEBEBI FROM RESCEK AS RC WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                                        cmd.Parameters.AddWithValue("@R_A", "R");
                                        cmd.Parameters.AddWithValue("@SAAT", alisverisBilgisi.dateTime.ToString("HH:mm:ss"));
                                        cmd.Parameters.AddWithValue("@SIPARIS_YERI", "R");
                                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                        cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();

                                        cmd.CommandText = "DELETE FROM RESCEK WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                                        cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                        cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }

                                    cmd.Transaction.Commit();
                                    MessageBox.Show("Ödeme/kazanma işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    tsbYenile.PerformClick();
                                }
                                catch (Exception ex)
                                {
                                    cmd.Transaction.Rollback();
                                    MessageBox.Show("Ödeme/kazanma işlemi başarılı. Ancak veritabanına kaydederken hata oluştu." + Environment.NewLine + ex.Message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                    tsbYenile.PerformClick();
                                }
                            }
                            //}
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Bu adisyonda daha önce hopi ödemesi/kazanması kullanılmış.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                cmd.Dispose();
                cnn.Close();
            }
        }

        private void tsbKapatPanel_Click(object sender, EventArgs e)
        {

            pnlIslemler.Visible = false;
            pnlIslemler.Tag = 0;
            lblMasaNo.Text = "";
        }

        private void tsbKullanici_Click(object sender, EventArgs e)
        {
            tsbKapatPanel.PerformClick();
            this.Visible = false;
            if (clsGenel.kullaniciGirisi())
            {
                clsGenel.hopiIndirimOdemeKodu = clsGenel.odemeTipiGetir(clsGenel.OdemeTipi.HopiIndirim);
                clsGenel.hopiOdemeKodu = clsGenel.odemeTipiGetir(clsGenel.OdemeTipi.HopiTahsilat);
                clsGenel.magazaKodu = clsGenel.parametreOku("MAGAZA_KODU", "").TOSTRING();
                clsGenel.storeCode = "brs1"; //clsGenel.magazaKodu;

                clsGenel.kampanyaServerName = clsGenel.parametreOku("SERVER_NAME", "").TOSTRING();
                clsGenel.kampanyaDatabaseName = clsGenel.parametreOku("DATABASE_NAME", "").TOSTRING();
                clsGenel.kampanyaUserName = clsGenel.parametreOku("USER_NAME", "").TOSTRING();
                clsGenel.kampanyaPassword = clsGenel.parametreOku("PASSWORD", "").TOSTRING();

                clsGenel.kullaniciKoduWS = clsGenel.parametreOku("HOPI_USERNAME", "").TOSTRING();
                clsGenel.sifreWS = clsGenel.parametreOku("HOPI_PASSWORD", "").TOSTRING();

                lblKullanici.Text = clsGenel.kullaniciAdi;
                tsbYenile.PerformClick();
                this.Visible = true;

                if (!string.IsNullOrWhiteSpace(clsGenel.kampanyaServerName.Trim()))
                    clsGenel.kampanyaGuncelle();
            }
            else
                this.Close();
        }

        private void tsbHopiIade_Click(object sender, EventArgs e)
        {
            int masaNo = pnlIslemler.AccessibleDescription.TOINT();
            clsSiniflar.AdisyonBilgisi adisyonBilgisi = clsGenel.secAdisyon(masaNo, true);
            if (adisyonBilgisi.adisyonNo > 0)
            {
                if (MessageBox.Show("Bu adisyonu iade etmek istediğinize emin misiniz?", "İade İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    clsHopi.AlisverisIadeBilgisi alisverisIadeBilgisi = new clsHopi.AlisverisIadeBilgisi();


                    alisverisIadeBilgisi.storeCode = clsGenel.storeCode;

                    SqlConnection cnn = clsGenel.createDBConnection();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                    cmd.Parameters.AddWithValue("@MASANO", masaNo);
                    cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader["KAMPANYA_KODU"].TOSTRING()))
                        {
                            clsHopi.KampanyaIade kampanyaIade = new clsHopi.KampanyaIade();
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
                        foreach (clsHopi.KampanyaIade kampanya in alisverisIadeBilgisi.kampanyalar)
                        {
                            if (!string.IsNullOrWhiteSpace(strKampanyalar)) strKampanyalar += ", ";
                            strKampanyalar += "'" + kampanya.kampanyaKodu + "'";
                        }
                    }
                    else
                    {
                        strKampanyalar = "''";
                    }


                    cmd.CommandText = "SELECT DISTINCT RC.STOKKODU, RC.SATICIKODU, RC.SIRANO, RC.X, RC.KDV, RC.MIKTAR, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI, CAST(0 AS FLOAT)) AS TUTAR, " + //"ISNULL(RC.ISKONTOTUTARI, CAST(0 AS FLOAT)) AS ISKONTOTUTARI, " +
                                              "STUFF(ISNULL((SELECT ',' + KOD FROM HOPI WHERE KOD IN (" + strKampanyalar + ") AND AKTIF = 1 AND ((STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU)) OR (ANAGRUP = SM.YEMEK_KODU1) OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '')) FOR XML PATH('')), CAST('' AS VARCHAR)), 1, 1, '') AS KAMPANYA " +
                                              "FROM RESCEK AS RC " +
                                              "INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC.GARNITUR_MASTER_STOKKODU, RC.STOKKODU) " +
                                              "WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO ";
                    cmd.Parameters.AddWithValue("@MASANO", masaNo);
                    cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        clsHopi.Urun urun = new clsHopi.Urun();
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

                    clsHopi.AlisverisIadeResponse alisverisIadeResponse = clsHopiIslem.setIadeAlisverisBilgisi(clsGenel.kullaniciKoduWS, clsGenel.sifreWS, alisverisIadeBilgisi);
                    if (alisverisIadeResponse.returnTrxId != 0)
                    {
                        if (clsHopiIslem.setIadeAlisverisBilgisiIslemiBitir(clsGenel.kullaniciKoduWS, clsGenel.sifreWS, clsGenel.storeCode, alisverisIadeResponse.returnTrxId))
                        {
                            clsHopiIslem.setIadeEdilecekParacikBilgisi(clsGenel.kullaniciKoduWS, clsGenel.sifreWS, clsGenel.storeCode, alisverisIadeBilgisi.odemeTransactionId.TOULONG(), alisverisIadeBilgisi.kazanilanParacik);

                            cmd.Transaction = cnn.BeginTransaction();

                            try
                            {
                                cmd.CommandText = "INSERT INTO HOPIIPT (MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID) " +
                                                  "SELECT MASANO, CEKNO, KAMPANYA_KODU, TUTAR, INDIRIM, KAZANILAN_PARACIK, HARCANAN_PARACIK, TRANSACTION_ID, BIRDID, ODEME_TRANSACTION_ID FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.CommandText = "DELETE FROM HOPIHRY WHERE MASANO = @MASANO AND CEKNO = @CEKNO";
                                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.CommandText = "UPDATE RESCEK SET ISKONTOTUTARI = CAST(0 AS FLOAT), BIRDID = 0, KAMPANYA_KODU = '' WHERE MASANO = @MASANO AND CEKNO = @CEKNO"; //ISNULL(ISKONTOTUTARI, CAST(0 AS FLOAT)) - ISNULL(HOPI_ISKONTOTUTARI, CAST(0 AS FLOAT))
                                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.CommandText = "DELETE FROM ODEME_TEMP WHERE CEKNO = @CEKNO AND KARTKODU = @KARTKODU";
                                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                cmd.Parameters.AddWithValue("@KARTKODU", clsGenel.hopiIndirimOdemeKodu);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.CommandText = "DELETE FROM RESCEK WHERE MASANO = @MASANO AND CEKNO = @CEKNO AND KARTKODU = @KARTKODU";
                                cmd.Parameters.AddWithValue("@MASANO", masaNo);
                                cmd.Parameters.AddWithValue("@CEKNO", adisyonBilgisi.adisyonNo);
                                cmd.Parameters.AddWithValue("@KARTKODU", clsGenel.hopiOdemeKodu);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.Transaction.Commit();

                                MessageBox.Show("İade işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                tsbYenile.PerformClick();
                            }
                            catch (Exception ex)
                            {
                                cmd.Transaction.Rollback();

                                MessageBox.Show("İade işlemi başarılı. Ancak veritabanına kaydederken hata oluştu." + Environment.NewLine + ex.Message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                tsbYenile.PerformClick();
                            }
                        }
                    }

                    cmd.Dispose();
                    cnn.Close();
                }
            }
        }
    }
}
