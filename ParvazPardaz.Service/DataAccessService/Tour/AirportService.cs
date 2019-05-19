using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class AirportService : BaseService<Airport>, IAirportService
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Airport> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public AirportService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Airport>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridAirportViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridAirportViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllAirPortOfSelectListItem
        public IEnumerable<SelectListItem> GetAllAirPortOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion
        #region GetAllAirPortIataOfSelectListItem
        public IEnumerable<SelectListItem> GetAllAirPortIataOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title+"-"+x.IataCode, Value = x.IataCode }).AsEnumerable();
        }
        #endregion
    }
}
