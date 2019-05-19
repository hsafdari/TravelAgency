using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.LinkRedirection;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Link;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.LinkRedirection
{
    public class LinkRedirectionService : BaseService<EntityNS.LinkRedirection>, ILinkRedirectionService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.LinkRedirection> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public LinkRedirectionService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.LinkRedirection>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridLinkRedirectionViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridLinkRedirectionViewModel>(_mappingEngine);
        }
        #endregion
    }
}
