using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Winsell.Hopi.Data.Entity;

namespace Winsell.Hopi.Data
{
    public class HopiContext : DbContext
    {
        public DbSet<RESCEK> RESCEKS { get; set; }
    }
}
