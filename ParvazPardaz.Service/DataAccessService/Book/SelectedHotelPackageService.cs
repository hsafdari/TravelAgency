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

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class SelectedHotelPackageService : BaseService<SelectedHotelPackage>, ISelectedHotelPackageService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<SelectedHotelPackage> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public SelectedHotelPackageService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<SelectedHotelPackage>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridSelectedHotelPackageViewModel>(_mappingEngine);
        }
        #endregion


        public IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype, int? cityid, int? hotelid)
        {
            var query = _dbSet.Where(w => !w.IsDeleted);
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.Order.FlightDateTime)) && (DbFunctions.TruncateTime(w.Order.FlightDateTime) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            if (cityid != null && cityid != 0)
            {
                query = query.Where(x => x.SelectedHotels.Any(y => y.Hotel.CityId == cityid.Value));
            }
            if (hotelid != null && hotelid != 0)
            {
                query = query.Where(x => x.SelectedHotels.Any(y => y.HotelId == hotelid.Value));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridSelectedHotelPackageViewModel>(_mappingEngine);
        }

        public IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, int? cityid, int? hotelid, string calendertype)
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
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && _fromDate != null && _todateDate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.Order.FlightDateTime)) && (DbFunctions.TruncateTime(w.Order.FlightDateTime) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            if (cityid!=null && cityid!=0)
            {                
                query = query.Where(x => x.SelectedHotels.Any(y =>y.Hotel.CityId==cityid.Value));
            }
            if (hotelid!=null  && hotelid!=0)
            {
                query = query.Where(x => x.SelectedHotels.Any(y => y.HotelId == hotelid.Value));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridSelectedHotelPackageViewModel>(_mappingEngine);
        }
    }
}
