using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// برنامه سفر هر تور که میتواند به ازای تعداد روز هر تور یا به ازای هر شهر تور باشد
    /// </summary>
    public class TourProgram : BaseEntity
    {
        #region Properties
        /// <summary>
        /// مدت زمان برنامه سفر که تعداد روز را مشخص می کند
        /// </summary>
        public int DurationDay { get; set; }
        /// <summary>
        /// ترتیب برنامه سفر را در یک تور مشخص میکند
        /// </summary>
        public int DayOrder { get; set; }
        /// <summary>
        /// شهر محل اقامت این برنامه سفر را مشخص میکند
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual Tour Tour { get; set; }
        public int TourId { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourProgramActivity> TourProgramActivities { get; set; }
        public virtual ICollection<Leader> Leaders { get; set; }
        public virtual ICollection<TourProgramDetail> TourProgramDetails { get; set; }
        #endregion
    }
}
