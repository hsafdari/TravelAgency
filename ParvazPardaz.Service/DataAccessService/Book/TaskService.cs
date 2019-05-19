using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Book;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.ViewModel;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class TaskService : BaseService<EntityNS.Task>, ITaskService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.Task> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TaskService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.Task>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTaskViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTaskViewModel>(_mappingEngine);
        }
        #endregion
    }
}
