using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class Stok
    {
        public string kodu { get; set;}
        public string adi { get; set; }
        public decimal miktari { get; set; }
        public decimal miktariDeg { get; set; }
        public string miktariAck { get; set; }
        public decimal tutari { get; set; }
        public int x { get; set; }
    }
}
