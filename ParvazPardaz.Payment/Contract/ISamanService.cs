using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Payment.Contract
{
    /// <summary>
    /// بانک سامان
    /// <see href="https://www.sb24.com">وبسایت</see>
    /// </summary>
    public interface ISamanService
    {
        #region Payment With Token
        /// <summary>
        /// ارسال اطلاعات به درگاه بانک به روش توکن
        /// </summary>
        /// <param name="totalAmount">کل مبلغ قابل پرداخت</param>
        /// <param name="redirectUrl">آدرس صفحه بازگشت</param>
        /// <param name="paymentUniqueNumberId">شناسه ی یکتای پرداخت</param>
        /// <param name="bankId">شناسه ی بانک انتخاب شده</param>
        /// <param name="message">پیامی که می توانیم به مشتری نمایش دهیم</param>
        void PaymentWithToken(long totalAmount, string redirectUrl, long paymentUniqueNumberId, int bankId, out string message);
        #endregion

        #region Verify Transaction
        /// <summary>
        /// تاییدیه پرداخت به بانک
        /// </summary>
        /// <param name="paymentUniqueNum">شناسه یکتای پرداخت</param>
        /// <param name="merchantId">شناسه پرداخت</param>
        /// <returns>نتیجه عددی</returns>
        double VerifyTransaction(string paymentUniqueNum, string merchantId);
        #endregion

        #region Reverse Transaction
        /// <summary>
        /// باز پس فرستادن مبلغ به مشتری
        /// </summary>
        /// <param name="paymentUniqueNum">شناسه یکتای پرداخت</param>
        /// <param name="merchantId">شناسه پرداخت</param>
        /// <param name="bankUserName">نام کاربری که بانک داده</param>
        /// <param name="bankPassword">کلمه عبور که بانک داده</param>
        /// <returns>نتیجه عددی</returns>
        double ReverseTransaction(string paymentUniqueNum, string merchantId, string bankUserName, string bankPassword);
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

        #region Update Payment Log
        /// <summary>
        /// به روز رسانی لاگ پرداخت
        /// </summary>
        /// <param name="paymentUniqueNumberId">شناسه یکتای پرداخت</param>
        /// <param name="paymentResponseMessage">متن پیام پاسخ</param>
        /// <param name="paymentResponseCode">کد پاسخ</param>
        /// <param name="trackingCode">رسید دیجیتالی خرید موفق یا کد پیگیری</param>
        /// <param name="isSuccessful">پرداخت موفق بوده؟</param>
        void UpdatePaymentLog(long paymentUniqueNumberId, string paymentResponseMessage, string paymentResponseCode, string trackingCode, bool isSuccessful = false);
        #endregion
    }
}
