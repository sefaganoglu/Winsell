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
        public decimal kazanc { get; set; }
        public decimal indirim { get; set; }
        public string dikkat { get; set; }
        public int kullanilabilir { get; set; }
        public decimal indirimOrani { get; set; }
    }
}
