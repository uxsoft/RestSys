using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSOrder : IRSEntity
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }
        public string Notes { get; set; }

        //Relations
        public virtual ICollection<RSOrderItem> Items { get; set; }
        public virtual ICollection<RSReceipt> Receipts { get; set; }
    }
}
