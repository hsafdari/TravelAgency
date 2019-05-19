using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// این جدول نشان میدهد که تور در زمان مشخصی با چه شرکت های مسافربری ارتباط دارد
    /// </summary>
    public class TourScheduleCompanyTransfer : BaseEntity
    {
        #region Properties
        /// <summary>
        /// تاریخ و زمان شروع حرکت
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// تاریخ و زمان خاتمه حرکت
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// مبدا حرکت
        /// </summary>
        public Nullable<int> FromAirportId { get; set; }
        /// <summary>
        /// مقصد حرکت
        /// </summary>
        public Nullable<int> DestinationAirportId { get; set; }
        /// <summary>
        /// مدت رمان مسیر
        /// </summary>
        public DateTime? DurationTime { get; set; }
        /// <summary>
        /// ظرفیت وسلیه نقلیه
        /// </summary>
        public int? Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        ///// <summary>
        ///// کلاس پروازی
        ///// </summary>
        //public string FlightClass { get; set; }
        /// <summary>
        /// شماره پرواز
        /// </summary>
        public string FlightNumber { get; set; }
        /// <summary>
        /// میزان بار مجاز
        /// </summary>
        public string BaggageAmount { get; set; }
        /// <summary>
        /// پرواز رفت یا برگشت
        /// </summary>
        public EnumFlightDirectionType FlightDirection { get; set; }
        #endregion

        #region Reference Navigation Properties
        public Nullable<int> VehicleTypeClassId { get; set; }
        public virtual VehicleTypeClass VehicleTypeClass { get; set; }

        public int CompanyTransferId { get; set; }
        public virtual CompanyTransfer CompanyTransfer { get; set; }
        
        public int TourScheduleId { get; set; }
        public virtual TourSchedule TourSchedule { get; set; }
        #endregion

       
    }
}
