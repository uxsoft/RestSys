using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSDbContext : DbContext
    {
        //public DbSet<RSDiscount> Discounts { get; set; }
        //public DbSet<RSOrder> Orders { get; set; }
        //public DbSet<RSProduct> Products { get; set; }
        //public DbSet<RSShift> Shifts { get; set; }
        //public DbSet<RSStock> Stocks { get; set; }
        public DbSet<RSStyle> Styles { get; set; }
        //public DbSet<RSUser> Users { get; set; }    
    }
}