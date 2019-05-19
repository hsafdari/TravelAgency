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
    public interface ITourProgramDetailService : IBaseService<TourProgramDetail>
    {
        #region CreateTourProgramDetail
        TourProgramDetail CreateTourProgramDetail(TourProgramDetailViewModel viewModel);
        #endregion

        #region Remove
        bool Remove(int id);
        #endregion
    }
}
