using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourPackageService : IBaseService<TourPackage>
    {
        //#region GetViewModelForGrid
        IQueryable<GridTourPackageViewModel> GetViewModelForGrid();
        //#endregion
        #region GetUsersSeller
        IEnumerable<SelectListItem> GetUsersSeller();
        #endregion

        #region FindTourPackageByTourId
        SelectList FindTourPackageByTourId(int? tourId);
        Task<int> EditAsync(EditTourPackageViewModel editTourPackageViewModel);
        #endregion
    }
}
