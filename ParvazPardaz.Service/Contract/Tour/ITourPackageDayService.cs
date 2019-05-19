using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourPackageDayService : IBaseService<TourPackageDay>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTourPackageDayViewModel> GetViewModelForGrid();
        #endregion
        #region GetAllTourTypesOfSelectListItem
        /// <summary>
        /// همه دیتا را به صورت لیستی از سلکت لیست آیتم بر میگرداند
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllTourPackageDayOfSelectListItem();        
        #endregion

    }
}
