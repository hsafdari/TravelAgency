using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// صفحه تایید اطلاعات و پرداخت
    /// </summary>
    public class ConfirmViewModel
    {
        #region Constructor
        public ConfirmViewModel()
        {
            //VoucherReceivers = new List<VoucherReceiverViewModel>();
            SelectedFlights = new List<TourPacakgeFlightsViewModel>();
            HotelInfos = new List<HotelClientViewModel>();
            RoomTypePriceInfos = new List<RoomTypePriceInfoViewModel>();
            VoucherSectionIndex = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// عنوان تور
        /// </summary>
        public string TourTitle { get; set; }

        /// <summary>
        /// تصویر تور
        /// </summary>
        public string TourImageUrl { get; set; }

        /// <summary>
        /// مبلغ خام
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// کل مبلغ تخفیف
        /// </summary>
        public decimal TotalDiscountPrice { get; set; }

        /// <summary>
        /// کل مبلغ مالیات
        /// </summary>
        public decimal TotalTaxPrice { get; set; }

        /// <summary>
        /// مبلغ پرداخت شده
        /// </summary>
        public decimal TotalPayPrice { get; set; }
        #endregion

        #region VoucherReceivers Section
        /// <summary>
        /// دریافت کنندگان اطلاعات تور
        /// </summary>
        public List<VoucherReceiverViewModel> VoucherReceivers { get; set; }

        /// <summary>
        /// اطلاعات مورد نیاز یک دریافت کننده : استفاده در افزودن سکشن
        /// </summary>
        public VoucherReceiverViewModel VoucherReceiverItem { get; set; }

        /// <summary>
        /// ایندکس بخش دریافت کنندگان اطلاعات تور
        /// </summary>
        public int VoucherSectionIndex { get; set; }
        #endregion

        #region RoomTypePriceInfos
        /// <summary>
        /// لیستی از اتاق ها و اطلاعات قیمت و تعداد افرادی به تفکیک رده سنی
        /// </summary>
        public List<RoomTypePriceInfoViewModel> RoomTypePriceInfos { get; set; }
        #endregion

        #region Flight Section
        /// <summary>
        /// اطلاعات پرواز های انتخاب شده
        /// </summary>
        public List<TourPacakgeFlightsViewModel> SelectedFlights { get; set; }
        #endregion

        #region Hotel Section
        /// <summary>
        /// اطلاعات هتل های موجود در این پکیج هتل انتخاب شده
        /// </summary>
        public List<HotelClientViewModel> HotelInfos { get; set; }
        #endregion

        #region Credit Properties
        /// <summary>
        /// مبلغ مانده اعتبار کاربر لاگین کرده
        /// </summary>
        public string CreditValue { get; set; }

        /// <summary>
        /// کاربر پرداخت اعتباری رو انتخاب کرده؟
        /// </summary>
        public bool IsBeCreditPay { get; set; }

        /// <summary>
        /// پرداخت اعتباری مجازه؟
        /// مجاز = اعتبار باقیمانده بیشتـــــر از مبلغ نهایی
        /// </summary>
        public bool IsAvailableCreditPay { get; set; }
        #endregion
    }
}
