using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class AlisverisBilgisi
    {
        public List<Kampanya> kampanyalar { get; set; }

        public int masaNo { get; set; }

        public string masaNoStr { get; set; }

        public int adisyonNo { get; set; }

        public decimal adisyonTutari { get; set; }

        public decimal harcanacakParacik { get; set; }

        public long birdId { get; set; }

        public string kullaniciKodu { get; set; }
    }
}
