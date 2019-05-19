using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridOrderViewModel : BigBaseViewModelOfEntity
    {
        public GridOrderViewModel()
        {
            this.TourReserve = new TourReserveViewModel();
            OrderInfo = new GridOrderInformationViewModel();
        }

        #region Properties
        [Display(Name = "Id", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }
        ///<summary>
        ///کد پیگیری
        ///</summary>
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TrackingCode { get; set; }
        /// <summary>
        /// کد تعریف شده تور
        /// </summary>
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourCode { get; set; }
        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        /// <summary>
        /// کد پکیج
        /// </summary>
        [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackageCode { get; set; }
        /// <summary>
        /// عنوان پکیج
        /// </summary>
        [Display(Name = "TourPackage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackage { get; set; }
        /// <summary>
        /// قیمت کل
        /// </summary>
        [Display(Name = "TotalPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// تخفیف کل
        /// </summary>
        [Display(Name = "TotalDiscountPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalDiscountPrice { get; set; }

        /// <summary>
        /// مالیات کل
        /// </summary>
        [Display(Name = "TotalTaxPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalTaxPrice { get; set; }

        /// <summary>
        /// کل مبلغ قابل پرداخت
        /// </summary>
        [Display(Name = "TotalPayPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalPayPrice { get; set; }

        /// <summary>
        /// وضعیت سفارش
        /// </summary>
        [Display(Name = "OrderStep", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public EnumOrderStep OrderStep { get; set; }

        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        [Display(Name = "AdultCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کودک
        /// </summary>
        [Display(Name = "ChildCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        [Display(Name = "InfantCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int InfantCount { get; set; }

        /// <summary>
        /// ظرفیت باقیمانده پرواز
        /// </summary>
        [Display(Name = "RemainingFlightCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int RemainingFlightCapacity { get; set; }

        /// <summary>
        /// موفق؟
        /// </summary>
        [Display(Name = "IsSuccessful", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public bool IsSuccessful { get; set; }
        [Display(Name = "Buyer", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BuyerTitle { get; set; }
        [Display(Name = "ReserveDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime FlightDateTime { get; set; }
        [Display(Name = "PayExpiredDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime PayExpiredDateTime { get; set; }
        [Display(Name = "NationalCodePassportNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string NationalCode { get; set; }
        public TourReserveViewModel TourReserve { get; set; }
        public GridOrderInformationViewModel OrderInfo { get; set; }
        [Display(Name = "BaseCommission", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public decimal? BaseCommission { get; set; }
        [Display(Name = "CommissionPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public decimal? CommissionPrice { get; set; }
        #endregion
    }
}
