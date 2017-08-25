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
    public partial class frmKullaniciGirisi : Form
    {
        DialogResult drReturn = DialogResult.Cancel;
        public string kullaniciKodu = "";
        public string kullaniciAdi = "";

        public frmKullaniciGirisi()
        {
            InitializeComponent();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            txtSifre.Clear();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtSifre.Text += ((Button)sender).Text;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKullaniciGirisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = clsGenel.createDBConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT K_KODU, KULLANICI_ADI FROM KRDRMZ WHERE MOBIL_SIFRE = @MOBIL_SIFRE";
            cmd.Parameters.AddWithValue("@MOBIL_SIFRE", txtSifre.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                kullaniciKodu = reader["K_KODU"].TOSTRING();
                kullaniciAdi = reader["KULLANICI_ADI"].TOSTRING();
                drReturn = DialogResult.OK;
                this.Close();
            }
            else
            {
                btnSil.PerformClick();
            }
            cmd.Dispose();
            cnn.Close();
        }

        private void txtSifre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnGiris.PerformClick();
            }
        }
    }
}
