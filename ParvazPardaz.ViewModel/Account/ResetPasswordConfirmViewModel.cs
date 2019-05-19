using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ResetPasswordConfirmViewModel : BaseViewModelId
    {
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Username { get; set; }
        /// <summary>
        /// کلمه عبور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(50, ErrorMessageResourceName = "MaxLengthPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Password { get; set; }
        /// <summary>
        /// تکرار کلمه عبور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ConfirmPassword { get; set; }
        public string RecoveryPasswordCode { get; set; }
        public bool IsActive { get; set; }
    }
}
