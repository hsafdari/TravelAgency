using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ClientHotelRoomInfoViewModel
    {
        #region Constructor
        public ClientHotelRoomInfoViewModel()
        {
            hotelRoom = new HotelRoom();
            hotelRoomInPackage = new HotelPackageHotelRoom();
        }
        #endregion

        #region Properties
        /// <summary>
        /// اطلاعات اتاق
        /// </summary>
        public HotelRoom hotelRoom { get; set; }

        /// <summary>
        /// اطلاعات اتاق در پکیج هتل
        /// </summary>
        public HotelPackageHotelRoom hotelRoomInPackage { get; set; }
        #endregion
    }
}
