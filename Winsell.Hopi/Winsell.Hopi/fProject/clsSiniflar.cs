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
        public enum KampanyaTipi
        {
            Indirim = 0,
            Kazanc = 1
        }

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
            public KampanyaTipi kampanyaTipi = KampanyaTipi.Indirim;
            public string kampanyaKodu = "";
            public decimal paracikKazanc = 0;
            public decimal indirimTutari = 0;
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
