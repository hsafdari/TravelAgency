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
    public class UserGroupService : BaseService<UserGroup>, IUserGroupService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserGroup> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public UserGroupService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<UserGroup>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridUserGroupViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridUserGroupViewModel>(_mappingEngine);
        }
        #endregion
    }
}

