using AutoMapper;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;
using System.Linq.Expressions;
using ParvazPardaz.Service.Contract.Users;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourLevelService : BaseService<TourLevel>, ITourLevelService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourLevel> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourLevelService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourLevel>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourLevelViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourLevelViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllTourLevelsOfSelectListItem
        public IEnumerable<SelectListItem> GetAllTourLevelsOfSelectListItem()
        {
            return _dbSet.Where(x=>x.IsDeleted==false).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
        }
        #endregion


    }
}
