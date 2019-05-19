using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class SiteMapViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public Nullable<DateTime> PubDate { get; set; }
        
        public Nullable<EnumChangeFreq> Changefreq { get; set; }
        
        public Nullable<decimal> Priority { get; set; }
    }
}
