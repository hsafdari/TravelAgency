using ParvazPardaz.Common.Filters;
using ParvazPardaz.ViewModel.TourViewModels.Tour.TourClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// صفحه ی بعد جستجوی صفحه اول
    /// </summary>
    public class TourReserveViewModel
    {
        #region Constructor
        public TourReserveViewModel()
        {
            HotelInfos = new List<HotelClientViewModel>();
            SelectedFlights = new List<TourPacakgeFlightsViewModel>();
            HotelRoomInfos = new List<HotelPackageHotelRoomViewModel>();
            //PassengerList = new List<AddPassengerViewModel>();
            TourSearchParams = new TourSearchViewModel();
            hotelRoomPassengerSection = new List<HotelRoomPassengerSectionViewModel>();
            VoucherReceivers = new List<VoucherReceiverViewModel>();
            RoomSectionIndex = 0;
        }
        #endregion

        #region Searched Info properties
        /// <summary>
        /// موارد جستجو شده
        /// </summary>
        public TourSearchViewModel TourSearchParams { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// پرواز رفت انتخاب شده
        /// </summary>
        public int SelectedDepartureFlightId { get; set; }

        /// <summary>
        /// پرواز برگشت انتخاب شده
        /// </summary>
        public int SelectedArrivalFlightId { get; set; }

        /// <summary>
        /// اطلاعات پرواز های انتخاب شده
        /// </summary>
        public List<TourPacakgeFlightsViewModel> SelectedFlights { get; set; }

        /// <summary>
        /// شناسه پکیج هتل انتخاب شده
        /// </summary>
        public int SelectedHotelPackageId { get; set; }

        /// <summary>
        /// اطلاعات هتل های موجود در این پکیج هتل انتخاب شده
        /// </summary>
        public List<HotelClientViewModel> HotelInfos { get; set; }

        /// <summary>
        /// اطلاعات انواع تخت برای این پکیج هتل انتخاب شده
        /// </summary>
        public List<HotelPackageHotelRoomViewModel> HotelRoomInfos { get; set; }

        public string CouponCode { get; set; }

        /// <summary>
        /// قبول قوانین و مقررات
        /// </summary>
        [EnforceTrueAttribute()]
        [Display(Name = "TermsAndConditions", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public bool TermsAndConditions { get; set; }
        #endregion

        #region VoucherReceivers
        /// <summary>
        /// دریافت کنندگان اطلاعات تور
        /// </summary>
        public List<VoucherReceiverViewModel> VoucherReceivers { get; set; }
        #endregion

        #region Room Section Properties
        /// <summary>
        /// سکشن اتاق ها
        /// </summary>
        public List<RoomSectionViewModel> RoomSections { get; set; }
        public int RoomSectionIndex { get; set; }
        #endregion

        #region LoggedInUser
        /// <summary>
        /// شناسه کاربر لاگین کرده
        /// </summary>
        public long LoggedInUserId { get; set; }
        #endregion

        #region Passenger partial view properties
        /// <summary>
        /// تور خارجی؟
        /// </summary>
        public bool IsForeignTour { get; set; }

        /// <summary>
        /// مسافران
        /// </summary>
        public List<AddPassengerViewModel> PassengerList { get; set; }
        #endregion

        public List<HotelRoomPassengerSectionViewModel> hotelRoomPassengerSection { get; set; }
    }
}
