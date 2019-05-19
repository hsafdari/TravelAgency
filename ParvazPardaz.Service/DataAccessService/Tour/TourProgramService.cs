using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using RefactorThis.GraphDiff;
using ParvazPardaz.Service.Contract.Country;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourProgramService : BaseService<TourProgram>, ITourProgramService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourProgram> _dbSet;
        private readonly IMappingEngine _mappingEngine;

        private readonly IActivityService _activityService;
        private readonly ITourProgramActivityService _tourProgramActivityService;
        #endregion

        #region Ctor
        public TourProgramService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IActivityService activityService, ITourProgramActivityService tourProgramActivityService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourProgram>();
            _activityService = activityService;
            _tourProgramActivityService = tourProgramActivityService;
        }
        #endregion

        #region CreateTourProgram
        public TourProgram CreateTourProgram(TourProgramViewModel addTourProgramViewModel)
        {
            var tourProgram = _mappingEngine.Map<TourProgram>(addTourProgramViewModel);

            if (addTourProgramViewModel.SelectedActivities != null && addTourProgramViewModel.SelectedActivities.Any())
            {
                var activities = _activityService.Filter(a => addTourProgramViewModel.SelectedActivities.Any(act => act == a.Id));
                tourProgram.TourProgramActivities = new List<TourProgramActivity>();
                foreach (var activity in activities)
                {
                    tourProgram.TourProgramActivities.Add(new TourProgramActivity()
                                                            {
                                                                ActivityId = activity.Id,
                                                                TourProgramId = tourProgram.Id
                                                            });
                }
            }
            _dbSet.Add(tourProgram);
            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().Include(x => x.TourProgramActivities).FirstOrDefault(t => t.Id == tourProgram.Id);
        }
        #endregion

        #region UpdateTourProgram
        public TourProgram UpdateTourProgram(TourProgramViewModel editTourProgramViewModel)
        {
            var tourProgram = base.GetById(t => t.Id == editTourProgramViewModel.Id);
            _mappingEngine.Map(editTourProgramViewModel, tourProgram);
            // آپدیت جدول میانی در صورتی که جدوی میانی در مدل برنامه وجود داشته باشد
            foreach (var item in tourProgram.TourProgramActivities.ToList())
            {
                _unitOfWork.MarkAsDeleted<TourProgramActivity>(item);
            }
            if (editTourProgramViewModel.SelectedActivities != null && editTourProgramViewModel.SelectedActivities.Any())
            {
                tourProgram.TourProgramActivities = new List<TourProgramActivity>();
                var activities = _activityService.Filter(a => editTourProgramViewModel.SelectedActivities.Any(act => act == a.Id));
                foreach (var activity in activities)
                {
                    tourProgram.TourProgramActivities.Add(new TourProgramActivity()
                    {
                        ActivityId = activity.Id,
                        TourProgramId = tourProgram.Id
                    });
                }
            }
            _unitOfWork.SaveAllChanges(false);
            return _dbSet.AsNoTracking().Include(x => x.TourProgramActivities).FirstOrDefault(t => t.Id == tourProgram.Id);
        }
        #endregion
    }
}
