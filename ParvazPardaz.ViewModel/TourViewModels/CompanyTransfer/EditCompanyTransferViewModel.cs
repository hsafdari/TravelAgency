using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class EditCompanyTransferViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام شرکت مسافربری
        /// </summary>
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        [Display(Name = "TitleEn", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TitleEn { get; set; }

        /// <summary>
        /// کد یاتا
        /// </summary>
        [Display(Name = "IataCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(4, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string IataCode { get; set; }

        /// <summary>
        /// شماره تلفن شرکت
        /// </summary>
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Tel { get; set; }
        /// <summary>
        /// آدرس 
        /// </summary>
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Address { get; set; }
        /// <summary>
        /// آپلود عکس 
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// مسیر عکسی از لوگوی شرکت
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس لوگو
        /// </summary>
        public string ImageType { get; set; }
        /// <summary>
        /// نام فایل عکس لوگو
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
    }
}
