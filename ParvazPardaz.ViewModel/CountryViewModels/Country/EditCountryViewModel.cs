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
    public class EditCountryViewModel:BaseViewModelId
    {
        /// <summary>
        /// نام کشور
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
        /// پرچم کشور
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// مسیر عکسی از کشور
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

        [Display(Name = "SeoText", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string SeoText { get; set; }
    }
}
