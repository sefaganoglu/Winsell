using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winsell.Hopi.fProject;

namespace Winsell.Hopi
{
    public partial class frmKampanyaListesi : Form
    {
        public clsSiniflar.KampanyaBilgisi kampanyaBilgisi = new clsSiniflar.KampanyaBilgisi();
        public DialogResult drReturn = DialogResult.Cancel;

        private int masaNoX = 0;
        private int adisyonNoX = 0;
        private decimal paracikX = 0;
        private decimal adisyonTutariX = 0;
        private DataTable dtKampanyalar = new DataTable();

        public frmKampanyaListesi(int masaNo, int adisyonNo, decimal paracik, decimal adisyonTutari)
        {
            InitializeComponent();

            masaNoX = masaNo;
            adisyonNoX = adisyonNo;
            paracikX = paracik;
            adisyonTutariX = adisyonTutari;
            btnSeparator.Text = clsGenel.numberSeparator;
            lblKullanilabilirLimit.Text = paracik.ToString("N2");
            lblAdisyonTutari.Text = adisyonTutari.ToString("N2");

            txtParacik_TextChanged(txtParacik, new EventArgs());
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtParacik.Text += ((Button)sender).Text;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            txtParacik.Clear();
        }

        private void txtParacik_TextChanged(object sender, EventArgs e)
        {
            kampanyaListesiGetir();
        }

        private void kampanyaListesiGetir()
        {
            dtKampanyalar.Rows.Clear();

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT CAST(0 AS INT) AS Sec, KOD, ACIKLAMA, CASE WHEN Kazanc > MAX_KAZAN_PARA AND MAX_KAZAN_PARA <> CAST(0 AS FLOAT) THEN MAX_KAZAN_PARA ELSE Kazanc END AS Kazanc, Indirim, (CASE WHEN Tutar > 0 THEN Indirim / Tutar * 100 ELSE CAST(0 AS FLOAT) END) IndirimOrani, " +
                              "CASE WHEN IndirimOdeme > Tutar THEN CAST(0 AS INT) ELSE CAST(1 AS INT) END AS Kullanilabilir, " +
                              "CASE WHEN IndirimOdeme > Tutar THEN 'Kullanılan paracık bu kampanyada ödeme tutarının aşılmasına sebep oluyor. Paracık kullanımı maksimum ' + CAST(CONVERT(Decimal(8,2), Tutar / ISNULL(HOPI_KAT, CAST(1 AS FLOAT))) AS VARCHAR) + ' olmalı.' ELSE CAST('' AS VARCHAR) END AS Dikkat, " +
                              "CAST('...' AS VARCHAR) AS DikkatShow " +
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
                              (string.IsNullOrWhiteSpace(clsGenel.kampanyaServerName.Trim()) ? "AND (SELECT COUNT(KOD) FROM HOPI_SIRKET WHERE KOD = H.KOD AND SIRKET_KODU = @SIRKET_KODU) > 0" : "") +
                              ") AS A INNER JOIN HOPI H ON A.KOD = H.KOD " +
                              "WHERE " +
                              "(ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(ADET, CAST(0 AS FLOAT)) <= StokMiktar AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= StokTutar) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0))) " +
                              "OR (ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(ADET, CAST(0 AS FLOAT)) <= GrupMiktar AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= GrupTutar) OR (ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0))) " +
                              "OR (ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '' AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= AdisyonTutar)) AS B WHERE Tutar <> 0) AS C " +
                              "WHERE (Kazanc <> 0 OR Indirim <> 0) " + 
                              "ORDER BY Indirim DESC";

            cmd.Parameters.AddWithValue("@Paracik", txtParacik.Text.TODECIMAL());
            cmd.Parameters.AddWithValue("@MASANO", masaNoX);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNoX);
            cmd.Parameters.AddWithValue("@SIRKET_KODU", clsGenel.magazaKodu);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtKampanyalar);
            DA.Dispose();
            cmd.Dispose();
            cnn.Close();

