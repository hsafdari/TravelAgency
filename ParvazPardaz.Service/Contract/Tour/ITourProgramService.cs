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
    public interface ITourProgramService : IBaseService<TourProgram>
    {
        #region CreateTourProgram
        TourProgram CreateTourProgram(TourProgramViewModel addTourProgramViewModel); 
        #endregion

        #region UpdateTourProgram
        TourProgram UpdateTourProgram(TourProgramViewModel editTourProgramViewModel); 
        #endregion
    }
}
