using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.Model.Book
{
    /// <summary>
    /// لاگ های اعتبار
    /// </summary>
    public class Credit : BaseBigEntity
    {
        #region Properties
        /// <summary>
        /// مبلغ : + بستانکار و - بدهکار
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// نوع لاگ اعتبار
        /// </summary>
        public EnumCreditType CreditType { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Navigation properties
        /// <summary>
        /// برای کدام سفارش؟
        /// </summary>
        public Nullable<long> OrderId { get; set; }
        public virtual Order Order { get; set; }

        /// <summary>
        /// برای کدام آژانس؟ یا سی.آی.پی؟
        /// </summary>
        public Nullable<int> UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        #endregion
    }
}
