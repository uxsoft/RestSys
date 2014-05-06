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
        public RSOrder()
        {
            Active = true;
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public String Title { get; set; }
        [Required]
        public DateTime CreatedOn { get; private set; }
        [Required]
        public bool Active { get; set; }
        public string Notes { get; set; }

        //Relations
        public virtual ICollection<RSOrderItem> Items { get; set; }
        public virtual ICollection<RSReceipt> Receipts { get; set; }
        public virtual ICollection<RSUser> Attendees { get; set; }
    }
}
