using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddStateViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام استان
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Remote("IsUniqueTitle", "State", "Admin")]
        public string Title { get; set; }
        /// <summary>
        /// تصویری از استان
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// آیدی کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CountryId { get; set; }
        /// <summary>
        /// کل کشورهای حذف نشده
        /// </summary>
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
