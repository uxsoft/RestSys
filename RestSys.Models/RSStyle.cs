using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestSys.Models
{
    public class RSStyle : IRSEntity
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public RSStyleType Type { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public bool Selected { get; set; }
    }
    public enum RSStyleType
    {
        MenuStyle,
        ReceiptStyle
    }
}
