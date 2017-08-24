using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class Adisyon
    {
        public int no { get; set; }
        public decimal tutar { get; set; }

        public List<Stok> stoklar { get; set; }
    }
}