            dgvKampanyalar.DataSource = dtKampanyalar;
        }

        private void dgvKampanyalar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dgvKampanyalar.Rows[e.RowIndex].Cells[colKullanilabilir.Name].Value.TOINT() == 0)
                {
                    e.CellStyle.BackColor = Color.Salmon;
                }
            }
        }

        private void dgvKampanyalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvKampanyalar.Rows[e.RowIndex].Cells[colKullanilabilir.Name].Value.TOINT() == 0 && dgvKampanyalar.Columns[e.ColumnIndex].Name == colDikkatShow.Name)
                {
                    MessageBox.Show(dgvKampanyalar.Rows[e.RowIndex].Cells[colDikkat.Name].Value.TOSTRING(),
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    dgvKampanyalar.Rows[e.RowIndex].Cells[colSec.Name].Value = dgvKampanyalar.Rows[e.RowIndex].Cells[colSec.Name].Value.TOINT() == 1 ? 0 : 1;
                }
            }
        }

        private void tsbTamam_Click(object sender, EventArgs e)
        {
            dgvKampanyalar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (dgvKampanyalar.Rows.Count > 0)
                dgvKampanyalar.CurrentCell = dgvKampanyalar.Rows[0].Cells[colKampanya.Name];
            dtKampanyalar.AcceptChanges();

            DataRow[] arrDR = dtKampanyalar.Select("Sec = 1");
            if (dgvKampanyalar.Rows.Count == 0 || arrDR.Length > 0)
            {


                if (txtParacik.Text.TODECIMAL() <= paracikX)
                {
                    if (txtParacik.Text.TODECIMAL() <= adisyonTutariX)
                    {
                        string[] arrHataliDR = arrDR.Where(p => p["Kullanilabilir"].TOINT() == 0).Select(p => p["KOD"].TOSTRING() + " kodlu kampanyanın mesajı: " + Environment.NewLine + p["Dikkat"].TOSTRING()).ToArray();
                        if (arrHataliDR.Length == 0)
                        {
                            kampanyaBilgisi.paracik = txtParacik.Text.TODECIMAL().ROUNDTWO();
                            if (dgvKampanyalar.Rows.Count != 0)
                            {
                                List<decimal> lstIndirimler = new List<decimal>();
                                foreach (DataRow DR in arrDR)
                                {
                                    clsSiniflar.Kampanya kampanya = new clsSiniflar.Kampanya();
                                    kampanya.kampanyaKodu = DR["KOD"].TOSTRING();
                                    kampanya.indirimTutari = DR["Indirim"].TODECIMAL().ROUNDTWO();
                                    kampanya.indirimOrani = DR["IndirimOrani"].TODECIMAL().ROUNDTWO();
                                    decimal dKazanc = DR["Kazanc"].TODECIMAL().ROUNDTWO();
                                    foreach (decimal d in lstIndirimler) dKazanc -= dKazanc * d / 100;
                                    kampanya.paracikKazanc = dKazanc.ROUNDTWO();
                                    kampanya.kampanyaTipi = kampanya.paracikKazanc != 0 ? clsSiniflar.KampanyaTipi.Kazanc : clsSiniflar.KampanyaTipi.Indirim;
                                    kampanyaBilgisi.kampanyalar.Add(kampanya);

                                    if (kampanya.indirimOrani > 0) lstIndirimler.Add(kampanya.indirimOrani);
                                }
                            }
                            //else
                            //{
                            //    clsSiniflar.Kampanya kampanya = new clsSiniflar.Kampanya();
                            //    kampanya.kampanyaKodu = "";
                            //    kampanya.indirim = 0;
                            //    kampanya.indirimOrani = 0;
                            //    kampanya.kazanc = 0;
                            //    kampanyaBilgisi.kampanyalar.Add(kampanya);
                            //}

                            drReturn = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(arrHataliDR.Aggregate((i, j) => i + Environment.NewLine + Environment.NewLine + j),
                                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Girilen paracık miktarı adisyon tutarından büyük olamaz.",
                                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("Girilen paracık miktarı " + paracikX.ToString("N2") + " olan bakiyeyi aşıyor.",
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir kampanya seçiniz.",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmKampanyaListesi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }

        private void tsbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
