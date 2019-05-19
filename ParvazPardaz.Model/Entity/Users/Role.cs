using Microsoft.AspNet.Identity.EntityFramework;
using ParvazPardaz.Model.Entity.AccessLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Users
{
    public class Role : IdentityRole<int, UserRole>
    {
        /// <summary>
        /// آیا نقش سیستمی هستند؟
        /// </summary>
        public bool IsSystemRole { get; set; }
        /// <summary>
        /// آیا حساب  کاربران این گروه کاربری مسدود شود؟
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// برای مسائل همزمانی 
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime? CreatorDateTime { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime? ModifierDateTime { get; set; }

        #region Collection navigation properties
        /// <summary>
        /// این نقش کاربری چه دسترسی هایی و به کدام کنترلر ها دارد؟
        /// </summary>
        public virtual ICollection<RolePermissionController> RolePermissionControllers { get; set; }
        #endregion
    }
}
