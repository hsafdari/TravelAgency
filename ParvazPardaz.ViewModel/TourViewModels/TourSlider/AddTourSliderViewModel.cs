using ParvazPardaz.Common.HtmlHelpers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class AddTourSliderViewModel : BaseViewModelId
    {
        public int TourId { get; set; }
        /// <summary>
        /// آپلود اسلایدرهای تور
        /// </summary>
        [Display(Name = "ImagesGallery", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// آپلود فایل اصلی اسلاید تور
        /// </summary>
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "HeaderImage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase PrimarySlider { get; set; }
        public List<EditModeFileUpload> EditModeFileUploads { get; set; }
        public List<EditModeFileUpload> EditModeFileUploadsForPrimarySlider { get; set; }
    }
}
