using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.TourViewModels.Tour.TourClient
{
    public class HotelPackageHotelRoomViewModel
    {
        #region Constructor
        public HotelPackageHotelRoomViewModel()
        {

        }
        #endregion

        #region HotelPackageHotelRoom Properties
        //علیزاده : فکر کنم پراپرتی 
        //TotalPassengerPrice
        //معادل همین پراپرتی باشه
        ///// <summary>
        ///// کل قیمتی که باید در صفحه رزرو تور برای هر نوع تخت نشون بدیم
        ///// </summary>
        ///// صفدری:نیاز به بازبینی دارد
        //public decimal TotalPrice { get { return ((AdultOtherCurrencyPrice + ChildOtherCurrencyPrice + InfantOtherCurrencyPrice) * BaseRialPrice) + AdultRialPrice; } }

        ///// <summary>
        ///// قیمت ریالی
        ///// </summary>
        //public decimal RialPrice { get; set; }

        /// <summary>
        /// قیمت ریالی
        /// </summary>
        public decimal AdultRialPrice { get; set; }

        /// <summary>
        /// قیمت ریالی
        /// </summary>
        public decimal ChildRialPrice { get; set; }

        /// <summary>
        /// قیمت ریالی
        /// </summary>
        public decimal InfantRialPrice { get; set; }

        /// <summary>
        /// قیمت واحد پولی بر اساس واحد ريال
        /// از جدول واحدهای پولی
        /// </summary>
        public decimal BaseRialPrice { get; set; }

        ///// <summary>
        ///// قیمت ارزی
        ///// </summary>
        //public decimal OtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی برای بزرگسال
        /// </summary>
        public decimal AdultOtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی برای کودک
        /// </summary>
        public decimal ChildOtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی برای نوزاد
        /// </summary>
        public decimal InfantOtherCurrencyPrice { get; set; }

        /// <summary>
        /// واحد پولی قیمت ارزی
        /// </summary>
        public string OtherCurrencyTitle { get; set; }

        ///// <summary>
        ///// موجودی 
        ///// </summary>
        //public int Capacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت بزرگسال در این پکیج هتل
        /// </summary>
        public int AdultCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت کودک در این پکیج هتل
        /// </summary>
        public int ChildCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت نوزاد در این پکیج هتل
        /// </summary>
        public int InfantCapacity { get; set; }

        ///// <summary>
        ///// ظرفیت فروخته شده
        ///// </summary>
        //public int CapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده بزرگسال
        /// </summary>
        public int AdultCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده کودک
        /// </summary>
        public int ChildCapacitySold { get; set; }

        /// <summary>
        /// ظرفیت فروخته شده نوزاد
        /// </summary>
        public int InfantCapacitySold { get; set; }
        #endregion

        #region HotelRoom properties
        /// <summary>
        /// شناسه نوع تخت
        /// </summary>
        public int HotelRoomId { get; set; }

        /// <summary>
        /// نام نوع اتاق
        /// </summary>
        public string Title { get; set; }
        public string TitleEn { get; set; }
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

        #region Count properties
        /// <summary>
        /// ظرفیتی که کاربر از این نوع تخت درخواست کرده
        /// </summary>
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }

        ///// <summary>
        ///// ظرفیت قابل ارایه : ظرفیت تعریف شده منهای ظرفیت فروخته شده
        ///// </summary>
        //public int AvailableCapacity { get; set; }

        /// <summary>
        /// ظرفیت قابل ارایه بزرگسال : ظرفیت تعریف شده منهای ظرفیت فروخته شده
        /// </summary>
        public int AdultAvailableCapacity { get; set; }

        /// <summary>
        /// ظرفیت قابل ارایه کودک : ظرفیت تعریف شده منهای ظرفیت فروخته شده
        /// </summary>
        public int ChildAvailableCapacity { get; set; }

        /// <summary>
        /// ظرفیت قابل ارایه نوزاد : ظرفیت تعریف شده منهای ظرفیت فروخته شده
        /// </summary>
        public int InfantAvailableCapacity { get; set; }
        #endregion

        #region Admin panel properties
        /// <summary>
        /// Σ [AgeRangeCount * ( AgeRangeCurrencyToRialPrice + AgeRangeRialPrice )]
        /// </summary>
        public decimal TotalPassengerPrice
        {
            get
            {
                return (AdultCount * ((AdultOtherCurrencyPrice * BaseRialPrice) + AdultRialPrice))
                    + (ChildCount * ((ChildOtherCurrencyPrice * BaseRialPrice) + ChildRialPrice))
                    + (InfantCount * ((InfantOtherCurrencyPrice * BaseRialPrice) + InfantRialPrice));
            }
        }

        /// <summary>
        /// قیمت مالیات بر ارزش افزوده
        /// </summary>
        public decimal VATPrice { get; set; }

        /// <summary>
        /// مجموع قیمت با احتساب مالیات بر ارزش افزوده
        /// 
        /// </summary>
        public decimal TotalPayedPrice { get { return (VATPrice + TotalPassengerPrice); } }
        #endregion
    }
}
