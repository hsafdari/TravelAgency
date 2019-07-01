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
using ParvazPardaz.ViewModel.TourViewModels.Request;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class RequestService : BaseService<Request>, IRequestService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Request> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public RequestService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Request>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridRequestViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridRequestViewModel>(_mappingEngine);
        }
        #endregion
    }
}
