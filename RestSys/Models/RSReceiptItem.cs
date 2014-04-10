using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSReceiptItem : IRSEntity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        //Relations
        public RSProduct Product { get; set; }
        public RSReceipt Order { get; set; }
    }
}