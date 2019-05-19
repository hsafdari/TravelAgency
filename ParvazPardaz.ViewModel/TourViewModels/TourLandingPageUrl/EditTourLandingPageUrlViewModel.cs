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
    public class EditTourLandingPageUrlViewModel : BaseViewModelId
    {
        #region Constructor
        public EditTourLandingPageUrlViewModel()
        {

        }
        #endregion

        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(400, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(400, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string URL { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "IsAvailable", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsAvailable { get; set; }

        [Display(Name = "LandingPageUrlType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public EnumLandingPageUrlType LandingPageUrlType { get; set; }
        #endregion

        #region Reference navigation property
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> CityDDL { get; set; }
        #endregion
    }
}
