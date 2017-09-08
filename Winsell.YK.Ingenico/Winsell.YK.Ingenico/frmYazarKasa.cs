using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;

namespace Winsell.YK.Ingenico
{
    public partial class frmYazarKasa : Form
    {
        public frmYazarKasa()
        {
            InitializeComponent();
        }

        bool blnDevam = true;
        int intCihazTipi = ConfigurationManager.AppSettings["CihazTipi"].TOINTEGER();
        private void frmCihaz_Load(object sender, EventArgs e)
        {
            clsCihazIngenico.prcdCreate(rtbLog);

            tSatis.Enabled = true;
        }

        private void btnBelgeIptal_Click(object sender, EventArgs e)
        {
            clsCihazIngenico.prcdBelgeIptal();
        }

        private void tSatis_Tick(object sender, EventArgs e)
        {
            clsGenel.DirectoryControl(ConfigurationManager.AppSettings["FilePath"]);
            tSatis.Enabled = false;
            string[] arrFiles = Directory.GetFiles(ConfigurationManager.AppSettings["FilePath"]);
            try
            {
                foreach (string strFile in arrFiles)
                {
                    string[] arrLines = File.ReadAllLines(strFile, Encoding.Default);
                    foreach (string strLine in arrLines)
                    {
                        if (strLine.Split('#')[0] == "ODM")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            if (strLineMember.Split('|')[0] == "05" || strLineMember.Split('|')[0] == "06" || strLineMember.Split('|')[0] == "07")
                            {
                                clsCihazIngenico.prcdYemekCekiBaslat();
                            }
                            break;
                        }
                    }

                    ushort intRowIndex = 0;
                    foreach (string strLine in arrLines)
                    {
                        //if (strLine.Split('#')[0] == "HDR")
                        //{
                        //    string strLineMember = strLine.Split('#')[1];
                        //    if (intCihazTipi == 1)
                        //    {
                        //        clsCihazOlivetti.prcdBaslikDegistir(strLineMember.Split('|'));
                        //    }
                        //}

                        //if (!blnYemekCekiBasladi)
                        //{
                        //    if (strLine.Split('#')[0] == "BGN")
                        //    {
                        //        if (intCihazTipi == 0)
                        //        {
                        //            clsCihazHugin.prcdBelgeBaslat();
                        //        }
                        //        else if (intCihazTipi == 1)
                        //        {
                        //            clsCihazOlivetti.prcdBelgeBaslat();
                        //        }
                        //    }
                        //}

                        if (strLine.Split('#')[0] == "STS")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            clsCihazIngenico.prcdSatisYap(strLineMember.Split('|')[1],
                                                          strLineMember.Split('|')[3],
                                                          strLineMember.Split('|')[4],
                                                          strLineMember.Split('|')[5],
                                                          strLineMember.Split('|')[6].TODOUBLE(),
                                                          strLineMember.Split('|')[7].TODOUBLE());

                            intRowIndex++;
                        }

                        if (strLine.Split('#')[0] == "IND")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            byte bytIndirimTipi = strLineMember.Split('|')[0].TOBYTE();
                            clsCihazIngenico.prcdIskontoYap(strLineMember.Split('|')[3],
                                                            (bytIndirimTipi == 0 ? (intRowIndex - 1).TOUSHORT() : 0xFFFF.TOUSHORT()),
                                                            strLineMember.Split('|')[7].TODOUBLE());
                        }

                        if (strLine.Split('#')[0] == "ART")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            byte bytArttirimTipi = strLineMember.Split('|')[0].TOBYTE();
                            clsCihazIngenico.prcdArttirimYap(strLineMember.Split('|')[3],
                                                            (bytArttirimTipi == 0 ? (intRowIndex - 1).TOUSHORT() : 0xFFFF.TOUSHORT()),
                                                             strLineMember.Split('|')[7].TODOUBLE());
                        }

                        //if (strLine.Split('#')[0] == "ACK")
                        //{
                        //    string strLineMember = strLine.Split('#')[1];
                        //    if (intCihazTipi == 1)
                        //    {
                        //        clsCihazOlivetti.prcdAciklamaEkle(strLineMember.Split('|')[0],
                        //                                          strLineMember.Split('|')[1],
                        //                                          strLineMember.Split('|')[2]);
                        //    }
                        //}

                        if (strLine.Split('#')[0] == "ODM")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            if (strLineMember.Split('|')[0] != "01")
                            {
                                clsCihazIngenico.prcdOdemeYap(strLineMember.Split('|')[0],
                                                              strLineMember.Split('|')[2].TODOUBLE(),
                                                              strLineMember.Split('|')[4]);
                            }
                        }
                    }


                    foreach (string strLine in arrLines)
                    {
                        if (strLine.Split('#')[0] == "ODM")
                        {
                            string strLineMember = strLine.Split('#')[1];
                            if (strLineMember.Split('|')[0] == "01")
                            {
                                clsCihazIngenico.prcdOdemeYap(strLineMember.Split('|')[0],
                                                              strLineMember.Split('|')[2].TODOUBLE(),
                                                              strLineMember.Split('|')[4]);
                            }
                        }

                        //if (strLine.Split('#')[0] == "END")
                        //{
                        //    string strLineMember = strLine.Split('#')[1];
                        //    if (intCihazTipi == 0)
                        //    {
                        //        clsCihazHugin.prcdBelgeBitir(strLineMember.Split('|')[0].TOBOOLEAN());
                        //    }
                        //    else if (intCihazTipi == 1)
                        //    {
                        //        clsCihazOlivetti.prcdBelgeBitir(strLineMember.Split('|')[0],
                        //                                        strLineMember.Split('|')[1],
                        //                                        strLineMember.Split('|')[2]);
                        //    }
                        //}
                    }
                    File.Delete(strFile);
                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                rtbLog.Invoke(new EventHandler(delegate
                {
                    rtbLog.SelectedText = string.Empty;
                    rtbLog.SelectionFont = new Font(rtbLog.SelectionFont, FontStyle.Bold);
                    rtbLog.SelectionColor = Color.Red;
                    rtbLog.AppendText(ex.Message);
                    rtbLog.ScrollToCaret();
                }));
            }
            tSatis.Enabled = blnDevam;
        }

        private void btnIslem_Click(object sender, EventArgs e)
        {
            blnDevam = !blnDevam;
            tSatis.Enabled = blnDevam;
            btnIslem.Text = "İşlem " + (blnDevam ? "Durdur" : "Başlat");
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void frmYazarKasa_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void niTask_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                gösterToolStripMenuItem.PerformClick();
            }
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void frmYazarKasa_VisibleChanged(object sender, EventArgs e)
        {
            gösterToolStripMenuItem.Text = this.Visible ? "Gizle" : "Göster";
        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            frmRaporlar frForm = new frmRaporlar();
            frForm.ShowDialog();
            frForm.Dispose();
        }
    }
}
