using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class KampanyaRequest
    {
        public decimal paracik { get; set; }
        public int masaNo { get; set; }
        public int adisyonNo { get; set; }
        public string sirketKodu { get; set; }
        public bool serverVar { get; set; }
    }
}
