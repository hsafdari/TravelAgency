using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class SelectedHotelPackage : BaseEntity
    {
        #region Properties
        public int SelectedRoomCount { get; set; }
        /// <summary>
        /// تعداد کل بزرگسالان در این پکیج
        /// </summary>
        public int AdultCount { get; set; }
        /// <summary>
        /// تعداد کل کودکان در این پکیج
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// تعداد کل نوزادان در این پکیج
        /// </summary>
        public int InfantCount { get; set; }

        //public decimal TotalServicePrice { get; set; }
        public decimal TotalAdultPrice { get; set; }
        public decimal TotalChildPrice { get; set; }
        public decimal TotalInfantPrice { get; set; }
        #endregion

        #region Reference navigation property
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int HotelPackageId { get; set; }
        public virtual HotelPackage HotelPackage { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<SelectedRoom> SelectedRooms { get; set; }
        public virtual ICollection<SelectedHotel> SelectedHotels { get; set; }
        #endregion
    }
}
