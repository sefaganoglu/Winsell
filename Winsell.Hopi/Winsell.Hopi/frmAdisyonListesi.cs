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
    public partial class frmAdisyonListesi : Form
    {
        public clsSiniflar.AdisyonBilgisi adisyonBilgisi = new clsSiniflar.AdisyonBilgisi();
        private DataTable dtAdisyonlar = new DataTable();
        private DataTable dtStoklar = new DataTable();
        private DataView dvStoklar;
        private DialogResult drReturn = DialogResult.Cancel;
        private bool hopiOlacakX = false;

        public frmAdisyonListesi(int masaNo, bool hopiOlacak = false)
        {
            InitializeComponent();
            hopiOlacakX = hopiOlacak;
            adisyonListesiGetir(masaNo);
        }

        private void adisyonListesiGetir(int masaNo)
        {
            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.CEKNO, SUM(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) - " +
                              "(ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS FLOAT)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS FLOAT)) / 100) " +
                              "AS Tutar, CASE WHEN (SELECT COUNT(CEKNO) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO AND ISNULL(BIRDID, CAST(0 AS INT)) > 0) = 0 THEN CAST(0 AS INT) ELSE CAST(1 AS INT) END AS Hopi_Var FROM RESCEK AS RC WHERE RC.MASANO = @MASANO GROUP BY RC.MASANO, RC.CEKNO";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtAdisyonlar);
            DA.Dispose();
            cmd.Parameters.Clear();

            cmd.CommandText = "SELECT RC.CEKNO, RC.STOKKODU, RC.STOKADI, MIKTAR_ACK, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) - " +
                              "ROUND(ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS MONEY)) * ((ISNULL((SELECT SUM(CAST(REPLACE(REPLACE(ISNULL(ISK, CAST('0' AS VARCHAR)), ',', ''), '.', '') AS MONEY)) FROM ODEME_TEMP WHERE CEKNO = RC.CEKNO), CAST(0 AS MONEY)) / CAST(100 AS MONEY)) / ISNULL((SELECT SUM(ISNULL(MIKTAR * SFIY, CAST(0 AS MONEY))) FROM RESCEK WHERE MASANO = RC.MASANO AND CEKNO = RC.CEKNO), CAST(0 AS MONEY))), 2) " +
                              "AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.KOD = 'B'";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            DA = new SqlDataAdapter(cmd);
            DA.Fill(dtStoklar);
            DA.Dispose();
            cmd.Dispose();
            cnn.Close();



            dgvAdisyonlar.DataSource = dtAdisyonlar;
            dvStoklar = new DataView(dtStoklar);

            dgvStoklar.DataSource = dvStoklar;

            if (dgvAdisyonlar.Rows.Count > 0)
                dgvAdisyonlar_CellEnter(dgvAdisyonlar, new DataGridViewCellEventArgs(0, 0));
        }

        private void dgvAdisyonlar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dvStoklar.RowFilter = "CEKNO = " + dgvAdisyonlar.Rows[e.RowIndex].Cells[colAdisyonNo.Name].Value.TOSTRING();
            }
        }

        private void tsbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbTamam_Click(object sender, EventArgs e)
        {
            if (dgvAdisyonlar.CurrentRow != null)
            {
                if ((!hopiOlacakX && dgvAdisyonlar.CurrentRow.Cells[colHopiVar.Name].Value.TOINT() == 0) || (hopiOlacakX && dgvAdisyonlar.CurrentRow.Cells[colHopiVar.Name].Value.TOINT() != 0))
                {
                    adisyonBilgisi.adisyonNo = dgvAdisyonlar.CurrentRow.Cells[colAdisyonNo.Name].Value.TOINT();
                    adisyonBilgisi.adisyonTutar = dgvAdisyonlar.CurrentRow.Cells[colTutar.Name].Value.TODECIMAL();
                    drReturn = DialogResult.OK;
                    Close();
                }
                else
                {
                    if (!hopiOlacakX)
                        MessageBox.Show("Bu adisyonda daha önce hopi ödemesi/kazanımı yapılmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    else
                        MessageBox.Show("Bu adisyonda daha önce hopi ödemesi/kazanımı yapılmamış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void frmAdisyonListesi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }

        private void dgvAdisyonlar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dgvAdisyonlar.Rows[e.RowIndex].Cells[colHopiVar.Name].Value.TOINT() == 1)
                {
                    e.CellStyle.BackColor = Color.Salmon;
                }
            }
        }
    }
}
