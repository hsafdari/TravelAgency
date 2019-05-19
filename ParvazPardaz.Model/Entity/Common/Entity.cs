using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.Model.Entity.Common
{
    public abstract class Entity
    {
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
        /// کاربر ایجاد کننده
        /// </summary>
        public virtual User CreatorUser { get; set; }
        /// <summary>
        /// آیدی کلید خارجی کاربر ایجاد کننده
        /// </summary>
        public Nullable<int> CreatorUserId { get; set; }
        /// <summary>
        /// <summary>
        /// آی پی کاربر ایجاد کننده
        /// </summary>
        [MaxLength(20)]
        public string CreatorUserIpAddress { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime? ModifierDateTime { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public virtual User ModifierUser { get; set; }
        /// <summary>
        ///  آیدی کلید خارجی کاربر ویرایش کننده 
        /// </summary>
        public Nullable<int> ModifierUserId { get; set; }
        /// <summary>
        /// آی پی کاربر آخرین ویرایش
        /// </summary>
        [MaxLength(20)]
        public string ModifierUserIpAddress { get; set; }

        /// <summary>
        /// برای مسائل مربوط به همزمانی ها
        /// gets or sets TimeStamp for prevent concurrency Problems
        /// </summary>
        public virtual byte[] RowVersion { get; set; }
    }
}
