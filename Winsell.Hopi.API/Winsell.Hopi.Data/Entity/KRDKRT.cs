using System;
using System.Collections.Generic;
using System.Text;

namespace Winsell.Hopi.Data.Entity
{
    public class KRDKRT
    {
        public int SIRANO { get; set; }

        public string KOD { get; set; }

        public string ACIKLAMA { get; set; }

        public decimal? KESINTIORANI { get; set; }

        public byte[] RESIM { get; set; }

        public string VADELI_HESAP { get; set; }

        public string BANKA_KARTI { get; set; }

        public int? PUAN_KUL_VALOR { get; set; }

        public int? TEK_ODEME_VALOR { get; set; }

        public int? TAKSITLI_SATIS_VALOR { get; set; }

        public int? PESIN_KATKI_PAYI { get; set; }

        public int? TAKSIT_KATKI_PAYI { get; set; }

        public string TAKSITI_AYLIK_YAP { get; set; }

        public string DVZ_KODU { get; set; }

        public decimal? PUAN_KESINTI_ORANI { get; set; }

        public decimal? PESIN_KESINTI_ORANI { get; set; }

        public decimal? TAKSIT_KESINTI_ORANI { get; set; }

        public int? ILK_TAKSIT_GUN_SAYISI { get; set; }

        public int? SON_TAKSIT_GUN_SAYISI { get; set; }

        public string RESIM_YOLU { get; set; }

        public string MUHASEBE_STSKOD { get; set; }

        public string POS_KREDI_KART_KODU { get; set; }

        public string SATIS_TURU { get; set; }

        public string DEFAULT_R_A { get; set; }

        public string TAHSILAT_TURU { get; set; }

        public string FAZLA_DOVIZ_ALABILIR { get; set; }

        public decimal? TUTAR { get; set; }

        public int? YSEPETI_Id { get; set; }

        public string PLU_EKSTRA { get; set; }

        public byte? TAHSILAT_ORANI { get; set; }

        public string FT2002_ODEME_INDEX { get; set; }

        public int? MAX_SIPARIS_INDIRIM_ORANI { get; set; }

        public int? SIRALAMA { get; set; }

        public int? TRACEITUP { get; set; }
    }

}
