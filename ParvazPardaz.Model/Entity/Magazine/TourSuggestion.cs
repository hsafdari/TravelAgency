using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Magazine
{
    public class TourSuggestion: BaseEntity
    {
        #region Properties
        public string TourTitle { get; set; }
        public string TourDate { get; set; }
        public string AirlineTitle { get; set; }
        public string TourDuration { get; set; }
        public string TourPrice { get; set; }
        public string ImageURL { get; set; }
        public string NavigationUrl { get; set; }
        public int Priority { get; set; }
        public bool ImageIsActive { get; set; }
        #endregion
        #region Reference properties
        public int LocationId { get; set; }
        public virtual Location Locations { get; set; }
    
        #endregion
    }
}
