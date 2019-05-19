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
    public interface IAirportService : IBaseService<Airport>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridAirportViewModel> GetViewModelForGrid(); 
        #endregion

        #region GetAllAirPortOfSelectListItem
        IEnumerable<SelectListItem> GetAllAirPortOfSelectListItem(); 
        #endregion

        IEnumerable<SelectListItem> GetAllAirPortIataOfSelectListItem();
    }
}
