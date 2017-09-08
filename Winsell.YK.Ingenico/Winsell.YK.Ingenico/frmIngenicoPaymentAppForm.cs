using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Winsell.YK.Ingenico
{
    public partial class frmIngenicoPaymentAppForm : Form
    {
        public frmIngenicoPaymentAppForm()
        {
            InitializeComponent();
        }

        public ST_PAYMENT_APPLICATION_INFO[] stPaymentApplicationInfo2;
        public frmIngenicoPaymentAppForm(byte numberOfTotalRecordsReceived, ST_PAYMENT_APPLICATION_INFO[] stPaymentApplicationInfo)
        {
            InitializeComponent();

            stPaymentApplicationInfo2 = new ST_PAYMENT_APPLICATION_INFO[24];
            Array.Copy(stPaymentApplicationInfo, stPaymentApplicationInfo2, stPaymentApplicationInfo.Length);
            for (int i = 0; i < numberOfTotalRecordsReceived; i++)
            {

                string str = "";
                str += GMP_Tools.GetStringFromBytes(stPaymentApplicationInfo[i].name) + " [" + stPaymentApplicationInfo[i].u16BKMId.ToString() + "] " + " [" + stPaymentApplicationInfo[i].u16AppId.ToString() + "] " +
                                " [" + stPaymentApplicationInfo[i].Status.ToString() + "] " + " [" + stPaymentApplicationInfo[i].Priority.ToString() + "]";

                lstOdemeUygulamalari.Items.Add(str);
            }
            if (numberOfTotalRecordsReceived > 0)
                pstPaymentApplicationInfoSelected = null;
        }

        public byte numberOfTotalRecords;
        public ST_PAYMENT_APPLICATION_INFO pstPaymentApplicationInfoSelected;
        private void PaymentAppForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void m_listPaymentApplications_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstOdemeUygulamalari.SelectedIndex != -1)
            {
                int i = lstOdemeUygulamalari.SelectedIndex;
                pstPaymentApplicationInfoSelected = stPaymentApplicationInfo2[i];                
            }
        }
    }
}
