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
        [Key]
        public int Id { get; set; }
        public DateTime DateOrdered { get; set; }
        public double TotalPrice { get; set; }
        public string Notes { get; set; }
        public RSOrderPaymentStatus PaymentStatus { get; set; }

        [Required]
        public virtual RSUser Waiter { get; set; }
        public virtual ICollection<RSDiscount> Discounts { get; set; }
        public virtual ICollection<RSProduct> Products { get; set; }
    }

    public enum RSOrderPaymentStatus
    {
        NotPaid,
        CreditCard,
        Cash,
        Check
    }
}
