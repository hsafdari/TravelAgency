using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
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

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class AllowedBannedService : BaseService<AllowedBanned>, IAllowedBannedService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<AllowedBanned> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public AllowedBannedService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<AllowedBanned>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridAllowedBannedViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridAllowedBannedViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllAllowBansOfSelectListItem
        public IEnumerable<SelectListItem> GetAllAllowBansOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Where(x=>x.IsActive==true).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
        }
        #endregion
    }
}
