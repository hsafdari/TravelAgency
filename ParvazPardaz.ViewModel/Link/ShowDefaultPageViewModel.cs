using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ShowDefaultPageViewModel:BaseViewModelBigId
    {
        public int typeId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }                
        public bool Visible { get; set; }
        public LinkType linkType { get; set; }        
        public int VisitCount { get; set; }
        public decimal? PostRateAvg { get; set; }
        public int? PostRateCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
