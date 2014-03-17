using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSProduct : IRSEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool ShowOnMenu { get; set; }
        public string SerialNumber { get; set; }
        public double Amount { get; set; }

        //Relations
        public virtual ICollection<RSStock> Stocks { get; set; }
        public virtual ICollection<RSOrder> DependentOrders { get; set; }

    }
}
