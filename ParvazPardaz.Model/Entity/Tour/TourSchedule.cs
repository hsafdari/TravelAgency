using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// زمانبندی تور - هر تور در چندین زمانبندی با قیمت های متفاوت ثبت می شود
    /// </summary>
    public class TourSchedule : BaseEntity
    {
        #region Properties
        /// <summary>
        /// شروع بازه زمانی تور
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// پایان بازه زمانی تور 
        /// </summary>
        public DateTime ToDate { get; set; }
        /// <summary>
        /// قمیت تور
        /// </summary>
        public Nullable<decimal> Price { get; set; }
        /// <summary>
        /// ظرفیت تور
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود تور ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// تاریخ انقضای  تور که مشخص میکند تور تا چه تاریخی باز است
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        #endregion

        #region Reference Navigation Property
        //public virtual Tour Tour { get; set; }
        //public int TourId { get; set; }
        public virtual TourPackage TourPackage { get; set; }
        public int TourPackageId { get; set; }
        public virtual Currency Currency { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        #endregion

        #region Collection Navigation Properties
        /// <summary>
        /// کالکشن شرکت مسافربری تور
        /// هر تور در زمان مشخصی شاید چندین پرواز از مبدا های مختلفی داشته باشد
        /// </summary>
        public virtual ICollection<TourScheduleCompanyTransfer> TourScheduleCompanyTransfers { get; set; }
        /// <summary>
        /// کالکشن زمانبندی هتل های تور
        /// هر تور در زمان مشخصی میتواند چندین هتل به ازای برنامه سفرش داشته باشد
        /// به ازای اقامت در هر  روز برنامه سفر
        /// </summary>
        public virtual ICollection<TourScheduleHotel> TourScheduleHotels { get; set; }
        /// <summary>
        /// کالکشن اتاقهای تورها
        ///  هر تور در زمان مشخصی میتواند چندین اتاق با قیمت های متفاوت داشته باشد
        /// </summary>
        //public virtual ICollection<TourScheduleHotelRoom> TourScheduleHotelRooms { get; set; }
        /// <summary>
        /// کالکشن خدمات اضافه تور
        ///  هر تور در زمان مشخصی میتواند چندین خدمات با قیمت های متفاوت داشته باشد
        /// </summary>
        public virtual ICollection<TourScheduleAdditionalService> TourScheduleAdditionalServices { get; set; }
        /// <summary>
        /// کالکشن رزور های تور
        /// </summary>
        //public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<SelectedFlight> SelectedFlights { get; set; }
        #endregion

    }
}
