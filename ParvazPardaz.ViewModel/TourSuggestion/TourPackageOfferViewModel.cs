using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourSuggestionViewModel
    {
        #region Properties
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TourTitle { get; set; }
        [Display(Name = "DateTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TourDate { get; set; }
        [Display(Name = "Transportation", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string AirlineTitle { get; set; }
        [Display(Name = "TourDuration", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TourDuration { get; set; }
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TourPrice { get; set; }
        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ImageUrl { get; set; }
        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string NavigationUrl { get; set; }
        [Display(Name = "LocationId", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int LocationId { get; set; }
        /// <summary>
        /// اولویت نمایش اسلایدر
        /// </summary>

        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int Priority { get; set; }
        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool ImageIsActive { get; set; }
        #endregion
    }
}
