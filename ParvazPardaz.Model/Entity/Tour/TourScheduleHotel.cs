using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// هر تور در زمان مشخصی میتواند چندین هتل به ازای برنامه سفرش داشته باشد
    /// به ازای اقامت در هر  روز برنامه سفر
    /// </summary>
    public class TourScheduleHotel : BaseEntity
    {
        #region Properties
        /// <summary>
        /// قمیت هتل
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ظرفیت هتل
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود هتل ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        #endregion

        #region Reference Navigation Properties
        public int TourScheduleId { get; set; }
        public virtual TourSchedule TourSchedule { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel.Hotel Hotel { get; set; }
        #endregion
    }
}
