using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParvazPardaz.ViewModel
{
    public class AddUserViewModel : BaseViewModelId
    {
        public AddUserViewModel()
        {
            Status = StatusUser.Active;
        }

        /// <summary>
        /// نام کاربری
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(256, ErrorMessageResourceName = "MaxLengthUserName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ReqularFormatUserName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string UserName { get; set; }
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
        /// <summary>
        /// نام کاربر
        /// </summary>
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string LastName { get; set; }
        /// <summary>
        /// ایمیل کاربر
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Email { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public Gender Gender { get; set; }
        /// <summary>
        // لیست آیدیهای انتخاب شده از کنترل مالتی سلکت در جدول نقشها 
        /// </summary>
        [Display(Name = "Role", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public List<int> SelectedRoles { get; set; }

        /// <summary>
        /// وضعیت کاربر
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public StatusUser Status { get; set; }
    }
}
