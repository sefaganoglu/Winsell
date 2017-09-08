using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Winsell.YK.Ingenico
{
    class IngenicoErrorClass
    {
        public static void DisplayErrorMessage(UInt32 errorCode)
        {
            byte[] TempErrorBuffer = new byte[256];

            IngenicoGMPSmartDLL.GetErrorMessage(errorCode, TempErrorBuffer);

            clsCihazIngenico.TransactionInfo("Hata Kodu = 0x" + errorCode.ToString("X2").PadLeft(4, '0') + " : " + GMP_Tools.SetEncoding(TempErrorBuffer));
        }
    }
}
