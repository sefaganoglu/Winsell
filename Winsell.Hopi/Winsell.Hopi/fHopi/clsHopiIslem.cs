﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Winsell.Hopi.fProject;
using Winsell.Hopi.HopiWS;
using Message = System.ServiceModel.Channels.Message;

namespace Winsell.Hopi
{
    public static class clsHopiIslem
    {
        public static clsHopi.KullaniciResponse getKullaniciBilgi(string kullaniciKoduWS, string sifreWS, clsHopi.Kullanici kullanici)
        {
            clsHopi.KullaniciResponse kullaniciResponse = new clsHopi.KullaniciResponse();

            HopiWS.GetBirdUserInfoRequest gbuirRequest = new HopiWS.GetBirdUserInfoRequest
            {
                storeCode = kullanici.storeCode,
                ItemElementName = HopiWS.ItemChoiceType.token,
                Item = kullanici.token
            };

            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    HopiWS.GetBirdUserInfoResponse response = client.GetBirdUserInfo(gbuirRequest);
                    kullaniciResponse.birdId = response.birdId;
                    kullaniciResponse.paracik = response.coinBalance;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kimlik kontrolü yapılırken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return kullaniciResponse;
        }

        public static bool setAlisverisBilgisi(string kullaniciKoduWS, string sifreWS, clsHopi.AlisverisBilgisi alisverisBilgisi)
        {
            bool blnReturn = false;

            HopiWS.Checkout cRequest = new HopiWS.Checkout
            {
                birdId = alisverisBilgisi.birdId,
                transactionId = alisverisBilgisi.transactionId,
                cashDeskTag = alisverisBilgisi.kasaNo,
                storeCode = alisverisBilgisi.storeCode,
                dateTime = alisverisBilgisi.dateTime
            };

            //KDV GRUPLU ÖDEME BİLGİLERİ (TÜMÜ)
            var vKdvGrupluTutarlar = from tab in alisverisBilgisi.urunler.AsEnumerable()
                                     group tab by tab.kdv
                                         into groupDt
                                         select new
                                         {
                                             Kdv = groupDt.Key,
                                             SumTutar = groupDt.Sum((r) => (r.tutar))
                                         };

            List<HopiWS.AmountDetail> arrDetails = new List<HopiWS.AmountDetail>();
            foreach (var v in vKdvGrupluTutarlar)
            {
                HopiWS.AmountDetail adDetail = new HopiWS.AmountDetail();
                adDetail.amount = v.SumTutar.ROUNDTWO();
                adDetail.Item = v.Kdv.TOINT(); //Eğer KDV oranı gidiyorsa TOINT kdv tutarı gidiyorsa TODECIMAL yapılır.
                arrDetails.Add(adDetail);
            }
            cRequest.paymentDetails = arrDetails.ToArray();
            cRequest.subtotalDetails = arrDetails.ToArray(); //Şimdilik böyle. Normalde Gift Card, Loyalty Card puanlarının ödeme (indirim) olarak alınması gibi indirimler öncesi olan tutar yazılır.
            ///////////////////////////////

            //KDV GRUPLU ÖDEME BİLGİLERİ (KAMPANYASIZLAR)
            var vKdvGrupluTutarlar2 = from tab in alisverisBilgisi.urunler.AsEnumerable()
                                      where tab.kampanyaKodlari.Count == 0
                                      group tab by tab.kdv
                                          into groupDt
                                          select new
                                          {
                                              Kdv = groupDt.Key,
                                              SumTutar = groupDt.Sum((r) => (r.tutar))
                                          };

            List<HopiWS.AmountDetail> arrDetails2 = new List<HopiWS.AmountDetail>();
            foreach (var v in vKdvGrupluTutarlar2)
            {
                HopiWS.AmountDetail adDetail = new HopiWS.AmountDetail();
                adDetail.amount = v.SumTutar.ROUNDTWO();
                adDetail.Item = v.Kdv.TOINT(); //Eğer KDV oranı gidiyorsa TOINT kdv tutarı gidiyorsa TODECIMAL yapılır.
                arrDetails2.Add(adDetail);
            }
            cRequest.campaignFreePaymentDetails = arrDetails2.ToArray();
            ///////////////////////////////

            //KAMPANYA BİLGİLERİ
            List<HopiWS.UsedCampaignDetail> arrCampains = new List<HopiWS.UsedCampaignDetail>();
            //var kampanyalar = alisverisBilgisi.urunler.Where(p => p.kampanya != "").Select(p => p.kampanya).Distinct();

            foreach (clsHopi.Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                if (!string.IsNullOrWhiteSpace(kampanya.kampanyaKodu))
                {
                    HopiWS.UsedCampaignDetail ucdDetail = new HopiWS.UsedCampaignDetail();

                    ucdDetail.campaignCode = kampanya.kampanyaKodu;

                    var vKampanyaliKdvGrupluTutarlar = from tab in alisverisBilgisi.urunler.AsEnumerable()
                                                       where tab.kampanyaKodlari.Contains(kampanya.kampanyaKodu)
                                                       group tab by tab.kdv
                                                           into groupDt
                                                           select new
                                                           {
                                                               Kdv = groupDt.Key,
                                                               SumTutar = groupDt.Sum((r) => (r.tutar)),
                                                               SumIndirimTutar = groupDt.Sum((r) => (r.indirimTutari)),
                                                           };

                    Decimal dKazanc = kampanya.paracikKazanc.ROUNDTWO();
                    List<object> arrIndirim = new List<object>();
                    List<HopiWS.AmountDetail> arrCampDetails = new List<HopiWS.AmountDetail>();
                    foreach (var v in vKampanyaliKdvGrupluTutarlar)
                    {
                        HopiWS.AmountDetail adCampDetail = new HopiWS.AmountDetail
                        {
                            amount = v.SumTutar.ROUNDTWO(),
                            Item = v.Kdv.TOINT() //Eğer KDV oranı gidiyorsa TOINT kdv tutarı gidiyorsa TODECIMAL yapılır.
                        };
                        arrCampDetails.Add(adCampDetail);

                        HopiWS.AmountDetail adIndirim = new HopiWS.AmountDetail
                        {
                            Item = v.Kdv.TOINT(),
                            amount = v.SumIndirimTutar.ROUNDTWO()
                        };
                        arrIndirim.Add(adIndirim);
                    }
                    ucdDetail.amountDetails = arrCampDetails.ToArray();
                    //KAZANÇ İNDİRİM BİLGİSİ
                    ucdDetail.benefit = new HopiWS.BenefitDetail
                    {
                        Items = dKazanc != 0 ? new object[] { dKazanc } : arrIndirim.ToArray() //Eğer paracık kazancı varsa decimal tipinde o gönderilir. Eğer ki indirim varsa AmountDetail tipinde o gönderilir.
                    };
                    /////////////////////////

                    arrCampains.Add(ucdDetail);
                }
            }
            cRequest.usedCampaignDetails = arrCampains.ToArray();
            ///////////////////////////////////////////////

            //SATIŞ DETAYLARI (ÜRÜN BİLGİLERİ)
            List<HopiWS.TransactionInfo> arrInfo = new List<HopiWS.TransactionInfo>();
            foreach (clsHopi.Urun urun in alisverisBilgisi.urunler)
            {
                HopiWS.TransactionInfo tiInfo = new HopiWS.TransactionInfo
                {
                    barcode = urun.barkod,
                    quantity = urun.miktar.ROUNDTWO(),
                    amount = urun.tutar.ROUNDTWO(),
                    campaign = urun.kampanyaKodlari.ToArray()
                };
                arrInfo.Add(tiInfo);
            }
            cRequest.transactionInfos = arrInfo.ToArray();
            /////////////////////////////////////

            //HOPI PAY ID BİLGİSİ (MOBİL ÖDEMELER İÇİN)
            if (alisverisBilgisi.hopiPayId > 0)
                cRequest.usedPaymentDetails = new HopiWS.UsedPaymentDetail[] { new HopiWS.UsedPaymentDetail() { hopiPayId = alisverisBilgisi.hopiPayId } };
            //////////////////////////////////////////

            bool blnDevam = true;
            //PARACIK KULLANIM BİLGİSİ
            if (alisverisBilgisi.kullanilacakParacik != 0)
            {
                ulong provisionIdGelen = setKullanilacakParacikBilgisi(kullaniciKoduWS, sifreWS, alisverisBilgisi.storeCode, alisverisBilgisi.birdId, alisverisBilgisi.kullanilacakParacik);
                if (provisionIdGelen > 0)
                {
                    if (setKullanilacakParacikBilgisiIslemiBitir(kullaniciKoduWS, sifreWS, alisverisBilgisi.storeCode, provisionIdGelen))
                    {
                        cRequest.usedCoinDetails = new[] { new HopiWS.UsedCoinDetail() { provisionId = provisionIdGelen, amount = alisverisBilgisi.kullanilacakParacik } };
                    }
                    else
                    {
                        blnDevam = false;
                    }
                }
                else
                {
                    blnDevam = false;
                }

            }
            /////////////////////////////////////////

            if (blnDevam)
            {
                HopiWS.PosPortClient client = new HopiWS.PosPortClient();
                try
                {
                    using (new OperationContextScope(client.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                        string BeyanXML = clsHopi.SerializeObject(cRequest).Remove(0, 1);

                        string strFilePath = clsGenel.getExePath() + @"\Send\" + DateTime.Now.ToString("yyyy-MM-dd");
                        clsGenel.directoryControl(strFilePath);
                        StreamWriter SW = new StreamWriter(strFilePath + @"\" + alisverisBilgisi.transactionId + ".xml");
                        SW.Write(BeyanXML);
                        SW.Close();
                        SW.Dispose();

                        client.NotifyCheckout(cRequest);

                        blnReturn = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kampanyalı alışveriş bildiriminde bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    if (cRequest.usedCoinDetails != null && cRequest.usedCoinDetails.Length > 0)
                        setIadeEdilecekParacikBilgisi(kullaniciKoduWS, sifreWS, alisverisBilgisi.storeCode, cRequest.usedCoinDetails[0].provisionId, cRequest.usedCoinDetails[0].amount);
                }
                client.Close();
            }


            return blnReturn;
        }

        public static ulong setIadeAlisverisBilgisi(string kullaniciKoduWS, string sifreWS, clsHopi.AlisverisBilgisi alisverisBilgisi)
        {
            ulong uReturn = 0;

            StartReturnTransactionRequest srtrRequest = new StartReturnTransactionRequest
            {
                storeCode = alisverisBilgisi.storeCode,
                transactionId = alisverisBilgisi.transactionId,
                returnCampaignDetails = alisverisBilgisi.kampanyalar.Select(kampanya => new ReturnCampaignDetail
                {
                    campaignCode = kampanya.kampanyaKodu,
                    returnPayment = alisverisBilgisi.kullanilacakParacik,
                    requestedCoinReturnAmount = kampanya.paracikKazanc
                }).ToArray(),
                transactionInfos = alisverisBilgisi.urunler.Select(urun => new HopiWS.TransactionInfo
                {
                    barcode = urun.barkod,
                    quantity = urun.miktar,
                    amount = urun.tutar,
                    campaign = urun.kampanyaKodlari.ToArray()
                }).ToArray(),
                campaignFreeAmount =
                    alisverisBilgisi.urunler.AsEnumerable().Where(u => u.kampanyaKodlari.Count == 0).Sum(u => u.tutar)
            };

            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    StartReturnTransactionResponse response = client.StartReturnTransaction(srtrRequest);
                    uReturn = response.returnTrxId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alışveriş iadesi bildiriminde bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return uReturn;
        }

        public static ulong setKullanilacakParacikBilgisi(string kullaniciKoduWS, string sifreWS, string storeCode, long birdId, decimal kullanilacakParacik)
        {
            ulong ulReturn = 0;
            StartCoinTransactionRequest coinTransactionRequest = new StartCoinTransactionRequest
            {
                birdId = birdId,
                storeCode = storeCode,
                amount = kullanilacakParacik
            };


            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    StartCoinTransactionResponse startCoinTransactionResponse = client.StartCoinTransaction(coinTransactionRequest);
                    ulReturn = startCoinTransactionResponse.provisionId; //Ödeme sisteminde ilgili işlem için yaratılan ödeme kimlik numarası.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanılacak paracık bilgisi gönderilirken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return ulReturn;
        }

        public static bool setKullanilacakParacikBilgisiIslemiBitir(string kullaniciKoduWS, string sifreWS, string storeCode, ulong provisionId)
        {
            bool blnReturn = false;
            CompleteCoinTransactionRequest coinTransactionRequest = new CompleteCoinTransactionRequest
            {
                storeCode = storeCode,
                provisionId = provisionId
            };


            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    client.CompleteCoinTransaction(coinTransactionRequest);
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanılacak paracık bilgisi gönderim işlemi tamamlanırken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return blnReturn;
        }

        public static bool setIptalEdilecekParacikBilgisi(string kullaniciKoduWS, string sifreWS, string storeCode, ulong provisionId)
        {
            bool blnReturn = false;

            CancelCoinTransactionRequest cancelCoinTransaction = new CancelCoinTransactionRequest
            {
                storeCode = storeCode,
                provisionId = provisionId
            };

            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    client.CancelCoinTransaction(cancelCoinTransaction);
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İptal edilecek paracık bilgisi gönderilirken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return blnReturn;
        }

        public static bool setIadeEdilecekParacikBilgisi(string kullaniciKoduWS, string sifreWS, string storeCode, ulong provisionId, decimal iadeParacikTutari)
        {
            bool blnReturn = false;
            RefundCoinRequest refundCoinTransactionRequest = new RefundCoinRequest
            {
                provisionId = provisionId,
                amount = iadeParacikTutari,
                storeCode = storeCode
            };

            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    client.RefundCoin(refundCoinTransactionRequest);
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İade edilecek paracık bilgisi gönderilirken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return blnReturn;
        }

        public static bool setIadeAlisverisBilgisiIslemiBitir(string kullaniciKoduWS, string sifreWS, string storeCode, ulong returnTrxId)
        {
            bool blnReturn = false;
            CompleteReturnTransactionRequest returnTransactionRequest = new CompleteReturnTransactionRequest
            {
                storeCode = storeCode,
                returnTrxId = returnTrxId
            };


            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));
                    client.CompleteReturnTransaction(returnTransactionRequest);
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İade edilen alışveriş bilgisi gönderim işlemi tamamlanırken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return blnReturn;
        }
    }
}