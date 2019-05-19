using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class Currency : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string CurrenySign { get; set; }
        /// <summary>
        /// قیمت بر اساس واحد ريال
        /// </summary>
        public decimal BaseRialPrice { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourSchedule> TourSchedules { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //public virtual ICollection<Passenger> Passengers { get; set; }
        /// <summary>
        /// کدام اتاق های هتل از این واحد پولی برای قیمت ارزی خود استفاده کرده اند؟
        /// </summary>
        public virtual ICollection<HotelPackageHotelRoom> HotelPackageHotelRooms { get; set; }
        
        #endregion
    }
}
