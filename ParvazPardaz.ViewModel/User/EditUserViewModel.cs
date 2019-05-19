using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditUserViewModel : BaseViewModelId
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(256, ErrorMessageResourceName = "MaxLengthUserName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessageResourceName = "ReqularFormatUserName", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string UserName { get; set; }
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
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        public string PhoneNumber { get; set; }
        ///// <summary>
        ///// جنسیت
        ///// </summary>
        //[Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public Gender Gender { get; set; }
        /// <summary>
        // لیست آیدیهای انتخاب شده از کنترل مالتی سلکت در جدول نقشها 
        /// </summary>
        [Display(Name = "Role", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public List<int> SelectedRoles { get; set; }
        /// <summary>
        /// لیستی از نقشهای فعال جهت بایند به کنترل مالتی سلکت 
        /// </summary>
        //public IEnumerable<SelectListItem> Roles { get; set; }

        /// <summary>
        /// وضعیت کاربر
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public StatusUser Status { get; set; }
    }
}
