using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class GridTourLandingPageUrlViewModel : BaseViewModelOfEntity
    {
        #region Constructor
        public GridTourLandingPageUrlViewModel()
        {

        }
        #endregion

        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string URL { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [ScaffoldColumn(false)]
        public string Description { get; set; }

        [Display(Name = "IsAvailable", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsAvailable { get; set; }

        [Display(Name = "LandingPageUrlType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public EnumLandingPageUrlType LandingPageUrlType { get; set; }
        #endregion

        #region Reference navigation property
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle{ get; set; }
        #endregion
    }
}
