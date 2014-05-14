using RestSys.Models.Exports;
using RestSys.Resources;
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
        [Display(Name = "Název")]
        public string Title { get; set; }

        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "V menu?")]
        public bool ShowOnMenu { get; set; }

        [Display(Name = "Kód")]
        public string SerialNumber { get; set; }

        [Display(Name = "Kategorie")]
        public string Category { get; set; }

        [Display(Name = "Podkategorie")]
        public string SubCategory { get; set; }

        //Relations
        public virtual ICollection<RSStockItem> Stocks { get; set; }
        public virtual ICollection<RSOrderItem> DependentOrderItems { get; set; }
        public virtual ICollection<RSNavigationItem> DependentNavigationItems { get; set; }
    }
}
