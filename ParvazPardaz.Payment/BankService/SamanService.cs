using ParvazPardaz.Payment.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitPayment = ParvazPardaz.Payment.SamanBankInitPayment;
using ReferencePayment = ParvazPardaz.Payment.SamanBankReferencePayment;
using ParvazPardaz.Payment.Infrastructure.Extension;
using System.Collections.Specialized;
using ParvazPardaz.Payment.Infrastructure.Helpers;
using System.Web;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using ParvazPardaz.Model.Book;
//using ParvazPardaz.Model.Book;

namespace ParvazPardaz.Payment.BankService
{
    /// <summary>
    /// بانک سامان
    /// <see href="https://www.sb24.com">وبسایت</see>
    /// </summary>
    public class SamanService : ISamanService
    {
        #region PaymentWithToken
        /// <summary>
        /// ارسال اطلاعات به درگاه بانک به روش توکن
        /// </summary>
        /// <param name="totalAmount">کل مبلغ قابل پرداخت</param>
        /// <param name="redirectUrl">آدرس صفحه بازگشت</param>
        /// <param name="paymentUniqueNumberId">شناسه ی یکتای پرداخت</param>
        /// <param name="bankId">شناسه ی بانک انتخاب شده</param>
        /// <param name="message">پیامی که می توانیم به مشتری نمایش دهیم</param>
        public void PaymentWithToken(long totalAmount, string redirectUrl, long paymentUniqueNumberId, int bankId, out string message)
        {
            message = "";
            ApplicationDbContext db = null;
            
            try
            {
                db = new ApplicationDbContext();

                //گرفتن شناسه ی ترمینال
                var selectedBank = db.Set<Bank>().Find(bankId);
                string terminalId = selectedBank.BankTerminalId;

                //ایجاد شی ای از 
                //SepInitPayment
                //برای پرداخت
                var initPayment = new InitPayment.PaymentIFBinding();

                // دریافت توکن از بانک سامان
                string token = initPayment.RequestToken(terminalId, paymentUniqueNumberId.ToString(), long.Parse(totalAmount.ToString()), 0, 0, 0, 0, 0, 0, "", "", 0);

                // بررسی وجود توکن دریافت شده از درگاه بانک
                if (!String.IsNullOrEmpty(token))
                {
                    //اگر به درگاه متصل شده باشیم باید دیتاهای لازم رو در 
                    //شی ای از نیم-ولیو-کالکشن بریزیم ، بعدش فرم خودمون رو بسازیم و پستش کنیم
                    //if (String.IsNullOrEmpty(token.GetDicMessage()))
                    //{
                    // ایجاد یک شی از نیم-ولیو-کالکشن
                    NameValueCollection datacollection = new NameValueCollection();

                    // اضافه کردن توکن به شی ساخت شده از نیم ولیو کالکشن
                    datacollection.Add("Token", token);

                    // اضافه کردن آدرس برگشت از درگاه ، به شی ساخت شده از نیم-ولیو-کالکشن
                    datacollection.Add("RedirectURL", redirectUrl);

                    // ارسال اطلاعات به درگاه
                    HttpContext.Current.Response.Write(BankHttpHelper.PreparePOSTForm("https://sep.shaparak.ir/payment.aspx", datacollection));
                    //}
                    //else
                    //{
                    //    //به روز رسانی لاگ پرداخت که قبلا درج شده ، به خاطر عدم اتصال به بانک
                    //    UpdatePaymentLog(paymentUniqueNumberId, token.GetDicMessage(), token, null, false);

                    //    //مقداردهی پیام
                    //    message = token.GetDicMessage();
                    //}
                }
                else
                {
                    //مقداردهی پیام
                    message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
                }
            }
            catch (Exception ex)
            {
                //مقداردهی پیام
                message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
            }
            finally
            {
                //if (db != null)
                //{
                //    db.Dispose();
                //    db = null;
                //}
            }
        }
        #endregion

