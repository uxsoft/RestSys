using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSDbContext : DbContext
    {
        public DbSet<RSNavigationItem> Navigation { get; set; }
        public DbSet<RSOrder> Orders { get; set; }
        public DbSet<RSProduct> Products { get; set; }
        public DbSet<RSReceipt> Receipts { get; set; }
        public DbSet<RSStock> Stocks { get; set; }
        public DbSet<RSStyle> Styles { get; set; }
        public DbSet<RSUser> Users { get; set; }    
    }
}