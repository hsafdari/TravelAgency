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
    /// <summary>
    /// درخواست تور کاربر
    /// </summary>
    public class Request : BaseEntity
    {
        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        public int InfantCount { get; set; }
        /// <summary>
        /// تعداد کودک
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        public int AdultCount { get; set; }
        //public RoomType RoomType { get; set; }
        /// <summary>
        /// نوع اتاق
        /// </summary>
        public string RoomType { get; set; }
        //public virtual TourPackage TourPackage { get; set; }
        public int TourPackageId { get; set; }
        public string TourPackageTitle { get; set; }
        //public virtual HotelPackage HotelPackage { get; set; }
        public int HotelPackageId { get; set; }
        public string HotelPackageTitle { get; set; }

        /// <summary>
        /// پرواز رفت
        /// </summary>
        public int DepartureFlightId { get; set; }

        /// <summary>
        /// پرواز برگشت
        /// </summary>
        public int ArrivalFlightId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
