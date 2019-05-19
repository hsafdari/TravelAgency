using ParvazPardaz.Model.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Users
{
    public class UserProfile : BaseEntity
    {
        #region Profile Properties
        /// <summary>
        /// نام 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// نام نمایش کاربر
        /// </summary>
        public string DisplayName { get; set; }

        #region Avatar image
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
        /// تاریخ تولد
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// شرح محرمانه
        /// </summary>
        public string OwnDescription { get; set; }
        /// <summary>
        /// شرح عمومی
        /// </summary>
        public string PublicDescription { get; set; }
        /// <summary>
        /// موبایل کاربر
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// ایمیل بازیابی
        /// </summary>
        public string RecoveryEmail { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// وب سایت کاربر
        /// </summary>
        public string WebSiteUrl { get; set; }
        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// سازمان
        /// </summary>
        public string Organization { get; set; }
        public string NationalCode { get; set; }
        /// <summary>
        /// شبکه های اجتماعی
        /// </summary>
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Telegram { get; set; }
        public string OtherSocialNetwork { get; set; }
        public UserProfileType ProfileType { get; set; }

        /// <summary>
        /// مانده اعتبار تا الان
        /// </summary>
        public Nullable<decimal> RemainingCreditValue { get; set; }

        /// <summary>
        /// برای مسایل همزمانی 
        /// </summary>
        public byte[] RowVersion { get; set; }
        #endregion

        #region One to One navigation property
        public virtual User User { get; set; }
        public virtual UserCommission UserCommission { get; set; }
        #endregion

        #region Reference navigation properties
        public int? UserGroupId { get; set; }
        public virtual UserGroup UserGroup { get; set; }

        #endregion

        #region Collection Navigation Property
        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// لاگ های اعتبار
        /// </summary>
        public virtual ICollection<Credit> Credits { get; set; }
        #endregion
    }
}
