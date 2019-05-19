using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.ViewModel;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelFacilityService : IBaseService<HotelFacility>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelFacilityViewModel> GetViewModelForGrid();
        #endregion

        #region  GetAllHotelFacilityOfSelectListItem
        IEnumerable<SelectListItem> GetAllHotelFacilityOfSelectListItem();
        #endregion
    }
}
