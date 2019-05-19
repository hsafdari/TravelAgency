using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourScheduleHotelService : IBaseService<TourScheduleHotel>
    {
        #region CreateTourScheduleHotel
        TourScheduleHotel CreateTourScheduleHotel(TourScheduleHotelViewModel addTourScheduleHotelViewModel);
        #endregion

        #region UpdateTourScheduleHotel
        TourScheduleHotel UpdateTourScheduleHotel(TourScheduleHotelViewModel editTourScheduleHotelViewModel);
        #endregion
    }
}
