using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity
{
    public class Coupon:BaseEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// کد کاربری اعتبار سنجی شده
        /// </summary>
        public User ValidatedUser { get; set; }
        public int? ValidatedUserId { get; set; }

        /// <summary>
        /// تاریخ اعتبار سنجی شده
        /// </summary>
        public DateTime? ValidatedDate { get; set; }
        public long? OrderId { get; set; }
    }
}