        #region verifyTransaction
        /// <summary>
        /// تاییدیه پرداخت به بانک
        /// </summary>
        /// <param name="paymentUniqueNum">شناسه یکتای پرداخت</param>
        /// <param name="merchantId">شناسه پرداخت</param>
        /// <returns>نتیجه عددی</returns>
        public double VerifyTransaction(string paymentUniqueNum, string merchantId)
        {
            // ایجاد یک شی از 
            //PaymentIFBindingSoapClient
            //برای پرداخت  
            var srv = new ReferencePayment.PaymentIFBinding();
            // var srv = new ReferencePayment.PaymentIFBinding();
            var result = srv.verifyTransaction(paymentUniqueNum, merchantId);
            // تایید اطلاعات پرداخت و دریافت نتیجه
            return result;
        }
        #endregion

        #region ReverseTransaction
        /// <summary>
        /// باز پس فرستادن مبلغ به مشتری
        /// </summary>
        /// <param name="paymentUniqueNum">شناسه یکتای پرداخت</param>
        /// <param name="merchantId">شناسه پرداخت</param>
        /// <param name="bankUserName">نام کاربری که بانک داده</param>
        /// <param name="bankPassword">کلمه عبور که بانک داده</param>
        /// <returns>نتیجه عددی</returns>
        public double ReverseTransaction(string paymentUniqueNum, string merchantId, string bankUserName, string bankPassword)
        {
            var srv = new ReferencePayment.PaymentIFBinding();

            // فراخوانی متد ریورس ترنزاکشن برای بازگشت دادن مبلغ به حساب خریدار
            return srv.reverseTransaction(paymentUniqueNum, merchantId, bankUserName, bankPassword);
        }
        #endregion

        #region InsertPaymentLog
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
        public void InsertPaymentLog(int bankId, long orderId, long paymentUniqueNumberID, decimal totalAmount, string paymentResponseCode = "", string paymentResponseMessage = "", string trackingCode = "", bool isSuccessful = false)
        {
            DataContext db = null;
            try
            {
                db = new DataContext();
                //ایجاد لاگ جدید
                var newPaymentLog = new PaymentLog()
                {
                    BankId = bankId,
                    OrderId = orderId,
                    PaymentUniqueNumberID = paymentUniqueNumberID,
                    TotalAmount = totalAmount,
                    PaymentResponseCode = paymentResponseCode,
                    PaymentResponseMessage = paymentResponseMessage,
                    TrackingCode = trackingCode,
                    PaymentDate = DateTime.Now,
                    IsSuccessful = isSuccessful
                };
                db.PaymentLogs.Add(newPaymentLog);
                db.SaveAllChanges();
            }
            catch (Exception ex)
            {
                //عدم درج در دیتابیس سایتمون
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        #endregion

        #region UpdatePaymentLog
        /// <summary>
        /// به روز رسانی لاگ پرداخت
        /// </summary>
        /// <param name="paymentUniqueNumberId">شناسه یکتای پرداخت</param>
        /// <param name="paymentResponseMessage">متن پیام پاسخ</param>
        /// <param name="paymentResponseCode">کد پاسخ</param>
        /// <param name="trackingCode">رسید دیجیتالی خرید موفق یا کد پیگیری</param>
        /// <param name="isSuccessful">پرداخت موفق بوده؟</param>
        public void UpdatePaymentLog(long paymentUniqueNumberId, string paymentResponseMessage, string paymentResponseCode, string trackingCode, bool isSuccessful = false)
        {
            DataContext db = null;
            try
            {
                db = new DataContext();
                //یافتن لاگ پرداخت برای به روز رسانی آن
                var paymentLog = db.PaymentLogs.FirstOrDefault(x => x.PaymentUniqueNumberID == paymentUniqueNumberId);
                //اگر لاگ موجود بود؟
                if (paymentLog != null)
                {
                    paymentLog.PaymentResponseCode = paymentResponseCode;
                    paymentLog.PaymentResponseMessage = paymentResponseMessage;
                    paymentLog.IsSuccessful = isSuccessful;
                    paymentLog.TrackingCode = (trackingCode != null ? trackingCode : paymentLog.TrackingCode);

                    db.Entry(paymentLog).State = EntityState.Modified;
                    db.SaveAllChanges();
                }
                else
                {
                    //لاگی یافت نشد
                }
            }
            catch (Exception ex)
            {
                //عدم اتصال به دیتابیس سایتمون
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        #endregion
    }
}

