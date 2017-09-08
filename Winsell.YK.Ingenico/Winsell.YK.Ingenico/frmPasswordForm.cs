using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winsell.YK.Ingenico
{
    public partial class frmPasswordForm : Form
    {
        DialogResult drReturn = DialogResult.Cancel;
        public string strYoneticiSifresi = "";
        public frmPasswordForm()
        {
            InitializeComponent();
        }

        private void txtYoneticiSifresi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnTamam.PerformClick();
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (!txtYoneticiSifresi.Text.ISNULLOREMPTY())
            {
                strYoneticiSifresi = txtYoneticiSifresi.Text;
                drReturn = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Lütfen şifreyi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmPasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = drReturn;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
