using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Model.Entity.Link
{
    public class LinkTable : BaseBigEntity
    {
        public int typeId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public LinkType linkType { get; set; }
        public string CustomMetaTags { get; set; }
        public int VisitCount { get; set; }
        public decimal? PostRateAvg { get; set; }
        public int? PostRateCount { get; set; }
        public Nullable<EnumChangeFreq> Changefreq { get; set; }
        public Nullable<decimal> Priority { get; set; }
    }
}
