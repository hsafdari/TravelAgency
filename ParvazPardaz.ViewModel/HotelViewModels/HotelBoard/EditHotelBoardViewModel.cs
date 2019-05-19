using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class EditHotelBoardViewModel : BaseViewModelId
    {
        #region Properties
        [Display(Name = "TitleFA", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "TitleEN", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Name { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public bool IsActive { get; set; }
        /// <summary>
        ///فایل
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
        #endregion
    }
}
