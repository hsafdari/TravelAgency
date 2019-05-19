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
    public interface ITourCategoryService : IBaseService<TourCategory>
    {
        #region  GetForGridView

        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTourCategoryViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllTourCategoriesOfSelectListItem
        /// <summary>
        /// همه دیتا را به صورت لیستی از سلکت لیست آیتم بر میگرداند
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllTourCategoriesOfSelectListItem();
        #endregion
    }
}
