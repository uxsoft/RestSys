using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using RestSys.Models;
using System.Web;

namespace RestSys.Models
{
    public partial class RSDbContext : DbContext
    {
        public RSDbContext()
        {
        }

        public DbSet<RSNavigationItem> Navigation { get; set; }
        public DbSet<RSOrder> Orders { get; set; }
        public DbSet<RSProduct> Products { get; set; }
        public DbSet<RSReceipt> Receipts { get; set; }
        public DbSet<RSStock> Stocks { get; set; }
        public DbSet<RSStyle> Styles { get; set; }
        public DbSet<RSUser> Users { get; set; }

        public DbSet<RSStockItem> StockItems { get; set; }
        public DbSet<RSOrderItem> OrderItems { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}