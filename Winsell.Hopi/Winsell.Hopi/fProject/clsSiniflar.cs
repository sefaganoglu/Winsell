using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsell.Hopi.fProject
{
    public static class clsSiniflar
    {
        public class Masa
        {
            public int masaNo = 0;
            public string masaNoStr = "";
            public decimal tutar = 0;
            public int width = 100;
            public int height = 100;
            public string yetki = "000";
            public Color color = Color.Salmon;
        }
        
        public class KampanyaBilgisi
        {
            public List<Kampanya> kampanyalar = new List<Kampanya>();
            public decimal paracik = 0;
        }

        public class Kampanya
        {
            public string kampanyaKodu = "";
            public int adet = 0;
            public int kazancOrani = 0;
            public decimal kazancParacik = 0;
            public decimal indirimKat = 0;
            public int indirimOrani = 0;
            public decimal maksimumKatParacik = 0;
            public decimal maksimumKazanc = 0;
            public decimal miktar = 0;
            public decimal fiyatsalSinir = 0;
            public Dictionary<int, decimal> indirimler = new Dictionary<int, decimal>();
            public Dictionary<int, decimal> tutarlar = new Dictionary<int, decimal>();
        }

        public class KimlikBilgisi
        {
            public long birdId = 0;
            public decimal paracik = 0;
        }

        public class AdisyonBilgisi
        {
            public int adisyonNo = 0;
            public decimal adisyonTutar = 0;
        }
    }
}
