using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;

namespace Winsell.YK.Ingenico
{
    public class ST_GMP_PAIR
    {
        public byte[] In_ProcOrderNum;
        public byte[] In_ProcDate;
        public byte[] In_ProcTime;
        public byte[] In_DeviceBrand;
        public byte[] In_DeviceModel;
        public byte[] In_DeviceSerialNum;
        public byte[] In_DeviceEcrRegisterNo;

        public ST_GMP_PAIR()
        {
            In_ProcOrderNum = new byte[3];
            In_ProcDate = new byte[3];
            In_ProcTime = new byte[3];
            In_DeviceBrand = new byte[16];
            In_DeviceModel = new byte[16];
            In_DeviceSerialNum = new byte[16];
            In_DeviceEcrRegisterNo = new byte[17];
        }
    };

    public class ST_GMP_PAIR_RESP
    {
        public byte[] Out_ProcOrderNum;
        public byte[] Out_ProcDate;
        public byte[] Out_ProcTime;
        public byte[] Out_DeviceBrand;
        public byte[] Out_DeviceModel;
        public byte[] Out_DeviceSerialNum;
        public byte[] Out_ErrorRespCode;
        public byte[] Out_StatusCode;
        public byte[] Out_VersionNumber;

        public ST_GMP_PAIR_RESP()
        {
            Out_ProcOrderNum = new byte[6];
            Out_ProcDate = new byte[6];
            Out_ProcTime = new byte[6];
            Out_DeviceBrand = new byte[16];
            Out_DeviceModel = new byte[16];
            Out_DeviceSerialNum = new byte[16];
            Out_ErrorRespCode = new byte[2];
            Out_StatusCode = new byte[2];
            Out_VersionNumber = new byte[8];
        }
    };

    public class ST_ECHO
    {
        public UInt32 retcode;
        public UInt32 status;
        public byte[] kvc;
        public byte ecrMode;
        public UInt16 mtuSize;
        public byte[] ecrVersion;
        public ST_DATE date;
        public ST_TIME time;
        public ST_CASHIER activeCashier;

        public ST_ECHO()
        {
            kvc = new byte[8];
            ecrMode = 0;
            ecrVersion = new byte[16];
            activeCashier = new ST_CASHIER();
            date = new ST_DATE();
            time = new ST_TIME();
        }
    };

    public class _ST_PAYMENT_REQUEST_ORGINAL_DATA
    {
        public UInt32 TransactionAmount;              /**< tag 21 OrgTransAmount[6] bcd */
        public UInt32 LoyaltyAmount;                  /**< tag 25 OrgLoyaltyAmount[6] bcd */
        public UInt16 NumberOfinstallments;           /**< tag 22 Number of installments, Zero if not used */
        public byte[] AuthorizationCode;			    /**< tag 45 ascii */
        public byte[] rrn; 						        /**< tag 46 ascii */
        public byte[] TransactionDate;                /**< tag 47 OrgTransDate[5] bcd YY- YYMMDDHHMM */
        public byte[] MerchantId;					    /**< tag 67 ascii */
        public byte TransactionType;                  /**< tag 70 byte */
        public byte[] referenceCodeOfTransaction;	    /**< tag 75 ascii */
    };

    public class ST_PAYMENT_REQUEST
    {
        public UInt32 typeOfPayment;
        public UInt32 subtypeOfPayment;
        public UInt32 payAmount;
        public UInt32 payAmountBonus;
        public UInt16 payAmountCurrencyCode;
        public UInt16 bankBkmId;
        public UInt16 numberOfinstallments;
        public byte[] terminalId;

        public _ST_PAYMENT_REQUEST_ORGINAL_DATA OrgTransData;

        public UInt32 batchNo;
        public UInt32 stanNo;
        public UInt16 rawDataLen;
        public byte[] rawData;
        public string paymentName;
        public string paymentInfo;

        public ST_CARD_INFO stCard;
        public UInt32 transactionFlag;				/**< External Device Transaction Flags - 1 */
        public UInt32 flags;							/**< Payment request process flags */

        public ST_PAYMENT_REQUEST()
        {
            terminalId = new byte[8];
            rawDataLen = 0;
            rawData = new byte[512];
            stCard = new ST_CARD_INFO();
            OrgTransData = new _ST_PAYMENT_REQUEST_ORGINAL_DATA();
        }
    };

    public struct ST_EcrSettings
    {
        public byte InvoiceSettings;
        public byte Z_Settings;
        public UInt16 Z_Time_In_Minute;
        public byte Copy_Button_Secured;
        public UInt16 Backlight_Timeout;
        public UInt16 Backlight_Level;
        public UInt16 Keylock_Timeout;
    };


    public class ST_EXCHANGE
    {

        public UInt16 code;
        public string prefix;
        public string sign;
        public byte locationOfSign;
        public byte tousandSeperator;
        public byte centSeperator;
        public byte numberOfCentDigit;
        public UInt64 rate;

        public ST_EXCHANGE()
        {
            prefix = "";
            sign = "";
        }
    };

    public class ST_PaymentErrMessage
    {
        public string ErrorCode;        // bank error code
        public string ErrorMsg;
        public string AppErrorCode;     // payment application specific error code
        public string AppErrorMsg;

        public ST_PaymentErrMessage()
        {
            ErrorCode = "";             // bank error code
            ErrorMsg = "";
            AppErrorCode = "";          // payment application specific error code
            AppErrorMsg = "";
        }
    };

    public class ST_BankSubPaymentInfo
    {
        public UInt16 type; 				// EPaymentSubType
        public UInt32 amount;
        public string name;

        public ST_BankSubPaymentInfo()
        {
            name = "";
        }
    };

    public class ST_BANK_PAYMENT_INFO
    {
        public UInt32 batchNo;
        public UInt32 stan;
        public UInt32 balance;
        public UInt16 bankBkmId;
        public byte numberOfdiscount;
        public byte numberOfbonus;
        public string authorizeCode;
        public byte[] transFlag;
        public string terminalId;
        public string rrn;
        public string merchantId;
        public string bankName;
        public byte numberOfInstallments;
        public byte numberOfsubPayment;
        public byte numberOferrorMessage;
        public ST_BankSubPaymentInfo[] stBankSubPaymentInfo;
        public ST_CARD_INFO stCard;
        public ST_PaymentErrMessage stPaymentErrMessage;

        public ST_BANK_PAYMENT_INFO()
        {
            authorizeCode = "";
            transFlag = new byte[2];
            terminalId = "";
            merchantId = "";
            bankName = "";

            stBankSubPaymentInfo = new ST_BankSubPaymentInfo[12];
            stCard = new ST_CARD_INFO();
            stPaymentErrMessage = new ST_PaymentErrMessage();
        }
    };

    public class ST_PAYMENT
    {
        public byte flags;
        public UInt32 dateOfPayment;
        public UInt32 typeOfPayment;				// EPaymentTypes
        public byte subtypeOfPayment;			    // EPaymentSubtypes
        public UInt32 orgAmount;					// Exp; Currency Amount
        public UInt16 orgAmountCurrencyCode;		// as defined in currecyTable from GIB
        public UInt32 payAmount;					// always TL with precision 2
        public UInt16 payAmountCurrencyCode;		// always TL
        public UInt32 cashBackAmountInTL;			// Para üstü, her zaman TL with precision 2
        public UInt32 cashBackAmountInDoviz;		// Para Üstü, döviz satış ise döviz karşılığı
        public ST_BANK_PAYMENT_INFO stBankPayment;	// Keeps all payment info related with bank


        public ST_PAYMENT()
        {
            stBankPayment = new ST_BANK_PAYMENT_INFO();
        }
    };

    public class ST_printerDataForOneLine
    {
        public UInt32 Flag;
        public byte lineLen;
        public string line;

        public ST_printerDataForOneLine()
        {
            line = "";
        }
    };

    public class ST_SALEINFO
    {
        public byte ItemType;
        public UInt64 ItemPrice;
        public UInt64 IncAmount;
        public UInt64 DecAmount;
        public UInt32 OrigialItemAmount; // Eğer kısım bilgisi TL olarak tanımlanmamışsa, kısım tutarı buraya kaydedilir ve diğer amout yeniden kur bilgisi ile hesaplanılarak ezilir
        public UInt16 OriginalItemAmountCurrency;
        public UInt16 ItemVatRate;
        public UInt16 ItemCurrencyType;
        public byte ItemVatIndex;
        public byte ItemCountPrecision;
        public int ItemCount;
        public byte ItemUnitType;
        public byte DeptIndex;
        public UInt32 Flag;
        public string Name;
        public string Barcode;

        public ST_SALEINFO()
        {
            Name = "";
            Barcode = "";
        }
    };

    public class ST_VATDetail
    {
        public int u32VAT;						/**< Total Tax in TL with precition 2 */
        public int u32Amount;					/**< Total Amount in TL with precition 2 */
        public UInt16 u16VATPercentage;			/**< Tax rate, it is 1800 for %18 */

        public ST_VATDetail()
        {
            u32VAT = 0;
            u32Amount = 0;
            u16VATPercentage = 0;
        }
    };

    public class Z_department
    {
        public long totalAmount;
        public long totalQuantity;
        public byte[] name;
        public Z_department()
        {
            name = new byte[25];
        }
    };

    public class Z_exchange
    {
        public long totalAmount;
        public long totalAmountInTL;
        public byte[] name;
        public Z_exchange()
        {
            name = new byte[13];
        }
    }

    public class Z_tax
    {
        public long totalAmount;
        public long totalTax;
        public UInt16 taxRate;
    }

    public class Z_cashier
    {
        public long totalAmount;
        public byte[] name;
        public Z_cashier()
        {
            name = new byte[12];
        }
    }

    public struct Z_countOf									    /**< Counters based data*/
    {
        public int increaments;									/**< int 999999  , Total number of increases */
        public int decreases; 									/**< int 999999  , Total number of decreases */
        public int corrections; 								/**< int 999999  , Total number of corrections */
        public int fiscalReceipts; 								/**< int 999999  , Total number of Fiscal Tickets */
        public int nonfiscalReceipts; 							/**< int 999999  , Total number of non Fiscal Tickets */
        public int customerReceipts; 							/**< int 999999	 , Total number of Tickets */
        public int voidReceipts; 								/**< int 999999  , Total number of Void Tickets */
        public int invoiceSaleReceipts; 						/**< int 999999  , Total number of Invoice Tickets */
        public int yemekcekiReceipts; 							/**< int 999999  , Total number of YemekCeki Tickets */
        public int carParkingEntryReceipts;						/**< int 999999  , Total number of CarParking Tickets */
        public int fiscalReportReceipts;						/**< int 999999  , Total number of FiscalReport Ticket counts */
        public int tasnifDisiReceipts; 							/**< int 999999  , Total number of info Ticket counts */
        public int invoiceReceipts; 							/**< int 999999  , Total number of Invoice Ticket counts */
        public int matrahsizReceipts; 							/**< int 999999  , Total number of Matrahsiz Ticket counts */
        public int serviceModeEntry; 							/**< int 999999  , Total number of entries into Service Menu of ECR */
    };

    public struct Z_invoice
    {
        public long TotalAmount;
        public long classicTotalAmount;
        public long e_invoiceTotalAmount;
        public long e_archiveTotalAmount;
        public long creditTotalAmount;
        public long cashTotalAmount;
    };

    public struct Z_other
    {
        public long mobil;
        public long hediyeCeki;
        public long ikram;
        public long odemesiz;
        public long kapora;
        public long puan;
        public long giderPusulasi;
        public long cek;
        public long bankaTransfer;
    };

    public class Z_payment
    {
        public long cashTotal;
        public long creditTotal;
        public long otherTotal;

        public Z_other other;
        public Z_payment()
        {
            other = new Z_other();
        }
    }

    public struct Z_sale
    {
        public UInt16 KoltukSayisi;
        public UInt16 reserved;
        public long totalAmount;
        public long totalTax;
    }
    public struct Z_refund
    {
        public UInt16 KoltukSayisi;
        public UInt16 reserved;
        public long totalAmount;
        public long totalTax;
    }

    public struct Z_invoiceSale
    {
        public UInt16 KoltukSayisi;
        public UInt16 reserved;
        public long totalAmount;
        public long totalTax;
    }

    public class Z_cinema
    {
        public Z_sale sale;
        public Z_refund refund;
        public Z_invoiceSale invoiceSale;
    }

    public class Z_sectorData
    {
        public Z_cinema[] cinema;
        public Z_sectorData()
        {
            cinema = new Z_cinema[Defines.MAX_CINEMA_DEPARTMENT_COUNT];
        }
    }


    public class ST_Z_REPORT
    {
        public int StructSize;
        public byte[] Date;
        public byte[] Time;
        public int FNo;
        public int ZNo;
        public int EJNo;
        public Z_countOf countOf;
        public long IncTotAmount;
        public long DecTotAmount;
        public long SaleVoidTotAmount;
        public long CorrectionTotAmount;
        public long InvoiceSaleTotAmount;
        public long FoodRcptTotAmount;
        public long DailyTotAmount;
        public long DailyTotTax;
        public long CumulativeTotAmount;
        public long CumulativeTotTax;
        public long AvansTotalAmount;
        public long OdemeTotalAmount;
        public long TaxRefundTotalAmount;
        public long MatrahsizTotalAmount;
        public Z_department[] department;
        public Z_exchange[] exchange;
        public Z_tax[] tax;
        public Z_cashier[] cashier;
        public Z_invoice invoice;
        public Z_payment payment;
        public Z_sectorData sectorData;

        public ST_Z_REPORT()
        {
            Date = new byte[3];
            Time = new byte[2];
            countOf = new Z_countOf();
            department = new Z_department[Defines.MAX_DEPARTMENT_COUNT];
            exchange = new Z_exchange[Defines.MAX_EXCHANGE_COUNT];
            tax = new Z_tax[Defines.MAX_TAXRATE_COUNT];
            cashier = new Z_cashier[Defines.MAX_CASHIER_COUNT];
            invoice = new Z_invoice();
            payment = new Z_payment();
            sectorData = new Z_sectorData();
        }
    }

    public class ST_TICKET
    {
        public UInt32 TransactionFlags;
        public UInt32 OptionFlags;
        public UInt16 ZNo;
        public UInt16 FNo;
        public UInt16 EJNo;
        public UInt32 TotalReceiptAmount;
        public UInt32 TotalReceiptTax;
        public UInt32 TotalReceiptDiscount;
        public UInt32 TotalReceiptIncrement;
        public UInt32 CashBackAmount;
        public UInt32 TotalReceiptItemCancel;
        public UInt32 TotalReceiptPayment;
        public UInt32 TotalReceiptReversedPayment;
        public UInt32 KasaAvansAmount;
        public UInt32 KasaPaymentAmount;
        public UInt32 invoiceAmount;
        public UInt32 invoiceAmountCurrency;
        public UInt32 KatkiPayiAmount;
        public UInt32 TaxFreeRefund;
        public UInt32 TaxFreeCalculated;
        public byte[] bcdTicketDate;
        public byte[] bcdTicketTime;
        public byte ticketType;
        public UInt16 totalNumberOfItems;
        public UInt16 numberOfItemsInThis;
        public UInt16 totalNumberOfPayments;
        public UInt16 numberOfPaymentsInThis;
        public string TckNo;
        public string invoiceNo;
        public UInt32 invoiceDate;
        public byte invoiceType;
        public int totalNumberOfPrinterLines;
        public int numberOfPrinterLinesInThis;
        public byte[] uniqueId;
        public ST_SALEINFO[] SaleInfo;
        public ST_PAYMENT[] stPayment;
        public ST_VATDetail[] stTaxDetails;
        public ST_printerDataForOneLine[] stPrinterCopy;
        public byte[] UserData;

        public ST_TICKET()
        {
            TckNo = "";
            invoiceNo = "";
            uniqueId = new byte[24];
            SaleInfo = new ST_SALEINFO[512];
            stPayment = new ST_PAYMENT[24];
            stTaxDetails = new ST_VATDetail[8];
            stPrinterCopy = new ST_printerDataForOneLine[1024];
        }
    };

    public class ST_ITEM
    {
        public byte type;
        public byte subType;
        public byte deptIndex;
        public byte unitType;
        public UInt32 amount;
        public UInt16 currency;
        public UInt32 count;
        public UInt32 flag;
        public byte countPrecition;
        public byte pluPriceIndex;
        public string name;
        public string barcode;
        public string firm;
        public string invoiceNo;
        public string subscriberId;
        public string tckno;
        public UInt32 Reserved;
        public byte[] Date;
        //public UInt32 date;
        public promotion promotion;

