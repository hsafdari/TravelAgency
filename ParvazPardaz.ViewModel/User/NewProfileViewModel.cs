using ParvazPardaz.Model.Enum;
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
    public class NewProfileViewModel : BaseViewModelId
    {
        #region Properties
        /// <summary>
        /// نام کاربر
        /// </summary>
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string LastName { get; set; }

        /// <summary>
        /// نام نمایش کاربر
        /// </summary>
        [ScaffoldColumn(false)]
        public string DisplayName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        #region Avatar image
        /// <summary>
        /// آپلود عکس 
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }

        /// مسیر عکس  پروفایل
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// پسوند عکس پروفایل
        /// </summary>
        public string AvatarExtension { get; set; }
        /// <summary>
        /// اندازه فایل پروفایل
        /// </summary>
        public long AvatarSize { get; set; }
        /// <summary>
        /// نام فایل عکس پروفایل
        /// </summary>
        public string AvatarFileName { get; set; }
        #endregion

        /// <summary>
        /// تازیخ تولد
        /// </summary>
        [Display(Name = "Birthday", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [UIHint("KendoDateTimePicker")]
        public Nullable<DateTime> BirthDate { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Gender Gender { get; set; }

        /// <summary>
        /// ایمیل بازیابی
        /// </summary>
        [Display(Name = "RecoveryEmail", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string RecoveryEmail { get; set; }

        /// <summary>
        /// موبایل کاربر
        /// </summary>
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "MobileNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(15, ErrorMessageResourceName = "MaxLengthMobileNumber", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages), MinimumLength = 5)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string MobileNumber { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// فکس
        /// </summary>
        [Display(Name = "Fax", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Fax { get; set; }
        /// <summary>
        /// سازمان
        /// </summary>
        [Display(Name = "Organization", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Organization { get; set; }
        [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string NationalCode { get; set; }

        /// <summary>
        /// وب سایت کاربر
        /// </summary>
        [Display(Name = "WebSiteUrl", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// شبکه های اجتماعی
        /// </summary>
        [Display(Name = "Facebook", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Facebook { get; set; }
        [Display(Name = "Twitter", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Twitter { get; set; }
        [Display(Name = "Instagram", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Instagram { get; set; }
        [Display(Name = "LinkedIn", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string LinkedIn { get; set; }
        [Display(Name = "Telegram", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Telegram { get; set; }
        [Display(Name = "OtherSocialNetwork", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string OtherSocialNetwork { get; set; }

        /// <summary>
        /// شرح محرمانه
        /// </summary>
        [Display(Name = "OwnDescription", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [AllowHtml]
        [UIHint("TinyMCE_Modern")]
        public string OwnDescription { get; set; }

        /// <summary>
        /// شرح عمومی
        /// </summary>
        [Display(Name = "PublicDescription", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [AllowHtml]
        [UIHint("TinyMCE_Modern")]
        public string PublicDescription { get; set; }
        #endregion
    }
}
