using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// جدول میانی زمانبندی تور و خدمات اضافه تور که مشخص میکند هر تور در زمان مشخصی چه خدماتی دارد با چه قیمتی  
    /// </summary>
    public class TourScheduleAdditionalService : BaseEntity
    {
        #region Properties
        /// <summary>
        /// قیمت خدمات 
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// ظرفیت خدمات
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// ظرفیت نامحدود خدمات ؟
        /// </summary>
        public bool NonLimit { get; set; }
        /// <summary>
        /// تعداد فروخته شده
        /// </summary>
        public int SoldQuantity { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual TourSchedule TourSchedule { get; set; }
        public int TourScheduleId { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }
        public int AdditionalServiceId { get; set; }
        #endregion
    }
}
