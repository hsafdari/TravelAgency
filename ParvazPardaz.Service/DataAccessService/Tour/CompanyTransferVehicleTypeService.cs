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

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class CompanyTransferVehicleTypeService : BaseService<CompanyTransferVehicleType>,ICompanyTransferVehicleTypeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<CompanyTransferVehicleType> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public CompanyTransferVehicleTypeService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<CompanyTransferVehicleType>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridCompanyTransferVehicleTypeViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridCompanyTransferVehicleTypeViewModel>(_mappingEngine);
        }
        #endregion
    }
}
