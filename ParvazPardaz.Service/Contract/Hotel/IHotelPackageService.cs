using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelPackageService : IBaseService<HotelPackage>
    {

        HotelPackage CreateHotelPackageWithRoomPrice(HotelPackageViewModel hotelPackage);
        HotelPackage UpdateHotelPackageWithRoomPrice(HotelPackageViewModel hotelPackage);

        #region CopyHotelPackagesByTourPackageId
        TourPackage CopyHotelPackagesByTourPackageId(int fromTourPackageId = 0, int toTourPackageId = 0);
        #endregion

        #region GetHotelRoomInPackageTable
        /// <summary>
        /// دریافت اطلاعات انواع اتاق موجود در پکیج های هتل
        /// برای ویرایش قیمت و ظرفیت
        /// </summary>
        /// <returns></returns>
        IQueryable<EditHotelRoomInPackageViewModel> GetHotelRoomInPackageTable();
        #endregion
        bool InlineUpdate(int id, string property, string value);
    }
}
