using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddFAQViewModel : BaseViewModelId
    {
        /// <summary>
        ///  سوال
        /// </summary>
        /// 
        [Display(Name = "Question", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string Question { get; set; }
        /// <summary>
        /// جواب
        /// </summary>
        /// 
        [Display(Name = "Answer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string Answer { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن 
        /// </summary>
        /// 
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        
        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public string  TourTitle { get; set; }
  
        [Display(Name = "Tour", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int TourId { get; set; }
        public IEnumerable<SelectListItem> TourList { get; set; }
    }
}
