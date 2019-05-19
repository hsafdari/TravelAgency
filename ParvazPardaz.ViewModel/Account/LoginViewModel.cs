using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.User
{
    public class LoginViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ReqularFormatUserName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string UserName { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        [Display(Name = "Password", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(50, ErrorMessageResourceName = "MaxLengthPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// مرا به خاطر بسپار
        /// </summary>
        [Display(Name = "RememberMe", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public bool RememberMe { get; set; }
    }
}
