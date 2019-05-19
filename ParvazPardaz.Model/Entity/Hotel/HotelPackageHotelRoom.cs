using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelPackageHotelRoom : BaseEntity
    {
        #region Properties
        ///// <summary>
        ///// قیمت 
        ///// </summary>
        //public decimal Price { get; set; }

        /// <summary>
        /// قیمت  بزرگسال
        /// </summary>
        public decimal AdultPrice { get; set; }
        
        /// <summary>
        /// قیمت کودک
        /// </summary>
        public decimal ChildPrice { get; set; }
        
        /// <summary>
        /// قیمت نوزاد
        /// </summary>
        public decimal InfantPrice { get; set; }

        ///// <summary>
        ///// قیمت ارزی
        ///// </summary>
        //public Nullable<decimal> OtherCurrencyPrice { get; set; }

        ///// <summary>
        ///// ظرفیت تعریف شده 
        ///// </summary>
        //public int Capacity { get; set; } 
       

        /// <summary>
        /// قیمت ارزی برای بزرگسال
        /// </summary>
        public Nullable<decimal> AdultOtherCurrencyPrice { get; set; }

        /// <summary>
        /// قیمت ارزی برای کودک
        /// </summary>
        public Nullable<decimal> ChildOtherCurrencyPrice { get; set; }
        
        /// <summary>
        /// قیمت ارزی برای نوزاد
        /// </summary>
        public Nullable<decimal> InfantOtherCurrencyPrice { get; set; }

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

        /// <summary>
        /// ظرفیت نامحدود ؟
        /// </summary>
        public bool NonLimit { get; set; }
        #endregion

        #region Reference Navigation Property
        public virtual HotelPackage HotelPackage { get; set; }
        public int HotelPackageId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }
        public int HotelRoomId { get; set; }

        /// <summary>
        /// واحد پولی قیمت ارزی
        /// </summary>
        public Nullable<int> OtherCurrencyId { get; set; }
        /// <summary>
        /// این اتاق هتل از چه واحد پولی ای برای قیمت ارزی خود استفاده کرده است؟
        /// </summary>
        public virtual Currency Currency { get; set; }
        #endregion
    }
}
