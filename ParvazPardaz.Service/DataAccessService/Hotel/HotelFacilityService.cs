using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using AutoMapper;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelFacilityService : BaseService<HotelFacility>, IHotelFacilityService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<HotelFacility> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public HotelFacilityService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<HotelFacility>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridHotelFacilityViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridHotelFacilityViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllHotelFacilityOfSelectListItem
        public IEnumerable<SelectListItem> GetAllHotelFacilityOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(r => new SelectListItem() { Text = r.Title, Value = r.Id.ToString() }).AsEnumerable();

        }
        #endregion
    }
}
