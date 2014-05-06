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
        public string Title { get; set; }
        public string Notes { get; set; }
        public string SerialNumber { get; set; }

        //Relations
        public virtual ICollection<RSStockItem> Items { get; set; }
    }
}
