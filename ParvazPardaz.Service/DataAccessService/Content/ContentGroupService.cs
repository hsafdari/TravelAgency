using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.Service.Contract.Content;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Content
{
    public class ContentGroupService : BaseService<ContentGroup>, IContentGroupService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ContentGroup> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public ContentGroupService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<ContentGroup>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridContentGroupViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridContentGroupViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllCountryOfSelectListItem
        public SelectList GetContentGroupDDL()
        {
            return new SelectList(_dbSet.Where(a => a.IsDeleted == false), "Id", "Title");
        }

        public SelectList GetContentGroupDDL(int selectedGroupId)
        {
            return new SelectList(_dbSet.Where(a => a.IsDeleted == false), "Id", "Title", selectedGroupId);
        }
        #endregion
    }
}
