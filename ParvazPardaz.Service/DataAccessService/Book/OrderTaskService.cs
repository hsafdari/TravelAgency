﻿using AutoMapper;
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
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class OrderTaskService : BaseService<OrderTask>, IOrderTaskService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<OrderTask> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public OrderTaskService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<OrderTask>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridOrderTaskViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridOrderTaskViewModel>(_mappingEngine);
        }
        #endregion
    }
}