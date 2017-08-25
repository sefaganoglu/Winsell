using System;
using System.Collections.Generic;
using System.Text;

namespace Winsell.Hopi.Data.Entity
{
    public class HOPI
    {
        public int ID { get; set; }

        public string KOD { get; set; }

        public DateTime BAS_TARIHI { get; set; }

        public DateTime BIT_TARIHI { get; set; }

        public decimal? FIYATSAL_SINIR { get; set; }

        public int? ADET { get; set; }

        public string STOKKODU { get; set; }

        public string ANAGRUP { get; set; }

        public int? HOPI_KAZAN_ORANI { get; set; }

        public decimal? HOPI_KAZAN_PARA { get; set; }

        public decimal? HOPI_KAT { get; set; }

        public bool? AKTIF { get; set; }

        public string ACIKLAMA { get; set; }

        public decimal? HOPI_KAT_UST_SINIR { get; set; }

        public decimal? KAZANILACAK_PARACIK_UST_SINIR { get; set; }

        public int? INDIRIM_ORANI { get; set; }

        public string SIRKET_KODU { get; set; }
    }

}
