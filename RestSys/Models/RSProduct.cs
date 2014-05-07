﻿using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSProduct : IRSEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool ShowOnMenu { get; set; }
        public string SerialNumber { get; set; }
        public string Category { get; set; }

        //Relations
        public virtual ICollection<RSStockItem> Stocks { get; set; }
        public virtual ICollection<RSOrderItem> DependentOrderItems { get; set; }
        public virtual ICollection<RSNavigationItem> DependentNavigationItems { get; set; }
    }
}
