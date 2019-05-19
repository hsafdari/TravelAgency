
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using AutoMapper;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Entity.Tour;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Model.Entity.Country;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourScheduleCompanyTransferService : BaseService<TourScheduleCompanyTransfer>, ITourScheduleCompanyTransferService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourScheduleCompanyTransfer> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourScheduleCompanyTransferService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourScheduleCompanyTransfer>();
        }
        #endregion
    }
}
