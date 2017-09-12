using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.HopiWS
{
    public static class HopiRequestClasses
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
    }
}
