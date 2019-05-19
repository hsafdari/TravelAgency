using ParvazPardaz.Model.Book;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface IOrderService : IBaseService<Order>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// سفارش های من
        /// </summary>
        /// <returns></returns>
        IQueryable<GridOrderViewModel> GetViewModelForGridByUserId(int creatorUserId);

        /// <summary>
        /// جهت لود دیتا برای گرید ویو بصورت شمسی
        /// </summary>
        /// <returns></returns>
        IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType, DateTime? fromdate, DateTime? todate, string reporttype = "");

        /// <summary>
        /// لود دیتا بصورت میلادی و شمسی
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="calendertype"></param>
        /// <param name="reporttype"></param>
        /// <returns></returns>
        IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType, string fromdate = "", string todate = "", string calendertype = "persian", string reporttype = "");

        IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType);
        IQueryable<GridOrderViewModel> GetViewModelForGrid();
        #endregion

        #region 1. PreReserve : After search
        /// <summary>
        /// واکشی اطلاعات تور بر اساس پکیج هتل انتخاب شده و موارد جستجو شده
        /// </summary>
        /// <param name="viewmodel"></param>
        TourReserveViewModel PreReserve(TourReserveViewModel viewmodel);
        #endregion

        #region 2. Reserve : Before bank payment
        /// <summary>
        /// رزرو قبل رفتن به درگاه بانکی
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        Order Reserve(TourReserveViewModel viewmodel, out bool isAllowedCreditPay);
        #endregion

        #region 3. Voucher Info
        /// <summary>
        /// واکشی اطلاعات واچر
        /// </summary>
        /// <param name="trackingCode">کد پیگیری</param>
        /// <returns></returns>
        VoucherViewModel VoucherInfo(string trackingCode);

        /// <summary>
        /// واکشی اطلاعات واچر سمت مشتری
        /// </summary>
        /// <param name="trackingCode">کد پیگیری</param>
        /// <returns></returns>
        VoucherViewModel VoucherInfo(string trackingCode, int loggedInUserId);
        #endregion

        #region CreditPayment
        /// <summary>
        /// پرداخت اعتباری
        /// </summary>
        /// <param name="orderId">شناسه سفارش</param>
        /// <returns></returns>
        bool CreditPayment(long orderId);
        #endregion

        #region GetSuccessfulReserve
        //IQueryable<ReserveViewModel> GetSuccessfulReserve();
        #endregion

        #region GeneratePaymentUniqueNumber
        /// <summary>
        /// ساخت شناسه یکتای پرداخت
        /// </summary>
        /// <param name="order">سفارش ثبت شده ی موقت</param>
        /// <returns>شناسه سفارش و شناسه یکتای پرداخت</returns>
        PaymentUniqueNumber GeneratePaymentUniqueNumber(Order order);
        #endregion
    }
}
