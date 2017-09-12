using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            service.Params.Add("amount", kullanilacakParacik.TOSTRING().Replace(',', '.'));
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
            service.Params.Add("amount", iadeParacikTutari.TOSTRING().Replace(',', '.'));
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
            //service.Params.Add("storeCode", storeCode);
            //service.Params.Add("token", token);
            return service.Invoke(false);
        }

        public static WebServiceResult SetAlisverisIadeBilgisi(HopiRequestClasses.AlisverisIadeBilgisi alisverisIadeBilgisi)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "StartReturnTransaction";
            //service.Params.Add("storeCode", storeCode);
            //service.Params.Add("token", token);
            return service.Invoke(false);
        }
    }
}
