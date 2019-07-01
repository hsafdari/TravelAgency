using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using ParvazPardaz.ViewModel.TourViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface IRequestService : IBaseService<Request>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridRequestViewModel> GetViewModelForGrid();
    }
}
