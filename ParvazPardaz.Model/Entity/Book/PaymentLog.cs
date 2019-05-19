using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Common;

namespace ParvazPardaz.Model.Book
{
    public class PaymentLog : BaseBigEntity
    {
        #region Properties
        /// <summary>
        /// کد پیگیری
        /// </summary>
        public string TrackingCode { get; set; }

        /// <summary>
        /// کد پاسخ
        /// </summary>
        public string PaymentResponseCode { get; set; }
       
        /// <summary>
        /// پیام پاسخ
        /// </summary>
        public string PaymentResponseMessage { get; set; }
        
        /// <summary>
        /// موفق بود؟
        /// </summary>
        public bool IsSuccessful { get; set; }
        
        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// کل مبلغ
        /// </summary>
        public decimal TotalAmount { get; set; }
        #endregion

        #region Reference navigation properties
        /// <summary>
        /// از طریق کدوم بانک؟
        /// </summary>
        public Nullable<int> BankId { get; set; }
        public virtual Bank Bank { get; set; }

        /// <summary>
        /// مربوط به کدام سفارش؟
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// شناسه یکتای پرداخت
        /// </summary>
        public long PaymentUniqueNumberID { get; set; }
        #endregion
    }
}
