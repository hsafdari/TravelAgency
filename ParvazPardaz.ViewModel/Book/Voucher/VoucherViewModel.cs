using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel.TourViewModels.Tour.TourClient;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.ViewModel
{
    public class VoucherViewModel : BigBaseViewModelOfEntity
    {
        public VoucherViewModel()
        {
            this.Footer = new List<FooterUIViewModel>();
        }
        #region Order Properties
        /// <summary>
        /// کد پیگیری
        /// </summary>
        public string TrackingCode { get; set; }

        /// <summary>
        /// مبلغ خام
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// کل مبلغ تخفیف
        /// </summary>
        public decimal TotalDiscountPrice { get; set; }

        /// <summary>
        /// درصد مالیات بر ارزش افزوده هنگام سفارش چقدر بوده؟
        /// </summary>
        public int CurrentTaxPercentage { get; set; }

        /// <summary>
        /// کل مبلغ مالیات
        /// </summary>
        public decimal TotalTaxPrice { get; set; }

        /// <summary>
        /// مبلغ پرداخت شده
        /// </summary>
        public decimal TotalPayPrice { get; set; }

        /// <summary>
        /// مهلت پرداخت تا کی بوده؟
        /// </summary>
        public DateTime PayExpiredDateTime { get; set; }

        /// <summary>
        /// سفارش در چه مرحله ای قرار داره؟
        /// </summary>
        public EnumOrderStep OrderStep { get; set; }

        /// <summary>
        /// تاریخ پرواز
        /// </summary>
        public DateTime FlightDateTime { get; set; }

        /// <summary>
        /// تعداد کل بزرگسال : مجموع بزرگسالان تمامی پکیج ها
        /// </summary>
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کل کودکان : مجموع کودکان تمامی پکیج ها
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد کل نوزادان : مجموع نوزادان تمامی پکیج ها
        /// </summary>
        public int InfantCount { get; set; }

        /// <summary>
        /// فعال؟
        /// </summary>
        public bool IsSuccessful { get; set; }
        public List<FooterUIViewModel> Footer { get; set; }
        
        #endregion

        #region Collection navigation Properties
        /// <summary>
        /// اطلاعات پرواز های انتخاب شده
        /// </summary>
        public List<TourPacakgeFlightsViewModel> SelectedFlights { get; set; }

        /// <summary>
        /// اطلاعات هتل های موجود در این پکیج هتل انتخاب شده
        /// </summary>
        public List<HotelClientViewModel> HotelInfos { get; set; }

        /// <summary>
        /// اطلاعات انواع تخت برای این پکیج هتل انتخاب شده
        /// </summary>
        public List<HotelPackageHotelRoomViewModel> HotelRoomInfos { get; set; }

        /// <summary>
        /// مسافران
        /// </summary>
        public List<AddPassengerViewModel> PassengerList { get; set; }
        #endregion
        /// <summary>
        /// TourTitle-PackgeTitle
        /// </summary>
        public string TourTitle { get; set; }
    }
}
