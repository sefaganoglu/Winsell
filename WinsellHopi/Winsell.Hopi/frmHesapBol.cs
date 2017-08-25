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
    public partial class frmHesapBol : Form
    {
        private DataTable dtStoklar = new DataTable();
        private int masaNoX = 0;
        private int adisyonNoX = 0;


        public frmHesapBol(int masaNo, int adisyonNo)
        {
            InitializeComponent();
            hesapListesiGetir(masaNo, adisyonNo);
        }

        private void hesapListesiGetir(int masaNo, int adisyonNo)
        {
            masaNoX = masaNo;
            adisyonNoX = adisyonNo;

            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT CAST(0 AS INT) AS Sec, RC.X, RC.STOKKODU, RC.STOKADI, RC.MIKTAR, RC.MIKTAR AS MIKTAR_DEG, RC.MIKTAR_ACK, (ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) + (SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS FLOAT)) - ISNULL(ISKONTOTUTARI1, CAST(0 AS FLOAT))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND KOD = RC.KOD AND X = RC.X) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.CEKNO = @CEKNO AND RC.KOD = 'B' AND LTRIM(RTRIM(ISNULL(RC.GARNITUR_MASTER_STOKKODU, CAST('' AS VARCHAR)))) = ''";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            cmd.Parameters.AddWithValue("@CEKNO", adisyonNo);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtStoklar);
            DA.Dispose();
            cmd.Dispose();
            cnn.Close();

            dgvStoklar.DataSource = dtStoklar;
        }

        private void tsbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbHesabiBol_Click(object sender, EventArgs e)
        {
            dgvStoklar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            dgvStoklar.CurrentCell = dgvStoklar.Rows[0].Cells[colStokAdi.Name];
            dtStoklar.AcceptChanges();

            if (dtStoklar.Select("Sec = '0'").Length != 0 || (dtStoklar.Select("Sec = '0'").Length == 0 && dtStoklar.Compute("SUM(MIKTAR_DEG)", "").TODECIMAL() < dtStoklar.Compute("SUM(MIKTAR)", "").TODECIMAL()))
            {
                DataRow[] arrDR = dtStoklar.Select("Sec = '1'");
                if (arrDR.Length != 0)
                {
                    SqlConnection cnn = clsGenel.createDBConnection();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.Transaction = cnn.BeginTransaction();

                    try
                    {
                        int islemSayisi = 0;
                        foreach (DataRow DR in arrDR)
                        {
                            if (DR["MIKTAR_DEG"].TODECIMAL() == DR["MIKTAR"].TODECIMAL())
                            {
                                cmd.CommandText = "UPDATE RESCEK SET CEKNO = ISNULL((SELECT TOP 1 ADISYONNO FROM SIRANO), CAST(0 AS INT)) + 1 WHERE MASANO = @MASANO AND X = @X AND KOD = 'B'";
                                cmd.Parameters.AddWithValue("@MASANO", masaNoX);
                                cmd.Parameters.AddWithValue("@X", DR["X"]);
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
                                cmd.Parameters.AddWithValue("@MIKTAR", DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@MIKTAR_ACK", DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@ADET", DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@MASANO", masaNoX);
                                cmd.Parameters.AddWithValue("@X", DR["X"]);
                                islemSayisi += cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                cmd.CommandText = "UPDATE RESCEK SET MIKTAR = @MIKTAR, ADET = @ADET, MIKTAR_ACK = CAST(CONVERT(Decimal(8,0), @MIKTAR_ACK) AS VARCHAR) + '*' + CAST(CONVERT(Decimal(8,0), PORSIYON_MIK) AS VARCHAR) WHERE MASANO = @MASANO AND X = @X AND KOD = 'B'";
                                cmd.Parameters.AddWithValue("@MIKTAR", DR["MIKTAR"].TODECIMAL() - DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@ADET", DR["MIKTAR"].TODECIMAL() - DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@MIKTAR_ACK", DR["MIKTAR"].TODECIMAL() - DR["MIKTAR_DEG"].TODECIMAL());
                                cmd.Parameters.AddWithValue("@MASANO", masaNoX);
                                cmd.Parameters.AddWithValue("@X", DR["X"]);
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

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();

                        MessageBox.Show(
                        "Kayıt aşamasında hata oluştu:" + Environment.NewLine + ex.Message,
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    cmd.Dispose();
                    cnn.Close();
                }
                else
                {
                    MessageBox.Show(
                            "Hesap bölme işlemi için en az 1 ürün seçilmeli.",
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                        "Hesap bölme işleminde tüm ürünler seçilemez.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dgvStoklar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvStoklar.Rows[e.RowIndex].Cells[colSec.Name].Value =
                    dgvStoklar.Rows[e.RowIndex].Cells[colSec.Name].Value.TOINT() == 1 ? 0 : 1;
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            DataGridViewRow DR = dgvStoklar.CurrentRow;
            if (DR != null)
            {
                if (DR.Cells[colMiktarDegisim.Name].Value.TODECIMAL() == DR.Cells[colMiktarGercek.Name].Value.TODECIMAL())
                    DR.Cells[colMiktarDegisim.Name].Value = 0;

                DR.Cells[colMiktarDegisim.Name].Value =
                    (DR.Cells[colMiktarDegisim.Name].Value.ToString() + ((Button)sender).Text).TODECIMAL();

                if (DR.Cells[colMiktarDegisim.Name].Value.TODECIMAL() > DR.Cells[colMiktarGercek.Name].Value.TODECIMAL() || DR.Cells[colMiktarDegisim.Name].Value.TODECIMAL() == 0)
                    DR.Cells[colMiktarDegisim.Name].Value = DR.Cells[colMiktarGercek.Name].Value;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow DR = dgvStoklar.CurrentRow;
            if (DR != null)
            {
                DR.Cells[colMiktarDegisim.Name].Value = DR.Cells[colMiktarGercek.Name].Value;
            }
        }
    }
}
