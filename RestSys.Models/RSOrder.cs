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
        [Display(Name = "Název")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Vytvořeno")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Display(Name = "Aktivní")]
        public bool Active { get; set; }

        [Display(Name = "Poznámky")]
        public string Notes { get; set; }


        //Relations
        public virtual ICollection<RSOrderItem> Items { get; set; }
        public virtual ICollection<RSReceipt> Receipts { get; set; }
        public virtual ICollection<RSUser> Attendees { get; set; }
    }
}
