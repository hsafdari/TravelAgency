//using ParvazPardaz.Model.Entity.Common;
//using ParvazPardaz.Model.Entity.Hotel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ParvazPardaz.Model.Entity.Tour
//{
//    /// <summary>
//    /// هر تور در زمان مشخصی و اتاق مشخصی (بچه و کودک و یک تخته و دو تخته و ...) قیمت دارد
//    /// </summary>
//    public class TourScheduleHotelRoom : BaseEntity
//    {
//        #region Properties
//        /// <summary>
//        /// قیمت 
//        /// </summary>
//        public decimal Price { get; set; }
//        /// <summary>
//        /// ظرفیت 
//        /// </summary>
//        public int Capacity { get; set; }
//        /// <summary>
//        /// ظرفیت نامحدود ؟
//        /// </summary>
//        public bool NonLimit { get; set; }
//        #endregion

//        #region Reference Navigation Property
//        public virtual TourSchedule TourSchedule { get; set; }
//        public int TourScheduleId { get; set; }
//        public virtual HotelRoom HotelRoom { get; set; }
//        public int HotelRoomId { get; set; }
//        #endregion
//    }
//}
