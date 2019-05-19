using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Entity.Users
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        #region Properties
        /// <summary>
        /// نام 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// نام نمایش کاربر
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// آیا کاربر سیستمی است؟
        /// </summary>
        public bool IsSystemAccount { get; set; }
        /// <summary>
        /// نشانده دهنده قفل بودن کاربر است
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// برای مسائل مربوط به همزمانی ها
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// حذف به صورت منطقی
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// تاریخ حذف به صورت منطقی
        /// </summary>
        public DateTime? DeletedDateTime { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime? CreatorDateTime { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime? ModifierDateTime { get; set; }

        /// <summary>
        /// وضعیت کاربر
        /// </summary>
        public StatusUser Status { get; set; }

        /// <summary>
        /// کد درخواست بازیابی رمز عبور
        /// </summary>
        public string RecoveryPasswordCode { get; set; }
        /// <summary>
        /// زمان ایجاد کد درخواست بازیابی رمز عبور 
        /// </summary>
        public DateTime? RecoveryPasswordCreatedDateTime { get; set; }
        /// <summary>
        /// زمان انقضای کد درخواست بازیابی رمز عبور
        /// </summary>
        public DateTime? RecoveryPasswordExpireDate { get; set; }
        /// <summary>
        /// وضعیت کد درخواست بازیابی رمز عبور -شامل فعال و غیر فعال
        /// </summary>
        public bool? RecoveryPasswordStatus { get; set; }

        /// <summary>
        /// میزان اعتبار باقی مانده
        /// </summary>
        public decimal RemainCreditValue { get; set; }
        #endregion

        #region One to One navigation property
        public virtual UserProfile UserProfile { get; set; }

        #endregion

        #region Collection navigation properties
        public virtual ICollection<OrderInformation> OrderInformations { get; set; }
        #endregion
    }
}
