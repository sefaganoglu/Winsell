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

        TextBox txtFocusedTextBox = null;
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

            txtFocusedTextBox = txtParacik;

            kampanyaListesiGetir();
        }


        private void btn7_Click(object sender, EventArgs e)
        {
            txtFocusedTextBox.Text += ((Button)sender).Text;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            txtParacik.Clear();
            txtParacikKusurat.Clear();
            txtParacik.Focus();
        }

        private void kampanyaListesiGetir()
        {
            dtKampanyalar.Rows.Clear();

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT CAST(0 AS INT) AS Sec, H.KOD, H.ACIKLAMA, ISNULL(H.FIYATSAL_SINIR, CAST(0 AS INT)) AS FIYATSAL_SINIR, ISNULL(H.ADET, CAST(0 AS INT)) AS ADET, ISNULL(H.HOPI_KAZAN_ORANI, CAST(0 AS INT)) AS HOPI_KAZAN_ORANI, ISNULL(H.HOPI_KAZAN_PARA, CAST(0 AS money)) AS HOPI_KAZAN_PARA, ISNULL(H.HOPI_KAT, CAST(0 AS INT)) AS HOPI_KAT, ISNULL(H.INDIRIM_ORANI, CAST(0 AS INT)) AS INDIRIM_ORANI, ISNULL(H.HOPI_KAT_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_HOPI_KAT, ISNULL(H.KAZANILACAK_PARACIK_UST_SINIR, CAST(0 AS FLOAT)) AS MAX_KAZAN_PARA, " +

                              "CASE WHEN ISNULL(LTRIM(RTRIM(STOKKODU)), CAST('' AS VARCHAR)) <> '' THEN A.StokMiktar " +
                              "WHEN ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) <> '' THEN A.GrupMiktar " +
                              "WHEN ISNULL(LTRIM(RTRIM(SINIF_KODU)), CAST('' AS VARCHAR)) <> '' THEN A.SinifMiktar " +
                              "ELSE CAST(0 AS FLOAT) END AS Miktar " +

                              "FROM " +
                              "(SELECT DISTINCT H.KOD, H.ACIKLAMA, " +
                              "ISNULL((SELECT SUM(ISNULL((MIKTAR), CAST(0 AS FLOAT))) AS Miktar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND STOKKODU = H.STOKKODU), CAST(0 AS FLOAT)) AS StokMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON RC1.STOKKODU = SM.STOKKODU WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.YEMEK_KODU1 = H.ANAGRUP), CAST(0 AS FLOAT)) AS GrupMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.MIKTAR, CAST(0 AS FLOAT))) AS Miktar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON RC1.STOKKODU = SM.STOKKODU WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.SINIF_KODU = H.SINIF_KODU), CAST(0 AS FLOAT)) AS SinifMiktar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND H.STOKKODU = ISNULL(GARNITUR_MASTER_STOKKODU, STOKKODU)), CAST(0 AS FLOAT)) AS StokTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.SFIY * RC1.MIKTAR, CAST(0 AS FLOAT)) - ISNULL(RC1.ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(RC1.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.YEMEK_KODU1 = H.ANAGRUP), CAST(0 AS FLOAT)) AS GrupTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(RC1.SFIY * RC1.MIKTAR, CAST(0 AS FLOAT)) - ISNULL(RC1.ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(RC1.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC1 INNER JOIN STKMAS AS SM ON SM.STOKKODU = ISNULL(RC1.GARNITUR_MASTER_STOKKODU, RC1.STOKKODU) WHERE RC1.MASANO = RC.MASANO AND RC1.CEKNO = RC.CEKNO AND SM.SINIF_KODU = H.SINIF_KODU), CAST(0 AS FLOAT)) AS SinifTutar, " +
                              "ISNULL((SELECT SUM(ISNULL(SFIY * MIKTAR, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT)) - ISNULL(ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) AS AdisyonTutar " +
                              "FROM HOPI AS H " +
                              "INNER JOIN RESCEK AS RC ON RC.TARIH >= H.BAS_TARIHI AND RC.TARIH <= H.BIT_TARIHI AND RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO " +
                              "WHERE H.AKTIF = 1 " +
                              (string.IsNullOrWhiteSpace(clsGenel.kampanyaServerName.Trim()) ? "AND (SELECT COUNT(KOD) FROM HOPI_SIRKET WHERE KOD = H.KOD AND SIRKET_KODU = @SIRKET_KODU) > 0" : "") +
                              ") AS A INNER JOIN HOPI H ON A.KOD = H.KOD " +
                              "WHERE " +
                              "   (ISNULL(LTRIM(RTRIM(STOKKODU  )), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= StokMiktar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= StokTutar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (StokMiktar <> 0 OR StokTutar <> 0)))) " +
                              "OR (ISNULL(LTRIM(RTRIM(ANAGRUP   )), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= GrupMiktar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= GrupTutar ) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (GrupMiktar <> 0 OR GrupTutar <> 0)))) " +
                              "OR (ISNULL(LTRIM(RTRIM(SINIF_KODU)), CAST('' AS VARCHAR)) <> '' AND ((ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) <= SinifMiktar) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <> 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= SinifTutar) OR (ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) = 0 AND ISNULL(ADET, CAST(0 AS FLOAT)) = 0 AND (SinifMiktar <> 0 OR SinifTutar <> 0)))) " +
                              "OR (ISNULL(LTRIM(RTRIM(STOKKODU  )), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(ANAGRUP)), CAST('' AS VARCHAR)) = '' AND ISNULL(LTRIM(RTRIM(SINIF_KODU)), CAST('' AS VARCHAR)) = '' AND ISNULL(FIYATSAL_SINIR, CAST(0 AS FLOAT)) <= AdisyonTutar) " +
                              "ORDER BY HOPI_KAT DESC, INDIRIM_ORANI DESC, HOPI_KAZAN_ORANI DESC, HOPI_KAZAN_PARA DESC";

            cmd.Parameters.AddWithValue("@MASANO", masaNoX);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNoX);
            if (string.IsNullOrWhiteSpace(clsGenel.kampanyaServerName.Trim()))
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
            //if (e.RowIndex > -1 && e.ColumnIndex > -1)
            //{
            //    if (dgvKampanyalar.Rows[e.RowIndex].Cells[colKullanilabilir.Name].Value.TOINT() == 0)
            //    {
            //        e.CellStyle.BackColor = Color.Salmon;
            //    }
            //}
        }

        private void dgvKampanyalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //if (dgvKampanyalar.Rows[e.RowIndex].Cells[colKullanilabilir.Name].Value.TOINT() == 0 && dgvKampanyalar.Columns[e.ColumnIndex].Name == colDikkatShow.Name)
                //{
                //    MessageBox.Show(dgvKampanyalar.Rows[e.RowIndex].Cells[colDikkat.Name].Value.TOSTRING(),
                //        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //}
                //else
                //{
                dgvKampanyalar.Rows[e.RowIndex].Cells[colSec.Name].Value = dgvKampanyalar.Rows[e.RowIndex].Cells[colSec.Name].Value.TOINT() == 1 ? 0 : 1;
                //}
            }
        }

        private void tsbTamam_Click(object sender, EventArgs e)
        {
            dgvKampanyalar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (dgvKampanyalar.Rows.Count > 0)
                dgvKampanyalar.CurrentCell = dgvKampanyalar.Rows[0].Cells[colKampanya.Name];
            dtKampanyalar.AcceptChanges();

            decimal paracikTop = txtParacik.Text.TODECIMAL();
            paracikTop += txtParacikKusurat.Text.TODECIMAL() / 100;

            string[] arrKodlar = dtKampanyalar.AsEnumerable().Where(p => p["Sec"].TOINT() == 1 && p["HOPI_KAT"].TOINT() != 0).Select(p => p["KOD"].TOSTRING()).ToArray();

            if (arrKodlar.Length > 0 && paracikTop == 0)
            {
                MessageBox.Show("Seçili kampanyalardan biri veya bazıları paracık harcaması olmadan çalışmaz." + Environment.NewLine + Environment.NewLine + "Paracık harcaması olmadan çalışmayan kampanyalar; " + arrKodlar.Aggregate((i, j) => i + ", " + j),
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            

            DataRow[] arrDR = dtKampanyalar.Select("Sec = 1");
            if (dgvKampanyalar.Rows.Count == 0 || arrDR.Length > 0)
            {
                if (paracikTop <= paracikX)
                {
                    if (paracikTop <= adisyonTutariX)
                    {
                        string[] arrHataliDR = new string[] { };// = arrDR.Where(p => p["Kullanilabilir"].TOINT() == 0).Select(p => p["KOD"].TOSTRING() + " kodlu kampanyanın mesajı: " + Environment.NewLine + p["Dikkat"].TOSTRING()).ToArray();
                        if (arrHataliDR.Length == 0)
                        {
                            kampanyaBilgisi.paracik = paracikTop.ROUNDTWO();
                            if (dgvKampanyalar.Rows.Count != 0)
                            {
                                foreach (DataRow DR in arrDR)
                                {
                                    clsSiniflar.Kampanya kampanya = new clsSiniflar.Kampanya()
                                    {
                                        adet = DR["ADET"].TOINT(),
                                        indirimKat = DR["HOPI_KAT"].TODECIMAL().ROUNDTWO(),
                                        indirimOrani = DR["INDIRIM_ORANI"].TOINT(),
                                        kampanyaKodu = DR["KOD"].TOSTRING(),
                                        kazancOrani = DR["HOPI_KAZAN_ORANI"].TOINT(),
                                        kazancParacik = DR["HOPI_KAZAN_PARA"].TODECIMAL().ROUNDTWO(),
                                        maksimumKatParacik = DR["MAX_HOPI_KAT"].TODECIMAL().ROUNDTWO(),
                                        maksimumKazanc = DR["MAX_KAZAN_PARA"].TODECIMAL().ROUNDTWO(),
                                        miktar = DR["Miktar"].TODECIMAL().ROUNDTWO(),
                                        fiyatsalSinir = DR["FIYATSAL_SINIR"].TODECIMAL().ROUNDTWO()
                                    };
                                    kampanyaBilgisi.kampanyalar.Add(kampanya);
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

        private void btnSeparator_Click(object sender, EventArgs e)
        {
            txtParacikKusurat.Focus();
            txtParacikKusurat.Clear();
        }

        private void txtParacik_Enter(object sender, EventArgs e)
        {
            txtFocusedTextBox = ((TextBox)sender);
        }
    }
}
