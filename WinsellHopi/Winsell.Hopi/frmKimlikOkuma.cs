using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winsell.Hopi.fProject;

namespace Winsell.Hopi
{
    public partial class frmKimlikOkuma : Form
    {
        DialogResult drReturn = DialogResult.Cancel;
        public clsSiniflar.KimlikBilgisi kimlikBilgisi = new clsSiniflar.KimlikBilgisi();

        public frmKimlikOkuma()
        {
            InitializeComponent();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            mtbKimlik.Text = mtbKimlik.Text.Replace(" ", "") + ((Button)sender).Text;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            mtbKimlik.Clear();
        }

        private void frmKimlikOkuma_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }

        private void tsbTamam_Click(object sender, EventArgs e)
        {
            clsHopi.Kullanici kullanici = new clsHopi.Kullanici();
            kullanici.storeCode = clsGenel.storeCode;
            kullanici.token = mtbKimlik.Text.Trim();

            clsHopi.KullaniciResponse kullaniciResponse = clsHopiIslem.getKullaniciBilgi(clsGenel.kullaniciKoduWS, clsGenel.sifreWS, kullanici);
            if (kullaniciResponse.birdId > 0)
            {
                kimlikBilgisi.birdId = kullaniciResponse.birdId;
                kimlikBilgisi.paracik = kullaniciResponse.paracik;
                drReturn = DialogResult.OK;
                this.Close();
            }

        }

        private void tsbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mtbKimlik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                tsbTamam.PerformClick();
            }
        }
    }
}
