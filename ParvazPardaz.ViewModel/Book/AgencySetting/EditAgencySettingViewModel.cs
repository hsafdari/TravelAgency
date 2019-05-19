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
    public class EditAgencySettingViewModel : BaseViewModelId
    {
        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Name { get; set; }

        #region Image properties
        /// <summary>
        /// آپلود تصویر
        /// </summary>
        [Display(Name = "LogoUrl", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
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
        #endregion

        [Display(Name = "PrintText", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [UIHint("TinyMCE_Modern")]
        [AllowHtml]
        public string PrintText { get; set; }
        #endregion
    }
}
