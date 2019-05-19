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
    public interface ITourScheduleAdditionalServService : IBaseService<TourScheduleAdditionalService>
    {
        #region CreateTourScheduleAdditionalService
        TourScheduleAdditionalService CreateTourScheduleAdditionalService(TourScheduleAdditionalServiceViewModel addTourScheduleAdditionalServiceViewModel);
        #endregion

        #region UpdateTourScheduleAdditionalService
        TourScheduleAdditionalService UpdateTourScheduleAdditionalService(TourScheduleAdditionalServiceViewModel editTourScheduleAdditionalServiceViewModel);
        #endregion
    }
}
