using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using AutoMapper;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;


namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class MenuGroupService : BaseService<MenuGroup>, IMenuGroupService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<MenuGroup> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public MenuGroupService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<MenuGroup>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridMenuGroupViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridMenuGroupViewModel>(_mappingEngine);
        }
        #endregion
    }
}
