using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class LoginCustomerViewModel : BaseViewModelId
    {
        #region Login Properties
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessageResourceName = "IncorrectEmail", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [StringLength(50, ErrorMessageResourceName = "MaxLengthPassword", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Password { get; set; }

        /// <summary>
        /// مرا به خاطر بسپار؟
        /// </summary>
        [Display(Name = "RememberMe", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public bool RememberMe { get; set; }
        #endregion
    }
}
