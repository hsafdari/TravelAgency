using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    /// <summary>
    /// نوع اتاق هتل که میتواند شامل نک تخته یا دو تخته و بچه و کودک و ... باشد
    /// </summary>
    public class HotelRoom : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام نوع اتاق
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نوع اتاق های اصلی را مشخص میکند که به صورت پیش فرض نمایش داده میشود
        /// </summary>
        public bool  IsPrimary { get; set; }
        /// <summary>
        /// ترتیب نمایش سمت کاربر
        /// </summary>
        public Nullable<int> Priority { get; set; }
        /// <summary>
        /// بزرگسال؟
        /// </summary>
        public bool HasAdult { get; set; }
        /// <summary>
        /// کودک؟
        /// </summary>
        public bool HasChild { get; set; }
        /// <summary>
        /// نوزاد؟
        /// </summary>
        public bool HasInfant { get; set; }

        /// <summary>
        /// حداکثر ظرفیت بزرگسال
        /// </summary>
        public int AdultMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت بزرگسال
        /// </summary>
        public int AdultMinCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت کودک
        /// </summary>
        public int ChildMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت کودک
        /// </summary>
        public int ChildMinCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت نوزاد
        /// </summary>
        public int InfantMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت نوزاد
        /// </summary>
        public int InfantMinCapacity { get; set; }
        #endregion

        #region Collection Navigation Properties
        //public virtual ICollection<TourScheduleHotelRoom> TourScheduleHotelRooms { get; set; }
        public virtual ICollection<HotelPackageHotelRoom> HotelPackageHotelRooms { get; set; }
        public virtual ICollection<SelectedRoom> SelectedRooms { get; set; }
        public virtual ICollection<RoomTypeHotelRoom> RoomTypeHotelRooms { get; set; }
        #endregion
    }
}
