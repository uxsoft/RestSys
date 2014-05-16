using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestSys.Models
{
    public class RSReceipt : IRSEntity
    {
        public RSReceipt()
        {
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        //Relations
        public RSUser User { get; set; }
        public RSOrder Order { get; set; }
        public virtual ICollection<RSOrderItem> PaidItems { get; set; }
    }
}