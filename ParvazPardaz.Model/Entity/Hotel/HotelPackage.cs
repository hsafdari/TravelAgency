using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelPackage : BaseEntity
    {
        #region Properties
        public int OrderId { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual TourPackage TourPackage { get; set; }
        public int TourPackageId { get; set; }
        #endregion

        #region Collection Navigation Properties
        //public virtual ICollection<ParvazPardaz.Model.Entity.Hotel.Hotel> Hotels { get; set; } // باید بعدا برداشته شود
        public virtual ICollection<HotelPackageHotel> HotelPackageHotels { get; set; }

        public virtual ICollection<HotelPackageHotelRoom> HotelPackageHotelRooms { get; set; }
        public virtual ICollection<SelectedHotelPackage> SelectedHotelPackages { get; set; }
        #endregion
    }
}
