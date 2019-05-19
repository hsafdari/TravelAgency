using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridLocationViewModel : BaseViewModelOfEntity
    {
        #region Properties
        /// <summary>
        /// نام موقعیت مکانی
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        /// <summary>
        /// عنوان به زبان انگلیسی
        /// </summary>
        [Display(Name = "ENTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ENTitle { get; set; }

        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string URL { get; set; }
        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ImageUrl { get; set; }
        /// <summary>
        /// متن سئو
        /// </summary>
        //[Display(Name = "SeoText", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //public string SeoText { get; set; }
        #endregion
    }
}
