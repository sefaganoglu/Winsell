using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class Kullanici
    {
        public DurumResponse durum { get; set; }
        public string kullaniciKodu { get; set; }
        public string kullaniciAdi { get; set; }
    }
}
