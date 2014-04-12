using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSNavigationItem : IRSEntity
    {
        public int Id { get; set; }

        public string ChildrenOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        //Relations
        public RSProduct ProductLink { get; set; }
        public virtual ICollection<RSNavigationItem> Children { get; set; }
        public RSNavigationItem Parent { get; set; }
    }
}