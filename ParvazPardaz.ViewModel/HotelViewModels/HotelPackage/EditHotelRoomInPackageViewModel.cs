using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditHotelRoomInPackageViewModel : BaseViewModelOfEntity
    {
        #region Tour Properties
        /// <summary>
        /// شناسه تور
        /// </summary>
        [Display(Name = "TourId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TourId { get; set; }

        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TourTitle { get; set; }
        #endregion

        #region TourPackage Properties
        /// <summary>
        /// شناسه پکیج تور
        /// </summary>
        [Display(Name = "TourPackageId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TourPackageId { get; set; }

        /// <summary>
        /// عنوان پکیج تور
        /// </summary>
        [Display(Name = "TourPackageTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TourPackageTitle { get; set; }
        #endregion

        #region HotelPackage Properties
        /// <summary>
        /// شناسه پکیج هتل
        /// </summary>
        [Display(Name = "HotelPackageId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int HotelPackageId { get; set; }

        /// <summary>
        /// عنوان پکیج هتل
        /// </summary>
        [Display(Name = "HotelPackageTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string HotelPackageTitleString { get { return string.Join(", ", HotelPackageTitle); } set { } }
        public IEnumerable<string> HotelPackageTitle { get; set; }
        #endregion

        #region HotelPackageHotelRooms Properties
        /// <summary>
        /// شناسه جدول میانی اتاق هتل_پکیج هتل برای ویرایش قیمت و ظرفیت 
        /// </summary>
        [Display(Name = "HotelPackageHotelRoomId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int HotelPackageHotelRoomId { get; set; }

        /// <summary>
        /// عنوان اتاق
        /// </summary>
        [Display(Name = "HotelRoomTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string HotelRoomTitle { get; set; }

        ///// <summary>
        ///// ظرفیت تعریف شده کل
        ///// </summary>
        //[Display(Name = "TotalCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        //public int TotalCapacity { get; set; }

        /// <summary>
        /// ظرفیت تعریف شده کل
        /// </summary>
        [Display(Name = "TotalAdultCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TotalAdultCapacity { get; set; }

        /// <summary>
        /// ظرفیت تعریف شده کل
        /// </summary>
        [Display(Name = "TotalChildCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TotalChildCapacity { get; set; }

        /// <summary>
        /// ظرفیت تعریف شده کل
        /// </summary>
        [Display(Name = "TotalInfantCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TotalInfantCapacity { get; set; }


        /// <summary>
        /// ظرفیت در حال رزرو
        /// </summary>
        [Display(Name = "BookingCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int BookingCapacity { get; set; }

        /// <summary>
        /// ظرفیت باقی مانده آزاد برای فروش
        /// </summary>
        [Display(Name = "RemainBookingCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int RemainBookingCapacity { get; set; }

        ///// <summary>
        ///// ظرفیت فروخته شده
        ///// </summary>
        //[Display(Name = "CapacitySold", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        //public int CapacitySold { get; set; }

        /// <summary>
        /// ظرفیت بزرگسال فروخته شده
        /// </summary>
        [Display(Name = "AdultCapacitySold", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int AdultCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت کودک فروخته شده
        /// </summary>
        [Display(Name = "ChildCapacitySold", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int ChildCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت نوزاد فروخته شده
        /// </summary>
        [Display(Name = "InfantCapacitySold", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int InfantCapacitySold { get; set; }


        /// <summary>
        /// ظرفیت باقی مانده
        /// RemainingTotalCapacity = BookingCapacity + RemainBookingCapacity
        /// </summary>
        [Display(Name = "RemainTotalCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int RemainTotalCapacity { get { return (BookingCapacity + RemainBookingCapacity); } }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "AdultPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "ChildPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Display(Name = "InfantPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal InfantPrice { get; set; }


        ///// <summary>
        ///// قیمت ارزی
        ///// </summary>
        //[Display(Name = "OtherCurrencyPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        //public Nullable<decimal> OtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی
        /// </summary>
        [Display(Name = "AdultOtherCurrencyPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<decimal> AdultOtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی
        /// </summary>
        [Display(Name = "ChildOtherCurrencyPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<decimal> ChildOtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی
        /// </summary>
        [Display(Name = "InfantOtherCurrencyPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public Nullable<decimal> InfantOtherCurrencyPrice { get; set; }


        /// <summary>
        /// واحد پولی قیمت ارزی
        /// </summary>
        [Display(Name = "OtherCurrency", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string OtherCurrencyTitle { get; set; }
        #endregion

        //ویوو مدل اطلاعات مرتبط هم در اینجا گذارده شود
    }
}
