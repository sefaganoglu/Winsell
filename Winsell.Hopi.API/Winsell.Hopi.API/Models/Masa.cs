using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Winsell.Hopi.API.Models
{
    public class Masa
    {
        public int no { get; set; }
        public string noStr { get; set; }
        public decimal tutar { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string yetki { get; set; }
        public Color color { get; set; }
    }
}
