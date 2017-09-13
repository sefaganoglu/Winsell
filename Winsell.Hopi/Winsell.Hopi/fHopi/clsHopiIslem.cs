using System;
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

                    clsHopi.SaveToFile("Kullanici", DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + kullanici.storeCode + "_" + kullanici.token.Replace(" ", ""), gbuirRequest);

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

        public static clsHopi.AlisverisResponse setAlisverisBilgisi(string kullaniciKoduWS, string sifreWS, clsHopi.AlisverisBilgisi alisverisBilgisi)
        {
            clsHopi.AlisverisResponse alisverisResponse = new clsHopi.AlisverisResponse();

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
                                         SumTutar = groupDt.Sum((r) => (r.tutar)),
                                         SumIndirim = groupDt.Sum((r) => (r.indirimTutari))
                                     };

            List<HopiWS.AmountDetail> arrDetails = new List<HopiWS.AmountDetail>();
            foreach (var v in vKdvGrupluTutarlar)
            {
                HopiWS.AmountDetail adDetail = new HopiWS.AmountDetail();
                adDetail.amount = (v.SumTutar - v.SumIndirim).ROUNDTWO();
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
                                          SumTutar = groupDt.Sum((r) => (r.tutar)),
                                          SumIndirim = groupDt.Sum((r) => (r.indirimTutari))
                                      };

            List<HopiWS.AmountDetail> arrDetails2 = new List<HopiWS.AmountDetail>();
            foreach (var v in vKdvGrupluTutarlar2)
            {
                HopiWS.AmountDetail adDetail = new HopiWS.AmountDetail();
                adDetail.amount = (v.SumTutar - v.SumIndirim).ROUNDTWO();
                adDetail.Item = v.Kdv.TOINT(); //Eğer KDV oranı gidiyorsa TOINT kdv tutarı gidiyorsa TODECIMAL yapılır.
                arrDetails2.Add(adDetail);
            }
            cRequest.campaignFreePaymentDetails = arrDetails2.ToArray();
            ///////////////////////////////

            //KAMPANYA BİLGİLERİ
            List<HopiWS.UsedCampaignDetail> arrCampains = new List<HopiWS.UsedCampaignDetail>();
            foreach (clsHopi.Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                if (!string.IsNullOrWhiteSpace(kampanya.kampanyaKodu))
                {
                    HopiWS.UsedCampaignDetail ucdDetail = new HopiWS.UsedCampaignDetail();

                    Decimal dKazanc = kampanya.paracikKazanc.ROUNDTWO();
                    List<object> arrIndirim = new List<object>();
                    List<HopiWS.AmountDetail> arrCampDetails = new List<HopiWS.AmountDetail>();

                    ucdDetail.campaignCode = kampanya.kampanyaKodu;

                    foreach (int intKey in kampanya.tutarlar.Keys)
                    {
                        HopiWS.AmountDetail adCampDetail = new HopiWS.AmountDetail
                        {
                            Item = intKey, 
                            amount = kampanya.tutarlar[intKey].ROUNDTWO() 
                        };
                        arrCampDetails.Add(adCampDetail);
                    }

                    foreach (int intKey in kampanya.indirimler.Keys)
                    {
                        HopiWS.AmountDetail adIndirim = new HopiWS.AmountDetail
                        {
                            Item = intKey,
                            amount = kampanya.indirimler[intKey].ROUNDTWO()
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

                        clsHopi.SaveToFile("Satis", alisverisBilgisi.transactionId, cRequest);

                        client.NotifyCheckout(cRequest);

                        alisverisResponse.basarili = true;
                        if (cRequest.usedCoinDetails != null && cRequest.usedCoinDetails.Length > 0)
                            alisverisResponse.odemeTransactionId = cRequest.usedCoinDetails[0].provisionId.TOSTRING();
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


            return alisverisResponse;
        }

        public static clsHopi.AlisverisIadeResponse setIadeAlisverisBilgisi(string kullaniciKoduWS, string sifreWS, clsHopi.AlisverisIadeBilgisi alisverisIadeBilgisi)
        {
            clsHopi.AlisverisIadeResponse alisverisIadeResponse = new clsHopi.AlisverisIadeResponse();

            StartReturnTransactionRequest srtrRequest = new StartReturnTransactionRequest
            {
                storeCode = alisverisIadeBilgisi.storeCode,
                transactionId = alisverisIadeBilgisi.transactionId,
                campaignFreeAmountSpecified = alisverisIadeBilgisi.kampanyasizTutar != 0,
                campaignFreeAmount = alisverisIadeBilgisi.kampanyasizTutar,
                returnCampaignDetails = alisverisIadeBilgisi.kampanyalar.Select(kampanya => new ReturnCampaignDetail
                {
                    campaignCode = kampanya.kampanyaKodu,
                    returnPayment = kampanya.tutar,
                    requestedCoinReturnAmount = kampanya.iadeParacik
                }).ToArray(),

                transactionInfos = alisverisIadeBilgisi.urunler.Select(urun => new HopiWS.TransactionInfo
                {
                    barcode = urun.barkod,
                    quantity = urun.miktar,
                    amount = urun.tutar,
                    campaign = urun.kampanyaKodlari.ToArray()
                }).ToArray()
            };

            HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            try
            {
                using (new OperationContextScope(client.InnerChannel))
                {
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));

                    clsHopi.SaveToFile("SatisIade", alisverisIadeBilgisi.transactionId, srtrRequest);

                    StartReturnTransactionResponse response = client.StartReturnTransaction(srtrRequest);
                    alisverisIadeResponse.returnTrxId = response.returnTrxId;
                    alisverisIadeResponse.artan = response.residual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alışveriş iadesi bildiriminde bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            client.Close();

            return alisverisIadeResponse;
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
                    clsHopi.SaveToFile("Harcama", DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + storeCode + "_" + birdId.TOSTRING(), coinTransactionRequest);
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
                    clsHopi.SaveToFile("HarcamaComplete", storeCode + "_" + provisionId.TOSTRING(), coinTransactionRequest);
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
                    clsHopi.SaveToFile("HarcamaIptal", cancelCoinTransaction.storeCode + "_" + provisionId.TOSTRING(), cancelCoinTransaction);
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
                    clsHopi.SaveToFile("HarcamaIade", refundCoinTransactionRequest.storeCode + "_" + provisionId.TOSTRING(), refundCoinTransactionRequest);
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
                    clsHopi.SaveToFile("SatisIadeComplete", storeCode + "_" + returnTrxId.TOSTRING(), returnTransactionRequest);
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
