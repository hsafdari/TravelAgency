using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// اطلاعات هر پرواز
    /// همان TourScheduleCompanyTransfer
    /// </summary>
    public class FlightViewModel : BaseViewModelId
    {
        #region Constructor
        public FlightViewModel()
        {

        }
        #endregion

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
        /// یاتاکد شرکت هواپیمایی مبدا
        /// </summary>
        public string FromAirportIataCode { get; set; }
        /// <summary>
        /// عنوان از فرودگاه مبدا
        /// </summary>
        public string FromAirportTitle { get; set; }
        /// <summary>
        /// عنوان فرودگاه مقصد
        /// </summary>
        public string DestinationAirportTitle { get; set; }
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
        /// <summary>
        /// کلاس پروازی
        /// </summary>
        public string FlightClass { get; set; }
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
        /// <summary>
        /// برای زمان سرچ هنگامی که پرواز های برگشت نمایش داده می شود پرواز های رفت آن دیتاش نگهداری می شود
        /// </summary>
        public int DepartureFlightId { get; set; }
        public string DepartureFlight { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual CompanyTransfer CompanyTransfer { get; set; }
        public int CompanyTransferId { get; set; }
        public virtual TourSchedule TourSchedule { get; set; }
        public int TourScheduleId { get; set; }
        public string DestinationAirportIataCode { get; set; }
        #endregion
    }
}
