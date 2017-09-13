using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Winsell.Hopi.API.HopiWS
{
    public class HopiWSProvider
    {
        public static WebServiceResult GetKullaniciBilgi(string token)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "GetBirdUserInfo";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("token", token);
            return service.Invoke(false);
        }

        public static WebServiceResult SetKullanilacakParacikBilgisi(long birdId, decimal kullanilacakParacik)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "StartCoinTransaction";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("birdId", birdId.TOSTRING());
            service.Params.Add("amount", kullanilacakParacik.TOSTRDECIMAL());
            return service.Invoke(false);
        }

        public static WebServiceResult SetKullanilacakParacikBilgisiIslemiBitir(ulong provisionId)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "CompleteCoinTransaction";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("provisionId", provisionId.TOSTRING());
            return service.Invoke(false);
        }

        public static WebServiceResult SetIptalEdilecekParacikBilgisi(ulong provisionId)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "CancelCoinTransaction";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("provisionId", provisionId.TOSTRING());
            return service.Invoke(false);
        }

        public static WebServiceResult SetIadeEdilecekParacikBilgisi(ulong provisionId, decimal iadeParacikTutari)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "RefundCoin";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("provisionId", provisionId.TOSTRING());
            service.Params.Add("amount", iadeParacikTutari.TOSTRDECIMAL());
            return service.Invoke(false);
        }

        public static WebServiceResult SetAlisverisIadeBilgisiIslemiBitir(ulong returnTrxId)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "CompleteReturnTransaction";
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("returnTrxId", returnTrxId.TOSTRING());
            return service.Invoke(false);
        }

        public static WebServiceResult SetAlisverisBilgisi(HopiRequestClasses.AlisverisBilgisi alisverisBilgisi)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "NotifyCheckout";

            service.Params.Add("birdId", alisverisBilgisi.birdId.TOSTRING());
            service.Params.Add("transactionId", alisverisBilgisi.transactionId);
            service.Params.Add("cashDeskTag", alisverisBilgisi.kasaNo);
            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("dateTime", alisverisBilgisi.dateTime.ToString("yyyy-MM-dd") + "T" + alisverisBilgisi.dateTime.ToString("HH:mm:ss"));

            StringBuilder sb = new StringBuilder();

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

            foreach (var v in vKdvGrupluTutarlar)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", (v.SumTutar - v.SumIndirim).ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:percent", v.Kdv.TOINT(), Environment.NewLine);
                service.Params.Add("paymentDetails", sb.ToString());
            }
            //KDV GRUPLU ÖDEME BİLGİLERİ (TÜMÜ) BİTTİ


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

            foreach (var v in vKdvGrupluTutarlar2)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", (v.SumTutar - v.SumIndirim).ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:percent", v.Kdv.TOINT(), Environment.NewLine);
                service.Params.Add("campaignFreePaymentDetails", sb.ToString());
            }
            //KDV GRUPLU ÖDEME BİLGİLERİ (KAMPANYASIZLAR) BİTTİ


            //KAMPANYA BİLGİLERİ
            foreach (HopiRequestClasses.Kampanya kampanya in alisverisBilgisi.kampanyalar)
            {
                if (!string.IsNullOrWhiteSpace(kampanya.kampanyaKodu))
                {
                    sb.Clear();
                    sb.AppendLine();
                    sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:campaignCode", kampanya.kampanyaKodu, Environment.NewLine);

                    Decimal dKazanc = kampanya.paracikKazanc.ROUNDTWO();
                    List<object> arrIndirim = new List<object>();

                    foreach (int intKey in kampanya.tutarlar.Keys)
                    {
                        sb.AppendFormat("<{0}>{1}", "pos:amountDetails", Environment.NewLine);
                        sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", kampanya.tutarlar[intKey].ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                        sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:percent", intKey, Environment.NewLine);
                        sb.AppendFormat("</{0}>{1}", "pos:amountDetails", Environment.NewLine);
                    }

                    //KAZANÇ İNDİRİM BİLGİSİ
                    sb.AppendFormat("<{0}>{1}", "pos:benefit", Environment.NewLine);
                    if (dKazanc != 0)
                        sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:coins", dKazanc.TOSTRDECIMAL(), Environment.NewLine);
                    else
                    {
                        foreach (int intKey in kampanya.indirimler.Keys)
                        {
                            sb.AppendFormat("<{0}>{1}", "pos:discounts", Environment.NewLine);
                            sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", kampanya.tutarlar[intKey].ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                            sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:percent", intKey, Environment.NewLine);
                            sb.AppendFormat("</{0}>{1}", "pos:discounts", Environment.NewLine);
                        }
                    }
                    sb.AppendFormat("</{0}>{1}", "pos:benefit", Environment.NewLine);
                    //KAZANÇ İNDİRİM BİLGİSİ BİTTİ

                    service.Params.Add("usedCampaignDetails", sb.ToString());
                }
            }
            //KAMPANYA BİLGİLERİ BİTTİ

            //SATIŞ DETAYLARI (ÜRÜN BİLGİLERİ)
            foreach (HopiRequestClasses.Urun urun in alisverisBilgisi.urunler)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:barcode", urun.barkod, Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:quantity", urun.miktar.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", urun.tutar.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                foreach (string kampanyaKodu in urun.kampanyaKodlari)
                    sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:campaign", kampanyaKodu, Environment.NewLine);

                service.Params.Add("transactionInfos", sb.ToString());
            }
            //SATIŞ DETAYLARI (ÜRÜN BİLGİLERİ) BİTTİ

            //HOPI PAY ID BİLGİSİ (MOBİL ÖDEMELER İÇİN)
            if (alisverisBilgisi.hopiPayId > 0)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:hopiPayId", alisverisBilgisi.hopiPayId, Environment.NewLine);
                service.Params.Add("usedPaymentDetails", sb.ToString());
            }
            //HOPI PAY ID BİLGİSİ (MOBİL ÖDEMELER İÇİN) BİTTİ

            ulong provisionId = 0;
            WebServiceResult webServiceResult = new WebServiceResult();
            bool blnDevam = true;
            //PARACIK KULLANIM BİLGİSİ
            if (alisverisBilgisi.kullanilacakParacik != 0)
            {
                webServiceResult = SetKullanilacakParacikBilgisi(alisverisBilgisi.birdId, alisverisBilgisi.kullanilacakParacik);
                if (webServiceResult.IsSuccess)
                {
                    HopiResponseClasses.HopiProvision hopiProvision = JsonConvert.DeserializeObject<HopiResponseClasses.HopiProvision>(webServiceResult.ResultString);
                    provisionId = hopiProvision.provisionId;
                    webServiceResult = SetKullanilacakParacikBilgisiIslemiBitir(hopiProvision.provisionId);
                    if (webServiceResult.IsSuccess)
                    {
                        sb.Clear();
                        sb.AppendLine();
                        sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:provisionId", hopiProvision.provisionId, Environment.NewLine);
                        sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", alisverisBilgisi.kullanilacakParacik.TOSTRDECIMAL(), Environment.NewLine);
                        service.Params.Add("usedCoinDetails", sb.ToString());
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
                webServiceResult = service.Invoke(false);

            webServiceResult.ResultAttachment = provisionId;
            return webServiceResult;
        }

        public static WebServiceResult SetAlisverisIadeBilgisi(HopiRequestClasses.AlisverisIadeBilgisi alisverisIadeBilgisi)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "StartReturnTransaction";

            service.Params.Add("storeCode", Genel.storeCode);
            service.Params.Add("transactionId", alisverisIadeBilgisi.transactionId);
            if (alisverisIadeBilgisi.kampanyasizTutar != 0)
                service.Params.Add("campaignFreeAmount", alisverisIadeBilgisi.kampanyasizTutar.ROUNDTWO().TOSTRDECIMAL());

            StringBuilder sb = new StringBuilder();
            foreach (HopiRequestClasses.KampanyaIade kampanyaIade in alisverisIadeBilgisi.kampanyalar)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:campaignCode", kampanyaIade.kampanyaKodu, Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:returnPayment", kampanyaIade.tutar.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:requestedCoinReturnAmount", kampanyaIade.iadeParacik.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);

                service.Params.Add("returnCampaignDetails", sb.ToString());
            }

            foreach (HopiRequestClasses.Urun urun in alisverisIadeBilgisi.urunler)
            {
                sb.Clear();
                sb.AppendLine();
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:barcode", urun.barkod, Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:quantity", urun.miktar.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:amount", urun.tutar.ROUNDTWO().TOSTRDECIMAL(), Environment.NewLine);
                foreach (string kampanyaKodu in urun.kampanyaKodlari)
                    sb.AppendFormat("<{0}>{1}</{0}>{2}", "pos:campaign", kampanyaKodu, Environment.NewLine);

                service.Params.Add("transactionInfos", sb.ToString());
            }

            return service.Invoke(false);
        }
    }
}
