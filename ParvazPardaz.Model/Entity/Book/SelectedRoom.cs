using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model
{
    public class SelectedRoom : BaseEntity
    {
        #region Properties
        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کودک
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        public int InfantCount { get; set; }

        ///// <summary>
        ///// ظرفیتی که این نوع اتاق داشته چند تا بوده؟
        ///// </summary>
        //public int CurrentCapacity { get; set; }

        /// <summary>
        /// ظرفیتی که این نوع اتاق برای بزرگسال داشته چند تا بوده؟
        /// </summary>
        public int CurrentAdultCapacity { get; set; }

        /// <summary>
        /// ظرفیتی که این نوع اتاق برای کودک داشته چند تا بوده؟
        /// </summary>
        public int CurrentChildCapacity { get; set; }


        /// <summary>
        /// ظرفیتی که این نوع اتاق برای نوزاد داشته چند تا بوده؟
        /// </summary>
        public int CurrentInfantCapacity { get; set; }

        ///// <summary>
        ///// تعداد درخواستی
        ///// </summary>
        //public int ReserveCapacity { get; set; }
        /// <summary>
        /// مبلغ ریالی جاری 
        /// </summary>
        public decimal RialPrice { get; set; }

        ///// <summary>
        ///// مبلغ ارزی جاری
        ///// </summary>
        //public decimal? CurrencyPrice { get; set; }

        /// <summary>
        /// مبلغ ارزی جاری بزرگسال
        /// </summary>
        public decimal? AdultCurrencyPrice { get; set; }

        /// <summary>
        /// مبلغ ارزی جاری کودک
        /// </summary>
        public decimal? ChildCurrencyPrice { get; set; }

        /// <summary>
        /// مبلغ ارزی جاری برای نوزاد
        /// </summary>
        public decimal? InfantCurrencyPrice { get; set; }

        /// <summary>
        /// هر واحد ارزی به ریال چقدر بوده است؟
        /// </summary>
        public decimal? BaseCurrencyToRialPrice { get; set; }

        /// <summary>
        /// علامت واحد پولی جاری
        /// </summary>
        public string CurrencySign { get; set; }

        ///// <summary>
        ///// مبلغ واحد به ریال
        ///// ToDo : UnitPrice = RialPrice + (CurrencyPrice * BaseCurrencyToRialPrice)
        ///// ToDo : TotalPrice = ReserveCapacity * UnitPrice
        ///// </summary>
        //public decimal UnitPrice { get; set; }

        /// <summary>
        /// مبلغ واحد بزرگسال به ریال
        /// ToDo : AdultUnitPrice = RialPrice + (AdultCurrencyPrice * BaseCurrencyToRialPrice)
        /// </summary>
        public decimal AdultUnitPrice { get; set; }

        /// <summary>
        /// مبلغ واحد کودک به ریال
        /// ToDo : ChildUnitPrice = RialPrice + (ChildCurrencyPrice * BaseCurrencyToRialPrice)
        /// </summary>
        public decimal ChildUnitPrice { get; set; }

        /// <summary>
        /// مبلغ واحد نوزاد به ریال
        /// ToDo : InfantUnitPrice = RialPrice + (InfantCurrencyPrice * BaseCurrencyToRialPrice)
        /// </summary>
        public decimal InfantUnitPrice { get; set; }

        /// <summary>
        /// درصد مالیات بر ارزش افزوده
        /// </summary>
        public int VAT { get; set; }

        /// <summary>
        /// مبلغ مالیات بر ارزش افزوده
        /// (VAT * (UnitPrice * ReserveCapacity)) /100
        /// </summary>
        public decimal VATPrice { get; set; }
        #endregion

        #region Reference navigation property
        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }

        public int SelectedHotelPackageId { get; set; }
        public virtual SelectedHotelPackage SelectedHotelPackage { get; set; }
        #endregion

        #region Collection navigation properties
        /// <summary>
        /// مسافران این اتاق
        /// </summary>
        public virtual ICollection<Passenger> Passengers { get; set; }
        #endregion
    }
}
