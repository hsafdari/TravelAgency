using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelRoomService : IBaseService<HotelRoom>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelRoomViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllHotelRoomsOfSelectListItem
        IEnumerable<SelectListItem> GetAllHotelRoomsOfSelectListItem();
      
        #endregion
    }
}
