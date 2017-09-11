using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Winsell.YK.Ingenico
{
    public static class clsCihazIngenico
    {
        #region Classes

        static IngenicoParserClass parsClass;
        static IngenicoErrorClass errClass;
        static public byte[] m_dllVersion = new byte[24];
        static byte[] m_uniqueId;
        static byte[] TsmSign = null;
        public static RichTextBox rtbLog;

        public static void HandleErrorCode(UInt32 errorCode)
        {
            IngenicoErrorClass.DisplayErrorMessage(errorCode);

            if (errorCode == Defines.APP_ERR_GMP3_INVALID_HANDLE)
            {
                if (MessageBox.Show("ÖKC fişi yenilenmiş. Yüklemek ister misiniz?", "Uyarı", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;

                UInt32 retcode = IngenicoGMPSmartDLL.FiscalPrinter_Start(m_uniqueId, m_uniqueId.Length, TsmSign, TsmSign == null ? 0 : TsmSign.Length, null, 0, 10000);

                if (retcode == Defines.APP_ERR_ALREADY_DONE)
                    retcode = ReloadTransaction();

                IngenicoErrorClass.DisplayErrorMessage(retcode);
            }
        }

        private static UInt32 ReloadTransaction()
        {
            UInt32 retcode;
            ST_TICKET m_stTicket = new ST_TICKET();
            UInt64 activeFlags = 0;

            retcode = IngenicoGMPSmartDLL.FiscalPrinter_OptionFlags(ref activeFlags, Defines.GMP3_OPTION_ECHO_PRINTER | Defines.GMP3_OPTION_ECHO_ITEM_DETAILS | Defines.GMP3_OPTION_ECHO_PAYMENT_DETAILS, 0, Defines.TIMEOUT_DEFAULT);

            while (true)
            {

                retcode = Json_GMPSmartDLL.FiscalPrinter_GetTicket(ref m_stTicket, Defines.TIMEOUT_DEFAULT);

                for (int i = 0; i < m_stTicket.numberOfPaymentsInThis; i++)
                {
                    rtbLog.AppendText(m_stTicket.stPayment[i].stBankPayment.stPaymentErrMessage.ErrorMsg);
                    rtbLog.AppendText(Environment.NewLine + Environment.NewLine);
                }

                if (retcode != 0)
                    return retcode;

                if (m_stTicket.totalNumberOfPrinterLines > m_stTicket.numberOfPrinterLinesInThis)
                    continue;

                if (m_stTicket.totalNumberOfItems > m_stTicket.numberOfItemsInThis)
                    continue;

                if (m_stTicket.totalNumberOfPayments > m_stTicket.numberOfPaymentsInThis)
                    continue;

                // Tüm item ve printer satırları geldi
                break;
            }

            DisplayTransaction(m_stTicket, true);

            return Defines.TRAN_RESULT_OK;
        }

        private static void DisplayTransaction(ST_TICKET pstTicket, bool itemDetail)
        {
            try
            {
                string str_uniqueID = "";
                for (int i = 0; i < 24; i++)
                {
                    str_uniqueID += pstTicket.uniqueId[i].ToString("X2");
                }


                TransactionInfo(String.Format("UNIQUE ID        : " + str_uniqueID));
                TransactionInfo(String.Format("TICKET TYPE      : " + pstTicket.ticketType));
                TransactionInfo(String.Format("Z NO             : " + pstTicket.ZNo));
                TransactionInfo(String.Format("F NO             : " + pstTicket.FNo));
                TransactionInfo(String.Format("EJNO             : " + pstTicket.EJNo));
                TransactionInfo(String.Format("TRANSACTION FLAG : " + pstTicket.TransactionFlags.ToString().PadLeft(8, '0')));

                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_GMP3) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_GMP3"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TICKET_HEADER_PRINTED) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TICKET_HEADER_PRINTED"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TICKET_TOTALS_AND_PAYMENTS_PRINTED) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TICKET_TOTALS_AND_PAYMENTS_PRINTED"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TICKET_FOOTER_BEFORE_MF_PRINTED) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TICKET_FOOTER_BEFORE_MF_PRINTED"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TICKET_FOOTER_MF_PRINTED) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TICKET_FOOTER_MF_PRINTED"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TAXFREE_PARAMETERS_SET) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TAXFREE_PARAMETERS_SET"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_INVOICE_PARAMETERS_SET) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_INVOICE_PARAMETERS_SET"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_FULL_RCPT_CANCEL) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_FULL_RCPT_CANCEL"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_NONEY_COLLECTION_EXISTS) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_NONEY_COLLECTION_EXISTS"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TAXLESS_ITEM_EXISTS) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TAXLESS_ITEM_EXISTS"));
                if ((pstTicket.TransactionFlags & (uint)ETransactionFlags.FLG_XTRANS_TICKETTING_EXISTS) != 0) TransactionInfo(String.Format("                : FLG_XTRANS_TICKETTING_EXISTS"));

                TransactionInfo(String.Format("OPTION FLAG      : " + pstTicket.OptionFlags.ToString().PadLeft(8, '0')));
                TransactionInfo(String.Format("İşlem Tarihi".PadRight(30) + " : {0}/{1}/20{2}", pstTicket.bcdTicketDate[2].ToString("X2").PadLeft(2, '0'), pstTicket.bcdTicketDate[1].ToString("X2").PadLeft(2, '0'), pstTicket.bcdTicketDate[0].ToString("X2").PadLeft(2, '0')));
                TransactionInfo(String.Format("İşlem Saati".PadRight(30) + " : {0}:{1}:{2}", pstTicket.bcdTicketTime[0].ToString("X2").PadLeft(2, '0'), pstTicket.bcdTicketTime[1].ToString("X2").PadLeft(2, '0'), pstTicket.bcdTicketTime[2].ToString("X2").PadLeft(2, '0')));
                TransactionInfo(String.Format("Toplam".PadRight(30) + " : {0}.{1}", (pstTicket.TotalReceiptAmount / 100).ToString(), (pstTicket.TotalReceiptAmount % 100).ToString().PadLeft(2, '0')));
                if (pstTicket.KatkiPayiAmount != 0)
                    TransactionInfo(String.Format("MATRAHSZ        : {0}.{1}", pstTicket.KatkiPayiAmount / 100, pstTicket.KatkiPayiAmount % 100));

                TransactionInfo(String.Format("Toplam Vergi".PadRight(30) + " : {0}.{1}", pstTicket.TotalReceiptTax / 100, (pstTicket.TotalReceiptTax % 100).ToString().PadLeft(2, '0')));
                TransactionInfo("Ürün Tablosu".PadRight(30) + " : " + pstTicket.totalNumberOfItems);

                if (pstTicket.TotalReceiptDiscount != 0)
                    TransactionInfo(String.Format("TOTAL DISCOUNT  : {0}.{1}", pstTicket.TotalReceiptDiscount / 100, pstTicket.TotalReceiptDiscount % 100));

                if (pstTicket.TotalReceiptIncrement != 0)
                    TransactionInfo(String.Format("TOTAL INCREAE   : {0}.{1}", pstTicket.TotalReceiptIncrement / 100, pstTicket.TotalReceiptIncrement % 100));

                if (pstTicket.TotalReceiptItemCancel != 0)
                    TransactionInfo(String.Format("TOTAL VOID      : {0}.{1}", pstTicket.TotalReceiptItemCancel / 100, pstTicket.TotalReceiptItemCancel % 100));

                if (pstTicket.KasaAvansAmount != 0)
                    TransactionInfo(String.Format("KASA AVANS      : {0}.{1}", pstTicket.KasaAvansAmount / 100, pstTicket.KasaAvansAmount % 100));

                if (pstTicket.KasaPaymentAmount != 0)
                    TransactionInfo(String.Format("KASA PAYMENT    : {0}.{1}", pstTicket.KasaPaymentAmount / 100, pstTicket.KasaPaymentAmount % 100));

                if (pstTicket.CashBackAmount != 0)
                    TransactionInfo(String.Format("CASHBACK        : {0}.{1}", pstTicket.CashBackAmount / 100, pstTicket.CashBackAmount % 100));

                if (pstTicket.invoiceAmount != 0)
                    TransactionInfo(String.Format("INVOICE         : {0}.{1}", pstTicket.invoiceAmount / 100, pstTicket.invoiceAmount % 100));

                if (pstTicket.TaxFreeCalculated != 0)
                    TransactionInfo(String.Format("TAXFREE CALCULA : {0}.{1}", pstTicket.TaxFreeCalculated / 100, pstTicket.TaxFreeCalculated % 100));

                if (pstTicket.TaxFreeRefund != 0)
                    TransactionInfo(String.Format("TAXFREE REFUND  : {0}.{1}", pstTicket.TaxFreeRefund / 100, pstTicket.TaxFreeRefund % 100));

                if (pstTicket.TotalReceiptReversedPayment != 0)
                    TransactionInfo(String.Format("REVERSE PAYMENTS: {0} ", formatAmount(pstTicket.TotalReceiptReversedPayment, ECurrency.CURRENCY_NONE)));

                if (pstTicket.TotalReceiptPayment != 0)
                    TransactionInfo(String.Format("PAYMENTS        : {0} ", formatAmount(pstTicket.TotalReceiptPayment, ECurrency.CURRENCY_NONE)));

                for (int i = 0; i < pstTicket.stPayment.Length; i++)
                {
                    if (pstTicket.stPayment[i] != null)
                    {
                        for (int j = 0; j < pstTicket.stPayment[i].stBankPayment.numberOfsubPayment; j++)
                        {
                            if (pstTicket.stPayment[i].stBankPayment.stBankSubPaymentInfo[j].amount != 0)
                            {
                                TransactionInfo(String.Format("BONUS NAME      : {0} ", pstTicket.stPayment[i].stBankPayment.stBankSubPaymentInfo[j].name));
                                TransactionInfo(String.Format("BONUS TYPE      : {0} ", pstTicket.stPayment[i].stBankPayment.stBankSubPaymentInfo[j].type));
                                TransactionInfo(String.Format("BONUS AMOUNT    : {0} ", pstTicket.stPayment[i].stBankPayment.stBankSubPaymentInfo[j].amount));
                            }
                        }
                    }
                }

                for (int i = 0; i < pstTicket.numberOfPrinterLinesInThis; i++)
                    TransactionInfo(String.Format("{0}", pstTicket.stPrinterCopy[i].line));

                if (itemDetail)
                    for (int i = pstTicket.totalNumberOfItems - pstTicket.numberOfItemsInThis; i < pstTicket.totalNumberOfItems; i++)
                    {
                        TransactionInfo("-Ürün" + (i + 1));
                        TransactionInfo(String.Format("  " + "İsim".PadRight(20) + " : {0}", pstTicket.SaleInfo[i].Name));
                        TransactionInfo(String.Format("  " + "Barkod".PadRight(20) + " : {0}", pstTicket.SaleInfo[i].Barcode));
                        TransactionInfo(String.Format("  " + "Tutar".PadRight(20) + " : {0}", formatAmount((uint)pstTicket.SaleInfo[i].ItemPrice, (ECurrency)pstTicket.SaleInfo[i].ItemCurrencyType)));
                        TransactionInfo(String.Format("  " + "Orjinal Tutar".PadRight(20) + " : {0}", formatAmount(pstTicket.SaleInfo[i].OrigialItemAmount, (ECurrency)pstTicket.SaleInfo[i].OriginalItemAmountCurrency)));
                        TransactionInfo(String.Format("  " + "İndirim".PadRight(20) + " : {0}", formatAmount((uint)pstTicket.SaleInfo[i].DecAmount, ECurrency.CURRENCY_TL)));
                        TransactionInfo(String.Format("  " + "Arttırım".PadRight(20) + " : {0}", formatAmount((uint)pstTicket.SaleInfo[i].IncAmount, ECurrency.CURRENCY_TL)));
                        TransactionInfo(String.Format("  " + "Ürün Sayısı".PadRight(20) + " : {0}", formatCount(pstTicket.SaleInfo[i].ItemCount, pstTicket.SaleInfo[i].ItemCountPrecision, (EItemUnitTypes)pstTicket.SaleInfo[i].ItemUnitType)));
                    }

                TransactionInfo("Ödeme Tablosu".PadRight(30) + " : " + pstTicket.totalNumberOfPayments);

            }
            catch
            {

            }
        }

        public static void TransactionInfo(string Item)
        {
            rtbLog.SelectedText = string.Empty;
            rtbLog.SelectionFont = new Font(rtbLog.SelectionFont, FontStyle.Bold);
            rtbLog.SelectionColor = Color.Red;
            if (!string.IsNullOrEmpty(rtbLog.Text.Trim())) rtbLog.AppendText(Environment.NewLine + Environment.NewLine);
            rtbLog.AppendText(Item);
            rtbLog.ScrollToCaret();
        }

        public static string formatAmount(uint amount, ECurrency currency)
        {

            string amountStr = "";

            amountStr = String.Format("{0}.{1:00}", amount / 100, amount % 100);

            switch (currency)
            {
                case ECurrency.CURRENCY_NONE:
                    break;
                case ECurrency.CURRENCY_DOLAR:
                    amountStr += " $";
                    break;
                case ECurrency.CURRENCY_EU:
                    amountStr += " €";
                    break;
                case ECurrency.CURRENCY_TL:
                    amountStr += " TL";
                    break;
                default:
                    amountStr += " ?";
                    break;
            }

            return amountStr;
        }

        public static string formatAmount(int amount, ECurrency currency)
        {

            string amountStr = "";

            amountStr = String.Format("{0}.{1:00}", amount / 100, amount % 100);

            switch (currency)
            {
                case ECurrency.CURRENCY_NONE:
                    break;
                case ECurrency.CURRENCY_DOLAR:
                    amountStr += " $";
                    break;
                case ECurrency.CURRENCY_EU:
                    amountStr += " €";
                    break;
                case ECurrency.CURRENCY_TL:
                    amountStr += " TL";
                    break;
                default:
                    amountStr += " ?";
                    break;
            }

            return amountStr;
        }

        private static string formatCount(int itemCount, byte ItemCountPrecision, EItemUnitTypes itemUnitType)
        {
            return itemCount.ToString();
        }

        private static void OnBnClickedButtonVoidAll()
        {
            UInt32 retcode;
            ST_TICKET m_stTicket = new ST_TICKET();

            retcode = Json_GMPSmartDLL.FiscalPrinter_VoidAll(ref m_stTicket, Defines.TIMEOUT_DEFAULT);

            if (retcode != 0)
            {
                HandleErrorCode(retcode);
                return;
            }

            DisplayTransaction(m_stTicket, false);

            IngenicoGMPSmartDLL.FiscalPrinter_Close(Defines.TIMEOUT_DEFAULT);
            Array.Clear(m_uniqueId, 0, m_uniqueId.Length);

            HandleErrorCode(retcode);
        }

        private static UInt32 OnBnClickedButtonStatus()
        {
            UInt32 retcode;

            retcode = ReloadTransaction();
            if (retcode != 0)
            {
                HandleErrorCode(retcode);
            }

            return retcode;
        }

        private static UInt32 StartTicket(TTicketType ticketType)
        {
            UInt32 retcode = Defines.TRAN_RESULT_OK;
            start_again:
            ulong retcode_handle = IngenicoGMPSmartDLL.FiscalPrinter_GetHandle();
            if (retcode_handle == 0)
            {
                if (ticketType != TTicketType.TProcessSale)
                    Array.Clear(m_uniqueId, 0, m_uniqueId.Length);
                var sevenItems = new byte[] { 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54 };
                retcode = IngenicoGMPSmartDLL.FiscalPrinter_Start(m_uniqueId, m_uniqueId.Length, TsmSign, TsmSign == null ? 0 : TsmSign.Length, sevenItems, 7, 10000);
                if (retcode == Defines.APP_ERR_ALREADY_DONE)
                {
                    switch (MessageBox.Show("ÖKC'de tamamlanmamış işlem var. İşlem iptal edilsin mi?", "Tamamlanmamış İşlem", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        case DialogResult.Yes:
                            OnBnClickedButtonVoidAll();
                            goto start_again;
                        case DialogResult.No:
                            return ReloadTransaction();
                    }
                }
                else if (retcode == Defines.TRAN_RESULT_OK)
                    retcode = IngenicoGMPSmartDLL.FiscalPrinter_TicketHeader(ticketType, Defines.TIMEOUT_DEFAULT);

                if (retcode == Defines.TRAN_RESULT_OK)
                {
                    UInt64 activeFlags = 0;
                    retcode = IngenicoGMPSmartDLL.FiscalPrinter_OptionFlags(ref activeFlags, Defines.GMP3_OPTION_ECHO_PRINTER | Defines.GMP3_OPTION_ECHO_ITEM_DETAILS | Defines.GMP3_OPTION_ECHO_PAYMENT_DETAILS, 0x00000000, Defines.TIMEOUT_DEFAULT);
                }
            }

            if (retcode != Defines.TRAN_RESULT_OK)
            {
                HandleErrorCode(retcode);
                // Handle Açık kalmasın...
                IngenicoGMPSmartDLL.FiscalPrinter_Close(Defines.TIMEOUT_DEFAULT);
            }

            return retcode;
        }

        private static void prcdPortAc()
        {
            UInt32 resp;

            ST_GMP_PAIR pairing = new ST_GMP_PAIR();
            pairing.In_DeviceBrand = Encoding.Default.GetBytes("INGENICO");
            pairing.In_DeviceModel = Encoding.Default.GetBytes("IWE280");
            pairing.In_DeviceEcrRegisterNo = Encoding.Default.GetBytes("12344567");
            pairing.In_DeviceSerialNum = Encoding.Default.GetBytes("JHWE20000079");
            pairing.In_ProcOrderNum = parsClass.GMP_AscToBcd("000001");
            Array.Copy(parsClass.GMP_AscToBcd(DateTime.Now.ToString("ddMMyy")), pairing.In_ProcDate, pairing.In_ProcDate.Length);
            Array.Copy(parsClass.GMP_AscToBcd(DateTime.Now.ToString("ddMMyy")), pairing.In_ProcTime, pairing.In_ProcTime.Length);

            ST_GMP_PAIR_RESP pairingResp = new ST_GMP_PAIR_RESP();

            resp = Json_GMPSmartDLL.GMP_StartPairingInit(ref pairing, ref pairingResp);

            if (resp != Defines.TRAN_RESULT_OK)
            {
                HandleErrorCode(resp);
                return;
            }
            else
                IngenicoParserClass.PairingStructParser(pairingResp);

            /////////////////////////////////////////////////////////////////////////////

            byte[] arr = new byte[100];
            short len = 0;
            resp = IngenicoGMPSmartDLL.FiscalPrinter_GetTlvData(Defines.GMP_EXT_DEVICE_TAG_Z_NO, arr, (short)arr.Length, ref len);
            string TempZNo = "0";
            for (int i = 0; i < len; i++)
            {
                TempZNo += arr[i].ToString("X2");
            }
            int ZNo = Convert.ToInt32(TempZNo);
            HandleErrorCode(resp);

            /////////////////////////////////////////////////////////////////////////////

            byte[] TempArr = new byte[100];
            short TempLen = 0;
            resp = IngenicoGMPSmartDLL.FiscalPrinter_GetTlvData(Defines.GMP_EXT_DEVICE_FIS_LIMIT, TempArr, (short)TempArr.Length, ref TempLen);
            HandleErrorCode(resp);


            byte[] arr2 = new byte[100];
            arr2[0] = 0x00;
            arr2[1] = 0x00;
            arr2[2] = 0x10;
            arr2[3] = 0x00;
            arr2[4] = 0x10;
            arr2[5] = 0x00;
            short len2 = 6;
            resp = IngenicoGMPSmartDLL.FiscalPrinter_SetTlvData(Defines.GMP_EXT_DEVICE_FIS_LIMIT, arr2, (UInt16)len2);

            byte[] TempArr3 = new byte[100];
            short TempLen3 = 0;
            resp = IngenicoGMPSmartDLL.FiscalPrinter_GetTlvData(Defines.GMP_EXT_DEVICE_FIS_LIMIT, TempArr3, (short)TempArr3.Length, ref TempLen3);
            HandleErrorCode(resp);

            /////////////////////////////////////////////////////////////////////////////

            byte[] temp = new byte[24];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = 0xFF;
            resp = IngenicoGMPSmartDLL.FiscalPrinter_Start(temp, temp.Length, null, 0, null, 0, Defines.TIMEOUT_DEFAULT);

            int flag = 0;
            if (resp == Defines.APP_ERR_ALREADY_DONE)
            {
                DialogResult dr = MessageBox.Show("ÖKC'de tamamlanmamış işlem var. İşlem iptal edilsin mi?", "Tamamlanmamış İşlem", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                switch (dr)
                {
                    case DialogResult.Yes:
                        OnBnClickedButtonVoidAll();
                        break;
                    case DialogResult.No:
                        resp = OnBnClickedButtonStatus();
                        flag = 1;
                        break;
                }
            }

            if (flag != 1)
            {
                resp = IngenicoGMPSmartDLL.FiscalPrinter_Close(Defines.TIMEOUT_DEFAULT);
                Array.Clear(m_uniqueId, 0, m_uniqueId.Length);
                flag = 0;
            }
            if (resp == 0)
            {
                TransactionInfo("TRAN_RESULT_OK");
            }

            HandleErrorCode(resp);
        }

        private static UInt32 GetPayment(ST_PAYMENT_REQUEST[] stPaymentRequest, int numberOfPayments)
        {
            UInt32 retcode;
            ST_TICKET m_stTicket = new ST_TICKET();

            string display = "";

            retcode = Json_GMPSmartDLL.FiscalPrinter_Payment(ref stPaymentRequest[0], ref m_stTicket, 30000);

            for (int i = 0; i < m_stTicket.stPayment.Length; i++)
            {
                if (m_stTicket.stPayment[i] != null)
                {
                    if (m_stTicket.stPayment[i].stBankPayment.bankName != "")
                    {
                        display += m_stTicket.stPayment[i].stBankPayment.bankName + Environment.NewLine;
                        display += "Banking Error : " + m_stTicket.stPayment[i].stBankPayment.stPaymentErrMessage.ErrorCode + " " + m_stTicket.stPayment[i].stBankPayment.stPaymentErrMessage.ErrorMsg + Environment.NewLine;
                        display += "Application Error : " + m_stTicket.stPayment[i].stBankPayment.stPaymentErrMessage.AppErrorCode + " " + m_stTicket.stPayment[i].stBankPayment.stPaymentErrMessage.AppErrorMsg + Environment.NewLine;
                        display += "----------------------------------------------" + Environment.NewLine;
                    }
                }
            }

            UInt32 TicketAmount = m_stTicket.TotalReceiptAmount + m_stTicket.KatkiPayiAmount;

            switch (retcode)
            {
                case Defines.TRAN_RESULT_OK:

                    if (stPaymentRequest[0].numberOfinstallments != 0)
                        display += String.Format("TAKSIT SAYISI : {0}", stPaymentRequest[0].numberOfinstallments) + Environment.NewLine;

                    if (m_stTicket.KasaAvansAmount != 0)
                    {
                        display += String.Format("KASA AVANS TOTAL: {0}", formatAmount(m_stTicket.KasaAvansAmount, ECurrency.CURRENCY_TL)) + Environment.NewLine;
                        TicketAmount = m_stTicket.KasaAvansAmount;
                    }
                    else if (m_stTicket.invoiceAmount != 0)
                    {
                        display += String.Format("INVOICE TOTAL : {0}", formatAmount(m_stTicket.invoiceAmount, ECurrency.CURRENCY_TL)) + Environment.NewLine;
                        TicketAmount = m_stTicket.invoiceAmount;
                    }
                    else
                        display += String.Format("TOTAL : {0}", formatAmount(m_stTicket.TotalReceiptAmount, ECurrency.CURRENCY_TL)) + Environment.NewLine;

                    if (m_stTicket.CashBackAmount != 0)
                        display += String.Format(Environment.NewLine + "CASHBACK : {0}", formatAmount(m_stTicket.CashBackAmount, ECurrency.CURRENCY_TL)) + Environment.NewLine;
                    else
                        display += String.Format(Environment.NewLine + "REMAIN : {0}", formatAmount(TicketAmount - m_stTicket.TotalReceiptPayment, ECurrency.CURRENCY_TL)) + Environment.NewLine;

                    if ((stPaymentRequest[0].typeOfPayment == (uint)EPaymentTypes.PAYMENT_BANK_CARD) || (stPaymentRequest[0].typeOfPayment == (uint)EPaymentTypes.PAYMENT_MOBILE))
                    {
                        display += String.Format(Environment.NewLine + "{0}", m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.bankName) + Environment.NewLine;
                        display += String.Format(Environment.NewLine + "ONAY KODU : {0}", m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.authorizeCode) + Environment.NewLine;
                        display += String.Format(Environment.NewLine + "{0} / {1}", m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stCard.pan
                                                                                    , m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stCard.expireDate
                                                                                    ) + Environment.NewLine;
                    }

                    if (m_stTicket.TotalReceiptPayment >= TicketAmount)
                    {
                        retcode = IngenicoGMPSmartDLL.FiscalPrinter_PrintTotalsAndPayments(Defines.TIMEOUT_DEFAULT);
                        if (retcode != Defines.TRAN_RESULT_OK && retcode != Defines.APP_ERR_ALREADY_DONE)
                            break;

                        retcode = IngenicoGMPSmartDLL.FiscalPrinter_PrintBeforeMF(Defines.TIMEOUT_DEFAULT);
                        if (retcode != Defines.TRAN_RESULT_OK && retcode != Defines.APP_ERR_ALREADY_DONE)
                            break;

                        ST_USER_MESSAGE[] stUserMessage = new ST_USER_MESSAGE[2];
                        for (int i = 0; i < stUserMessage.Length; i++)
                        {
                            stUserMessage[i] = new ST_USER_MESSAGE();
                        }

                        stUserMessage[0].flag = Defines.PS_38 | Defines.PS_CENTER;
                        stUserMessage[0].message = "Teşekkür Ederiz";
                        stUserMessage[0].len = (byte)"Teşekkür Ederiz".Length;

                        stUserMessage[1].flag = Defines.PS_GRAPHIC | Defines.PS_CENTER;
                        stUserMessage[1].message = "/Bitmap/INGEW.bmp";
                        stUserMessage[1].len = (byte)"/Bitmap/INGEW.bmp".Length;

                        retcode = Json_GMPSmartDLL.FiscalPrinter_PrintUserMessage(ref stUserMessage, 2, ref m_stTicket, Defines.TIMEOUT_DEFAULT);

                        retcode = IngenicoGMPSmartDLL.FiscalPrinter_PrintMF(Defines.TIMEOUT_CARD_TRANSACTIONS);
                        if (retcode != Defines.TRAN_RESULT_OK && retcode != Defines.APP_ERR_ALREADY_DONE)
                            break;

                        Array.Clear(m_uniqueId, 0, m_uniqueId.Length);
                        retcode = IngenicoGMPSmartDLL.FiscalPrinter_Close(Defines.TIMEOUT_DEFAULT);
                    }

                    DisplayTransaction(m_stTicket, false);
                    break;

                case Defines.APP_ERR_PAYMENT_NOT_SUCCESSFUL_AND_NO_MORE_ERROR_CODE:
                    DisplayTransaction(m_stTicket, false);
                    break;

                case Defines.APP_ERR_PAYMENT_NOT_SUCCESSFUL_AND_MORE_ERROR_CODE:
                    DisplayTransaction(m_stTicket, false);

                    if (m_stTicket.totalNumberOfPayments != 0 && m_stTicket.stPayment[0] != null)
                    {
                        if ((stPaymentRequest[0].typeOfPayment == (uint)EPaymentTypes.PAYMENT_BANK_CARD) || (stPaymentRequest[0].typeOfPayment == (uint)EPaymentTypes.PAYMENT_MOBILE))
                        {
                            display += String.Format(Environment.NewLine + "{0}({1})", m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stPaymentErrMessage.ErrorMsg
                                                                                , m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stPaymentErrMessage.ErrorCode
                                                                                ) + Environment.NewLine;
                            display += String.Format(Environment.NewLine + "{0}({1})", m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stPaymentErrMessage.AppErrorMsg
                                                                                , m_stTicket.stPayment[m_stTicket.totalNumberOfPayments - 1].stBankPayment.stPaymentErrMessage.AppErrorCode
                                                                                ) + Environment.NewLine;
                        }
                    }

                    break;

                default:
                    break;
            }

            TransactionInfo(display);

            HandleErrorCode(retcode);

            return retcode;
        }
        #endregion

        public static void prcdCreate(RichTextBox rtbLogTextBox)
        {
            rtbLog = rtbLogTextBox;
            m_uniqueId = new byte[24];
            parsClass = new IngenicoParserClass();
            errClass = new IngenicoErrorClass();
            Array.Clear(m_dllVersion, 0, m_dllVersion.Length);
            UInt32 ret = IngenicoGMPSmartDLL.GMP_GetDllVersion(m_dllVersion);
            prcdPortAc();
        }

        public static void prcdSatisYap(string strDepartmanNo, string strStokAdi, string strBarkodu, string strBirimKodu, double dblMiktar, double dblFiyat)
        {
            ulong ActiveFlags = 0;
            UInt32 retcode;
            string name = strStokAdi;
            string barcode = strBarkodu;
            int amount = Math.Truncate(dblFiyat * 100).TOINTEGER();
            UInt16 currency = 949;

            byte itemCountPrecition = 3;
            UInt32 itemCount = Math.Truncate(dblMiktar * 1000).TOUINT32();

            ST_TICKET m_stTicket = new ST_TICKET();
            ST_ITEM stItem = new ST_ITEM();
            EItemUnitTypes iutUnitType = IngenicoExtensions.ParseEnum<EItemUnitTypes>("ITEM_" + strBirimKodu);
            stItem.type = Defines.ITEM_TYPE_DEPARTMENT;
            stItem.subType = 0;
            stItem.deptIndex = (byte)(strDepartmanNo.TOINTEGER());
            stItem.amount = (uint)amount;
            stItem.currency = currency;
            stItem.count = itemCount;
            stItem.unitType = (byte)iutUnitType;
            stItem.pluPriceIndex = 0;
            stItem.countPrecition = itemCountPrecition;
            stItem.name = name;
            stItem.barcode = barcode;

            Start:
            retcode = StartTicket(TTicketType.TProcessSale);
            if (retcode != Defines.TRAN_RESULT_OK)
                return;

            if (retcode == Defines.TRAN_RESULT_OK)
                retcode = IngenicoGMPSmartDLL.FiscalPrinter_OptionFlags(ref ActiveFlags, Defines.GMP3_OPTION_ECHO_PRINTER | Defines.GMP3_OPTION_ECHO_ITEM_DETAILS | Defines.GMP3_OPTION_ECHO_PAYMENT_DETAILS | Defines.GMP3_OPTION_NO_RECEIPT_LIMIT_CONTROL_FOR_ITEMS, 0, Defines.TIMEOUT_DEFAULT);


            if (IngenicoGMPSmartDLL.FiscalPrinter_GetHandle() == 0)
            {
                DisplayTransaction(m_stTicket, false);
            }

            retcode = Json_GMPSmartDLL.FiscalPrinter_ItemSale(ref stItem, ref m_stTicket, Defines.TIMEOUT_DEFAULT);

            if (retcode == Defines.APP_ERR_TICKET_HEADER_NOT_PRINTED)
            {
                Array.Clear(m_uniqueId, 0, m_uniqueId.Length);
                IngenicoGMPSmartDLL.FiscalPrinter_Close(Defines.TIMEOUT_DEFAULT);
                goto Start;
            }
            if (retcode != 0)
            {
                HandleErrorCode(retcode);
                return;
            }

            DisplayTransaction(m_stTicket, false);
            HandleErrorCode(retcode);
        }

        public static void prcdIskontoYap(string strAciklama, ushort intSatirIndex, double dblArttirimTutari)
        {
            UInt32 retcode = Defines.DLL_RETCODE_FAIL;
            ST_TICKET m_stTicket = new ST_TICKET();

            int changedAmount = Math.Truncate(dblArttirimTutari * 100).TOINTEGER();

            retcode = Json_GMPSmartDLL.FiscalPrinter_Minus_Ex(changedAmount, strAciklama, ref m_stTicket, intSatirIndex, Defines.TIMEOUT_DEFAULT);

            if (retcode != 0)
            {
                HandleErrorCode(retcode);
                return;
            }

            DisplayTransaction(m_stTicket, false);

            if (intSatirIndex == 0xFFFF)
            {
                TransactionInfo(String.Format("{0} ({1})" + Environment.NewLine + "{2}" + Environment.NewLine + "{3} {4}"
                                                , ""
                                                , "Tüm Fiş"
                                                , formatAmount(changedAmount, ECurrency.CURRENCY_TL)
                                                , "Ara Toplam"
                                                , formatAmount(m_stTicket.TotalReceiptAmount, ECurrency.CURRENCY_TL)
                                                ));
            }
            else
            {
                TransactionInfo(String.Format("{0} ({1} {2})" + Environment.NewLine + "+{3}" + Environment.NewLine + "{4} X {5} {6}"
                                          , strAciklama
                                          , "Ürün"
                                          , intSatirIndex
                                          , formatAmount(changedAmount, ECurrency.CURRENCY_TL)
                                          , formatCount(m_stTicket.SaleInfo[intSatirIndex].ItemCount, m_stTicket.SaleInfo[intSatirIndex].ItemCountPrecision, (EItemUnitTypes)m_stTicket.SaleInfo[intSatirIndex].ItemUnitType)
                                          , m_stTicket.SaleInfo[intSatirIndex].Name
                                          , formatAmount((uint)m_stTicket.SaleInfo[intSatirIndex].ItemPrice, (ECurrency)m_stTicket.SaleInfo[intSatirIndex].ItemCurrencyType)
                                         ));
            }
            HandleErrorCode(retcode);
        }

        public static void prcdArttirimYap(string strAciklama, ushort intSatirIndex, double dblIskontoTutari)
        {
            UInt32 retcode = Defines.DLL_RETCODE_FAIL;
            ST_TICKET m_stTicket = new ST_TICKET();

            int changedAmount = Math.Truncate(dblIskontoTutari * 100).TOINTEGER();

            retcode = Json_GMPSmartDLL.FiscalPrinter_Plus_Ex(changedAmount, strAciklama, ref m_stTicket, intSatirIndex, Defines.TIMEOUT_DEFAULT);

            if (retcode != 0)
            {
                HandleErrorCode(retcode);
                return;
            }

            DisplayTransaction(m_stTicket, false);

            if (intSatirIndex == 0xFFFF)
            {
                TransactionInfo(String.Format("{0} ({1})" + Environment.NewLine + "{2}" + Environment.NewLine + "{3} {4}"
                                                , ""
                                                , "Tüm Fiş"
                                                , formatAmount(changedAmount, ECurrency.CURRENCY_TL)
                                                , "Ara Toplam"
                                                , formatAmount(m_stTicket.TotalReceiptAmount, ECurrency.CURRENCY_TL)
                                                ));
            }
            else
            {
                TransactionInfo(String.Format("{0} ({1} {2})" + Environment.NewLine + "+{3}" + Environment.NewLine + "{4} X {5} {6}"
                                          , strAciklama
                                          , "Ürün"
                                          , intSatirIndex
                                          , formatAmount(changedAmount, ECurrency.CURRENCY_TL)
                                          , formatCount(m_stTicket.SaleInfo[intSatirIndex].ItemCount, m_stTicket.SaleInfo[intSatirIndex].ItemCountPrecision, (EItemUnitTypes)m_stTicket.SaleInfo[intSatirIndex].ItemUnitType)
                                          , m_stTicket.SaleInfo[intSatirIndex].Name
                                          , formatAmount((uint)m_stTicket.SaleInfo[intSatirIndex].ItemPrice, (ECurrency)m_stTicket.SaleInfo[intSatirIndex].ItemCurrencyType)
                                         ));
            }

            HandleErrorCode(retcode);
        }

        public static void prcdBelgeIptal()
        {
            OnBnClickedButtonVoidAll();
            Array.Clear(m_uniqueId, 0, m_uniqueId.Length);
        }

        public static void prcdYemekCekiBaslat()
        {
            UInt32 retcode = Defines.TRAN_RESULT_OK;
            UInt64 activeFlags = 0;

            if (IngenicoGMPSmartDLL.FiscalPrinter_GetHandle() == 0)
            {
                retcode = IngenicoGMPSmartDLL.FiscalPrinter_Start(m_uniqueId, m_uniqueId.Length, null, 0, null, 0, Defines.TIMEOUT_DEFAULT);
                if (retcode != Defines.TRAN_RESULT_OK)
                    HandleErrorCode(retcode);

                retcode = IngenicoGMPSmartDLL.FiscalPrinter_OptionFlags(ref activeFlags, (Defines.GMP3_OPTION_ECHO_PRINTER | Defines.GMP3_OPTION_ECHO_ITEM_DETAILS | Defines.GMP3_OPTION_ECHO_PAYMENT_DETAILS), 0x00000000, Defines.TIMEOUT_DEFAULT);
                if (retcode != Defines.TRAN_RESULT_OK)
                    HandleErrorCode(retcode);
            }

            retcode = IngenicoGMPSmartDLL.FiscalPrinter_TicketHeader(TTicketType.TYemekceki, Defines.TIMEOUT_DEFAULT);

            HandleErrorCode(retcode);
        }

        public static void prcdOdemeYap(string strOdemeTipi, double dblAmount, string strTaksitSayisi = "")
        {
            if (strOdemeTipi == "01" || strOdemeTipi == "08" || strOdemeTipi == "11")
            {
                UInt16 currencyOfPayment = 0;

                if (currencyOfPayment == (UInt16)ECurrency.CURRENCY_NONE)
                    currencyOfPayment = (UInt16)ECurrency.CURRENCY_TL;

                ST_PAYMENT_REQUEST[] stPaymentRequest = new ST_PAYMENT_REQUEST[1];
                for (int i = 0; i < stPaymentRequest.Length; i++)
                {
                    stPaymentRequest[i] = new ST_PAYMENT_REQUEST();
                }

                if (strOdemeTipi == "01")
                    stPaymentRequest[0].typeOfPayment = (uint)EPaymentTypes.PAYMENT_CASH_TL;
                else if (strOdemeTipi == "08")
                    stPaymentRequest[0].typeOfPayment = (uint)EPaymentTypes.PAYMENT_MOBILE;
                else if (strOdemeTipi == "11")
                    stPaymentRequest[0].typeOfPayment = (uint)EPaymentTypes.PAYMENT_PUAN;
                stPaymentRequest[0].subtypeOfPayment = 0;
                stPaymentRequest[0].payAmount = (dblAmount * 100).TOUINT32();
                stPaymentRequest[0].payAmountCurrencyCode = currencyOfPayment;
                stPaymentRequest[0].paymentName = "INGENICO";

                GetPayment(stPaymentRequest, 1);
            }
            else if (strOdemeTipi == "02")
            {
                byte numberOfTotalRecords = 0;
                byte numberOfTotalRecordsReceived = 0;
                ST_PAYMENT_APPLICATION_INFO[] stPaymentApplicationInfo = new ST_PAYMENT_APPLICATION_INFO[24];
                UInt16 currencyOfPayment = 0;

                UInt32 retcode = Json_GMPSmartDLL.FiscalPrinter_GetPaymentApplicationInfo(ref numberOfTotalRecords, ref numberOfTotalRecordsReceived, ref stPaymentApplicationInfo, 24);

                if (retcode != Defines.TRAN_RESULT_OK)
                    HandleErrorCode(retcode);
                else if (numberOfTotalRecordsReceived == 0)
                    MessageBox.Show("ÖKC üzerinde ödeme uygulanaması bulunamadı.", "Hata", MessageBoxButtons.OK);
                else
                {
                    ST_PAYMENT_REQUEST[] stPaymentRequest = new ST_PAYMENT_REQUEST[1];
                    for (int i = 0; i < stPaymentRequest.Length; i++)
                    {
                        stPaymentRequest[i] = new ST_PAYMENT_REQUEST();
                    }

                    frmIngenicoPaymentAppForm paf = new frmIngenicoPaymentAppForm(numberOfTotalRecordsReceived, stPaymentApplicationInfo);
                    DialogResult dr = paf.ShowDialog();
                    if (dr != System.Windows.Forms.DialogResult.OK)
                        return;

                    if (currencyOfPayment == (UInt16)ECurrency.CURRENCY_NONE)
                        currencyOfPayment = (UInt16)ECurrency.CURRENCY_TL;

                    frmIngenicoPaymentTypeForm ptf = new frmIngenicoPaymentTypeForm();
                    DialogResult ptfDr = ptf.ShowDialog();
                    if (ptfDr != System.Windows.Forms.DialogResult.OK)
                        return;

                    switch (frmIngenicoPaymentTypeForm.PaymentTypeIndex)
                    {
                        case 0:
                            stPaymentRequest[0].subtypeOfPayment = Defines.PAYMENT_SUBTYPE_PROCESS_ON_POS;
                            break;
                        case 1:
                            stPaymentRequest[0].subtypeOfPayment = Defines.PAYMENT_SUBTYPE_SALE;
                            break;
                        case 2:
                            stPaymentRequest[0].subtypeOfPayment = Defines.PAYMENT_SUBTYPE_INSTALMENT_SALE;
                            break;
                        case 3:
                            stPaymentRequest[0].subtypeOfPayment = Defines.PAYMENT_SUBTYPE_LOYALTY_PUAN;
                            break;
                        default:
                            return;
                    }

                    stPaymentRequest[0].typeOfPayment = (uint)EPaymentTypes.PAYMENT_BANK_CARD;
                    stPaymentRequest[0].payAmount = (dblAmount * 100).TOUINT32();
                    if (stPaymentRequest[0].subtypeOfPayment == Defines.PAYMENT_SUBTYPE_LOYALTY_PUAN)
                        stPaymentRequest[0].payAmountBonus = (dblAmount * 100).TOUINT32();
                    else
                        stPaymentRequest[0].payAmountBonus = 0;
                    stPaymentRequest[0].payAmountCurrencyCode = currencyOfPayment;
                    if (paf.pstPaymentApplicationInfoSelected == null)
                        stPaymentRequest[0].bankBkmId = 0;
                    else
                        stPaymentRequest[0].bankBkmId = paf.pstPaymentApplicationInfoSelected.u16BKMId;
                    stPaymentRequest[0].numberOfinstallments = strTaksitSayisi.TOUSHORT();

                    stPaymentRequest[0].transactionFlag = 0x00000000;
                    //if (m_chcManualPanEntryNotAllowed.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_MANUAL_PAN_ENTRY_NOT_ALLOWED;
                    //if (m_chcLoyaltyPointNotSupported.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_LOYALTY_POINT_NOT_SUPPORTED_FOR_TRANS;
                    //if (m_chcAllInputFromEcr.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_ALL_INPUT_FROM_EXTERNAL_SYSTEM;
                    //if (m_chcDoNotAskForMissingLoyaltyPoint.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_DO_NOT_ASK_FOR_MISSING_LOYALTY_POINT;
                    //if (m_chcAuthorisationForInvoicePayment.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_AUTHORISATION_FOR_INVOICE_PAYMENT;
                    //if (m_chcSaleWithoutCampaign.Checked)
                    //    stPaymentRequest[0].transactionFlag |= Defines.BANK_TRAN_FLAG_SALE_WITHOUT_CAMPAIGN;

                    stPaymentRequest[0].rawData = Encoding.Default.GetBytes("RawData from external application for the payment application");
                    stPaymentRequest[0].rawDataLen = (ushort)stPaymentRequest[0].rawData.Length;

                    GetPayment(stPaymentRequest, 1);
                }
            }
            else if (strOdemeTipi == "05" || strOdemeTipi == "06" || strOdemeTipi == "07")
            {
                byte NumberOfTotalRecord = 24;
                byte NumberOfTotalRecordReceived = 0;

                ST_PAYMENT_APPLICATION_INFO[] StPaymentApplicationInfo = new ST_PAYMENT_APPLICATION_INFO[24];
                for (int i = 0; i < StPaymentApplicationInfo.Length; i++)
                {
                    StPaymentApplicationInfo[i] = new ST_PAYMENT_APPLICATION_INFO();
                }

                uint retcode = Json_GMPSmartDLL.FiscalPrinter_GetVasApplicationInfo(ref NumberOfTotalRecord, ref NumberOfTotalRecordReceived, ref StPaymentApplicationInfo, (ushort)EVasType.TLV_OKC_ASSIST_VAS_TYPE_YEMEKCEKI);

                if (retcode != Defines.TRAN_RESULT_OK)
                    HandleErrorCode(retcode);
                else if (NumberOfTotalRecordReceived == 0)
                    MessageBox.Show("ÖKC Üzerinde Ödeme Uygulanaması Bulunamadı", "HATA", MessageBoxButtons.OK);
                else
                {
                    frmIngenicoPaymentAppForm paf = new frmIngenicoPaymentAppForm(NumberOfTotalRecordReceived, StPaymentApplicationInfo);
                    DialogResult dr = paf.ShowDialog();
                    if (dr != System.Windows.Forms.DialogResult.OK)
                        return;

                    UInt16 currencyOfPayment = 0;

                    if (currencyOfPayment == (UInt16)ECurrency.CURRENCY_NONE)
                        currencyOfPayment = (UInt16)ECurrency.CURRENCY_TL;

                    ST_PAYMENT_REQUEST[] stPaymentRequest = new ST_PAYMENT_REQUEST[1];
                    for (int i = 0; i < stPaymentRequest.Length; i++)
                    {
                        stPaymentRequest[i] = new ST_PAYMENT_REQUEST();
                    }

                    stPaymentRequest[0].typeOfPayment = (uint)EPaymentTypes.PAYMENT_YEMEKCEKI;
                    stPaymentRequest[0].subtypeOfPayment = 0;
                    stPaymentRequest[0].payAmount = (dblAmount * 100).TOUINT32();
                    stPaymentRequest[0].payAmountCurrencyCode = (ushort)ECurrency.CURRENCY_TL;
                    if (paf.pstPaymentApplicationInfoSelected.u16BKMId.Equals(null))
                        stPaymentRequest[0].bankBkmId = 0;
                    else
                        stPaymentRequest[0].bankBkmId = paf.pstPaymentApplicationInfoSelected.u16AppId;
                    stPaymentRequest[0].numberOfinstallments = 0;

                    GetPayment(stPaymentRequest, 1);
                }
            }
        }
    }
}
