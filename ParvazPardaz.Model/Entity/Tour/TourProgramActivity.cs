using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// فعالیتهای تور به ازای هر شهر یا برنامه تور
    /// </summary>
    public class TourProgramActivity : BaseEntity
    {
        #region Properties
        /// <summary>
        /// مدت زمان فعالیت
        /// </summary>
        public DateTime? DurationTime { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// اطلاعات و توضیح مربوط به وسیله نقلیه درون شهری
        /// </summary>
        public string TransferVehicleInfo { get; set; }
        /// <summary>
        /// قیمت فعالیت
        /// </summary>
        public decimal? Price { get; set; }
        #endregion

        #region Reference Navigation Property
        public virtual Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public virtual TourProgram TourProgram { get; set; }
        public int TourProgramId { get; set; }
        #endregion
    }

}
