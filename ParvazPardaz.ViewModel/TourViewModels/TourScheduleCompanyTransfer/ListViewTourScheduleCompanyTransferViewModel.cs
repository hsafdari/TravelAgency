using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourScheduleCompanyTransferViewModel : BaseViewModelId
    {
        /// <summary>
        /// تاریخ و زمان شروع حرکت
        /// </summary>
        public string StartDateTime { get; set; }
        /// <summary>
        /// تاریخ و زمان خاتمه حرکت
        /// </summary>
        public string EndDateTime { get; set; }
        /// <summary>
        /// مبدا حرکت
        /// </summary>
        public string FromAirportTitle { get; set; }
        /// <summary>
        /// مقصد حرکت
        /// </summary>
        public string DestinationAirportTitle { get; set; }
        /// <summary>
        /// مدت رمان مسیر
        /// </summary>
        public string DurationTime { get; set; }
        /// <summary>
        /// ظرفیت وسلیه نقلیه
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود تور ؟
        /// </summary>
        public bool NonLimit { get; set; }
        public string CompanyTransferTitle { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور  
        /// </summary>
        public int TourScheduleId { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        ///// <summary>
        ///// کلاس پروازی
        ///// </summary>
        //public string FlightClass { get; set; }

        /// <summary>
        /// عنوان کلاس نوع وسیله نقلیه
        /// </summary>
        public string VehicleTypeClassTitle { get; set; }
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
    }
}
