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
    public interface ITourTypeService : IBaseService<Model.Entity.Tour.TourType>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTourTypeViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllTourTypesOfSelectListItem
        /// <summary>
        /// همه دیتا را به صورت لیستی از سلکت لیست آیتم بر میگرداند
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllTourTypesOfSelectListItem();
        #endregion
    }
}
