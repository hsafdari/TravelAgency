using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ReportViewModel
    {

        #region Book Tour and TourSchedule
        /// <summary>
        /// وضعیت سفارش خرید
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public Status? Status { get; set; }
        /// <summary>
        /// نام تور
        /// </summary>
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        /// <summary>
        /// از تاریخ زمان تور
        /// </summary>
        [Display(Name = "FromDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.DateTime)]
        public DateTime? MinDate { get; set; }
        /// <summary>
        /// تا تاریخ زمان تور
        /// </summary>
        [Display(Name = "ToDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DataType(DataType.DateTime)]
        public DateTime? MaxDate { get; set; }
        /// <summary>
        /// کد تور
        /// </summary>
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourCode { get; set; }
        /// <summary>
        /// نام شهر مبدا 
        /// </summary>
        [Display(Name = "FromCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<string> SourceCityTitles { get; set; }
        /// <summary>
        /// آیدی شهر مبدا
        /// </summary>
        [Display(Name = "FromCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SourceCities { get; set; }
        /// <summary>
        /// نام شهر مقصد
        /// </summary>
        [Display(Name = "DestinationCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<string> DestinationCityTitles { get; set; }
        /// <summary>
        /// آیدی شهر مقصد
        /// </summary>
        [Display(Name = "DestinationCity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> DestinationCities { get; set; }
        /// <summary>
        /// حداقل قیمت تور
        /// </summary>
        [Display(Name = "FromPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [RegularExpression("^(?!0?(,0?0)?$)([0-9]{0,3}(,[0-9]{1,2})?)?$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// حداکثر قیمت تور
        /// </summary>
        [Display(Name = "ToPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [RegularExpression("^(?!0?(,0?0)?$)([0-9]{0,3}(,[0-9]{1,2})?)?$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal? MaxPrice { get; set; }
        #endregion

        #region Book AdditionalService
        /// <summary>
        /// نام خدمات اضافه
        /// </summary>
        [Display(Name = "AdditionalService", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> AdditionalServices { get; set; }
        /// <summary>
        /// حداقل قیمت خدمات اضافه
        /// </summary>
        [Display(Name = "FromAdditionalServicePrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal? MinAdditionalServicePrice { get; set; }
        /// <summary>
        /// حداکثر قیمت خدمات اضافه
        /// </summary>
        [Display(Name = "ToAdditionalServicePrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal? MaxAdditionalServicePrice { get; set; }
        #endregion


        #region Book Hotel
        public List<string> HotelTitles { get; set; }
        [Display(Name = "Hotel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> Hotels { get; set; }
        #endregion


        #region Book HotelRoom
        /// <summary>
        /// نام اتاق هتل
        /// </summary>
        [Display(Name = "HotelRoom", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<string> HotelRoomTitles { get; set; }
        /// <summary>
        /// نام اتاق هتل
        /// </summary>
        [Display(Name = "HotelRoom", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> HotelRooms { get; set; }
        /// <summary>
        /// حداقل قیمت اتاق هتل
        /// </summary>
        [Display(Name = "FromRoomPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[DisplayFormat(DataFormatString = "{0:N2}")]
        //[RegularExpression("^(?!0?(,0?0)?$)([0-9]{0,3}(,[0-9]{1,2})?)?$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal? MinHotelRoomPrice { get; set; }
        /// <summary>
        /// حداکثر قیمت اتاق هتل
        /// </summary>
        [Display(Name = "ToRoomPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [RegularExpression("^(?!0?(,0?0)?$)([0-9]{0,3}(,[0-9]{1,2})?)?$", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal? MaxHotelRoomPrice { get; set; }
        /// <summary>
        /// حداقل تعداد اتاق 
        /// </summary>
        [Display(Name = "FromRoomQuantity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int? MinHotelRoomQuantity { get; set; }
        /// <summary>
        /// حداکثر تعداد اتاق
        /// </summary> 
        [Display(Name = "ToRoomQuantity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int? MaxHotelRoomQuantity { get; set; }
        #endregion

        #region Book Passenger
        /// <summary>
        /// نام مسافر 
        /// </summary>
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PassengerFirstName { get; set; }
        /// <summary>
        /// نام خانوادگی مسافر
        /// </summary>
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(25, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PassengerLastName { get; set; }
        /// <summary>
        /// جنسیت مسافر
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public Gender? PassengerGender { get; set; }
        /// <summary>
        /// حداقل سن
        /// </summary>
        [Display(Name = "FromAge", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int? PassengerMinAge { get; set; }
        /// <summary>
        /// حداکثر سن
        /// </summary>
        [Display(Name = "ToAge", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int? PassengerMaxAge { get; set; }
        /// <summary>
        /// شماره موبایل مسافر
        /// </summary>
        [Display(Name = "MobileNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [StringLength(15, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "ReqularFormat", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string PassengerMobileNumber { get; set; }
        /// <summary>
        /// ملیت مسافر
        /// </summary>
        [Display(Name = "Nationality", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public List<int> PassengersNationality { get; set; }
        /// <summary>
        /// حداقل تعداد مسافر
        /// </summary>
        [Display(Name = "FromPassenger", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int? MinPassengerQuantity { get; set; }
        /// <summary>
        /// حداکثر تعداد مسافر
        /// </summary>
        [Display(Name = "ToPassenger", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int? MaxPassengerQuantity { get; set; }
        #endregion
    }
}
