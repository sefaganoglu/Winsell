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

        public frmAdisyonListesi(int masaNo)
        {
            InitializeComponent();
            adisyonListesiGetir(masaNo);
        }

        private void adisyonListesiGetir(int masaNo)
        {
            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT RC.CEKNO, SUM((ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT))) - ISNULL(RC.ALACAK, CAST(0 AS FLOAT))) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO GROUP BY RC.CEKNO";
            cmd.Parameters.AddWithValue("@MASANO", masaNo);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtAdisyonlar);
            DA.Dispose();
            cmd.Parameters.Clear();

            cmd.CommandText = "SELECT RC.CEKNO, RC.STOKKODU, RC.STOKADI, MIKTAR_ACK, ISNULL(RC.MIKTAR * RC.SFIY, CAST(0 AS FLOAT)) - ISNULL(RC.ISKONTOTUTARI1, CAST(0 AS FLOAT)) AS Tutar FROM RESCEK AS RC WHERE RC.MASANO = @MASANO AND RC.KOD = 'B'";
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
                adisyonBilgisi.adisyonNo = dgvAdisyonlar.CurrentRow.Cells[colAdisyonNo.Name].Value.TOINT();
                adisyonBilgisi.adisyonTutar = dgvAdisyonlar.CurrentRow.Cells[colTutar.Name].Value.TODECIMAL();
                drReturn = DialogResult.OK;
                Close();
            }
        }

        private void frmAdisyonListesi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }
    }
}
