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
    public class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Currency> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public CurrencyService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Currency>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridCurrencyViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridCurrencyViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllCurrenciesOfSelectListItem
        public IEnumerable<SelectListItem> GetAllCurrenciesOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(a => new SelectListItem() { Text = a.Title, Value = a.Id.ToString() }).AsEnumerable();

        }

        public IEnumerable<SelectListItem> GetAllCurrenciesOfSelectListItem(int selectedId)
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(a => new SelectListItem() { Text = a.Title, Value = a.Id.ToString(), Selected = (a.Id == selectedId) }).AsEnumerable();
        }
        #endregion
    }
}
