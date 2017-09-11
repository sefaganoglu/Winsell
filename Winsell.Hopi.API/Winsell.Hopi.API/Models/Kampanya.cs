using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class Kampanya
    {
        public string kodu { get; set; }
        public string aciklama { get; set; }
        public int adet { get; set; }
        public int kazancOrani { get; set; }
        public decimal kazancParacik { get; set; }
        public decimal indirimKat { get; set; }
        public int indirimOrani { get; set; }
        public decimal maksimumKatParacik { get; set; }
        public decimal maksimumKazanc { get; set; }
        public decimal miktar { get; set; }
        public decimal fiyatsalSinir { get; set; }
        public Dictionary<int, decimal> indirimler { get; set; }
        public Dictionary<int, decimal> tutarlar { get; set; }
    }
}