        public ST_ITEM()
        {
            name = "";
            barcode = "";
            firm = "";
            invoiceNo = "";
            subscriberId = "";
            tckno = "";
            promotion = new promotion();
            Date = new byte[3];
        }

    };

    public class promotion
    {
        public byte type;
        public int amount;
        public string ticketMsg;
        public promotion()
        {
            ticketMsg = "";
        }
    };

    public class ST_TICKET_HEADER
    {
        public string szMerchName1;
        public string szMerchName2;
        public string szMerchAddr1;
        public string szMerchAddr2;
        public string szMerchAddr3;
        public string VATOffice;
        public string VATNumber;
        public string MersisNo;
        public string TicariSicilNo;
        public string WebAddress;
        public int initDateTime;
        public short index;
        public short EJNo;

        public ST_TICKET_HEADER()
        {
            szMerchName1 = "";
            szMerchName2 = "";
            szMerchAddr1 = "";
            szMerchAddr2 = "";
            szMerchAddr3 = "";
            VATOffice = "";
            VATNumber = "";
            MersisNo = "";
            TicariSicilNo = "";
            WebAddress = "";
        }
    };

    /** ETransactionFlags */
    public enum ETransactionFlags
    {
        FLG_XTRANS_GMP3 = (1 << 1),
        FLG_XTRANS_FROM_FILE = (1 << 2),
        FLG_XTRANS_TAXFREE_PARAMETERS_SET = (1 << 8),
        FLG_XTRANS_TICKETTING_EXISTS = (1 << 9),
        FLG_XTRANS_FULL_RCPT_CANCEL = (1 << 12),
        FLG_XTRANS_NONEY_COLLECTION_EXISTS = (1 << 13),
        FLG_XTRANS_TAXLESS_ITEM_EXISTS = (1 << 14),
        FLG_XTRANS_INVOICE_PARAMETERS_SET = (1 << 15),
        FLG_XTRANS_TICKET_HEADER_PRINTED = (1 << 17),
        FLG_XTRANS_TICKET_TOTALS_AND_PAYMENTS_PRINTED = (1 << 18),
        FLG_XTRANS_TICKET_FOOTER_BEFORE_MF_PRINTED = (1 << 19),
        FLG_XTRANS_TICKET_FOOTER_MF_PRINTED = (1 << 20),
    };

    public enum EVasType
    {
        TLV_OKC_ASSIST_VAS_TYPE_ADISYON = 0x0001,   // Adisyon
        TLV_OKC_ASSIST_VAS_TYPE_IN_FLIGHT,              // INFLIGHT -->
        TLV_OKC_ASSIST_VAS_TYPE_INGENICO,               // ICIRO, FIND MY KASA
        TLV_OKC_ASSIST_VAS_TYPE_OTHER,                  // OTHER
        TLV_OKC_ASSIST_VAS_TYPE_AKTIFNOKTA,             // AKTIF NOKTA
        TLV_OKC_ASSIST_VAS_TYPE_MOBIL_ODEME,            // MOBIL ODEME
        TLV_OKC_ASSIST_VAS_TYPE_OTOPARK,             	// OTOPARK
        TLV_OKC_ASSIST_VAS_TYPE_YEMEKCEKI,             	// YEMEK KARTI
        TLV_OKC_ASSIST_VAS_TYPE_LOYALTY,             	// SADAKAT UYGULAMASI
        TLV_OKC_ASSIST_VAS_TYPE_ALL = 0x0100    // ALL
    };

    /** INFO Function type */
    public enum EInfo
    {
        TLV_FUNC_INFO_DEVICE = 0xEF10,     			   // 00 Info device
        TLV_FUNC_INFO_FISCAL,                          // 01 Info fiscal
        TLV_FUNC_INFO_FRAM,                            // 02 Info fram
        TLV_FUNC_INFO_DB,                              // 03 Info DB
        TLV_FUNC_INFO_DAILY,                           // 04 Info daily
        TLV_FUNC_INFO_EVENT,                           // 05 Info event
        TLV_FUNC_INFO_EKU,                             // 06 Info eku
    };

    public enum EItemOptions
    {
        ITEM_OPTION_TAX_EXCEPTION_TAXLESS = (1 << 12),
        ITEM_TAX_EXCEPTION_VAT_INCLUDED_TO_PRICE = (1 << 12),
        ITEM_TAX_EXCEPTION_VAT_NOT_INCLUDED_TO_PRICE = (1 << 15),
    };

    public enum EItemPromotionType
    {
        ITEM_PROMOTION_DISCOUNT = 1,
        ITEM_PROMOTION_INCREASE = 2,
    };

    public enum ECurrency
    {
        CURRENCY_NONE = 0,
        CURRENCY_TL = 949,
        CURRENCY_DOLAR = 840,
        CURRENCY_EU = 978,
        CURRENCY_GPR = 826,
    };

    public enum EPaymentTypes
    {
        PAYMENT_ALL = 0x000FFFFF,	            //  NAKIT  KREDI  OTHER  YCEKI  DOVIZ   MATRAH  MENU(ODEME TIPLERI)
        PAYMENT_CASH_TL = 0x00000001,	        // 	++++   xxxx   xxxx   xxxx   xxxx    ++++	xxxx
        PAYMENT_CASH_CURRENCY = 0x00000002,	    // 	xxxx   xxxx   xxxx   xxxx   ++++    ++++    xxxx
        PAYMENT_BANK_CARD = 0x00000004,	        //	xxxx   ++++   xxxx   xxxx   xxxx    ++++    xxxx
        PAYMENT_YEMEKCEKI = 0x00000008,	        //	xxxx   xxxx   xxxx   ++++   xxxx    xxxx    xxxx(Uygulama varsa)
        PAYMENT_MOBILE = 0x00000010,	        // 	xxxx   ++++   xxxx   xxxx   xxxx    ++++    xxxx(Uygulama varsa)
        PAYMENT_HEDIYE_CEKI = 0x00000020,       // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_IKRAM = 0x00000040,             // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_ODEMESIZ = 0x00000080,          // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_KAPORA = 0x00000100,            // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_PUAN = 0x00000200,              // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_GIDER_PUSULASI = 0x00000400,    // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_BANKA_TRANSFERI = 0x00000800,   // xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_CEK = 0x00001000,               // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++
        PAYMENT_ACIK_HESAP = 0x00002000,        // 	xxxx   xxxx   ++++   xxxx   xxxx    ++++    ++++

        //REVERSE_PAYMENT_ALL = 0xFFF00000,     //açılacak
        REVERSE_PAYMENT_CASH = 0x00100000,
        REVERSE_PAYMENT_BANK_CARD_VOID = 0x00200000,
        REVERSE_PAYMENT_BANK_CARD_REFUND = 0x00400000,
        REVERSE_PAYMENT_YEMEKCEKI = 0x00800000,
        REVERSE_PAYMENT_MOBILE = 0x01000000,
        REVERSE_PAYMENT_HEDIYE_CEKI = 0x02000000,
    };


    /** Sub types of the payment,refund for PAYMENT_TYPE_BANKING_CARD */
    public enum EPaymentSubtypes
    {
        PAYMENT_SUBTYPE_PROCESS_ON_POS = 0x00000000,
        PAYMENT_SUBTYPE_SALE = 0x00000001,
        PAYMENT_SUBTYPE_INSTALMENT_SALE = 0x00000002,
        PAYMENT_SUBTYPE_LOYALTY_PUAN = 0x00000003,
        PAYMENT_SUBTYPE_ADVANCE_REFUND = 0x00000004,
        PAYMENT_SUBTYPE_INSTALLMENT_REFUND = 0x00000005,
        PAYMENT_SUBTYPE_REFERENCED_REFUND = 0x00000006,
        PAYMENT_SUBTYPE_REFERENCED_REFUND_WITH_CARD = 0x00000007,
        PAYMENT_SUBTYPE_REFERENCED_REFUND_WITHOUT_CARD = 0x00000008,
    };



    /** Bank Aplication transaction flags*/
    public enum EBANK_APLICATION_TRANSACTION_FLAGS_t
    {

    };

    /** Sub types of the discount for Bank, VAS aplication or ticket discount process.*/
    public enum DiscountSubtypes
    {
        DICOUNT_SUBTYPE_BANKING_INDIRIM = 0x0001,
        DICOUNT_SUBTYPE_RECEIPT_INDIRIM = 0x0002,
        DICOUNT_SUBTYPE_BANKING_INDIRIM_MATRAHSIZ = 0x0003,
        DICOUNT_SUBTYPE_VAS_INDIRIM = 0x0004,
    };

    public enum EItemUnitTypes
    {
        ITEM_NONE,
        ITEM_NUMBER = 1,
        ITEM_KILOGRAM = 2,
        ITEM_GRAM = 3,
        ITEM_LITRE = 4,

        // Adetsel Birimler
        ITEM_DUZINE = 11,
        ITEM_DEMET, // 12
        ITEM_KASA, // 13
        ITEM_BAG, // 14

        // Aðýrlýk Birimler
        ITEM_MILIGRAM = 31,
        ITEM_TON, // 32
        ITEM_ONS, // 33
        ITEM_DESIGRAM, // 34
        ITEM_SANTIGRAM, // 35
        ITEM_POUND, // 36
        ITEM_KENTAL, // 37

        // Uzunluk Birimler
        ITEM_METRE = 51,
        ITEM_SANTIMETRE, // 52
        ITEM_MILIMETRE, // 53
        ITEM_DEKAMETRE, // 54
        ITEM_HEKTAMETRE, // 55
        ITEM_KILOMETRE, // 56
        ITEM_DESIMETRE, // 57
        ITEM_MIKRON, // 58
        ITEM_INC, // 59
        ITEM_FOOT, // 60
        ITEM_YARD, // 61
        ITEM_MIL, // 62

        // Hacim Birimler
        ITEM_METREKUP = 71,
        ITEM_DESIMETREKUP, // 72
        ITEM_SANTIMETREKUP, // 73
        ITEM_MILIMETREKUP, // 74
        ITEM_DEKALITRE, // 75
        ITEM_HEKTOLITRE, // 76
        ITEM_KILOLITRE, // 77
        ITEM_DESILITRE, // 78
        ITEM_SANTILITRE, // 79
        ITEM_MILILITRE, // 80
        ITEM_INCKUP, // 81
        ITEM_GALLON, // 82
        ITEM_BUSHEL, // 83

        // Alan Birimler
        ITEM_METREKARE = 91,
        ITEM_DEKAMETREKARE, // 92
        ITEM_AR, // 93
        ITEM_KILOMETREKARE, // 94
        ITEM_DESIMETREKARE, // 95
        ITEM_SANTIMETREKARE, // 96
        ITEM_MILIMETREKARE, // 97
        ITEM_DONUM, // 98
        ITEM_HEKTAR, // 99
        ITEM_INCKARE, // 100
    };

    public enum ETransactionFiscalType
    {
        TRANSACTION_FISCAL_TYPE_SALE = 0,
        TRANSACTION_FISCAL_TYPE_REFUND,
        TRANSACTION_FISCAL_TYPE_VOID,
        TRANSACTION_FISCAL_TYPE_NON_FISCAL_SALE,
        TRANSACTION_FISCAL_TYPE_INFO,
    };

    // FiscalPrinter_Function için Flaglar
    enum FunctionFlags
    {
        GMP_EXT_DEVICE_FUNC_BIT_PARAM_YUKLE = 0x00000001,
        GMP_EXT_DEVICE_FUNC_BIT_Z_RAPOR, // 0x00000002                          
        GMP_EXT_DEVICE_FUNC_BIT_X_RAPOR, // 0x00000003
        GMP_EXT_DEVICE_FUNC_BIT_MALI_RAPOR, // 0x00000004
        GMP_EXT_DEVICE_FUNC_BIT_EKU_RAPOR, // 0x00000005
        GMP_EXT_DEVICE_FUNC_BIT_MALI_KUMULATIF, // 0x00000006
        GMP_EXT_DEVICE_FUNC_BIT_Z_RAPOR_GONDER, // 0x00000007
        GMP_EXT_DEVICE_FUNC_BIT_KASIYER_SEC, // 0x00000008
        GMP_EXT_DEVICE_FUNC_BIT_KASIYER_LOGOUT, // 0x00000009
        GMP_EXT_DEVICE_FUNC_BIT_AVANS, // 0x0000000A
        GMP_EXT_DEVICE_FUNC_BIT_ODEME, // 0x0000000B
        GMP_EXT_DEVICE_FUNC_BIT_CEKMECE_ACMA, // 0x0000000C
        GMP_EXT_DEVICE_FUNC_READ_CARD, // 0x0000000D
        GMP_EXT_DEVICE_FUNC_GET_CARD_PUAN, // 0x0000000E
        GMP_EXT_DEVICE_FUNC_BIT_BANKA_GUN_SONU, // 0x0000000F
        GMP_EXT_DEVICE_FUNC_BIT_BANKA_PARAM_YUKLE, // 0x00000010
        GMP_EXT_DEVICE_FUNC_BANKA_IADE, // 0x00000011
        GMP_EXT_DEVICE_FUNC_GET_UNIQUE_ID_LIST, // 0x00000012
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_SON_FIS_KOPYA, // 0x00000013
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_SON_KOPYA, // 0x00000014
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_IKI_Z_ARASI, // 0x00000015
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_IKI_TARIH_ARASI, // 0x00000016
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_FISTEN_FISE, // 0x00000017
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_Z_KOPYA, // 0x00000018
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_DETAIL, // 0x00000019
        GMP_EXT_DEVICE_FUNC_EKU_RAPOR_SUMMARY, // 0x0000001A
        GMP_EXT_DEVICE_FUNC_CHANGE_RECEIPT_HEADER, // 0x0000001B
        GMP_EXT_DEVICE_FUNC_BANKA_IPTAL, // 0x0000001C
    };

    public enum TTicketType
    {
        TTasnifDisi = 0,
        TProcessSale = 1,       //Fiscal Ticket           
        TZReport = 2,
        TXReport = 3,
        TEJReport = 4,
        TFiscal2Z = 5,
        TFiscal2T = 6,
        TFiscalCumm = 7,
        TAvans = 8,             //Non_Fiscal Ticket
        TPayment = 9,           //Non_Fiscal Ticket
        TZDonemReport = 10,
        TXDonemReport = 11,
        TXPluSale = 12,
        TInvoice = 13,          //Non_Fiscal Ticket
        TVoidSale = 14,         //Non_Fiscal Ticket
        TRefund = 15,           //Non_Fiscal Ticket
        TYemekceki = 16,        //Non_Fiscal Ticket
        TOtopark = 17,          //Non_Fiscal Ticket 
        TZReportForce = 18,
        TInfo = 19,             //Non_Fiscal Ticket
        TTaxFree = 20,          //Fiscal Ticket
        TDailyMemory = 21,
        TKasaAvans = 22,        //Non_Fiscal Ticket
        TUniqueId = 127,
        TLAST              // Bu satir hep sonda kalmali
    };

    public enum EKU_STATUS_t
    {
        EKU_STATUS_VIRGIN,
        EKU_STATUS_ACTIVE,
        EKU_STATUS_CLOSED,
        EKU_STATUS_UNDEFINED
    };

