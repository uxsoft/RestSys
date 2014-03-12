using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSStock
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public double Quantity { get; set; }
        public string Notes { get; set; }
        public string SerialNumber { get; set; }
    }
}
