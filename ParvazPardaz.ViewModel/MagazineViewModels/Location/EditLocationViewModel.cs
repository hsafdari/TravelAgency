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
    public class EditLocationViewModel : BaseViewModelId
    {
        #region Constructor
        public EditLocationViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// نام موقعیت مکانی
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

        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string URL { get; set; }

        /// <summary>
        /// متن سئو
        /// </summary>
        [Display(Name = "SeoText", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string SeoText { get; set; }
        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ImageUrl { get; set; }
        #endregion
    }
}
