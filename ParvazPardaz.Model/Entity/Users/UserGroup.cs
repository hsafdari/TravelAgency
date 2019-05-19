using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Users
{
    public class UserGroup : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// میزان درصدی که به گروه کاربری تعلق میگیرد
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// حداقل میزان موجودی اعتبار برای گروه کاربری
        /// </summary>
        public decimal MinCreditValue { get; set; }

        /// <summary>
        /// حداکثر تعداد فروش در روز
        /// </summary>
        public int SalesCountPerDay { get; set; } 
        #endregion

        #region Collection navigation properties
        /// <summary>
        /// پروفایل های کاربری مرتبط با این گروه کاربری
        /// </summary>
        public virtual ICollection<UserProfile> UserProfiles { get; set; } 
        #endregion
    }
}
