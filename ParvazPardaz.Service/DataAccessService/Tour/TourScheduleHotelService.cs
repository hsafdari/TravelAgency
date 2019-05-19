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
    public class TourScheduleHotelService : BaseService<TourScheduleHotel>, ITourScheduleHotelService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourScheduleHotel> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourScheduleHotelService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourScheduleHotel>();
        }
        #endregion

        #region CreateTourProgram
        public TourScheduleHotel CreateTourScheduleHotel(TourScheduleHotelViewModel addTourScheduleHotelViewModel)
        {
            var tourScheduleHotel = _mappingEngine.Map<TourScheduleHotel>(addTourScheduleHotelViewModel);
            _dbSet.Add(tourScheduleHotel);
            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id == tourScheduleHotel.Id);
        }
        #endregion

        #region UpdateTourScheduleHotel
        public TourScheduleHotel UpdateTourScheduleHotel(TourScheduleHotelViewModel editTourScheduleHotelViewModel)
        {
            var TourScheduleHotel = base.GetById(t => t.Id == editTourScheduleHotelViewModel.Id);
            _mappingEngine.Map(editTourScheduleHotelViewModel, TourScheduleHotel);

            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id == TourScheduleHotel.Id);
        }
        #endregion
    }
}
