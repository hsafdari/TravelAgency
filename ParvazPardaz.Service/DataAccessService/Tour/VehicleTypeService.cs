using AutoMapper;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class VehicleTypeService : BaseService<VehicleType>, IVehicleTypeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<VehicleType> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public VehicleTypeService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<VehicleType>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridVehicleTypeViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridVehicleTypeViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllAVehicleTypesOfSelectListItem
        public IEnumerable<SelectListItem> GetAllVehicleTypesOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString()  }).AsEnumerable();

        }
        #endregion
    }
}
