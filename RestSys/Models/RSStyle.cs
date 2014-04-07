using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSStyle
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }

        public int Type { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool Selected { get; set; }
    }
    public enum RSStyleType : int
    {
        MenuStyle = 0,
        ReceiptStyle = 1
    }
}
