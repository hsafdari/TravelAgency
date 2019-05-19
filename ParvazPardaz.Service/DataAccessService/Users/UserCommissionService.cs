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
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Service.Contract.Users;

namespace ParvazPardaz.Service.DataAccessService.Users
{
    public class UserCommissionService : BaseService<UserCommission>, IUserCommissionService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserCommission> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public UserCommissionService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<UserCommission>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridUserCommissionViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridUserCommissionViewModel>(_mappingEngine);
        }
        #endregion
    }
}

