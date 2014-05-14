using Newtonsoft.Json.Linq;
using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestSys.Models
{
    public class RSNavigationItem : IRSEntity
    {
        public int Id { get; set; }

        public string ChildrenOrder { get; set; }
        [Display(Name = "Název")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsRoot { get; set; }
        public string Color { get; set; }

        //Relations
        public RSProduct ProductLink { get; set; }
        public virtual ICollection<RSNavigationItem> Children { get; set; }
        public RSNavigationItem Parent { get; set; }

        public IEnumerable<RSNavigationItem> OrderedChildren()
        {
            try
            {
                JArray order = JArray.Parse(ChildrenOrder);
                return Children.OrderBy(ni => order.IndexOf(order.Single(jt => jt.Value<int>() == ni.Id)));
            }
            catch
            {
                return Children;
            }
        }
    }
}