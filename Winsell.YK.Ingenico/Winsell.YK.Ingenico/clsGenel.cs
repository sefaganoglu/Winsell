using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Winsell.YK.Ingenico
{
    public static class clsGenel
    {
        public static void DirectoryControl(string strPath)
        {
            string[] arrSplit = strPath.Split('\\');
            string strPathConfig = "";
            for (int i = 0; i < arrSplit.Length; i++)
            {
                strPathConfig += arrSplit[i] + "\\";
                if (!Directory.Exists(strPathConfig))
                {
                    Directory.CreateDirectory(strPathConfig);
                }
            }
        }
    }
}
