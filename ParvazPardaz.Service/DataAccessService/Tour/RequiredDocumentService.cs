using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Tour;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.Contract.Tour;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class RequiredDocumentService : BaseService<EntityNS.RequiredDocument>, IRequiredDocumentService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.RequiredDocument> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public RequiredDocumentService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.RequiredDocument>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridRequiredDocumentViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridRequiredDocumentViewModel>(_mappingEngine);
        }
        #endregion
    }
}
