using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class FooterService : BaseService<Footer>, IFooterService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Footer> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public FooterService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Footer>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridFooterViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).OrderBy(x => x.FooterType).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridFooterViewModel>(_mappingEngine);
        }
        #endregion
    }
}
