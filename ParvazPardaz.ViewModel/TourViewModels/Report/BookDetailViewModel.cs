using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class BookDetailViewModel : BaseViewModelId
    {
        #region Peoperties  related to booking tour schedule
        [Display(Name = "BookingDateTime", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string BookingDateTime { get; set; }
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourCode { get; set; }
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        [Display(Name = "FromDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FromDate { get; set; }
        [Display(Name = "ToDate", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ToDate { get; set; }
        [Display(Name = "Capacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Capacity { get; set; }
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Price { get; set; }
        [Display(Name = "Total", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string Total { get; set; }
        [Display(Name = "Passenger", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string PassengerQuantity { get; set; }
        #endregion
        /// <summary>
        /// لیستی از مسافران سفارش رزرو شده
        /// </summary>
        public List<BookingPassenger> BookingPassengers { get; set; }
    }

    public class BookingPassenger
    {
        public string Index { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "FirstName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string LastName { get; set; }
        /// <summary>
        /// نام و نام خانودگی
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FullName { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// سن
        /// </summary>
        [Display(Name = "Age", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Age { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Address { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "ZipCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string ZipCode { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        [Display(Name = "MobileNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string MobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Email { get; set; }
        /// <summary>
        /// نام پاسپورت
        /// </summary>
        [Display(Name = "OfficialNameOfPassport", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string OfficialNameOfPassport { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        [Display(Name = "Nationality", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Nationality { get; set; }
        /// <summary>
        /// مکان صدور پاسپورت
        /// </summary>
        [Display(Name = "PlaceOfIssueOfPassport", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string PlaceOfIssueOfPassport { get; set; }
        /// <summary>
        /// تاریخ صدور پاسپورت
        /// </summary>
        [Display(Name = "DateOfIssueOfPasssprot", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string DateOfIssueOfPasssprot { get; set; }

    }
}
