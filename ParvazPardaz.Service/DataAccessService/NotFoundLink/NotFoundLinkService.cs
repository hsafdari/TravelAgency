using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.NotFoundLink;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel.NotFoundLink;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Link;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.NotFoundLink
{
    public class NotFoundLinkService : BaseService<EntityNS.NotFoundLink>, INotFoundLinkService
    {
         #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.NotFoundLink> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public NotFoundLinkService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.NotFoundLink>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridNotFoundLinkViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridNotFoundLinkViewModel>(_mappingEngine);
        }
        #endregion
    }
}
