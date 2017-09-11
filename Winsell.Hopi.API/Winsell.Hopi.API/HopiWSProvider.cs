using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Winsell.Hopi.API
{
    public class HopiWSProvider
    {

        public static string GetKullaniciBilgi(string storeCode, string token)
        {
            WebService service = new WebService();
            service.Url = Genel.hopiWSDL;
            service.Credentials = CredentialCache.DefaultCredentials;
            service.MethodName = "GetBirdUserInfo";
            service.Params.Add("storeCode", storeCode);
            service.Params.Add("token", token);

            service.Invoke(false);

            if (!string.IsNullOrEmpty(service.ResultString))
            {
                return service.ResultString;
            }
            else
            {
                return "";
            }

            //clsHopi.KullaniciResponse kullaniciResponse = new clsHopi.KullaniciResponse();

            //HopiWS.GetBirdUserInfoRequest gbuirRequest = new HopiWS.GetBirdUserInfoRequest
            //{
            //    storeCode = kullanici.storeCode,
            //    ItemElementName = HopiWS.ItemChoiceType.token,
            //    Item = kullanici.token
            //};

            //var response = await Helper.GetResponse<ApipmReturn<MProfileResult>>(string.Format(_appSettings.APIPMProfileResultUrl, id), "GET", null, _headers);

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(Genel.hopiWSDL);
            //client.get

            //HopiWS.PosPortClient client = new HopiWS.PosPortClient();
            //try
            //{
            //    using (new OperationContextScope(client.InnerChannel))
            //    {
            //        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("UsernameToken-49", kullaniciKoduWS, sifreWS));

            //        clsHopi.SaveToFile("Kullanici", DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + kullanici.storeCode + "_" + kullanici.token.Replace(" ", ""), gbuirRequest);

            //        HopiWS.GetBirdUserInfoResponse response = client.GetBirdUserInfo(gbuirRequest);
            //        kullaniciResponse.birdId = response.birdId;
            //        kullaniciResponse.paracik = response.coinBalance;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Kimlik kontrolü yapılırken bir hata oluştu:" + Environment.NewLine + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
            //client.Close();

            //return kullaniciResponse;
        }

    }
}
