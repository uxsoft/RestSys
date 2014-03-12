using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSShift
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public RSUser User { get; set; }
        public string Notes { get; set; }
    }
}
