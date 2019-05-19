using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditHotelGalleryViewModel:BaseViewModelId
    {
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        [Display(Name = "ImageFileName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ImageFileName { get; set; }
        /// <summary>
        /// مسیر عکسی از هتل
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [AdditionalMetadata("cropwidth", "500"), AdditionalMetadata("cropheight", "300"), AdditionalMetadata("width", "600"), AdditionalMetadata("height", "600")]
        [UIHint("UploaderCrop")]
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
