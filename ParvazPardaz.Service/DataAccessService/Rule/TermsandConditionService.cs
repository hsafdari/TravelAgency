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
using ParvazPardaz.Service.Contract.Rule;
using ParvazPardaz.Model.Entity.Rule;

namespace ParvazPardaz.Service.DataAccessService.Rule
{
    public class TermsandConditionService : BaseService<TermsandCondition>, ITermsandConditionService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TermsandCondition> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TermsandConditionService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TermsandCondition>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTermsandConditionViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTermsandConditionViewModel>(_mappingEngine);
        }
        #endregion
    }
}

