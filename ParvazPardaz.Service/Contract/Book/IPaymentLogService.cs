using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Book;

namespace ParvazPardaz.TravelAgency.UI.Services.Interface.Book
{
    public interface IPaymentLogService : IBaseService<PaymentLog>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <param name="isSuccessful">موفق : 1 ، ناموفق : 0</param>
        /// <returns></returns>
        IQueryable<GridPaymentLogViewModel> GetViewModelForGrid(int isSuccessful = 1);
        #endregion

        #region InsertPaymentLogAfterDelete
        /// <summary>
        /// درج لاگ بعد از حذف
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="totalAmount"></param>
        /// <param name="paymentResponseMessage"></param>
        /// <param name="isSuccessful"></param>
        void InsertPaymentLogAfterDelete(long orderId, decimal totalAmount, string paymentResponseMessage = "", bool isSuccessful = true);
        #endregion

        #region Insert Payment Log
        /// <summary>
        /// درج لاگ جدید پرداخت
        /// </summary>
        /// <param name="bankId">شناسه بانک</param>
        /// <param name="orderId">شناسه سفارش</param>
        /// <param name="paymentUniqueNumberID">شناسه یکتای پرداخت</param>
        /// <param name="totalAmount">مبلغ کل</param>
        /// <param name="paymentResponseCode">کد پاسخ دریافتی از بانک</param>
        /// <param name="paymentResponseMessage">پیام پاسخ دریافتی از بانک</param>
        /// <param name="trackingCode">کد پیگیری</param>
        /// <param name="isSuccessful">پرداخت موفق بوده؟</param>
        void InsertPaymentLog(int bankId, long orderId, long paymentUniqueNumberID, decimal totalAmount, string paymentResponseCode = "", string paymentResponseMessage = "", string trackingCode = "", bool isSuccessful = false);
        #endregion
    }
}
