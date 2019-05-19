using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class SelectedFlightService : BaseService<SelectedFlight>, ISelectedFlightService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<SelectedFlight> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public SelectedFlightService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<SelectedFlight>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype)
        {          
             var query=_dbSet.Where(w => !w.IsDeleted);
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && fromdate!=null && todate!=null)
            {
               query=query.Where(w=>((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value)<=(DbFunctions.TruncateTime(todate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.FlightDateTime)) && (DbFunctions.TruncateTime(w.FlightDateTime) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
           return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridSelectedFlightViewModel>(_mappingEngine);
        }
        #endregion

        public IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype, int? companytranferId, string fromairport, string toairport)
        {
            var query = _dbSet.Where(w => !w.IsDeleted);
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.FlightDateTime)) && (DbFunctions.TruncateTime(w.FlightDateTime) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            //companytranferId
            query = query.Where(w => (w.CompanyTransferId == companytranferId.Value) || (companytranferId == null || companytranferId == 0));
            query = query.Where(w => (w.FromIATACode == fromairport) || string.IsNullOrEmpty(fromairport));
            query = query.Where(w => (w.ToIATACode == toairport) || string.IsNullOrEmpty(toairport));
            return
             query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridSelectedFlightViewModel>(_mappingEngine);
        }

        public IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, int? companytranferId, string fromairport, string toairport, string calendertype)
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
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.FlightDateTime)) && (DbFunctions.TruncateTime(w.FlightDateTime) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            //companytranferId
            query = query.Where(w => (w.CompanyTransferId == companytranferId.Value) || (companytranferId == null ||companytranferId==0)); 
            query= query.Where(w => (w.FromIATACode == fromairport) || string.IsNullOrEmpty(fromairport));
            query = query.Where(w => (w.ToIATACode == toairport) || string.IsNullOrEmpty(toairport));
            return 
             query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridSelectedFlightViewModel>(_mappingEngine);
        }
    }
}
