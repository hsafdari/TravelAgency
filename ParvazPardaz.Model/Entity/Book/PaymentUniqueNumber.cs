using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Book
{
    /// <summary>
    /// جدول شناسه یکتای پرداخت
    /// </summary>
    public class PaymentUniqueNumber : BaseBigEntity
    {
        #region Reference navigation property
        /// <summary>
        /// مرتبط با کدام سفارش؟
        /// </summary>
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        #endregion
    }
}
