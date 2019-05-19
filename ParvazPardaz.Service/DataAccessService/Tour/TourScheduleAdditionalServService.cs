using AutoMapper;
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

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourScheduleAdditionalServService : BaseService<TourScheduleAdditionalService>, ITourScheduleAdditionalServService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourScheduleAdditionalService> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourScheduleAdditionalServService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourScheduleAdditionalService>();
        }
        #endregion  

        #region CreateTourScheduleAdditionalService
        public TourScheduleAdditionalService CreateTourScheduleAdditionalService(TourScheduleAdditionalServiceViewModel addTourScheduleAdditionalServiceViewModel)
        {
            var TourScheduleAdditionalService = _mappingEngine.Map<TourScheduleAdditionalService>(addTourScheduleAdditionalServiceViewModel);
            _dbSet.Add(TourScheduleAdditionalService);
            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id == TourScheduleAdditionalService.Id);
        }
        #endregion  

        #region UpdateTourScheduleAdditionalService
        public TourScheduleAdditionalService UpdateTourScheduleAdditionalService(TourScheduleAdditionalServiceViewModel editTourScheduleAdditionalServiceViewModel)
        {
            var TourScheduleAdditionalService = base.GetById(t => t.Id == editTourScheduleAdditionalServiceViewModel.Id);
            _mappingEngine.Map(editTourScheduleAdditionalServiceViewModel, TourScheduleAdditionalService);

            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id == TourScheduleAdditionalService.Id);
        }
        #endregion  
    }
}
