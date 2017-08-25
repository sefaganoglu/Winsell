using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Winsell.Hopi.Data.Entity;

namespace Winsell.Hopi.Data
{
    public class HopiContext : DbContext
    {
        public DbSet<HOPI> HOPIS { get; set; }
        public DbSet<HOPI_SIRKET> HOPI_SIRKETS { get; set; }
        public DbSet<KRDKRT> KRDKRTS { get; set; }
        public DbSet<KRDRMZ> KRDRMZS { get; set; }
        public DbSet<ODEME_TEMP> ODEME_TEMPS { get; set; }
        public DbSet<RESCEK> RESCEKS { get; set; }
        public DbSet<RESHRY> RESHRYS { get; set; }
        public DbSet<RESPRM> RESPRMS { get; set; }
        public DbSet<SIRANO> SIRANOS { get; set; }
        public DbSet<STKMAS> STKMASS { get; set; }
    }
}
