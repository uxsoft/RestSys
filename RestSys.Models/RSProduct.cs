using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSProduct
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool ShowOnMenu { get; set; }
        public string SerialNumber { get; set; }
        public double Amount { get; set; }

        public virtual ICollection<RSStock> Stocks { get; set; }
        public virtual ICollection<RSOrder> Orders { get; set; }
    
    }
}
