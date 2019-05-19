using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class PassengerDetailControl : BaseViewModelId
    {
        /// <summary>
        /// جنسیت
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// نام 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(500, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Address { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "ZipCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ZipCode { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        [Display(Name = "MobileNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email { get; set; }
        /// <summary>
        /// نام پاسپورت
        /// </summary>
        [Display(Name = "OfficialNameOfPassport", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string OfficialNameOfPassport { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        [Display(Name = "Nationality", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int Nationality { get; set; }
        /// <summary>
        /// مکان صدور پاسپورت
        /// </summary>
        [Display(Name = "PlaceOfIssueOfPassport", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int? PlaceOfIssueOfPassport { get; set; }
        /// <summary>
        /// تاریخ صدور پاسپورت
        /// </summary>
        [Display(Name = "DateOfIssueOfPasssprot", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public DateTime? DateOfIssueOfPasssprot { get; set; }
    }
}
