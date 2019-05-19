using System;
using System.Collections.Generic;
using System.Data.Entity;
using ParvazPardaz.Model.Entity.Common;
using AutoMapper;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.DataAccessService.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Book;
using ParvazPardaz.TravelAgency.UI.Services.Interface.Book;
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.TravelAgency.UI.Services.Service.Book
{
    public class PaymentLogService : BaseService<PaymentLog>, IPaymentLogService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<PaymentLog> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public PaymentLogService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _dbSet = _unitOfWork.Set<PaymentLog>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridPaymentLogViewModel> GetViewModelForGrid(int isSuccessful = 1)
        {
            bool isSuccess = (isSuccessful == 1);
            return _dbSet.Where(w => !w.IsDeleted && w.IsSuccessful == isSuccess).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridPaymentLogViewModel>(_mappingEngine);
        }
        #endregion

        #region لاگ حذف سفارش
        public void InsertPaymentLogAfterDelete(long orderId, decimal totalAmount, string paymentResponseMessage = "", bool isSuccessful = true)
        {
            var order = _unitOfWork.Set<Order>().FirstOrDefault(x => x.Id == orderId);
            //ایجاد لاگ جدید
            var newPaymentLog = new PaymentLog()
            {
                //BankId = ,
                OrderId = orderId,
                PaymentUniqueNumberID = order.PaymentUniqueNumbers.LastOrDefault().Id,
                TotalAmount = totalAmount,
                PaymentResponseCode = "",
                PaymentResponseMessage = paymentResponseMessage,
                TrackingCode = "0",
                PaymentDate = DateTime.Now,
                IsSuccessful = isSuccessful,
            };
            _dbSet.Add(newPaymentLog);
            _unitOfWork.SaveAllChanges();
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
            try
            {
                //ایجاد لاگ جدید
                var newPaymentLog = new PaymentLog()
                {
                    OrderId = orderId,
                    PaymentUniqueNumberID = paymentUniqueNumberID,
                    TotalAmount = totalAmount,
                    PaymentResponseCode = paymentResponseCode,
                    PaymentResponseMessage = paymentResponseMessage,
                    TrackingCode = trackingCode,
                    PaymentDate = DateTime.Now,
                    IsSuccessful = isSuccessful
                };

                if (bankId > 0)
                {
                    newPaymentLog.BankId = bankId;
                }
                _unitOfWork.Set<PaymentLog>().Add(newPaymentLog);
                _unitOfWork.SaveAllChanges();
            }
            catch (Exception ex)
            {
                //عدم درج در دیتابیس سایتمون
            }
        }
        #endregion
    }
}

