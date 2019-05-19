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
    public class AddCompanyTransferViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام شرکت مسافربری
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
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
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Tel { get; set; }

        /// <summary>
        /// آدرس 
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [StringLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Address { get; set; }

        /// <summary>
        /// آپلود عکس 
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
    }
}
