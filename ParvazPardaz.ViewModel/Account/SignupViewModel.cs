using ParvazPardaz.Common.Filters;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NSDataAnnotations = System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class SignupViewModel : BaseViewModelId
    {
        public SignupViewModel()
        {

        }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "NameFamily", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string FullName { get; set; }

        /// <summary>
        /// ایمیل کاربر
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Remote("IsUniqueCustomerEmail", "Account", AdditionalFields = "Id", ErrorMessageResourceName = "DuplicateEmail", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = (value != null) ? value.ToLower() : value;
            }
        }

        /// <summary>
        /// نام کاربری
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string UserName { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(50, ErrorMessageResourceName = "MaxLengthPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Password { get; set; }

        ///// <summary>
        ///// تکرار کلمه عبور
        ///// </summary>
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //[DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        //[NSDataAnnotations.Compare("Password", ErrorMessageResourceName = "ConfirmPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public Gender Gender { get; set; }
    }
}
