using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelGalleryViewModel:BaseViewModelOfEntity
    {
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        [Display(Name = "ImageFileName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageFileName { get; set; }
        /// <summary>
        /// مسیر عکسی از هتل
        /// </summary>
        [Display(Name = "ImageUrl", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageUrl { get; set; }
    }
}
