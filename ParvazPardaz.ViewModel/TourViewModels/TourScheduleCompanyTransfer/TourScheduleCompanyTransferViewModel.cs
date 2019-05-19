using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class TourScheduleCompanyTransferViewModel : BaseViewModelId
    {
        /// <summary>
        /// تاریخ شروع حرکت
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "DepartureDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        /// <summary>
        /// تاریخ خاتمه حرکت
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "ArrivalDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        /// <summary>
        /// زمان حرکت
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "DepartureTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan DepartureTime { get; set; }
        /// <summary>
        /// زمان خاتمه حرکت
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "ArrivalTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan ArrivalTime { get; set; }
        /// <summary>
        /// نام شهر مبدا
        /// </summary>
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "FromCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FromAirportTitle { get; set; }
        /// <summary>
        /// شناسه شهر مبدا
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "FromAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int FromAirportId { get; set; }
        /// <summary>
        /// نام شهر مقصد
        /// </summary>
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "DestinationCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string DestinationAirportTitle { get; set; }
        /// <summary>
        /// شناسه شهر مقصد
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "DestinationAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int DestinationAirportId { get; set; }
        ///// <summary>
        ///// مدت رمان مسیر
        ///// </summary>
        //public DateTime? DurationTime { get; set; }
        /// <summary>
        /// ظرفیت وسلیه نقلیه
        /// </summary>
        [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int? Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود ؟
        /// </summary>
        [Display(Name = "NonLimit", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool NonLimit { get; set; }
        /// <summary>
        /// شناسه نوع وسیله نقلیه جهت انتخاب و واکشی شرکت های حمل ونقل آن
        /// </summary>
        [Display(Name = "VehicleTypeId", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int VehicleTypeId { get; set; }
        /// <summary>
        /// شناسه شرکت حمل ونقل
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "CompanyTransfer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int CompanyTransferId { get; set; }
        /// <summary>
        /// شناسه زمانبندی تور
        /// </summary>
        public int TourScheduleId { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        /// 
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }

        ///// <summary>
        ///// کلاس پروازی
        ///// </summary>
        //[Display(Name = "FlightClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //public string FlightClass { get; set; }

        [Display(Name = "VehicleTypeClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public Nullable<int> VehicleTypeClassId { get; set; }

        /// <summary>
        /// شماره پرواز
        /// </summary>
        /// 
        [Display(Name = "FlightNumber", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FlightNumber { get; set; }
        public int? fromCityId { get; set; }
        public int? destCityId { get; set; }

        /// <summary>
        /// میزان بار مجاز
        /// </summary>
        public string BaggageAmount { get; set; }

        /// <summary>
        /// پرواز رفت یا برگشت
        /// </summary>
        public EnumFlightDirectionType FlightDirection { get; set; }

        /// <summary>
        /// لیستی از بخش های افزوده شده جهت نمایش در صفحه در حالت رفرش صفحه
        /// </summary>
        public List<ListViewTourScheduleCompanyTransferViewModel> ListView { get; set; }

    }
}
