using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSStockItem : IRSEntity
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        //Relations
        [Required]
        public RSStock Stock { get; set; }
        [Required]
        public RSProduct Product { get; set; }
    }
}