using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Winsell.Hopi
{
    public static class clsHopi
    {
        public class AlisverisBilgisi
        {
            public long birdId = 0;
            public string transactionId = "";
            public string kasaNo = "";
            public string storeCode = "";
            public DateTime dateTime = DateTime.Now;
            public long hopiPayId = 0;
            public List<Urun> urunler = new List<Urun>();
            public decimal kullanilacakParacik = 0;
            public List<Kampanya> kampanyalar = new List<Kampanya>();
        }

        public class AlisverisIadeBilgisi
        {
            public string transactionId = "";
            public string storeCode = "";
            public decimal kampanyasizTutar = 0;
            public decimal kazanilanParacik = 0;
            public string odemeTransactionId = "";
            public List<KampanyaIade> kampanyalar = new List<KampanyaIade>();
            public List<Urun> urunler = new List<Urun>();
        }

        public class AlisverisIadeResponse
        {
            public ulong returnTrxId = 0;
            public decimal artan = 0;
        }

        public class AlisverisResponse
        {
            public bool basarili = false;
            public string odemeTransactionId = "";
        }

        public class Urun
        {
            public string barkod = "";
            public decimal miktar = 0;
            public decimal tutar = 0;
            public int kdv = 0;
            public int x = 0;
            public int siraNo = 0;
            public List<string> kampanyaKodlari = new List<string>();
            public decimal indirimTutari = 0;
        }

        public class Kampanya
        {
            public string kampanyaKodu = "";
            public decimal paracikKazanc = 0;
            public Dictionary<int, decimal> indirimler = new Dictionary<int, decimal>();
            public Dictionary<int, decimal> tutarlar = new Dictionary<int, decimal>();
        }

        public class KampanyaIade
        {
            public string kampanyaKodu = "";
            public decimal tutar = 0;
            public decimal iadeParacik = 0;
        }

        public class Kullanici
        {
            public string storeCode = "";
            public string token = "";
        }

        public class KullaniciResponse
        {
            public long birdId = 0;
            public decimal paracik = 0;
        }


        public static String SerializeObject(Object pObject)
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return XmlizedString;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }


    public class SecurityHeader : MessageHeader
    {
        private readonly UsernameToken _usernameToken;

        public SecurityHeader(string id, string username, string password)
        {
            _usernameToken = new UsernameToken(id, username, password);
        }

        public override string Name
        {
            get { return "Security"; }
        }

        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"; }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UsernameToken));
            serializer.Serialize(writer, _usernameToken);
        }
    }

    [XmlRoot(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
    public class UsernameToken
    {
        public UsernameToken()
        {
        }

        public UsernameToken(string id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = new Password() { Value = password };
        }

        [XmlAttribute(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
        public string Id { get; set; }

        [XmlElement]
        public string Username { get; set; }

        [XmlElement]
        public Password Password { get; set; }
    }

    public class Password
    {
        public Password()
        {
            Type = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText";
        }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
