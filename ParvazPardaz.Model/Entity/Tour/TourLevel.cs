using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// سطح تور که شامل پیشرفته و ساده و ... میتواند باشد
    /// </summary>
    public class TourLevel : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان سطح تور
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Tour> Tours { get; set; }
        #endregion
    }
}
