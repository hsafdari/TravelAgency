using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class EditHotelRankViewModel:BaseViewModelId
    {
        /// <summary>
        /// عنوان رتبه هتل
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }
        /// <summary>
        /// ایکون رتبه هتل
        /// </summary>
        [Display(Name = "Icon", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Icon { get; set; }
        /// <summary>
        /// ترتیب رتبه هتل
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Rank", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int OrderId { get; set; }
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// پسوند عکس لوگو
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// نام فایل عکس لوگو
        /// </summary>
        public string ImageFileName { get; set; }
    }
}
