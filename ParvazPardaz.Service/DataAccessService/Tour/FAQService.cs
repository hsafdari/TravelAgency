using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using ParvazPardaz.Service.Contract.Tour;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class FAQService : BaseService<FAQ>,IFAQService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<FAQ> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion
        
        #region Ctor
        public FAQService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<FAQ>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridFAQViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridFAQViewModel>(_mappingEngine);
        }
        #endregion
    }
}
