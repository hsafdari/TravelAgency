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
    public class AddHotelGalleryViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        //[StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "ImageFileName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageFileName { get; set; }
        /// <summary>
        /// عکس هتل
        /// </summary>
        //[AdditionalMetadata("cropwidth", "500"), AdditionalMetadata("cropheight", "300"), AdditionalMetadata("width", "600"), AdditionalMetadata("height", "600")]
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [AdditionalMetadata("cropwidth", "500"), AdditionalMetadata("cropheight", "300"), AdditionalMetadata("width", "600"), AdditionalMetadata("height", "600")]
        [UIHint("UploaderCrop")]
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
