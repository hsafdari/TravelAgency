using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Web.Mvc;


namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourTypeService : BaseService<TourType>, ITourTypeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourType> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourTypeService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourType>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourTypeViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourTypeViewModel>(_mappingEngine);
        }
        #endregion


        #region GetAllTourTypesOfSelectListItem
        public IEnumerable<SelectListItem> GetAllTourTypesOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
        }
        #endregion
    }
}
