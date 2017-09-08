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
    public partial class frmIngenicoPaymentTypeForm : Form
    {
        public static int PaymentTypeIndex { get; set; }

        public frmIngenicoPaymentTypeForm()
        {
            InitializeComponent();
            List<String> paymentType = new List<String>();

            paymentType.Add("SELECT TRANSACTION TYPE ON DEVICE");
            paymentType.Add("SALE");
            paymentType.Add("INSTALMENT SALE");
            paymentType.Add("BONUS SALE");

            for (int i = 0; i < paymentType.Count; i++)
            {

                string str = "";
                str += paymentType[i];

                lstOdemeTipleri.Items.Add(str);
            }
            lstOdemeTipleri.SelectedIndex = 0;
        }

        private void m_listPaymentTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOdemeTipleri.SelectedIndex != -1)
            {
                PaymentTypeIndex = lstOdemeTipleri.SelectedIndex;
                
                //pstPaymentApplicationInfoSelected = stPaymentApplicationInfo2[i];
                //stPaymentRequest[0].subtypeOfPayment
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

    }
}
