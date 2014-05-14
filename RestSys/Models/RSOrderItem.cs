using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSOrderItem : IRSEntity
    {
        public RSOrderItem()
        {
            CreatedOn = DateTime.Now;
            State = (int)RSOrderState.New;
        }

        public RSOrderItem(RSProduct product, RSOrder order)
            : this()
        {
            Product = product;
            Order = order;
            Price = product.Price;
        }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int State { get; set; }
        public double Price { get; set; }

        //Relations
        public RSReceipt Receipt { get; set; }
        public RSProduct Product { get; set; }
        public RSOrder Order { get; set; }
    }

    public enum RSOrderState : int
    {
        New = 0,
        Dispatched = 1,
        Canceled = 2,
        Paid = 3
    }
}