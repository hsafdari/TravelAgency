using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Common.Extension;
using System.Web.Mvc;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class PassengerService : BaseService<Passenger>, IPassengerService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Passenger> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public PassengerService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Passenger>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridPassengerViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridPassengerViewModel>(_mappingEngine);
        }
        #endregion

        #region GetViewModelForGrid
        public IQueryable<GridPassengerViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype)
        {
            var query = _dbSet.Where(w => !w.IsDeleted);
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.SelectedRoom.SelectedHotelPackage.Order.FlightDateTime)) && (DbFunctions.TruncateTime(w.SelectedRoom.SelectedHotelPackage.Order.FlightDateTime) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridPassengerViewModel>(_mappingEngine);
        }

        public IQueryable<GridPassengerViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, string calendertype)
        {
            DateTime? _fromDate = DateTime.Now;
            DateTime? _todateDate = DateTime.Now;
            if (fromdate != "" && todate != "")
            {
                if (calendertype == "persian")
                {
                    string Date = fromdate.ToPersianNumber();
                    _fromDate = System.Convert.ToDateTime(Date);
                    Date = todate.ToPersianNumber();
                    _todateDate = System.Convert.ToDateTime(Date);
                }
                else if (calendertype == "gregorian")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    _fromDate = System.Convert.ToDateTime(fromdate);
                    _todateDate = System.Convert.ToDateTime(todate);
                }
            }

            var query = _dbSet.Where(w => !w.IsDeleted);
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && _fromDate != null && _todateDate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.SelectedRoom.SelectedHotelPackage.Order.FlightDateTime)) && (DbFunctions.TruncateTime(w.SelectedRoom.SelectedHotelPackage.Order.FlightDateTime) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridPassengerViewModel>(_mappingEngine);
        }
        #endregion

        #region Age range DDL for logged in user
        /// <summary>
        /// تمامی مسافران مرتبط با رزروهای کاربر لاگین شده
        /// </summary>
        /// <returns></returns>
        public SelectList GetPreviousPassengerDDL(int loggedInUserId, AgeRange ageRange)
        {
            //سفارشات این کاربر
            var orderList = _unitOfWork.Set<Order>().Where(x => !x.IsDeleted && x.CreatorUserId.Value == loggedInUserId).ToList();
            //مسافرین مرتبط
            var adultPassengerList = orderList.SelectMany(x => x.SelectedHotelPackages.SelectMany(y => y.SelectedRooms.SelectMany(z => z.Passengers.Where(p => !p.IsDeleted && p.AgeRange == ageRange)))).Select(s => new { Id = s.Id, FullName = s.FirstName + " " + s.LastName }).ToList();
            return new SelectList(adultPassengerList, "Id", "FullName");
        }
        #endregion
    }
}
