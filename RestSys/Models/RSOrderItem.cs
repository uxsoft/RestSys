using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSOrderItem : IRSEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        //Relations
        public RSProduct Product { get; set; }
        public RSOrder Order { get; set; }
    }
}