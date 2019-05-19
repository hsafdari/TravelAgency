using System;
using System.Collections.Generic;
using System.Data.Entity;
using ParvazPardaz.Model.Entity.Common;
using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.DataAccessService.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class VehicleTypeClassService : BaseService<VehicleTypeClass>, IVehicleTypeClassService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<VehicleTypeClass> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public VehicleTypeClassService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<VehicleTypeClass>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridVehicleTypeClassViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridVehicleTypeClassViewModel>(_mappingEngine);
        }
        #endregion
    }
}

