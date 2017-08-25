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
            public decimal kazanc = 0;
            public decimal indirim = 0;
            public decimal indirimOrani = 0;
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
