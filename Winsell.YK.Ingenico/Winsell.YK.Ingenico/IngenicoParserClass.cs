using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Winsell.YK.Ingenico
{
    class IngenicoParserClass
    {
        public const string ECR_SERVICE_MODE = "0";
        public const string ECR_USER_MODE = "1";
        public const string ECR_BLOCKED_MODE = "8";
        public const string ECR_MAINTENANCE_MODE = "9";
        public const string ECR_DEMO_MODE = "10";            //0a şeklinde gelme ihtimali var
        
        public byte[] GMP_AscToBcd(string Value)
        {
            byte[] tempbuf = new byte[32];
            byte[] tempbufbcd = new byte[16];
            Encoding asen = Encoding.Default;

            ushort fieldlen = (ushort)Value.Length;
            tempbuf = asen.GetBytes(Value);
            Utility.GMP_AscToBcd(ref tempbufbcd, (ushort)(fieldlen / 2), ref tempbuf, (ushort)fieldlen);

            return tempbufbcd;
        }

        public static void EchoStructParser(ST_ECHO echoStr)
        {
            DisplayStruct("dll Version", "10", GMP_Tools.SetEncoding(clsCihazIngenico.m_dllVersion), "DLL Version");
            DisplayStruct("retcode", Marshal.SizeOf(echoStr.retcode).ToString("X2"), echoStr.retcode.ToString("X8"), "Hata Cevap Kodu");
            DisplayStruct("MTU Size", Marshal.SizeOf(echoStr.mtuSize).ToString("X2"), echoStr.mtuSize.ToString("X4"), "MTU Size");
            DisplayStruct("status", Marshal.SizeOf(echoStr.status).ToString("X2"), echoStr.status.ToString("X8"), "Status Bitleri");
            DisplayStruct("kcv", "8", GetValOfStructMember(echoStr.kvc, 8), "Key Check Val");
            ParseStatusInfo(echoStr.status);
            switch (echoStr.ecrMode.ToString())
            {
                case ECR_SERVICE_MODE:
                    DisplayStruct("mode", "1", "ECR_SERVICE_MODE _ 0", "Mod");
                    break;
                case ECR_USER_MODE:
                    DisplayStruct("mode", "1", "ECR_USER_MODE _ 1", "Mod");
                    break;
                case ECR_BLOCKED_MODE:
                    DisplayStruct("mode", "1", "ECR_BLOCKED_MODE _ 8", "Mod");
                    break;
                case ECR_MAINTENANCE_MODE:
                    DisplayStruct("mode", "1", "ECR_MAINTENANCE_MODE _ 9", "Mod");
                    break;
                case ECR_DEMO_MODE:
                    DisplayStruct("mode", "1", "ECR_DEMO_MODE _ 10", "Mod");
                    break;
                default:
                    DisplayStruct("mode", "1", "ECR_INVALID_MODE", "Mod");
                    break;
            }

            DisplayStruct("Date", "3", echoStr.date.day.ToString().PadLeft(2, '0') + "/" + echoStr.date.month.ToString().PadLeft(2, '0') + "/" + echoStr.date.year.ToString().PadLeft(2, '0'), "ECR Date");
            DisplayStruct("Time", "3", echoStr.time.hour.ToString().PadLeft(2, '0') + ":" + echoStr.time.minute.ToString().PadLeft(2, '0') + ":" + echoStr.time.second.ToString().PadLeft(2, '0'), "ECR Time");
            DisplayStruct("version", "16", GMP_Tools.SetEncoding(echoStr.ecrVersion), "ECR SW Version");
            DisplayStruct("index", Marshal.SizeOf(echoStr.activeCashier.index).ToString("X2"), echoStr.activeCashier.index.ToString("X4"), "Kasiyer Index");
            DisplayStruct("name", "12", echoStr.activeCashier.name, "Kasiyer Isim");
            DisplayStruct("flags", Marshal.SizeOf(echoStr.activeCashier.flags).ToString("X2"), echoStr.activeCashier.flags.ToString("X8"), "Kasiyer Flag");
        }

        public static void PairingStructParser(ST_GMP_PAIR_RESP pairingResp)
        {
            DisplayStruct("Ret Code", "2", GetValOfStructMember(pairingResp.Out_ErrorRespCode, 2), "Hata Cevap Kodu");
            DisplayStruct("Proc Order Num", "3", GetValOfStructMember(pairingResp.Out_ProcOrderNum, 3), "İşlem Sıra Numarası");
            DisplayStruct("Proc Date", "3", GetValOfStructMember(pairingResp.Out_ProcDate, 3), "İşlem Tarihi");
            DisplayStruct("Proc Time", "3", GetValOfStructMember(pairingResp.Out_ProcTime, 3), "İşlem Saati");
            DisplayStruct("Device Brand", "16", GetValOfStructMemberAscii(pairingResp.Out_DeviceBrand, 16), "Cihaz Markası");
            DisplayStruct("Device Model", "16", GetValOfStructMemberAscii(pairingResp.Out_DeviceModel, 16), "Cihaz Modeli");
            DisplayStruct("Device Serial Num", "16", GetValOfStructMemberAscii(pairingResp.Out_DeviceSerialNum, 16), "Cihaz Seri No");
            DisplayStruct("Status Code", "2", GetValOfStructMember(pairingResp.Out_StatusCode, 2), "Status");
            DisplayStruct("Version Num", "16", GetValOfStructMember(pairingResp.Out_VersionNumber, 16), "Version No");
        }

        public static string GetValOfStructMember(byte[] member, int len)
        {
            string str = "";

            for (int i = 0; i < len; i++)
            {
                str += member[i].ToString("X2");
            }
            return str;
        }

        public static string GetValOfStructMemberAscii(byte[] member, int len)
        {
            string str = "";
            byte[] new_arr = new byte[len];

            int j = 0;
            for (int i = 0; i < len; i++)
            {
                if (member[i] != 0x00)
                {
                    new_arr[j] = member[i];
                    j++;
                }
            }
            str += GMP_Tools.SetEncoding(new_arr);

            //for (int i = 0; i < len; i++)
            //{
            //    str += member[i].ToString("X2");
            //}
            return str;
        }




        public static void ParseDeviceModeInfo(byte[] mode)
        {


            //int m = BitConverter.ToInt32(mode, 0);
            //string binValuem = Convert.ToString(mode, 10);
            string binValuem = "";

            for (int i = 0; i < 1; i++)
            {
                binValuem += mode[i].ToString();
            }



            switch (binValuem)
            {
                case ECR_SERVICE_MODE:
                    DisplayStruct(" set bit : " + ECR_SERVICE_MODE, "  -- ", "  -- ", "  " + "ECR_SERVICE_MODE");
                    break;
                case ECR_USER_MODE:
                    DisplayStruct(" set bit : " + ECR_USER_MODE, "  -- ", "  -- ", "  " + "ECR_USER_MODE");
                    break;
                case ECR_BLOCKED_MODE:
                    DisplayStruct(" set bit : " + ECR_BLOCKED_MODE, "  -- ", "  -- ", "  " + "ECR_BLOCKED_MODE");
                    break;
                case ECR_MAINTENANCE_MODE:
                    DisplayStruct(" set bit : " + ECR_MAINTENANCE_MODE, "  -- ", "  -- ", "  " + "ECR_MAINTENANCE_MODE");
                    break;
                case ECR_DEMO_MODE:
                    DisplayStruct(" set bit : " + ECR_DEMO_MODE, "  -- ", "  -- ", "  " + "ECR_DEMO_MODE");
                    break;
                default:
                    DisplayStruct(" set bit : " + "-1", "  -- ", "  -- ", "  " + "ECR_INVALID_MODE");
                    break;
            }



        }

        public static void ParseStatusInfo(uint statusInfo)
        {

            string binValuem = Convert.ToString(statusInfo, 2);

            string[] arrm = new string[] { "GMP3_STATE_BIT_PAIRED", "GMP3_STATE_BIT_KEY_EXPIRED", "GMP3_STATE_BIT_FISCALIZED", "GMP3_STATE_BIT_OKC_MALI_MOD", "GMP3_STATE_BIT_PARAMETRE", "GMP3_STATE_BIT_NO_PAPER" };

            int bitNum = 50;
            for (int j = 0; j < binValuem.Length; j++)
            {
                bitNum = Convert.ToInt32(binValuem.Substring(binValuem.Length - 1 - j, 1));

                switch (bitNum)
                {
                    case 0:
                        break;
                    case 1:
                        DisplayStruct(" set bit : " + j.ToString(), "  -- ", "  -- ", "  " + arrm[j]);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void DisplayStruct(string memberName, string len, string value, string description)
        {
            clsCihazIngenico.rtbLog.SelectedText = string.Empty;
            clsCihazIngenico.rtbLog.SelectionFont = new Font(clsCihazIngenico.rtbLog.SelectionFont, FontStyle.Bold);
            clsCihazIngenico.rtbLog.SelectionColor = Color.Red;
            if (!string.IsNullOrEmpty(clsCihazIngenico.rtbLog.Text.Trim())) clsCihazIngenico.rtbLog.AppendText(Environment.NewLine + Environment.NewLine);
            clsCihazIngenico.rtbLog.AppendText("Member: " + memberName);
            clsCihazIngenico.rtbLog.AppendText(Environment.NewLine);
            clsCihazIngenico.rtbLog.AppendText("Length: " + len);
            clsCihazIngenico.rtbLog.AppendText(Environment.NewLine);
            clsCihazIngenico.rtbLog.AppendText("Value: " + value);
            clsCihazIngenico.rtbLog.AppendText(Environment.NewLine);
            clsCihazIngenico.rtbLog.AppendText("Description: " + description);
            clsCihazIngenico.rtbLog.ScrollToCaret();
        }
    }
}
