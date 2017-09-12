using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class KimlikKontrolResponse
    {
        public DurumResponse durum { get; set; }

        public long birdId { get; set; }

        public decimal bakiye { get; set; }

    }
}
