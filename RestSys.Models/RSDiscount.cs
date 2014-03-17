using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSDiscount : IRSEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        [Required]
        public string ModuleId { get; set; }
        public string Data { get; set; }
        public string Notes { get; set; }

        //Relations
        public virtual RSUser Author { get; set; }
        public virtual ICollection<RSOrder> DependentOrders { get; set; }
    }
}
