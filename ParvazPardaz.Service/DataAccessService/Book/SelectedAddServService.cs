using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class SelectedAddServService : BaseService<SelectedAddServ>, ISelectedAddServService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<SelectedAddServ> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public SelectedAddServService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<SelectedAddServ>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridSelectedAddServViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridSelectedAddServViewModel>(_mappingEngine);
        }
        #endregion
    }
}
