using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class TourLandingPageUrl : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public EnumLandingPageUrlType LandingPageUrlType { get; set; }
        #endregion

        #region Reference navigation property
        public int CityId { get; set; }
        public virtual City City { get; set; }
        #endregion

        //#region 1:0-1 navigation property
        //public virtual Tour Tour { get; set; }
        //#endregion

    }
}
