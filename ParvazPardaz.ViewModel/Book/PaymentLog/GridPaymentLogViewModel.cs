using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPaymentLogViewModel : BigBaseViewModelOfEntity
    {
        #region Properties
        /// <summary>
        /// مربوط به کدام سفارش؟
        /// </summary>
        [Display(Name = "OrderId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }

        /// <summary>
        /// کد رهگیری سفارش
        /// </summary>
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TrackingCode { get; set; }

        /// <summary>
        /// شناسه یکتای پرداخت
        /// </summary>
        [Display(Name = "PaymentUniqueNumberID", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long PaymentUniqueNumberID { get; set; }

        /// <summary>
        /// از طریق کدوم بانک؟
        /// </summary>
        [Display(Name = "BankTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BankTitle { get; set; }

        /// <summary>
        /// کد پاسخ
        /// </summary>
        [Display(Name = "PaymentResponseCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string PaymentResponseCode { get; set; }

        /// <summary>
        /// پیام پاسخ
        /// </summary>
        [Display(Name = "PaymentResponseMessage", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string PaymentResponseMessage { get; set; }

        /// <summary>
        /// موفق بود؟
        /// </summary>
        [Display(Name = "IsSuccessful", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// تاریخ پرداخت
        /// </summary>
        [Display(Name = "PaymentDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// کل مبلغ
        /// </summary>
        [Display(Name = "TotalAmount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalAmount { get; set; }
        #endregion
    }
}
