using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class ImageSliderViewModel:BaseViewModelId
    {

        #region Properties
        public string ImageTitle { get; set; }
        public string ImageDesc { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه عکس
        /// </summary>
        public long ImageSize { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// عکس یرای اسلایدر اصلی بالای صفحه است ؟
        /// </summary>
        public bool IsPrimarySlider { get; set; }

        [Display(Name = "ImagesGallery", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; } 
        #endregion
        public int PostId { get; set; }
        public List<ParvazPardaz.Common.HtmlHelpers.Models.EditModeFileUpload> EditModeFileUploads { get; set; }
    }
}
