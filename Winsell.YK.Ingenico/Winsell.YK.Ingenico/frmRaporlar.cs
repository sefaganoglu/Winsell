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
    public partial class frmRaporlar : Form
    {
        public frmRaporlar()
        {
            InitializeComponent();
        }

        private void btnZRaporu_Click(object sender, EventArgs e)
        {
            UInt32 retcode = Defines.TRAN_RESULT_OK;

            FunctionFlags ffFlag = ((FunctionFlags)((Button)sender).AccessibleName.TOINTEGER());

            ST_FUNCTION_PARAMETERS stFunctionParameters = new ST_FUNCTION_PARAMETERS();

            if (!txtZNoBaslangic.Text.ISNULLOREMPTY())
                stFunctionParameters.ZNo_Start = txtZNoBaslangic.Text.TOUINT32();
            if (!txtZNoBitis.Text.ISNULLOREMPTY())
                stFunctionParameters.ZNo_Finish = txtZNoBitis.Text.TOUINT32();
            if (!txtFisNoBaslangic.Text.ISNULLOREMPTY())
                stFunctionParameters.FNo_Start = txtFisNoBaslangic.Text.TOUINT32();
            if (!txtFisNoBitis.Text.ISNULLOREMPTY())
                stFunctionParameters.FNo_Finish = txtFisNoBitis.Text.TOUINT32();
            if (!txtEkuNo.Text.ISNULLOREMPTY())
                stFunctionParameters.EKUNo = txtEkuNo.Text.TOUINT32();
            stFunctionParameters.supervisor = new byte[13];

            frmPasswordForm fpfForm = new frmPasswordForm();
            if (fpfForm.ShowDialog() == DialogResult.OK)
            {
                Array.Copy(Encoding.Default.GetBytes(fpfForm.strYoneticiSifresi), stFunctionParameters.supervisor, fpfForm.strYoneticiSifresi.Length);

                if (cbTarihliIslem.Checked)
                {
                    stFunctionParameters.date_Start.day = dtpTarihBaslangic.Value.Day.TOBYTE();
                    stFunctionParameters.date_Start.month = dtpTarihBaslangic.Value.Month.TOBYTE();
                    stFunctionParameters.date_Start.year = dtpTarihBaslangic.Value.Year.ToString().Substring(2, 2).TOBYTE();
                    stFunctionParameters.time_Start.hour = 00;
                    stFunctionParameters.time_Start.minute = 00;
                    stFunctionParameters.time_Start.second = 00;
                    stFunctionParameters.date_Finish.day = dtpTarihBitis.Value.Day.TOBYTE();
                    stFunctionParameters.date_Finish.month = dtpTarihBitis.Value.Month.TOBYTE();
                    stFunctionParameters.date_Finish.year = dtpTarihBitis.Value.Year.ToString().Substring(2, 2).TOBYTE();
                    stFunctionParameters.time_Finish.hour = 23;
                    stFunctionParameters.time_Finish.minute = 59;
                    stFunctionParameters.time_Finish.second = 59;
                }

                retcode = Json_GMPSmartDLL.FiscalPrinter_FunctionReports((int)ffFlag, ref stFunctionParameters, 120 * 1000);

                if (retcode == 2438)
                    MessageBox.Show("Girilen şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
