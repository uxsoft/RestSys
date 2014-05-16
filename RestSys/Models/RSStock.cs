using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSStock : IRSEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Název")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Množství")]
        public double Quantity { get; set; }

        [Display(Name = "Jednotky")]
        public string Unit { get; set; }

        [Display(Name = "Poznámky")]
        public string Notes { get; set; }

        [Display(Name = "Kód")]
        public string SerialNumber { get; set; }

        //Relations
        public virtual ICollection<RSStockItem> Items { get; set; }
    }
}
