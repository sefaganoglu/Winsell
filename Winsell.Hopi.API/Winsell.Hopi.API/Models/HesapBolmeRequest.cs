using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class HesapBolmeRequest
    {
        public int masaNo { get; set; }
        public List<Stok> stoklar { get; set; }
    }
}
