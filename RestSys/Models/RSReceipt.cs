using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSReceipt : IRSEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        //Relations
        [Required]
        public RSUser User { get; set; }
        public RSOrder Order { get; set; }
        public virtual ICollection<RSReceiptItem> PaidItems { get; set; }
    }
}