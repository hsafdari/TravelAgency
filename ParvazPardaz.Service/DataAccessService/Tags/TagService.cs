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
using System.Web;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.Contract.Tags;

namespace ParvazPardaz.Service.DataAccessService.Tags
{

    public class TagService : BaseService<ParvazPardaz.Model.Entity.Post.Tag>, ITagService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ParvazPardaz.Model.Entity.Post.Tag> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion
        #region Ctor
        public TagService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<ParvazPardaz.Model.Entity.Post.Tag>();
            _httpContextBase = httpContextBase;
        }
        #endregion
        #region GetForGrid
        public IQueryable<GridTagViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTagViewModel>(_mappingEngine);


        }
        #endregion



    }
}
