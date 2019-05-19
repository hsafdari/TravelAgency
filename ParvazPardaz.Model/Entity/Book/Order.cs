using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Book;

namespace ParvazPardaz.Model.Entity.Book
{
    public class Order : BaseBigEntity
    {
        #region Properties
        public string TrackingCode { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountPrice { get; set; }
        public int CurrentTaxPercentage { get; set; }
        public decimal TotalTaxPrice { get; set; }
        public decimal TotalPayPrice { get; set; }
        public EnumOrderStep OrderStep { get; set; }
        public DateTime FlightDateTime { get; set; }
        public DateTime? ReturnFlightDateTime { get; set; }
        public string TicketReferenceNo { get; set; }
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
        public int RemainingFlightCapacity { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime PayExpiredDateTime { get; set; }
        public decimal? BaseCommission { get; set; }
        public decimal? CommissionPrice { get; set; }
        public UserProfileType ProfileType { get; set; }
        #endregion

        #region Reference navigation properties
        public Nullable<int> CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public int OrderTypeId { get; set; }
        public virtual OrderType OrderType { get; set; }

        public int AgencySettingId { get; set; }
        public virtual AgencySetting AgencySetting { get; set; }
        /*
         * PackageId
         * OrderUniqueNumberId
         * OrderTypeId
         */
        #endregion

        #region Collection navigation properties
        public virtual ICollection<OrderTask> OrderTasks { get; set; }
        //public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<SelectedAddServ> SelectedAddServs { get; set; }
        public virtual ICollection<SelectedFlight> SelectedFlights { get; set; }
        public virtual ICollection<SelectedHotelPackage> SelectedHotelPackages { get; set; }
        /// <summary>
        /// شماره های منحصر به فرد پرداخت
        /// </summary>
        public virtual ICollection<PaymentUniqueNumber> PaymentUniqueNumbers { get; set; }
        /// <summary>
        /// دریافت کنندگان اطلاعات سفارش
        /// </summary>
        public virtual ICollection<VoucherReceiver> VoucherReceivers { get; set; }

        /// <summary>
        /// لاگ های اعتبار
        /// </summary>
        public virtual ICollection<Credit> Credits { get; set; }
        #endregion

        #region One-To-One navigation property
        public virtual OrderInformation OrderInformation { get; set; }
        #endregion
    }
}
