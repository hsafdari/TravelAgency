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
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourPackageDayService : BaseService<TourPackageDay>, ITourPackageDayService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourPackageDay> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourPackageDayService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourPackageDay>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourPackageDayViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourPackageDayViewModel>(_mappingEngine);
        }
        #endregion


        public IEnumerable<System.Web.Mvc.SelectListItem> GetAllTourPackageDayOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
        }
    }
}