    public enum EInvoiceFlags
    {
        INVOICE_FLAG_IRSALIYE = 0x00000001,
    };


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi), Serializable]
    public struct ST_TAX_RATE
    {
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 taxRate;
    };


    public class ST_DEPARTMENT
    {
        public string szDeptName;
        public byte u8TaxIndex;
        public ECurrency iCurrencyType;
        public EItemUnitTypes iUnitType;
        public UInt64 u64Limit;
        public UInt64 u64Price;

        public ST_DEPARTMENT()
        {
            szDeptName = "";
        }
    };

    public struct ST_DATE
    {
        public byte day;		// 1-31
        public byte month;		// 1-12
        public UInt16 year;		// 0-99
    };

    public struct ST_TIME
    {
        public byte hour;		// 0-23
        public byte minute;		// 0-59
        public byte second;		// 0-59
    };

    public struct ST_FUNCTION_PARAMETERS
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 EKUNo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] supervisor;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
        public byte[] cashier;
        //start
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ZNo_Start;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 FNo_Start;
        public ST_DATE date_Start;
        public ST_TIME time_Start;
        //finish
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ZNo_Finish;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 FNo_Finish;
        public ST_DATE date_Finish;
        public ST_TIME time_Finish;
    };

    public class ST_CASHIER
    {
        public UInt16 index;
        public UInt32 flags;
        public string name;

        public ST_CASHIER()
        {
            name = "";
        }
    };

    public class ST_INI_PARAM
    {
        public byte IsCheckStructVersion;
        public byte RetryCounter;
        public byte IpRetryCount;
        public UInt32 AckTimeOut;
        public UInt32 CommTimeOut;
        public UInt32 InterCharacterTimeOut;

        public string PortName;
        public UInt32 BaudRate;
        public byte ByteSize;
        public byte fParity;
        public byte Parity;
        public byte StopBit;
        public byte IsTcpConnection;
        public string IP;
        public UInt32 Port;

        public string LogPath;
        public byte LogPrintToFileOpen;
        public byte LogPrintToConsoleOpen;
        public byte LogGeneralOpen;
        public byte LogFunctionOpen;
        public byte LogSecurityOpen;
        public byte LogPrivateSecurityOpen;
        public byte LogCommOpen;
        public byte LogExtDevOpen;
        public byte LogJsonOpen;
        public byte LogJsonDataOpen;
        public byte LogGmp3TagsOpen;
        public byte LogPrintSerialNumOpen;
        public byte LogPrintDateOpen;
        public byte LogPrintTimeOpen;
        public byte LogPrintTypeOpen;
        public byte LogPrintVersionOpen;
        public byte LogPrintSourceFileOpen;
        public byte LogPrintSourceLineOpen;

        public ST_INI_PARAM()
        {
            PortName = "";
            IP = "";
            LogPath = "";
        }
    };

    public class ST_CARD_INFO
    {
        public byte inputType;
        public string pan;
        public string holderName;
        public byte[] type;
        public string expireDate;

        public ST_CARD_INFO()
        {
            inputType = new byte();
            pan = "";
            holderName = "";
            type = new byte[3];
            expireDate = "";
        }
    };


    public class ST_USER_MESSAGE
    {
        public UInt32 flag;
        public UInt16 len;
        public string message;

        public ST_USER_MESSAGE()
        {
            message = "";
        }
    };

    //public class ST_USER_MESSAGE_EX
    //{
    //    public UInt32 flag;
    //    public int len;
    //    public string message;

    //    public ST_USER_MESSAGE_EX()
    //    {
    //        message = "";
    //    }
    //};

    public class ST_PAYMENT_APPLICATION_INFO
    {
        public byte[] name;
        public byte index;
        public UInt16 u16BKMId;
        public UInt16 Status;
        public UInt16 Priority;
        public UInt16 u16AppId;

        public ST_PAYMENT_APPLICATION_INFO()
        {
            name = new byte[20];
        }
    };

    public struct ST_PLU_RECORD
    {
        public byte deptIndex;
        public byte unitType;
        public UInt32 flag;
        public UInt32 lastModified;
        public UInt16[] currency;
        public UInt32[] amount;
        public string barcode;
        public string name;
        public UInt32 groupParentId;
    };


    public struct ST_PLU_GROUP_RECORD
    {
        public UInt32 groupId;
        public UInt32 groupFlag;
        public string name;
    };

    public class ST_INVIOCE_INFO
    {
        public byte source;
        public byte[] no;
        public byte[] date;
        public byte[] tck_no;
        public byte[] vk_no;
        public UInt64 amount;
        public UInt16 currency;
        public UInt32 flag;

        public ST_INVIOCE_INFO()
        {
            no = new byte[25];
            date = new byte[3];
            tck_no = new byte[12];
            vk_no = new byte[12];
        }
    };

    public struct ST_FILE
    {
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        //public byte[] fileName;
        public string fileName;
        public int fileSize;
    };

    public class ST_UNIQUE_ID
    {
        public byte[] uniqueId;
        public UInt16 reserved1;
        public UInt32 reserved2;

        public ST_UNIQUE_ID()
        {
            uniqueId = new byte[24];
        }
    };

    // EKU record information
    public struct EKU_RECORD_t
    {
        public UInt32 DateTime;
        public UInt32 Amount;
        public UInt32 Vat;
        public UInt16 FNo;
        public UInt16 ZNo;
        public UInt16 Type;                                                              // 2 bytes for alignment, 1 byte written to FLASH
        public UInt16 Status;
    };

    // Eku info
    public struct EKU_INFO_t
    {
        public EKU_RECORD_t LastRecord;
        public UInt32 MapFreeArea;				   	// TLV_PARAM_INFO_EKU_MAP_FREE
        public UInt32 MapUsedArea;				   	// TLV_PARAM_INFO_EKU_MAP_USED
        public UInt32 DataFreeArea;				    // TLV_PARAM_INFO_EKU_DATA_FREE
        public UInt32 DataUsedArea;				    // TLV_PARAM_INFO_EKU_DATA_USED
        public UInt16 HeaderUsed;				    // TLV_PARAM_INFO_EKU_HEADER_USED
        public UInt16 HeaderTotal;				    // TLV_PARAM_INFO_EKU_HEADER_TOTAL
        public EKU_STATUS_t Status;                 // TLV_FUNC_INFO_EKU
        public UInt16 CpuCRC;
    };

    public struct FISCAL_INTEGRITY_t
    {
        public byte Fiscal;
        public byte Event;
        public byte Daily;
        public byte RFU;
    };


    public struct MEMORY_INFO_t
    {
        public byte[] ID;
        public UInt16 Size;
    };

    //public struct DEVICE_INFO_t
    //{
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] SoftVersion;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] HardVersion;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] CompileDate;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] Description;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] HardwareReference;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    //    public byte[] HardwareSerial;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    //    public byte[] CpuID;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    //    public byte[] Hash;
    //    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string BootVersion;
    //    public FISCAL_INTEGRITY_t Integrity;
    //    public MEMORY_INFO_t Flash1;
    //    public MEMORY_INFO_t Flash2;
    //    public MEMORY_INFO_t Fram;
    //    [MarshalAs(UnmanagedType.U2)]
    //    public UInt16 CpuCRC;
    //    [MarshalAs(UnmanagedType.U1)]
    //    public byte Authentication;
    //};

    public class DEVICE_INFO_t
    {
        public byte[] SoftVersion;
        public byte[] HardVersion;
        public byte[] CompileDate;
        public byte[] Description;
        public byte[] HardwareReference;
        public byte[] HardwareSerial;
        public byte[] CpuID;
        public byte[] Hash;
        public string BootVersion;
        public FISCAL_INTEGRITY_t Integrity;
        public MEMORY_INFO_t Flash1;
        public MEMORY_INFO_t Flash2;
        public MEMORY_INFO_t Fram;
        public UInt16 CpuCRC;
        public byte Authentication;

        public DEVICE_INFO_t()
        {
            SoftVersion = new byte[16];
            HardVersion = new byte[16];
            CompileDate = new byte[16];
            Description = new byte[16];
            HardwareReference = new byte[16];
            HardwareSerial = new byte[16];
            CpuID = new byte[12];
            Hash = new byte[32];
            BootVersion = "";
        }
    };

    public class ST_EKU_MODULE_INFO
    {
        public DEVICE_INFO_t Device;
        public EKU_INFO_t Eku;
    };

    // Init close structure
    public struct EKU_INI_CLS_t
    {
        public UInt32 DateTime;
        public UInt16 ZNo;
        public UInt16 FNo;
    };

    // Eku header
    public struct ST_EKU_HEADER
    {
        public byte[] SicilNo;
        public byte[] TerminalSerialNo;
        public byte[] TerminalProductCode;
        public byte[] SoftwareVersion;
        public byte[] MerchantName;
        public byte[] MerchantAddress;
        public byte[] VATOffice;
        public byte[] VATNumber;
        public EKU_INI_CLS_t Init;
        public EKU_INI_CLS_t Close;
        public UInt16 Active;
        public UInt16 EkuCount;
        public UInt16 HeaderIndex;
        public UInt16 HeaderTotal;
        public byte[] MersisNo;
        public byte[] TicariSicilNo;
        public byte[] WebAddress;
        public byte[] ApplicationUse;
    };


    public class ST_EKU_APPINF
    {
        public byte[] Buffer;
        public UInt32 Amount;
        public UInt32 Vat;
        public byte[] DateTime;		//YMDHMS
        public byte[] DateTimeDelta;	//YMDHMS
        public UInt16 BufLen;
        public UInt16 RecLen;
        public UInt16 RemLen;
        public UInt16 ZNo;
        public UInt16 FNo;
        public UInt16 Type;
        public UInt16 Func;
        public UInt16 DateTimeCount;
        public UInt16 RecordStatus;

        public ST_EKU_APPINF()
        {
            Buffer = new byte[1024];
            DateTime = new byte[6];
            DateTimeDelta = new byte[6];
        }
    };

    /** EConditionIds */
    public enum EConditionIds
    {
        GMP3_CONTITION_ID_PAYMENT_TOTAL_AMOUNT = 1,	/**< UINT32, Total Payment Amount value */
        GMP3_CONTITION_ID_IS_TICKET_PAYMENT_COMPLETED,		/**< BOOLEAN, Is All Ticket Payment completed */
    };

    /** EConditionTest */
    public enum EConditionTest
    {
        EIsEqual = 0,			        /**< "==" */
        EIsBigger,				        /**< ">" */
        EIsEqualOrBigger,				/**< "=>" */
        EIsSmaller,				        /**< "<" */
        EIsEqualOrSmaller,				/**< "<=" */
    };

    /** Subtype of the MATRAHSIZ transaction in TaxExceptioan  */
    public enum ETypeOfMatrahsiz
    {
        MATRAHSIZ_TYPE_ILAC = 1,
        MATRAHSIZ_TYPE_MUAYANE,
        MATRAHSIZ_TYPE_MUAYANE_RECETE,
        MATRAHSIZ_TYPE_INVOICE_COLLECTION,
    };

    /** Structure which is used to define a conditional case */
    public struct ST_CONDITIONAL_IF
    {
        public EConditionIds id;				    /**< One of EConditionIds */
        public EConditionTest eTestFormule;			/**< One of EConditionTest */
        public ulong ui64TestValue;					/**< A value to test  */
        public ushort IfTrue_GOTO;					/**< One of EConditionGoto OR index on subCommands list */
        public ushort IfFalse_GOTO;					/**< One of EConditionGoto OR index on subCommands list */
    };

    public struct ST_DATE_TIME
    {
        public UInt16 year;		    /**< Year (1900..2100)*/
        public byte month;		    /**< Month (1..12)*/
        public byte day;			/**< Day (1..31)*/
        public byte hour;			/**< Hour (0..23)*/
        public byte minute;		    /**< Minute (0..59)*/
        public byte second;		    /**< Second (0..59)*/
    };

    public class ST_MULTIPLE_RETURN_CODE
    {
        public UInt32 subCommand;						/**< subCommand which this result is produced for. If it is zero, then there is no subCommand and the data is produced automaticly by uApplication */
        public UInt32 retcode;							/**< subCommand return code (result of the subCommand on ECR) */
        public UInt32 tag;								/**< tag value provided by External Application (or one of GMP tag if it is automatic response from uApplcation) to mark the output data */
        public UInt16 indexOfSubCommand;				/**< order of the subCommand in the request message package. It is started by one and increased in each subcommand. if it is zero then there is no subCommand and the response is automatic from uApplication */
        public UInt16 lengthOfData;					/**< [IN] Maximum data buffer size [OUT] Length of the subCommand data returned*/
        public byte[] pData;							/**< pointer to the returned data of the subCommand. It must be allocated by External Application. If it is NULL, data will not be copied even if returned from ECR */

        public ST_MULTIPLE_RETURN_CODE()
        {
            pData = new byte[100];
        }
    };


    public class ST_DATABASE_COLOMN
    {
        public int typeOfData;
        public string data;
    };

    public class ST_DATABASE_LINE
    {
        public int indexOfLine;
        public int numberOfColomns;
        public ST_DATABASE_COLOMN[] pstColomnArray;

        public ST_DATABASE_LINE()
        {
            pstColomnArray = new ST_DATABASE_COLOMN[50];
        }
    };

    public class ST_DATABASE_RESULT
    {
        public int numberOfLines;
        public ST_DATABASE_LINE[] pstCaptionArray;
        public ST_DATABASE_LINE[] pstLineArray;

        public ST_DATABASE_RESULT()
        {
            pstCaptionArray = new ST_DATABASE_LINE[50];
            pstLineArray = new ST_DATABASE_LINE[50];
        }
    };

    public class ST_MULTIPLE_BANK_RESPONSE
    {
        public short bkmId;
        public short rescode;
    };

    class GMP_Tools
    {
        public static string SetEncoding(byte[] arr)
        {
            return Encoding.GetEncoding(65001).GetString(arr);
            //return Encoding.GetEncoding("iso-8859-9").GetString(arr);
        }

        public static string SetEncoding(byte[] arr, int index, int len)
        {
            return Encoding.GetEncoding("iso-8859-9").GetString(arr, index, len);
        }

        public static byte[] GetBytesFromString(string str)
        {
            byte[] Result = new byte[str.Length + 1];
            int Index = 0;
            foreach (var i in str)
            {
                if (i == 'Ğ')
                    Result[Index] = 0xD0;
                else if (i == 'Ü')
                    Result[Index] = 0xDC;
                else if (i == 'Ş')
                    Result[Index] = 0xDE;
                else if (i == 'İ')
                    Result[Index] = 0xDD;
                else if (i == 'Ö')
                    Result[Index] = 0xD6;
                else if (i == 'Ç')
                    Result[Index] = 0xC7;
                else if (i == 'I')
                    Result[Index] = 0x49;
                else if (i == 'ğ')
                    Result[Index] = 0xF0;
                else if (i == 'ü')
                    Result[Index] = 0xFC;
                else if (i == 'ş')
                    Result[Index] = 0xFE;
                else if (i == 'i')
                    Result[Index] = 0x69;
                else if (i == 'ö')
                    Result[Index] = 0xF6;
                else if (i == 'ç')
                    Result[Index] = 0xE7;
                else if (i == 'ı')
                    Result[Index] = 0xFD;
                else if (i == '€')
                    Result[Index] = 0x80;
                else
                    Result[Index] = Convert.ToByte(i);
                ++Index;
            }
            Result[Index] = 0x00;
            return Result;
        }

        public static string GetStringFromBytes(byte[] byt)
        {
            string Result = "";

            for (int i = 0; byt[i] != 0x00; i++)
            {

                if (i == byt.Length)
                    break;

                if (byt[i] == 0xD0)
                    Result += 'Ğ';
                else if (byt[i] == 0xDC)
                    Result += 'Ü';
                else if (byt[i] == 0xDE)
                    Result += 'Ş';
                else if (byt[i] == 0xDD)
                    Result += 'İ';
                else if (byt[i] == 0xD6)
                    Result += 'Ö';
                else if (byt[i] == 0xC7)
                    Result += 'Ç';
                else if (byt[i] == 0x49)
                    Result += 'I';
                else if (byt[i] == 0xF0)
                    Result += 'ğ';
                else if (byt[i] == 0xFC)
                    Result += 'ü';
                else if (byt[i] == 0xFE)
                    Result += 'ş';
                else if (byt[i] == 0x69)
                    Result += 'i';
                else if (byt[i] == 0xF6)
                    Result += 'ö';
                else if (byt[i] == 0xE7)
                    Result += 'ç';
                else if (byt[i] == 0xFD)
                    Result += 'ı';
                else if (byt[i] == 0x80)
                    Result += '€';
                else
                    Result += (char)byt[i];
            }
            return Result;
        }

    }

    class Json_GMPSmartDLL
    {

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetTaxRates", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetTaxRates(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, byte[] pJsonTaxRate, byte[] szJsonTaxRate_Out, int JsonTaxRateLen_Out, int NumberOfRecordsRequested);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetDepartments", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetDepartments(ref int pNumberOfTotalDepartments, ref int pNumberOfTotalDepartmentsReceived, byte[] pJsonDepartments, byte[] szJsonDepartments_Out, int JsonDepartmentsLen_Out, int NumberOfDepartmentRequested);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetExchangeTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetExchangeTable(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, byte[] pJsonExchange, byte[] pJsonExchange_Out, int pJsonExchangeLen_Out, int NumberOfRecordsRequested);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_SetDepartments", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_SetDepartments(byte[] pJsonDepartments, byte[] pJsonDepartments_Out, int pJsonDepartmentsLen_Out, byte NumberOfDepartmentRequested, byte[] szSupervisorPassword);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_KasaAvans", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_KasaAvans(int Amount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_CustomerAvans", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_CustomerAvans(int Amount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, byte[] szCustomerName, byte[] szTckn, byte[] szVkn, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_KasaPayment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_KasaPayment(int Amount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetTicket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetTicket(byte[] szJsonTicket, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_ItemSale", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_ItemSale(byte[] szJsonItem, byte[] szJsonItem_Out, int JsonItemLen_Out, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Payment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Payment(byte[] stPaymentRequest, byte[] Out_stPaymentRequest, int Out_stPaymentRequestLen, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);//TIMEOUT_CARD_TRANSACTIONS

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_PrintUserMessage", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_PrintUserMessage(byte[] szJsonUserMessage, byte[] szJsonUserMessage_Out, int JsonUserMessageLen_Out, UInt16 NumberOfMessage, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_PrintUserMessage_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_PrintUserMessage_Ex(byte[] szJsonUserMessage, byte[] szJsonUserMessage_Out, int JsonUserMessageLen_Out, UInt16 NumberOfMessage, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Plus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Plus(int Amount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Plus_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Plus_Ex(int Amount, byte[] szText, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Minus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Minus(int Amount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Minus_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Minus_Ex(int Amount, byte[] szText, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Inc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Inc(byte Rate, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Inc_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Inc_Ex(byte Rate, byte[] szText, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Dec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Dec(byte Rate, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Dec_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Dec_Ex(byte Rate, byte[] szText, byte[] szJsonTicket_Out, int JsonTicketLen_Out, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_VoidAll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_VoidAll(byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TmeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Pretotal", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Pretotal(byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Matrahsiz", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Matrahsiz(byte[] TckNo, ushort SubtypeOfTaxException, int MatrahsizAmount, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_VoidPayment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_VoidPayment(UInt16 Index, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds); // TIMEOUT_CARD_TRANSACTIONS

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_VoidItem", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_VoidItem(UInt16 Index, UInt64 ItemCount, byte ItemCountPrecision, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionGetUniqueIdList", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionGetUniqueIdList(byte[] szUniqueIdList, byte[] szUniqueIdList_Out, int UniqueIdListLen_Out, UInt16 MaxNumberOfitems, UInt16 IndexOfitemsToStart, ref UInt16 pTotalNumberOfItems, ref UInt16 pNumberOfItemsInThis, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionReadCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionReadCard(int CardReaderTypes, byte[] szCardInfo, byte[] szCardInfo_Out, int CardInfoLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetCashierTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetCashierTable(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, byte[] szCashier, byte[] szCashier_Out, int CashierLen_Out, int NumberOfRecordsRequested, ref short pActiveCashier);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_Echo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_Echo(byte[] szEcho_Out, int EchoLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_GMP_StartPairingInit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_GMP_StartPairingInit(byte[] szPairing, byte[] szPairingResp, int PairingRespLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetPaymentApplicationInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetPaymentApplicationInfo(ref byte pNumberOfTotalRecords, ref byte pNumberOfTotalRecordsReceived, byte[] szExchange, byte[] szExchange_Out, int ExchangeLen_Out, byte NumberOfRecordsRequested);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_SetInvoice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_SetInvoice(byte[] szJsonInvoiceInfo, byte[] szJsonInvoiceInfo_Out, int JsonInvoiceInfoLen_Out, byte[] szTicket_Out, int TicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_ItemSale", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_ItemSale(byte[] Buffer, int MaxSize, byte[] szJsonItem, byte[] szJsonItem_Out, int JsonItemLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_Payment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_Payment(byte[] Buffer, int MaxSize, byte[] szJsonPaymentRequest, byte[] szJsonPaymentRequest_Out, int JsonPaymentRequestLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionReports", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionReports(int FunctionFlags, byte[] szJsonFunctionParameters, byte[] szJsonFunctionParameters_Out, int JsonFunctionParametersLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionReadZReport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionReadZReport(byte[] szJsonFunctionParameters, byte[] szJsonZReport_Out, int JsonZReportLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_SetInvoice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_SetInvoice(byte[] Buffer, int MaxSize, byte[] szJsonInvoiceInfo, byte[] szJsonInvoiceInfo_Out, int JsonInvoiceInfoLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_PrintUserMessage", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_PrintUserMessage(byte[] Buffer, int MaxSize, byte[] szJsonUserMessage, byte[] szJsonUserMessage_Out, int JsonUserMessageLen_Out, ushort NumberOfMessage);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_PrintUserMessage_Ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_PrintUserMessage_Ex(byte[] Buffer, int MaxSize, byte[] szJsonUserMessage, byte[] szJsonUserMessage_Out, int JsonUserMessageLen_Out, ushort NumberOfMessage);

        [DllImport("GMPSmartDLL.dll", EntryPoint = "Json_parse_FiscalPrinter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_parse_FiscalPrinter(byte[] szJsonReturnCodes_Out, int JsonReturnCodestLen_Out, ref UInt16 pNumberOfreturnCodes, uint RecvMsgId, byte[] RecvFullBuffer, UInt16 RecvFullLen, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int MaxNumberOfReturnCode, int MaxReturnCodeDataLen);

        [DllImport("GMPSmartDLL.dll", EntryPoint = "Json_parse_GetEcr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_parse_GetEcr(byte[] szJsonReturnCodes_Out, int JsonReturnCodesLen_Out, ref int pNumberOfReturnCodes, UInt32 RecvMsgId, byte[] RecvFullBuffer, UInt16 RecvFullLen, int MaxNumberOfReturnCode, int MaxReturnCodeDataLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_ReversePayment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_ReversePayment(byte[] Buffer, int MaxSize, byte[] szJsonPaymentRequest, byte[] szJsonPaymentRequest_Out, int JsonPaymentRequestLen_Out, ushort NumberOfPaymentRequests);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_Date", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_Date(byte[] Buffer, int MaxSize, UInt32 Tag_Id, byte[] Title, byte[] Text, byte[] Mask, byte[] szJsonValue, byte[] szJsonValue_Out, int JsonValueLen_Out, int timeout);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_prepare_Condition", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Json_prepare_Condition(byte[] Buffer, int MaxSize, byte[] szJsonCondition, byte[] szJsonCondition_Out, int JsonConditionLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_ReversePayment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_ReversePayment(byte[] szJsonPaymentRequest, byte[] szJsonPaymentRequest_Out, int JsonPaymentRequestLen_Out, short NumberOfPaymentRequests, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_JumpToECR", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_JumpToECR(UInt64 JumpFlags, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_MultipleCommand", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_MultipleCommand(byte[] stJsonReturnCodes_Out, int JsonReturnCodesLen_Out, ref ushort pIndexOfReturnCodes, byte[] SendBuffer, UInt16 SendBufferLen, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_SetTaxFree", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_SetTaxFree(int Flag, byte[] szName, byte[] szSurname, byte[] szIdentificationNo, byte[] szCity, byte[] szCountry, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_SetParkingTicket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_SetParkingTicket(byte[] szCarIdentification, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_SetTaxFreeRefundAmount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_SetTaxFreeRefundAmount(int RefundAmount, ushort RefundAmountCurrency, byte[] szJsonTicket_Out, int JsonTicketLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionChangeTicketHeader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionChangeTicketHeader(byte[] szSupervisorPassword, ref ushort pNumberOfSpaceTotal, ref ushort pNumberOfSpaceUsed, byte[] szJsonTicketHeader, byte[] szJsonTicketHeader_Out, int JsonTicketHeaderLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetTicketHeader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetTicketHeader(ushort IndexOfHeader, byte[] szJsonTicketHeader, byte[] szJsonTicketHeader_Out, int JsonTicketHeaderLen_Out, ref ushort pNumberOfSpaceTotal, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_Database_QueryColomnCaptions", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_Database_QueryColomnCaptions(byte[] szJsonDatabaseResult_Out, int JsonDatabaseResultLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_Database_QueryReadLine", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_Database_QueryReadLine(ushort NumberOfLinesRequested, ushort NumberOfColomnsRequested, byte[] szJsonDatabaseResult_Out, int JsonDatabaseResultLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_Database_FreeStructure", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_Database_FreeStructure(byte[] szJsonDatabaseResult, byte[] szJsonDatabaseResult_Out, int JsonDatabaseResultLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_Database_Execute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_Database_Execute(byte[] szSqlWord, byte[] szJsonDatabaseResult_Out, int JsonDatabaseResultLen_Out);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetPLU", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetPLU(byte[] szBarcode, byte[] szJsonPluRecord, byte[] szJsonPluRecord_Out, int JsonPluRecordLen_Out, byte[] szJsonPluGroupRecord, byte[] szJsonPluGroupRecord_Out, int szJsonPluGroupRecordLen_Out, int MaxNumberOfGroupRecords, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_GetVasApplicationInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_GetVasApplicationInfo(ref byte pNumberOfTotalRecords, ref byte pNumberOfTotalRecordsReceived, byte[] szJsonPaymentApplicationInfo_Out, int JsonPaymentApplicationInfoLen_Out, UInt16 vasType);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionEkuSeek", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionEkuSeek(byte[] szJsonEKUAppInfo, byte[] szJsonEKUAppInfo_Out, int JsonEKUAppInfoLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FileSystem_DirListFiles", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FileSystem_DirListFiles(byte[] szDirName, byte[] szJsonStFile, byte[] szJsonStFile_Out, int JsonStFileLen_Out, short MaxNumberOfFiles, ref short pNumberOfFiles);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionEkuReadHeader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionEkuReadHeader(short Index, byte[] szJsonEkuHeader, byte[] szJsonEkuHeader_Out, int JsonEkuHeaderLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionEkuReadData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionEkuReadData(ref UInt16 pEkuDataBufferReceivedLen, byte[] szJsonEKUAppInfo, byte[] szJsonEKUAppInfo_Out, int JsonEKUAppInfoLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionEkuReadInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionEkuReadInfo(UInt16 EkuAccessFunction, byte[] szJsonEkuModuleInfo, byte[] szJsonEkuModuleInfo_Out, int JsonEkuModuleInfoLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionBankingRefund", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionBankingRefund(byte[] szJsonPaymentRequest, byte[] szJsonPaymentRequest_Out, int JsonPaymentRequestLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_FiscalPrinter_FunctionBankingBatch", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_FiscalPrinter_FunctionBankingBatch(UInt16 BkmId, ref UInt16 pNumberOfBankResponse, byte[] szJsonMultipleBankResponse_Out, int JsonMultipleBankResponseLen_Out, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_SetIniParameters", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_SetIniParameters(string szJsonIniParameter);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Json_GetIniParameters", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Json_GetIniParameters(byte[] szJsonIniParameter_Out, int szJsonIniParameterLen_Out);

        private static void MergeItemStruct(ST_TICKET StTicketDest, ST_TICKET StTicketSrc)
        {

            StTicketDest.bcdTicketDate = StTicketSrc.bcdTicketDate;
            StTicketDest.bcdTicketTime = StTicketSrc.bcdTicketTime;
            StTicketDest.CashBackAmount = StTicketSrc.CashBackAmount;
            StTicketDest.EJNo = StTicketSrc.EJNo;
            StTicketDest.FNo = StTicketSrc.FNo;
            StTicketDest.invoiceAmount = StTicketSrc.invoiceAmount;
            StTicketDest.invoiceAmountCurrency = StTicketSrc.invoiceAmountCurrency;
            StTicketDest.invoiceDate = StTicketSrc.invoiceDate;
            StTicketDest.invoiceNo = StTicketSrc.invoiceNo;
            StTicketDest.invoiceType = StTicketSrc.invoiceType;
            StTicketDest.KasaAvansAmount = StTicketSrc.KasaAvansAmount;
            StTicketDest.KasaPaymentAmount = StTicketSrc.KasaPaymentAmount;
            StTicketDest.KatkiPayiAmount = StTicketSrc.KatkiPayiAmount;
            StTicketDest.numberOfItemsInThis = StTicketSrc.numberOfItemsInThis;
            StTicketDest.numberOfPaymentsInThis = StTicketSrc.numberOfPaymentsInThis;
            StTicketDest.numberOfPrinterLinesInThis = StTicketSrc.numberOfPrinterLinesInThis;
            StTicketDest.OptionFlags = StTicketSrc.OptionFlags;
            StTicketDest.TaxFreeCalculated = StTicketSrc.TaxFreeCalculated;
            StTicketDest.TaxFreeRefund = StTicketSrc.TaxFreeRefund;
            StTicketDest.TckNo = StTicketSrc.TckNo;
            StTicketDest.ticketType = StTicketSrc.ticketType;
            StTicketDest.totalNumberOfItems = StTicketSrc.totalNumberOfItems;
            StTicketDest.totalNumberOfPayments = StTicketSrc.totalNumberOfPayments;
            StTicketDest.totalNumberOfPrinterLines = StTicketSrc.totalNumberOfPrinterLines;
            StTicketDest.TotalReceiptAmount = StTicketSrc.TotalReceiptAmount;
            StTicketDest.TotalReceiptDiscount = StTicketSrc.TotalReceiptDiscount;
            StTicketDest.TotalReceiptIncrement = StTicketSrc.TotalReceiptIncrement;
            StTicketDest.TotalReceiptItemCancel = StTicketSrc.TotalReceiptItemCancel;
            StTicketDest.TotalReceiptPayment = StTicketSrc.TotalReceiptPayment;
            StTicketDest.TotalReceiptReversedPayment = StTicketSrc.TotalReceiptReversedPayment;
            StTicketDest.TotalReceiptTax = StTicketSrc.TotalReceiptTax;
            StTicketDest.TransactionFlags = StTicketSrc.TransactionFlags;
            StTicketDest.uniqueId = StTicketSrc.uniqueId;
            StTicketDest.ZNo = StTicketSrc.ZNo;
            StTicketDest.UserData = StTicketSrc.UserData;


            StTicketDest.stPrinterCopy = new ST_printerDataForOneLine[StTicketSrc.totalNumberOfPrinterLines];
            for (int i = 0; i < StTicketDest.stPrinterCopy.Length; i++)
            {
                StTicketDest.stPrinterCopy[i] = new ST_printerDataForOneLine();
            }

            //StTicketDest.SaleInfo = new ST_SALEINFO[StTicketSrc.totalNumberOfItems];
            //for (int i = 0; i < StTicketDest.SaleInfo.Length; i++)
            //{
            //    StTicketDest.SaleInfo[i] = new ST_SALEINFO();
            //}

            //StTicketDest.stPayment = new ST_PAYMENT[StTicketSrc.totalNumberOfPayments];
            //for (int i = 0; i < StTicketDest.stPrinterCopy.Length; i++)
            //{
            //    StTicketDest.stPayment[i] = new ST_PAYMENT();
            //}

            for (int i = 0; i < StTicketSrc.totalNumberOfItems; ++i)
            {
                if (StTicketSrc.SaleInfo != null && StTicketSrc.SaleInfo[i] != null)
                    StTicketDest.SaleInfo[i] = StTicketSrc.SaleInfo[i];
            }

            for (int i = 0; i < StTicketSrc.totalNumberOfPayments; ++i)
            {
                if (StTicketSrc.stPayment != null && StTicketSrc.stPayment[i] != null)
                    StTicketDest.stPayment[i] = StTicketSrc.stPayment[i];
            }

            for (int i = 0; i < StTicketSrc.totalNumberOfPrinterLines; ++i)
            {
                if (StTicketSrc.stPrinterCopy != null && StTicketSrc.stPrinterCopy[i] != null)
                    StTicketDest.stPrinterCopy[i] = StTicketSrc.stPrinterCopy[i];
            }
        }

        public static UInt32 FiscalPrinter_GetTaxRates(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, ref ST_TAX_RATE[] pStTaxRate, int NumberOfRecordsRequested)
        {
            string szJsonTaxRates = JsonConvert.SerializeObject(pStTaxRate);
            byte[] szJsonTaxRates_In = GMP_Tools.GetBytesFromString(szJsonTaxRates);
            byte[] szJsonTaxRates_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetTaxRates(ref pNumberOfTotalRecords, ref pNumberOfTotalRecordsReceived, szJsonTaxRates_In, szJsonTaxRates_Out, szJsonTaxRates_Out.Length, NumberOfRecordsRequested);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTaxRates_Out);
                pStTaxRate = JsonConvert.DeserializeObject<ST_TAX_RATE[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetDepartments(ref int pNumberOfTotalDepartments, ref int pNumberOfTotalDepartmentsReceived, ref ST_DEPARTMENT[] pStDepartments, int NumberOfDepartmentRequested)
        {
            string szJsonDepartments = JsonConvert.SerializeObject(pStDepartments);
            byte[] szJsonDepartments_In = GMP_Tools.GetBytesFromString(szJsonDepartments);
            byte[] szJsonDepartments_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetDepartments(ref pNumberOfTotalDepartments, ref pNumberOfTotalDepartmentsReceived, szJsonDepartments_In, szJsonDepartments_Out, szJsonDepartments_Out.Length, NumberOfDepartmentRequested);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDepartments_Out);
                pStDepartments = JsonConvert.DeserializeObject<ST_DEPARTMENT[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetExchangeTable(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, ref ST_EXCHANGE[] pStExchange, int NumberOfRecordsRequested)
        {
            string szJsonExchangeTable = JsonConvert.SerializeObject(pStExchange);
            byte[] szJsonExchangeTable_In = GMP_Tools.GetBytesFromString(szJsonExchangeTable);
            byte[] szJsonExchangeTable_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetExchangeTable(ref pNumberOfTotalRecords, ref pNumberOfTotalRecordsReceived, szJsonExchangeTable_In, szJsonExchangeTable_Out, szJsonExchangeTable_Out.Length, NumberOfRecordsRequested);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonExchangeTable_Out);
                pStExchange = JsonConvert.DeserializeObject<ST_EXCHANGE[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_SetDepartments(ref ST_DEPARTMENT[] pStDepartments, byte NumberOfDepartmentRequested, string szSupervisorPassword)
        {
            string szJsonDepartments = JsonConvert.SerializeObject(pStDepartments);
            byte[] szJsonDepartments_In = GMP_Tools.GetBytesFromString(szJsonDepartments);
            byte[] supervisorPass = GMP_Tools.GetBytesFromString(szSupervisorPassword);

            byte[] szJsonDepartments_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_SetDepartments(szJsonDepartments_In, szJsonDepartments_Out, szJsonDepartments_Out.Length, NumberOfDepartmentRequested, supervisorPass);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDepartments_Out);
                pStDepartments = JsonConvert.DeserializeObject<ST_DEPARTMENT[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 Json_FiscalPrinter_CustomerAvans(int Amount, ref ST_TICKET pstTicket, string szCustomerName, string szTckn, string szVkn, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] CustomerName = GMP_Tools.GetBytesFromString(szCustomerName);
            byte[] Tckn = GMP_Tools.GetBytesFromString(szTckn);
            byte[] Vkn = GMP_Tools.GetBytesFromString(szVkn);

            UInt32 retcode = Json_FiscalPrinter_CustomerAvans(Amount, szJsonTicket_Out, szJsonTicket_Out.Length, CustomerName, Tckn, Vkn, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 SetIniParameter(ST_INI_PARAM pStIniParameter)
        {
            string szJsonIniParameter = JsonConvert.SerializeObject(pStIniParameter);

            UInt32 retcode = Json_SetIniParameters(szJsonIniParameter);

            return retcode;
        }

        public static UInt32 GetIniParameters(ref ST_INI_PARAM pStIniParameter)
        {
            byte[] szJsonIniParameter_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_GetIniParameters(szJsonIniParameter_Out, szJsonIniParameter_Out.Length);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.SetEncoding(szJsonIniParameter_Out);
                pStIniParameter = JsonConvert.DeserializeObject<ST_INI_PARAM>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_KasaAvans(int Amount, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_KasaAvans(Amount, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_KasaPayment(int Amount, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_KasaPayment(Amount, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_ItemSale(ref ST_ITEM StItem, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonItem = JsonConvert.SerializeObject(StItem);
            byte[] szJsonItem_In = GMP_Tools.GetBytesFromString(szJsonItem);
            byte[] szJsonItem_Out = new byte[Defines.GMP_TICKET_BUFFER];


            UInt32 retcode = Json_FiscalPrinter_ItemSale(szJsonItem_In, szJsonItem_Out, szJsonItem_Out.Length, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonItem_Out);
                StItem = JsonConvert.DeserializeObject<ST_ITEM>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Payment(ref ST_PAYMENT_REQUEST pStPaymentRequest, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonPaymentRequest = JsonConvert.SerializeObject(pStPaymentRequest);
            byte[] szJsonPaymentRequest_In = GMP_Tools.GetBytesFromString(szJsonPaymentRequest);
            byte[] szJsonPaymentRequest_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Payment(szJsonPaymentRequest_In, szJsonPaymentRequest_Out, szJsonPaymentRequest_Out.Length, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);

            if (retcode != 0)
            {
                return retcode;
            }

            string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPaymentRequest_Out);
            pStPaymentRequest = JsonConvert.DeserializeObject<ST_PAYMENT_REQUEST>(retJsonString);
            retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
            ST_TICKET StTicketTemp = new ST_TICKET();
            StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
            MergeItemStruct(pstTicket, StTicketTemp);

            return retcode;
        }

        public static UInt32 FiscalPrinter_PrintUserMessage(ref ST_USER_MESSAGE[] pStUser, UInt16 NumberOfMessage, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonUser = JsonConvert.SerializeObject(pStUser);
            byte[] szJsonUser_In = GMP_Tools.GetBytesFromString(szJsonUser);
            byte[] szJsonUser_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_PrintUserMessage(szJsonUser_In, szJsonUser_Out, szJsonUser_Out.Length, NumberOfMessage, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonUser_Out);
                pStUser = JsonConvert.DeserializeObject<ST_USER_MESSAGE[]>(retJsonString);
                //retJsonString = GMP_Tools.SetEncoding(szJsonTicket_Out);
                //ST_TICKET StTicketTemp = new ST_TICKET();
                //StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                //MergeItemStruct(pstTicket, StTicketTemp);    
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_PrintUserMessage_Ex(ref ST_USER_MESSAGE[] pStUser, UInt16 NumberOfMessage, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonUser = JsonConvert.SerializeObject(pStUser);
            byte[] szJsonUser_In = GMP_Tools.GetBytesFromString(szJsonUser);
            byte[] szJsonUser_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_PrintUserMessage_Ex(szJsonUser_In, szJsonUser_Out, szJsonUser_Out.Length, NumberOfMessage, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonUser_Out);
                pStUser = JsonConvert.DeserializeObject<ST_USER_MESSAGE[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetTicket(ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonTicket = JsonConvert.SerializeObject(pstTicket);
            byte[] szJsonTicket_In = GMP_Tools.GetBytesFromString(szJsonTicket);

            UInt32 retcode = Json_FiscalPrinter_GetTicket(szJsonTicket_In, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Plus(int Amount, ref ST_TICKET pstTicket, UInt16 IndexOfItem, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Plus(Amount, json_Out_stTicket, json_Out_stTicket.Length, IndexOfItem, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Minus(int Amount, ref ST_TICKET pstTicket, UInt16 IndexOfItem, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Minus(Amount, json_Out_stTicket, json_Out_stTicket.Length, IndexOfItem, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Dec(byte Rate, ref ST_TICKET pstTicket, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Dec(Rate, szJsonTicket_Out, szJsonTicket_Out.Length, IndexOfItem, ref pChangedAmount, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Inc(byte Rate, ref ST_TICKET pstTicket, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Inc(Rate, szJsonTicket_Out, szJsonTicket_Out.Length, IndexOfItem, ref pChangedAmount, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Plus_Ex(int Amount, string szText, ref ST_TICKET pstTicket, UInt16 IndexOfItem, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] Text = GMP_Tools.GetBytesFromString(szText);

            UInt32 retcode = Json_FiscalPrinter_Plus_Ex(Amount, Text, json_Out_stTicket, json_Out_stTicket.Length, IndexOfItem, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Minus_Ex(int Amount, string szText, ref ST_TICKET pstTicket, UInt16 IndexOfItem, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] Text = GMP_Tools.GetBytesFromString(szText);

            UInt32 retcode = Json_FiscalPrinter_Minus_Ex(Amount, Text, json_Out_stTicket, json_Out_stTicket.Length, IndexOfItem, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Dec_Ex(byte Rate, string szText, ref ST_TICKET pstTicket, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] Text = GMP_Tools.GetBytesFromString(szText);

            UInt32 retcode = Json_FiscalPrinter_Dec_Ex(Rate, Text, szJsonTicket_Out, szJsonTicket_Out.Length, IndexOfItem, ref pChangedAmount, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Inc_Ex(byte Rate, string szText, ref ST_TICKET pstTicket, UInt16 IndexOfItem, ref int pChangedAmount, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] Text = GMP_Tools.GetBytesFromString(szText);

            UInt32 retcode = Json_FiscalPrinter_Inc_Ex(Rate, Text, szJsonTicket_Out, szJsonTicket_Out.Length, IndexOfItem, ref pChangedAmount, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.SetEncoding(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_VoidAll(ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_VoidAll(szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Pretotal(ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Pretotal(szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Matrahsiz(string szTckNo, ushort SubtypeOfTaxException, int MatrahsizAmount, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] TckNo = GMP_Tools.GetBytesFromString(szTckNo);

            UInt32 retcode = Json_FiscalPrinter_Matrahsiz(TckNo, SubtypeOfTaxException, MatrahsizAmount, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_VoidPayment(UInt16 Index, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_VoidPayment(Index, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_VoidItem(UInt16 Index, UInt64 ItemCount, byte ItemCountPrecision, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_VoidItem(Index, ItemCount, ItemCountPrecision, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionGetUniqueIdList(ref ST_UNIQUE_ID[] pStUniqueIdList, UInt16 MaxNumberOfitems, UInt16 IndexOfitemsToStart, ref UInt16 pTotalNumberOfItems, ref UInt16 pNumberOfItemsInThis, int TimeoutInMiliseconds)
        {
            string szJsonUniqueIdList = JsonConvert.SerializeObject(pStUniqueIdList);
            byte[] szJsonUniqueIdList_In = GMP_Tools.GetBytesFromString(szJsonUniqueIdList);
            byte[] szJsonUniqueIdList_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionGetUniqueIdList(szJsonUniqueIdList_In, szJsonUniqueIdList_Out, szJsonUniqueIdList_Out.Length, MaxNumberOfitems, IndexOfitemsToStart, ref pTotalNumberOfItems, ref pNumberOfItemsInThis, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonUniqueIdList_Out);
                pStUniqueIdList = JsonConvert.DeserializeObject<ST_UNIQUE_ID[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionReadCard(int CardReaderTypes, ref ST_CARD_INFO pStCardInfo, int TimeoutInMiliseconds)
        {
            string szJsonCardInfo = JsonConvert.SerializeObject(pStCardInfo);
            byte[] szJsonCardInfo_In = GMP_Tools.GetBytesFromString(szJsonCardInfo);
            byte[] szJsonCardInfo_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionReadCard(CardReaderTypes, szJsonCardInfo_In, szJsonCardInfo_Out, szJsonCardInfo_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonCardInfo_Out);
                pStCardInfo = JsonConvert.DeserializeObject<ST_CARD_INFO>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetCashierTable(ref int pNumberOfTotalRecords, ref int pNumberOfTotalRecordsReceived, ref ST_CASHIER[] pStCashier, int NumberOfRecordsRequested, ref short pActiveCashier)
        {
            string szJsonCashier = JsonConvert.SerializeObject(pStCashier);
            byte[] szJsonCashier_In = GMP_Tools.GetBytesFromString(szJsonCashier);
            byte[] szJsonCashier_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetCashierTable(ref pNumberOfTotalRecords, ref pNumberOfTotalRecordsReceived, szJsonCashier_In, szJsonCashier_Out, szJsonCashier_Out.Length, NumberOfRecordsRequested, ref pActiveCashier);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonCashier_Out);
                pStCashier = JsonConvert.DeserializeObject<ST_CASHIER[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_Echo(ref ST_ECHO pStEcho, int TimeoutInMiliseconds)
        {
            byte[] szJsonEcho_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_Echo(szJsonEcho_Out, szJsonEcho_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonEcho_Out);
                pStEcho = JsonConvert.DeserializeObject<ST_ECHO>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 GMP_StartPairingInit(ref ST_GMP_PAIR pStPair, ref ST_GMP_PAIR_RESP pStPairResp, int TimeoutInMiliseconds = Defines.TIMEOUT_DEFAULT)
        {
            string szJsonPairing = JsonConvert.SerializeObject(pStPair);
            byte[] szJsonPairing_In = GMP_Tools.GetBytesFromString(szJsonPairing);
            byte[] szJsonPairing_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_GMP_StartPairingInit(szJsonPairing_In, szJsonPairing_Out, szJsonPairing_Out.Length);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPairing_Out);
                pStPairResp = JsonConvert.DeserializeObject<ST_GMP_PAIR_RESP>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetPaymentApplicationInfo(ref byte pNumberOfTotalRecords, ref byte pNumberOfTotalRecordsReceived, ref ST_PAYMENT_APPLICATION_INFO[] pStAppInfo, byte NumberOfRecordsRequested)
        {
            string szJsonAppInfo = JsonConvert.SerializeObject(pStAppInfo);
            byte[] szJsonAppInfo_In = GMP_Tools.GetBytesFromString(szJsonAppInfo);
            byte[] szJsonAppInfo_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetPaymentApplicationInfo(ref pNumberOfTotalRecords, ref pNumberOfTotalRecordsReceived, szJsonAppInfo_In, szJsonAppInfo_Out, szJsonAppInfo_Out.Length, NumberOfRecordsRequested);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonAppInfo_Out);
                pStAppInfo = JsonConvert.DeserializeObject<ST_PAYMENT_APPLICATION_INFO[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_SetInvoice(ref ST_INVIOCE_INFO pStInvoiceInfo, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonInvoiceInfo = JsonConvert.SerializeObject(pStInvoiceInfo);
            byte[] szJsonInvoiceInfo_In = GMP_Tools.GetBytesFromString(szJsonInvoiceInfo);
            byte[] szJsonInvoiceInfo_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_SetInvoice(szJsonInvoiceInfo_In, szJsonInvoiceInfo_Out, szJsonInvoiceInfo_Out.Length, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonInvoiceInfo_Out);
                pStInvoiceInfo = JsonConvert.DeserializeObject<ST_INVIOCE_INFO>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                pstTicket = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_ItemSale(byte[] Buffer, int MaxSize, ref ST_ITEM pStItem)
        {
            string szJsonItem = JsonConvert.SerializeObject(pStItem);
            byte[] szJsonItem_In = GMP_Tools.GetBytesFromString(szJsonItem);
            byte[] szJsonItem_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_ItemSale(Buffer, MaxSize, szJsonItem_In, szJsonItem_Out, szJsonItem_Out.Length);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonItem_Out);
                pStItem = JsonConvert.DeserializeObject<ST_ITEM>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_Payment(byte[] Buffer, int MaxSize, ref ST_PAYMENT_REQUEST pStPaymentRequest)
        {
            string szJsonPayment = JsonConvert.SerializeObject(pStPaymentRequest);
            byte[] szJsonPayment_In = GMP_Tools.GetBytesFromString(szJsonPayment);
            byte[] szJsonPayment_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_Payment(Buffer, MaxSize, szJsonPayment_In, szJsonPayment_Out, szJsonPayment_Out.Length);

            string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPayment_Out);
            pStPaymentRequest = JsonConvert.DeserializeObject<ST_PAYMENT_REQUEST>(retJsonString);

            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionReports(int functionFlags, ref ST_FUNCTION_PARAMETERS pStFunctionParameters, int TimeoutInMiliseconds)
        {
            string szJsonFunctionParameters = JsonConvert.SerializeObject(pStFunctionParameters);
            byte[] szJsonFunctionParameters_In = GMP_Tools.GetBytesFromString(szJsonFunctionParameters);
            byte[] szJsonFunctionParameters_Out = new byte[Defines.STANDART_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionReports(functionFlags, szJsonFunctionParameters_In, szJsonFunctionParameters_Out, szJsonFunctionParameters_Out.Length, TimeoutInMiliseconds);
            //if (retcode == 0)
            //{
            //    string retJsonString = GMP_Tools.SetEncoding(json_Out_stFunctionParameters);
            //    pstCardInfo = JsonConvert.DeserializeObject<ST_FUNCTION_PARAMETERS>(retJsonString);
            //}
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionReadZReport(ref ST_FUNCTION_PARAMETERS pStFunctionParameters, ref ST_Z_REPORT pStZReport, int TimeoutInMiliseconds)
        {
            string szJsonFunctionParameters = JsonConvert.SerializeObject(pStFunctionParameters);
            byte[] szJsonFunctionParameters_In = GMP_Tools.GetBytesFromString(szJsonFunctionParameters);
            byte[] szJsonFunctionParameters_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionReadZReport(szJsonFunctionParameters_In, szJsonFunctionParameters_Out, szJsonFunctionParameters_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonFunctionParameters_Out);
                pStZReport = JsonConvert.DeserializeObject<ST_Z_REPORT>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_SetInvoice(byte[] Buffer, int MaxSize, ref ST_INVIOCE_INFO pStInvoiceInfo)
        {
            string szJsonInvoiceInfo = JsonConvert.SerializeObject(pStInvoiceInfo);
            byte[] szJsonInvoiceInfo_In = GMP_Tools.GetBytesFromString(szJsonInvoiceInfo);
            byte[] szJsonInvoiceInfo_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_SetInvoice(Buffer, MaxSize, szJsonInvoiceInfo_In, szJsonInvoiceInfo_Out, szJsonInvoiceInfo_Out.Length);

            string retJsonString = GMP_Tools.GetStringFromBytes(szJsonInvoiceInfo_Out);
            pStInvoiceInfo = JsonConvert.DeserializeObject<ST_INVIOCE_INFO>(retJsonString);

            return retcode;
        }

        public static UInt32 parse_FiscalPrinter(ref ST_MULTIPLE_RETURN_CODE[] pStReturnCodes, ref UInt16 pNumberOfreturnCodes, UInt32 RecvMsgId, byte[] RecvFullBuffer, UInt16 RecvFullLen, ref ST_TICKET pstTicket, int MaxNumberOfReturnCode, int MaxReturnCodeDataLen)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] szJsonRetCodes_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_parse_FiscalPrinter(szJsonRetCodes_Out, szJsonRetCodes_Out.Length, ref pNumberOfreturnCodes, RecvMsgId, RecvFullBuffer, RecvFullLen, szJsonTicket_Out, szJsonTicket_Out.Length, MaxNumberOfReturnCode, MaxReturnCodeDataLen);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonRetCodes_Out);
                pStReturnCodes = JsonConvert.DeserializeObject<ST_MULTIPLE_RETURN_CODE[]>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                pstTicket = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 parse_GetEcr(ref ST_MULTIPLE_RETURN_CODE[] pStReturnCodes, ref int pNumberOfreturnCodes, UInt32 RecvMsgId, byte[] RecvFullBuffer, UInt16 RecvFullLen, int MaxNumberOfReturnCode, int MaxReturnCodeDataLen)
        {
            string szJsonRetCodes = JsonConvert.SerializeObject(pStReturnCodes);
            byte[] szJsonRetCodes_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_parse_GetEcr(szJsonRetCodes_Out, szJsonRetCodes_Out.Length, ref pNumberOfreturnCodes, RecvMsgId, RecvFullBuffer, RecvFullLen, MaxNumberOfReturnCode, MaxReturnCodeDataLen);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonRetCodes_Out);
                pStReturnCodes = JsonConvert.DeserializeObject<ST_MULTIPLE_RETURN_CODE[]>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_ReversePayment(byte[] Buffer, int MaxSize, ref ST_PAYMENT_REQUEST pStPaymentRequest, ushort NumberOfPaymentRequests)
        {
            string szJsonPayment = JsonConvert.SerializeObject(pStPaymentRequest);
            byte[] szJsonPayment_In = GMP_Tools.GetBytesFromString(szJsonPayment);
            byte[] szJsonPayment_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_ReversePayment(Buffer, MaxSize, szJsonPayment_In, szJsonPayment_Out, szJsonPayment_Out.Length, NumberOfPaymentRequests);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPayment_Out);
                pStPaymentRequest = JsonConvert.DeserializeObject<ST_PAYMENT_REQUEST>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_Date(byte[] Buffer, int MaxSize, UInt32 TagId, byte[] Title, byte[] Text, byte[] Mask, ref ST_DATE_TIME pStValue, int TimeoutInMiliseconds)
        {
            string szJsonDate = JsonConvert.SerializeObject(pStValue);
            byte[] szJsonDate_In = GMP_Tools.GetBytesFromString(szJsonDate);
            byte[] szJsonDate_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_Date(Buffer, MaxSize, TagId, Title, Text, Mask, szJsonDate_In, szJsonDate_Out, szJsonDate_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDate_Out);
                pStValue = JsonConvert.DeserializeObject<ST_DATE_TIME>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_Condition(byte[] Buffer, int MaxSize, ref ST_CONDITIONAL_IF pStCondition)
        {
            string szJsonCondition = JsonConvert.SerializeObject(pStCondition);
            byte[] szJsonCondition_In = GMP_Tools.GetBytesFromString(szJsonCondition);
            byte[] szJsonCondition_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_Condition(Buffer, MaxSize, szJsonCondition_In, szJsonCondition_Out, szJsonCondition_Out.Length);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonCondition_Out);
                pStCondition = JsonConvert.DeserializeObject<ST_CONDITIONAL_IF>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_ReversePayment(ref ST_PAYMENT_REQUEST pStPaymentRequest, short NumberOfPaymentRequests, ref ST_TICKET pstTicket, int TimeoutInMiliseconds) //TIMEOUT_CARD_TRANSACTIONS
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonPaymentRequest = JsonConvert.SerializeObject(pStPaymentRequest);
            byte[] szJsonPaymentRequest_In = GMP_Tools.GetBytesFromString(szJsonPaymentRequest);
            byte[] szJsonPaymentRequest_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_ReversePayment(szJsonPaymentRequest_In, szJsonPaymentRequest_Out, szJsonPaymentRequest_Out.Length, NumberOfPaymentRequests, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPaymentRequest_Out);
                pStPaymentRequest = JsonConvert.DeserializeObject<ST_PAYMENT_REQUEST>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                pstTicket = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_PrintUserMessage(byte[] Buffer, int MaxSize, ref ST_USER_MESSAGE[] pStUserMessage, ushort NumberOfMessage)
        {

            string szJsonUser = JsonConvert.SerializeObject(pStUserMessage);
            byte[] szJsonUser_In = GMP_Tools.GetBytesFromString(szJsonUser);
            byte[] szJsonUser_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_PrintUserMessage(Buffer, MaxSize, szJsonUser_In, szJsonUser_Out, szJsonUser_Out.Length, NumberOfMessage);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonUser_Out);
                pStUserMessage = JsonConvert.DeserializeObject<ST_USER_MESSAGE[]>(retJsonString);
            }
            return retcode;
        }

        public static int prepare_PrintUserMessage_Ex(byte[] Buffer, int MaxSize, ref ST_USER_MESSAGE[] pStUserMessage, ushort NumberOfMessage)
        {

            string szJsonUser = JsonConvert.SerializeObject(pStUserMessage);
            byte[] szJsonUser_In = GMP_Tools.GetBytesFromString(szJsonUser);
            byte[] szJsonUser_Out = new byte[Defines.GMP_TICKET_BUFFER];

            int retcode = Json_prepare_PrintUserMessage_Ex(Buffer, MaxSize, szJsonUser_In, szJsonUser_Out, szJsonUser_Out.Length, NumberOfMessage);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonUser_Out);
                pStUserMessage = JsonConvert.DeserializeObject<ST_USER_MESSAGE[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_JumpToECR(UInt64 JumpFlags, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_JumpToECR(JumpFlags, json_Out_stTicket, json_Out_stTicket.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_MultipleCommand(ref ST_MULTIPLE_RETURN_CODE[] pReturnCodes, ushort IndexOfReturnCodes, byte[] SendBuffer, UInt16 SendBufferLen, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] szJsonTicket_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] szJsonRetCodes_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_MultipleCommand(szJsonRetCodes_Out, szJsonRetCodes_Out.Length, ref IndexOfReturnCodes, SendBuffer, SendBufferLen, szJsonTicket_Out, szJsonTicket_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonRetCodes_Out);
                pReturnCodes = JsonConvert.DeserializeObject<ST_MULTIPLE_RETURN_CODE[]>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicket_Out);
                pstTicket = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_SetTaxFree(int szFlag, string szName, string szSurname, string szIdentificationNo, string szCity, string szCountry, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];

            byte[] Name = GMP_Tools.GetBytesFromString(szName);
            byte[] Surname = GMP_Tools.GetBytesFromString(szSurname);
            byte[] IdentificationNo = GMP_Tools.GetBytesFromString(szIdentificationNo);
            byte[] City = GMP_Tools.GetBytesFromString(szCity);
            byte[] Country = GMP_Tools.GetBytesFromString(szCountry);

            UInt32 retcode = Json_FiscalPrinter_SetTaxFree(szFlag, Name, Surname, IdentificationNo, City, Country, json_Out_stTicket, json_Out_stTicket.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_SetParkingTicket(string szCarIdentification, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] CarIdentification = GMP_Tools.GetBytesFromString(szCarIdentification);

            UInt32 retcode = Json_FiscalPrinter_SetParkingTicket(CarIdentification, json_Out_stTicket, json_Out_stTicket.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_SetTaxFreeRefundAmount(int RefundAmount, ushort RefundAmountCurrency, ref ST_TICKET pstTicket, int TimeoutInMiliseconds)
        {
            byte[] json_Out_stTicket = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_SetTaxFreeRefundAmount(RefundAmount, RefundAmountCurrency, json_Out_stTicket, json_Out_stTicket.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(json_Out_stTicket);
                ST_TICKET StTicketTemp = new ST_TICKET();
                StTicketTemp = JsonConvert.DeserializeObject<ST_TICKET>(retJsonString);
                MergeItemStruct(pstTicket, StTicketTemp);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionChangeTicketHeader(string szSupervisorPassword, ref ushort pNumberOfSpaceTotal, ref ushort pNumberOfSpaceUsed, ref ST_TICKET_HEADER pStTicketHeader, int TimeoutInMiliseconds)
        {
            string szJsonTicketHeader = JsonConvert.SerializeObject(pStTicketHeader);
            byte[] szJsonTicketHeader_In = GMP_Tools.GetBytesFromString(szJsonTicketHeader);
            byte[] szJsonTicketHeader_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] SupervisorPassword = GMP_Tools.GetBytesFromString(szSupervisorPassword);

            UInt32 retcode = Json_FiscalPrinter_FunctionChangeTicketHeader(SupervisorPassword, ref pNumberOfSpaceTotal, ref pNumberOfSpaceUsed, szJsonTicketHeader_In, szJsonTicketHeader_Out, szJsonTicketHeader_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicketHeader_Out);
                pStTicketHeader = JsonConvert.DeserializeObject<ST_TICKET_HEADER>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetTicketHeader(ushort IndexOfHeader, ref ST_TICKET_HEADER pStTicketHeader, ref ushort pNumberOfSpaceTotal, int TimeoutInMiliseconds)
        {
            string szJsonTicketHeader = JsonConvert.SerializeObject(pStTicketHeader);
            byte[] szJsonTicketHeader_In = GMP_Tools.GetBytesFromString(szJsonTicketHeader);
            byte[] szJsonTicketHeader_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetTicketHeader(IndexOfHeader, szJsonTicketHeader_In, szJsonTicketHeader_Out, szJsonTicketHeader_Out.Length, ref pNumberOfSpaceTotal, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonTicketHeader_Out);
                pStTicketHeader = JsonConvert.DeserializeObject<ST_TICKET_HEADER>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 Database_QueryColomnCaptions(ref ST_DATABASE_RESULT pStDatabaseResult)
        {
            byte[] szJsonDatabaseResult_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_Database_QueryColomnCaptions(szJsonDatabaseResult_Out, szJsonDatabaseResult_Out.Length);
            if (retcode == 0 || retcode == Defines.SQLITE_DONE || retcode == Defines.SQLITE_ROW)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDatabaseResult_Out);
                pStDatabaseResult = JsonConvert.DeserializeObject<ST_DATABASE_RESULT>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 Database_QueryReadLine(ushort NumberOfLinesRequested, ushort NumberOfColomnsRequested, ref ST_DATABASE_RESULT pStDatabaseResult)
        {
            byte[] szJsonDatabaseResult_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_Database_QueryReadLine(NumberOfLinesRequested, NumberOfColomnsRequested, szJsonDatabaseResult_Out, szJsonDatabaseResult_Out.Length);
            if (retcode == 0 || retcode == Defines.SQLITE_DONE || retcode == Defines.SQLITE_ROW)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDatabaseResult_Out);
                pStDatabaseResult = JsonConvert.DeserializeObject<ST_DATABASE_RESULT>(retJsonString);
            }
            return retcode;
        }

        public static void Database_FreeStructure(ref ST_DATABASE_RESULT pStDatabaseResult)
        {
            string szJsonDatabaseResult = JsonConvert.SerializeObject(pStDatabaseResult);
            byte[] szJsonDatabaseResult_In = GMP_Tools.GetBytesFromString(szJsonDatabaseResult);
            byte[] szJsonDatabaseResult_Out = new byte[Defines.GMP_TICKET_BUFFER];

            Json_Database_FreeStructure(szJsonDatabaseResult_In, szJsonDatabaseResult_Out, szJsonDatabaseResult_Out.Length);

            string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDatabaseResult_Out);
            pStDatabaseResult = JsonConvert.DeserializeObject<ST_DATABASE_RESULT>(retJsonString);
        }

        public static UInt32 Database_Execute(string szSqlWord, ref ST_DATABASE_RESULT pStDatabaseResult)
        {
            byte[] szJsonDatabaseResult_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] SqlWord = GMP_Tools.GetBytesFromString(szSqlWord);

            UInt32 retcode = Json_Database_Execute(SqlWord, szJsonDatabaseResult_Out, szJsonDatabaseResult_Out.Length);
            if (retcode == 0 || retcode == Defines.SQLITE_DONE || retcode == Defines.SQLITE_ROW)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonDatabaseResult_Out);
                pStDatabaseResult = JsonConvert.DeserializeObject<ST_DATABASE_RESULT>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetPLU(string szBarcode, ref ST_PLU_RECORD StPluRecord, ref ST_PLU_GROUP_RECORD[] StPluGroupRecord, int MaxNumberOfGroupRecords, int TimeoutInMiliseconds)
        {
            string szJsonPluRecord = JsonConvert.SerializeObject(StPluRecord);
            byte[] szJsonPluRecord_In = GMP_Tools.GetBytesFromString(szJsonPluRecord);
            byte[] szJsonPluRecord_Out = new byte[Defines.GMP_TICKET_BUFFER];
            string szJsonPluGroupRecord = JsonConvert.SerializeObject(StPluGroupRecord);
            byte[] szJsonPluGroupRecord_In = GMP_Tools.GetBytesFromString(szJsonPluGroupRecord);
            byte[] szJsonPluGroupRecord_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] Barcode = GMP_Tools.GetBytesFromString(szBarcode);

            UInt32 retcode = Json_FiscalPrinter_GetPLU(Barcode, szJsonPluRecord_In, szJsonPluRecord_Out, szJsonPluRecord_Out.Length, szJsonPluGroupRecord_In, szJsonPluGroupRecord_Out, szJsonPluGroupRecord_Out.Length, MaxNumberOfGroupRecords, TimeoutInMiliseconds);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPluRecord_Out);
                StPluRecord = JsonConvert.DeserializeObject<ST_PLU_RECORD>(retJsonString);
                retJsonString = GMP_Tools.GetStringFromBytes(szJsonPluGroupRecord_Out);
                StPluGroupRecord = JsonConvert.DeserializeObject<ST_PLU_GROUP_RECORD[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_GetVasApplicationInfo(ref byte pNumberOfTotalRecords, ref byte pNumberOfTotalRecordsReceived, ref ST_PAYMENT_APPLICATION_INFO[] StPaymentAppInfo, UInt16 vasType)
        {
            byte[] szJsonPluGroupRecord_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_GetVasApplicationInfo(ref pNumberOfTotalRecords, ref pNumberOfTotalRecordsReceived, szJsonPluGroupRecord_Out, szJsonPluGroupRecord_Out.Length, vasType);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPluGroupRecord_Out);
                StPaymentAppInfo = JsonConvert.DeserializeObject<ST_PAYMENT_APPLICATION_INFO[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionEkuSeek(ref ST_EKU_APPINF StEKUAppInfo, int TimeoutInMiliseconds)
        {
            string szJsonEKUAppInfo = JsonConvert.SerializeObject(StEKUAppInfo);
            byte[] szJsonEKUAppInfo_In = GMP_Tools.GetBytesFromString(szJsonEKUAppInfo);
            byte[] szJsonEKUAppInfo_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionEkuSeek(szJsonEKUAppInfo_In, szJsonEKUAppInfo_Out, szJsonEKUAppInfo_Out.Length, TimeoutInMiliseconds);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonEKUAppInfo_Out);
                StEKUAppInfo = JsonConvert.DeserializeObject<ST_EKU_APPINF>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FileSystem_DirListFiles(string szDirName, ref ST_FILE[] StFile, short MaxNumberOfFiles, ref short pNumberOfFiles)
        {
            string szJsonFile = JsonConvert.SerializeObject(StFile);
            byte[] szJsonFile_In = GMP_Tools.GetBytesFromString(szJsonFile);
            byte[] szJsonFile_Out = new byte[Defines.GMP_TICKET_BUFFER];
            byte[] DirName = GMP_Tools.GetBytesFromString(szDirName);

            UInt32 retcode = Json_FileSystem_DirListFiles(DirName, szJsonFile_In, szJsonFile_Out, szJsonFile_Out.Length, MaxNumberOfFiles, ref pNumberOfFiles);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonFile_Out);
                StFile = JsonConvert.DeserializeObject<ST_FILE[]>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionEkuReadHeader(short Index, ref ST_EKU_HEADER StEkuHeader, int TimeoutInMiliseconds)
        {
            string szJsonEkuHeader = JsonConvert.SerializeObject(StEkuHeader);
            byte[] szJsonEkuHeader_In = GMP_Tools.GetBytesFromString(szJsonEkuHeader);
            byte[] szJsonEkuHeader_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionEkuReadHeader(Index, szJsonEkuHeader_In, szJsonEkuHeader_Out, szJsonEkuHeader_Out.Length, TimeoutInMiliseconds);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonEkuHeader_Out);
                StEkuHeader = JsonConvert.DeserializeObject<ST_EKU_HEADER>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionEkuReadData(ref UInt16 pEkuDataBufferReceivedLen, ref ST_EKU_APPINF StEKUAppInfo, int TimeoutInMiliseconds)
        {
            string szJsonEKUAppInfo = JsonConvert.SerializeObject(StEKUAppInfo);
            byte[] szJsonEKUAppInfo_In = GMP_Tools.GetBytesFromString(szJsonEKUAppInfo);
            byte[] szJsonEKUAppInfo_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionEkuReadData(ref pEkuDataBufferReceivedLen, szJsonEKUAppInfo_In, szJsonEKUAppInfo_Out, szJsonEKUAppInfo_Out.Length, TimeoutInMiliseconds);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonEKUAppInfo_Out);
                StEKUAppInfo = JsonConvert.DeserializeObject<ST_EKU_APPINF>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionEkuReadInfo(UInt16 EkuAccessFunction, ref ST_EKU_MODULE_INFO StEkuModuleInfo, int TimeoutInMiliseconds)
        {
            string szJsonEkuModuleInfo = JsonConvert.SerializeObject(StEkuModuleInfo);
            byte[] szJsonEkuModuleInfo_In = GMP_Tools.GetBytesFromString(szJsonEkuModuleInfo);
            byte[] szJsonEkuModuleInfo_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionEkuReadInfo(EkuAccessFunction, szJsonEkuModuleInfo_In, szJsonEkuModuleInfo_Out, szJsonEkuModuleInfo_Out.Length, TimeoutInMiliseconds);

            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonEkuModuleInfo_Out);
                StEkuModuleInfo = JsonConvert.DeserializeObject<ST_EKU_MODULE_INFO>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionBankingRefund(ref ST_PAYMENT_REQUEST StReversePayment, int TimeoutInMiliseconds)
        {
            string szJsonPaymentRequest = JsonConvert.SerializeObject(StReversePayment);
            byte[] szJsonPaymentRequest_In = GMP_Tools.GetBytesFromString(szJsonPaymentRequest);
            byte[] szJsonPaymentRequest_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionBankingRefund(szJsonPaymentRequest_In, szJsonPaymentRequest_Out, szJsonPaymentRequest_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPaymentRequest_Out);
                StReversePayment = JsonConvert.DeserializeObject<ST_PAYMENT_REQUEST>(retJsonString);
            }
            return retcode;
        }

        public static UInt32 FiscalPrinter_FunctionBankingBatch(ushort BkmId, ref ushort pNumberOfBankResponse, ref ST_MULTIPLE_BANK_RESPONSE[] StMultipleBankResponse, int TimeoutInMiliseconds)
        {
            byte[] szJsonPaymentRequestMultipleBankResponse_Out = new byte[Defines.GMP_TICKET_BUFFER];

            UInt32 retcode = Json_FiscalPrinter_FunctionBankingBatch(BkmId, ref pNumberOfBankResponse, szJsonPaymentRequestMultipleBankResponse_Out, szJsonPaymentRequestMultipleBankResponse_Out.Length, TimeoutInMiliseconds);
            if (retcode == 0)
            {
                string retJsonString = GMP_Tools.GetStringFromBytes(szJsonPaymentRequestMultipleBankResponse_Out);
                StMultipleBankResponse = JsonConvert.DeserializeObject<ST_MULTIPLE_BANK_RESPONSE[]>(retJsonString);
            }
            return retcode;
        }
    }

    class IngenicoGMPSmartDLL
    {
        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionCashierLogin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionCashierLogin_WE(int CashierIndex, byte[] szCashierPassword); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionAddCashier", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionAddCashier_WE(ushort CashierIndex, byte[] szCashierName, byte[] szCashierPassword, byte[] szSupervisorPassword); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "SetIniFilePath", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetIniFilePath_WE(byte[] szPath); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_Open", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_Open_WE(byte[] szName); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FileSystem_DirChange", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FileSystem_DirChange_WE(byte[] szDirName); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FileSystem_FileRead", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FileSystem_FileRead_WE(byte[] szFileName, int Offset, int Whence, byte[] Buffer, int MaxLen, ref short pReadLen); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_SetPluDatabaseInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_SetPluDatabaseInfo_WE(byte[] szPluDbName, short szPluDbType); // Without Encoding

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_Query", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_Query_WE(byte[] szSqlWord, ref ushort pNumberOfColomns);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_SetTaxFree", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_SetTaxFree_WE(byte[] Buffer, int MaxSize, int Flag, byte[] szName, byte[] szSurname, byte[] szIdentificationNo, byte[] szCity, byte[] szCountry);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_KasaAvans", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_KasaAvans(byte[] Buffer, int MaxSize, int Amount, byte[] pCustomerName, byte[] pTckn, byte[] pVkn);

        public static UInt32 FiscalPrinter_FunctionCashierLogin(int CashierIndex, string szCashierPassword)
        {
            byte[] CashierPassword = GMP_Tools.GetBytesFromString(szCashierPassword);
            return FiscalPrinter_FunctionCashierLogin_WE(CashierIndex, CashierPassword);
        }

        public static UInt32 FiscalPrinter_FunctionAddCashier(ushort CashierIndex, string szCashierName, string szCashierPassword, string szSupervisorPassword)
        {
            byte[] CashierName = GMP_Tools.GetBytesFromString(szCashierName);
            byte[] CashierPassword = GMP_Tools.GetBytesFromString(szCashierPassword);
            byte[] SupervisorPassword = GMP_Tools.GetBytesFromString(szSupervisorPassword);

            return FiscalPrinter_FunctionAddCashier_WE(CashierIndex, CashierName, CashierPassword, SupervisorPassword);
        }

        public static void SetIniFilePath(string szPath)
        {
            byte[] Path = GMP_Tools.GetBytesFromString(szPath);
            SetIniFilePath_WE(Path);
        }

        public static UInt32 Database_Open(string szPath)
        {
            byte[] Path = GMP_Tools.GetBytesFromString(szPath);
            return Database_Open_WE(Path);
        }

        public static UInt32 FileSystem_DirChange(string szPath)
        {
            byte[] Path = GMP_Tools.GetBytesFromString(szPath);
            return FileSystem_DirChange_WE(Path);
        }

        public static UInt32 FileSystem_FileRead(string szFileName, int Offset, int Whence, byte[] Buffer, int MaxLen, ref short pReadLen)
        {
            byte[] FileName = GMP_Tools.GetBytesFromString(szFileName);
            return FileSystem_FileRead_WE(FileName, Offset, Whence, Buffer, MaxLen, ref pReadLen);
        }

        public static UInt32 FiscalPrinter_SetPluDatabaseInfo(string szPluDbName, short szPluDbType) // Without Encoding
        {
            byte[] PluDbName = GMP_Tools.GetBytesFromString(szPluDbName);
            return FiscalPrinter_SetPluDatabaseInfo_WE(PluDbName, szPluDbType);
        }

        public static UInt32 Database_Query(string szSqlWord, ref ushort pNumberOfColomns)
        {
            byte[] SqlWord = GMP_Tools.GetBytesFromString(szSqlWord);
            return Database_Query_WE(SqlWord, ref pNumberOfColomns);
        }

        public static int prepare_SetTaxFree(byte[] Buffer, int MaxSize, int Flag, string szName, string szSurname, string szIdentificationNo, string szCity, string szCountry)
        {
            byte[] Name = GMP_Tools.GetBytesFromString(szName);
            byte[] Surname = GMP_Tools.GetBytesFromString(szSurname);
            byte[] IdentificationNo = GMP_Tools.GetBytesFromString(szIdentificationNo);
            byte[] City = GMP_Tools.GetBytesFromString(szCity);
            byte[] Country = GMP_Tools.GetBytesFromString(szCountry);

            return prepare_SetTaxFree_WE(Buffer, MaxSize, Flag, Name, Surname, IdentificationNo, City, Country);
        }

        public static int prepare_KasaAvans(byte[] Buffer, int MaxSize, int Amount, string szCustomerName, string szTckn, string szVkn)
        {
            byte[] CustomerName = GMP_Tools.GetBytesFromString(szCustomerName);
            byte[] Tckn = GMP_Tools.GetBytesFromString(szTckn);
            byte[] Vkn = GMP_Tools.GetBytesFromString(szVkn);

            return prepare_KasaAvans(Buffer, MaxSize, Amount, CustomerName, Tckn, Vkn);
        }


        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetTagName", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte[] GetTagName(UInt32 Tag);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetErrorMessage", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetErrorMessage(UInt32 ErrorCode, byte[] Buffer);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_DisplayPaymentSummary", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FiscalPrinter_DisplayPaymentSummary(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionCashierLogout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FiscalPrinter_FunctionCashierLogout();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_GetHandle", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong FiscalPrinter_GetHandle();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_TicketHeader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_TicketHeader(TTicketType TicketType, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_PrintTotalsAndPayments", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_PrintTotalsAndPayments(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_PrintBeforeMF", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_PrintBeforeMF(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_PrintMF", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_PrintMF(int TimeoutInMiliseconds);  //TIMEOUT_PRINT_MF

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_Ping", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_Ping(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_Start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_Start(byte[] pUniqueId, int LengthOfUniqueId, byte[] pUniqueIdSign, int LengthOfUniqueIdSign, byte[] pUserData, int LengthOfUserData, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_Close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_Close(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionEcrParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionEcrParameter(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionBankingParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionBankingParameter(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionOpenDrawer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionOpenDrawer();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_GetCurrentFiscalCounters", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_GetCurrentFiscalCounters(ref ushort pZNo, ref ushort pFNo, ref ushort pEKUNo);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_GetHandle", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_GetHandle();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_Close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_Close();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_QueryReset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_QueryReset();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "Database_QueryFinish", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 Database_QueryFinish();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionEkuPing", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_FunctionEkuPing(int TimeoutInMiliseconds);

        //[DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionEkuPresent", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int FiscalPrinter_FunctionEkuPresent(int TimeoutInMiliseconds);

        //[DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_FunctionEkuReset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int FiscalPrinter_FunctionEkuReset(int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_GetTlvData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_GetTlvData(int Tag, byte[] pData, short MaxBufferLen, ref short pDataLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FileSystem_FileRemove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FileSystem_FileRemove(byte[] pFileName);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FileSystem_FileRename", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FileSystem_FileRename(byte[] pFileNameOld, byte[] pFileNameNew);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FileSystem_FileWrite", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FileSystem_FileWrite(byte[] pFileName, int Offset, int Whence, byte[] Buffer, short Len);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GMP_GetDllVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GMP_GetDllVersion(byte[] pVersion);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_GetPluDatabaseInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_GetPluDatabaseInfo(byte[] pPluDbName, ref short pPluDbType, ref uint pPluDbSize, ref uint pPluDbGrupsLastModified, ref uint pPluDbItemsLastModified);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpReadTLVlen_HL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 gmpReadTLVlen_HL(ref ushort pLen, byte[] pPtr, ushort PtrLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpReadTLVtag_HL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 gmpReadTLVtag(ref uint pTag, byte[] pPtr, ushort PtrLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpTlvSetLen", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 gmpTlvSetLen(byte[] pMsg, ushort TlvLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSetTLV_HL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 gmpSetTLV_HL(byte[] pMsg, int pMsgLen, uint Tag, byte[] pdata, ushort dataLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSetTLVbcd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 gmpSetTLVbcd(byte[] pMsg, uint Tag, uint Data, ushort BcdLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSearchTLV", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gmpSearchTLV(uint Tag, int Ocurience, byte[] RecvBuffer, ushort RecvBufferLen, byte[] pData, ushort DataMaxLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSearchTLVbcd_8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gmpSearchTLVbcd_8(uint Tag, int Ocurience, byte[] RecvBuffer, ushort RecvBufferLen, byte[] pData, byte BcdLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSearchTLVbcd_16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gmpSearchTLVbcd_16(uint Tag, int Ocurience, byte[] RecvBuffer, ushort RecvBufferLen, byte[] pData, byte BcdLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSearchTLVbcd_32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gmpSearchTLVbcd_32(uint Tag, int Ocurience, byte[] RecvBuffer, ushort RecvBufferLen, byte[] pData, byte BcdLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "gmpSearchTLVbcd_64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int gmpSearchTLVbcd_64(uint Tag, int Ocurience, byte[] RecvBuffer, ushort RecvBufferLen, byte[] pData, byte BcdLen);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Start(byte[] Buffer, int MaxSize, byte[] pUniqueId, int LengthOfUniqueId, byte[] pUniqueIdSign, int LengthOfUniqueIdSign, byte[] pUserData, int LengthOfUserData);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_TicketHeader", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_TicketHeader(byte[] Buffer, int MaxSize, TTicketType TicketType);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Close(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_OptionFlags", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_OptionFlags(byte[] Buffer, int MaxSize, ulong FlagsToBeSet, ulong FlagsToBeClear);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_PrintBeforeMF", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_PrintBeforeMF(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_PrintMF", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_PrintMF(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_SetParkingTicket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_SetParkingTicket(byte[] Buffer, int MaxSize, byte[] pCarIdentification);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_PrintTotalsAndPayments", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_PrintTotalsAndPayments(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_VoidItem", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_VoidItem(byte[] Buffer, int MaxSize, ushort Index, long ItemCount, byte ItemCountPrecision);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Matrahsiz", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Matrahsiz(byte[] Buffer, int MaxSize, byte[] pTckNo, long MatrahsizAmount);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Pretotal", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Pretotal(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_DisplayPaymentSummary", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_DisplayPaymentSummary(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Plus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Plus(byte[] Buffer, int MaxSize, int Amount, UInt16 IndexOfItem);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Minus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Minus(byte[] Buffer, int MaxSize, int Amount, ushort IndexOfItem);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Inc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Inc(byte[] Buffer, int MaxSize, byte Rate, ushort IndexOfItem);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Dec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Dec(byte[] Buffer, int MaxSize, byte Rate, ushort IndexOfItem);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_VoidPayment", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_VoidPayment(byte[] Buffer, int MaxSize, ushort Index);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_VoidAll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_VoidAll(byte[] Buffer, int MaxSize);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_JumpToECR", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_JumpToECR(byte[] Buffer, int MaxSize, ulong JumpFlags);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_SetTaxFreeRefundAmount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_SetTaxFreeRefundAmount(byte[] Buffer, int MaxSize, int RefundAmount, ushort RefundAmountCurrency);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Text", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Text(byte[] Buffer, int MaxSize, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Amount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Amount(byte[] Buffer, int MaxSize, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, int MaxLenOfValue, byte[] pSymbol, byte Align, int TimeoutInMiliseconds);
        //[DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Menu", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int prepare_Menu(byte[] Buffer, int MaxSize, UInt32 tag_id, byte typeOfMenu, char* title, const char * const * menu, ref UInt32 pvalue, byte buttons, int timeout );

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_Password", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_Password(byte[] Buffer, int MaxSize, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, UInt16 MaxLenOfValue, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "prepare_MsgBox", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int prepare_MsgBox(byte[] Buffer, int MaxSize, UInt32 TagId, byte[] pTitle, byte[] pText, byte Icon, byte Button, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_ResetHandle", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FiscalPrinter_ResetHandle();

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_SetuApplicationFunction", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_SetuApplicationFunction(byte[] pUApplicationName, UInt32 UApplicationId, byte[] pFunctionName, UInt32 FunctionId, UInt32 FunctionVersion, UInt32 FunctionFlags, byte[] pCommandBuffer, UInt32 CommandLen, string szSupervisorPassword);

        //[DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_MultipleCommand_HL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetDialogInput_MultipleCommand_HL([In, Out] ST_MULTIPLE_RETURN_CODE[] pReturnCodes, ushort IndexOfReturnCodes, ref UInt16 pNumberOfreturnCodes, byte[] pSendBuffer, UInt16 SendBufferLen, ref ST_TICKET pstTicket, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_Text", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDialogInput_Text(ref UInt32 pGL_Dialog_retcode, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, int MaxLenOfValue, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_Date", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDialogInput_Date(ref UInt32 pGL_Dialog_retcode, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, ref ST_DATE_TIME pValue, int MaxLenOfValue, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_Amount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDialogInput_Amount(ref UInt32 pGL_Dialog_retcode, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, int MaxLenOfValue, byte[] pSymbol, byte Align, int TimeoutInMiliseconds);
        //[DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_Menu", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int GetDialogInput_Menu( ref UInt32 pGL_Dialog_retcode, UInt32  TagId, uint8 typeOfMenu, char* title, const char * const * menu, uint32* pvalue, uint8 buttons, int TimeoutInMiliseconds );

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_MsgBox", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDialogInput_MsgBox(ref UInt32 pGL_Dialog_retcode, UInt32 TagId, byte[] pTitle, byte[] pText, byte Icon, byte Button, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "GetDialogInput_Password", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetDialogInput_Password(ref UInt32 pGL_Dialog_retcode, UInt32 TagId, byte[] pTitle, byte[] pText, byte[] pMask, byte[] pValue, int MaxLenOfValue, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_OptionFlags", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_OptionFlags(ref UInt64 pFlagsActive, UInt64 FlagsToBeSet, UInt64 FlagsToBeClear, int TimeoutInMiliseconds);

        [DllImport("GmpSmartDLL.dll", EntryPoint = "FiscalPrinter_SetTlvData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 FiscalPrinter_SetTlvData(UInt32 Tag, byte[] pData, UInt16 Len);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_open")]
        public static extern int sqlite3_open(string szFilename, out IntPtr pDb);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_close")]
        public static extern int sqlite3_close(IntPtr pDb);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_prepare_v2")]
        public static extern int sqlite3_prepare_v2(IntPtr pDb, string szSql, int nByte, out IntPtr ppStmpt, IntPtr pzTail);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_step")]
        public static extern int sqlite3_step(IntPtr stmHandle);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_finalize")]
        public static extern int sqlite3_finalize(IntPtr stmHandle);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_errmsg")]
        public static extern string sqlite3_errmsg(IntPtr db);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_count")]
        public static extern int sqlite3_column_count(IntPtr stmHandle);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_origin_name")]
        public static extern System.IntPtr sqlite3_column_origin_name(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_type")]
        public static extern int sqlite3_column_type(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_int")]
        public static extern int sqlite3_column_int(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_text")]
        public static extern System.IntPtr sqlite3_column_text(IntPtr stmHandle, int iCol);

        [DllImport("sqlite3.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_column_double")]
        public static extern double sqlite3_column_double(IntPtr stmHandle, int iCol);




    }

    class Defines
    {

        public const string m_deptIndCol = "Dept Index";
        public const string m_taxIndCol = "Tax Index";
        public const string m_nameCol = "Name";
        public const string m_unitCol = "Unit Type";
        public const string m_amountCol = "Amount";
        public const string m_currencyCol = "Currency";
        public const string m_limitAmountCol = "Limit Amount";

        public const int TIMEOUT_DEFAULT = 10000;	// 10 seconds
        public const int TIMEOUT_CARD_TRANSACTIONS = 100000;	// 100 seconds
        public const int TIMEOUT_ECHO = 10000;	// 10 seconds
        public const int TIMEOUT_PRINT_MF = 20000;	// 20 seconds
        public const int TIMEOUT_DATABASE_EXECUTE = 20000;	// 20 seconds
        public const int MAX_UNIQUE_ID = 256;
        public const string DLL_VERSION_MIN = "1602030800";

        public const uint BANK_TRAN_FLAG_DO_NOT_ASK_FOR_MISSING_LOYALTY_POINT = 0x04000000;	    // 0x04000000 Do not ask for missing loyalty point
        public const uint BANK_TRAN_FLAG_ALL_INPUT_FROM_EXTERNAL_SYSTEM = 0x08000000;           // 0x08000000 All Input from ECR
        public const uint BANK_TRAN_FLAG_ASK_FOR_MISSING_REFUND_INPUTS = 0x10000000;	        // 0x10000000 Ask for Missing Refund Inputs
        public const uint BANK_TRAN_FLAG_LOYALTY_POINT_NOT_SUPPORTED_FOR_TRANS = 0x20000000;	// 0x20000000 Loyalty point not supported for transaction.
        public const uint BANK_TRAN_FLAG_ONLINE_FORCED_TRANSACTION = 0x40000000;	            // 0x40000000 Reserved for internal use.
        public const uint BANK_TRAN_FLAG_MANUAL_PAN_ENTRY_NOT_ALLOWED = 0x80000000;	            // 0x80000000 Manual pan entry not allowed for transaction.
        public const uint BANK_TRAN_FLAG_AUTHORISATION_FOR_INVOICE_PAYMENT = 0x02000000;
        public const uint BANK_TRAN_FLAG_SALE_WITHOUT_CAMPAIGN = 0x01000000;


        public const int EKU_SEEK_MODE_ALL_TYPE = 0xFF;

        public const int STANDART_BUFFER = 50000;
        public const int GMP_TICKET_BUFFER = 200000;

        public const int GMP_EXT_DEVICE_FILEDIR_BITMAP = 0xDFEDF8;
        public const int GMP_EXT_DEVICE_TAG_Z_NO = 0xDFED06;
        public const int GMP_EXT_DEVICE_TAG_TRAN_DB_NAME = 0xDFEE55;
        public const int GMP_EXT_DEVICE_FIS_LIMIT = 0xDFED54;
        public const int GMP_TAG_GROUP_OKC_DOVIZ_TABLOSU = 0xDF79;


        // Jump Flags for GMP3
        public const int GMP3_OPTION_RETURN_AFTER_SINGLE_PAYMENT = (1 << 1);
        public const int GMP3_OPTION_RETURN_AFTER_COMPLETE_PAYMENT = (1 << 2);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_ITEM = (1 << 3);
        public const int GMP3_OPTION_DONT_ALLOW_VOID_ITEM = (1 << 4);
        public const int GMP3_OPTION_DONT_ALLOW_VOID_PAYMENT = (1 << 5);
        public const int GMP3_OPTION_CONTINUE_IN_OFFLINE_MODE = (1 << 6);
        public const int GMP3_OPTION_DONT_SEND_TRANSACTION_RESULT = (1 << 7);

        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_CASH_TL = (1 << 17);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_CASH_EXCHANGE = (1 << 18);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_BANKCARD = (1 << 19);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_YEMEKCEKI = (1 << 20);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_MOBILE = (1 << 21);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_HEDIYECEKI = (1 << 22);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_IKRAM = (1 << 23);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_ODEMESIZ = (1 << 24);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_KAPORA = (1 << 25);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_PUAN = (1 << 26);
        public const int GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT = (GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_CASH_TL | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_CASH_EXCHANGE | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_BANKCARD | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_YEMEKCEKI | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_MOBILE | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_HEDIYECEKI | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_IKRAM | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_ODEMESIZ | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_KAPORA | GMP3_OPTION_DONT_ALLOW_NEW_PAYMENT_PUAN);

        public const int MAX_TAXRATE_COUNT = 8;
        public const int MAX_DEPARTMENT_COUNT = 12;
        public const int MAX_EXCHANGE_COUNT = 6;
        public const int MAX_CASHIER_COUNT = 4;
        public const int MAX_CINEMA_DEPARTMENT_COUNT = 8;


        public const int GMP3_FISCAL_PRINTER_MODE_REQ = 0xFF8A89;	/**< Request Msg Id for FISCAL PRINTER */
        public const int GMP3_FISCAL_PRINTER_MODE_REQ_E = 0xFF8B89;	/**< Request(Encrypted) Msg Id for FISCAL PRINTER */
        public const int GMP3_FISCAL_PRINTER_MODE_RES = 0xFF8E89;	/**< Response Msg Id for FISCAL PRINTER */
        public const int GMP3_FISCAL_PRINTER_MODE_RES_E = 0xFF8F89;	/**<  Response(Encrypted) Msg Id for FISCAL PRINTER */

        public const int GMP3_EXT_DEVICE_GET_DATA_REQ = 0xFF8A80;	    /**< Request Msg Id for GET DATA */
        public const int GMP3_EXT_DEVICE_GET_DATA_REQ_E = 0xFF8B80;	    /**< Request(Encrypted) Msg Id for GET DATA */
        public const int GMP3_EXT_DEVICE_GET_DATA_RES = 0xFF8E80;	    /**< Response Msg Id for GET DATA */
        public const int GMP3_EXT_DEVICE_GET_DATA_RES_E = 0xFF8F80;	    /**< Response(Encrypted) Msg Id for GET DATA  */
        public const int GMP_EXT_DEVICE_FLIGHT_MODE = 0xDFEE69;     /**< Flight Mode Tag,   (uint8 1 byte)*/
        public const int GMP_EXT_DEVICE_TICKET_TIMEOUT = 0xDFEE6A;     /**< Timeout between bank tickets,   (uint8 1 byte)*/
        public const int GMP_EXT_DEVICE_COMM_STATUS = 0xDFEE6B;     /**< Communication Status Gprs,Flight Mode, Ethernet*/
        public const int GMP_EXT_DEVICE_COMM_SCENARIO = 0xDFEE6C;		/**< 0XDFEE6C, Communication Scenario Gprs, Ethernet, Gprs&Ethernet */
        public const int GMP_EXT_DEVICE_STAND_BY_TIME = 0xDFEE6D;     /**< 0xDFEE6D, Set standby time value */

        public const byte GMP3_STATE_BIT_FLIGHT_MODE = (1 << 0); /**< State of flight mode*/
        public const byte GMP3_STATE_BIT_GPRS_CONNECTED = (1 << 1); /**<  State of GPRS connection*/
        public const byte GMP3_STATE_BIT_ETHERNET_CONNECTED = (1 << 2); /**<  State of Ethernet connection*/

        public const int GMP3_OPTION_ECHO_PRINTER = 0;//(1 << 0);
        public const int GMP3_OPTION_ECHO_PAYMENT_DETAILS = (1 << 1);
        public const int GMP3_OPTION_ECHO_ITEM_DETAILS = (1 << 2);
        public const int GMP3_OPTION_NO_RECEIPT_LIMIT_CONTROL_FOR_ITEMS = (1 << 3);
        public const int GMP3_OPTION_DONOT_CONTROL_PAYMENTS_FOR_RECEIPT_CANCEL = (1 << 4);
        public const int GMP3_OPTION_GET_CONFIRMATION_FOR_PAYMENT_CANCEL = (1 << 5);

        public const int TRAN_RESULT_OK = 0x0000;
        public const int TRAN_RESULT_NOT_ALLOWED = 0x0001;
        public const int TRAN_RESULT_TIMEOUT = 0x0002;
        public const int TRAN_RESULT_USER_ABORT = 0x0004;
        public const int TRAN_RESULT_EKU_PROBLEM = 0x0008;
        public const int TRAN_RESULT_CONTINUE = 0x0010;
        public const int TRAN_RESULT_NO_PAPER = 0x0020;
        public const int APP_ERR_GMP3_INVALID_HANDLE = 2317;                      // Handle var fakat yanlış
        public const int APP_ERR_ALREADY_DONE = 2080;
        public const int APP_ERR_PAYMENT_NOT_SUCCESSFUL_AND_NO_MORE_ERROR_CODE = 2085;
        public const int APP_ERR_PAYMENT_NOT_SUCCESSFUL_AND_MORE_ERROR_CODE = 2086;
        public const int DLL_RETCODE_RECV_BUSY = 0xF01C;
        public const int APP_ERR_TICKET_HEADER_ALREADY_PRINTED = 2078;
        public const int APP_ERR_TICKET_HEADER_NOT_PRINTED = 2077;
        public const int APP_ERR_FIS_LIMITI_ASILAMAZ = 2067;
        public const int DLL_RETCODE_TIMEOUT = 0xF003;
        public const int APP_ERR_FILE_EOF = 2226;


        public const int SQLITE_OK = 0;		                //!< Successful result
        public const int SQLITE_ROW = 100;		            //!< SQLITE_step() has another row ready
        public const int SQLITE_DONE = 101;		            //!< SQLITE_step() has finished executing

        public const int SQLITE_INTEGER = 1;
        public const int SQLITE_FLOAT = 2;
        public const int SQLITE_TEXT = 3;
        public const int SQLITE_BLOB = 4;
        public const int SQLITE_NULL = 5;

        // Printer option definition
        public const int PS_24 = 0;
        public const int PS_12 = 1 << 0;
        public const int PS_32 = 1 << 1;
        public const int PS_48 = 1 << 2;
        public const int PS_BOLD = 1 << 3;
        public const int PS_CENTER = 1 << 4;
        public const int PS_RIGHT = 1 << 5;
        public const int PS_INVERTED = 1 << 6;
        public const int PS_UNIQUE_ID = 1 << 7;
        public const int PS_BARCODE = 1 << 8;
        public const int PS_ECR_TICKET_HEADER = 1 << 9;
        public const int PS_GRAPHIC = 1 << 10;
        public const int PS_QRCODE = 1 << 11;
        public const int PS_16 = 1 << 12;
        public const int PS_38 = 1 << 13;
        //public const int PS_ECR_TICKET_ITEM = 1 << 14;  // DONT USE
        //public const int PS_NO_BOS = 1 << 15;
        public const int PS_ECR_TICKET_ITEM = 1 << 16;
        public const int PS_ECR_TICKET_COPY = 1 << 17;
        public const int PS_ECR_USER_MSG_BEFORE_HEADER = 1 << 18;
        public const int PS_ECR_USER_MSG_AFTER_TOTALS = 1 << 19;
        public const int PS_ECR_USER_MSG_BEFORE_MF = 1 << 20;
        public const int PS_ECR_USER_MSG_AFTER_MF = 1 << 21;
        public const int PS_NO_PAPER_CHECK = 1 << 22;
        public const int PS_FEED_LINE = 1 << 23;

        public const int ITEM_TYPE_FREE = 0;
        public const int ITEM_TYPE_DEPARTMENT = 1;
        public const int ITEM_TYPE_PLU = 2;
        public const int ITEM_TYPE_TICKET = 3;
        public const int ITEM_TYPE_MONEYCOLLECTION = 9;

        public const int DLL_RETCODE_FAIL = 0xF00B;

        public const int PAYMENT_SUBTYPE_PROCESS_ON_POS = 0x00000000;
        public const int PAYMENT_SUBTYPE_SALE = 0x00000001;
        public const int PAYMENT_SUBTYPE_INSTALMENT_SALE = 0x00000002;
        public const int PAYMENT_SUBTYPE_LOYALTY_PUAN = 0x00000003;
        public const int PAYMENT_SUBTYPE_ADVANCE_REFUND = 0x00000004;
        public const int PAYMENT_SUBTYPE_INSTALLMENT_REFUND = 0x00000005;
        public const int PAYMENT_SUBTYPE_REFERENCED_REFUND = 0x00000006;
        public const int PAYMENT_SUBTYPE_REFERENCED_REFUND_WITH_CARD = 0x00000007;
        public const int PAYMENT_SUBTYPE_REFERENCED_REFUND_WITHOUT_CARD = 0x00000008;

        public const int APP_ERR_FISCAL_INVALID_ENTRY = 2009;

        public const int FLAG_SETSCENARIO_ETHERNET = 1;
        public const int FLAG_SETSCENARIO_GPRS = 2;
        public const int FLAG_SETSCENARIO_ETHERNET_GPRS = 3;

    }
}