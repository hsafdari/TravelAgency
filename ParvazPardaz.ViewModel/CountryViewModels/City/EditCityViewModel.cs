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
    public class EditCityViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام شهر
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        /// <summary>
        /// عنوان به زبان انگلیسی
        /// </summary>
        [Display(Name = "ENTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ENTitle { get; set; }

        /// <summary>
        /// شناسه کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int CountryId { get; set; }

        /// <summary>
        /// کل کشورهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> CountryList { get; set; }
        /// <summary>
        /// شناسه استان
        /// </summary>
        [Display(Name = "State", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int StateId { get; set; }
        /// <summary>
        /// کل کشورهای حذف نـشده
        /// </summary>
        public IEnumerable<SelectListItem> StateList { get; set; }
        /// <summary>
        /// آپلود تصویر
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// مسیر عکسی از شهر
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        [ScaffoldColumn(false)]
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        [ScaffoldColumn(false)]
        public long ImageSize { get; set; }
        [Display(Name = "IsDddlFrom", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsDddlFrom { get; set; }
        [Display(Name = "IsDddlDestination", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsDddlDestination { get; set; }

    }
}
