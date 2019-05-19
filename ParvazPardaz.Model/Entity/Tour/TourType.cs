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
    /// نوع تور که میتواند شامل خانوادگی و تحصیلی و ...  باشد 
    /// </summary>
    public class TourType : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام نوع تور
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Collection Navigation Property
        public virtual ICollection<Tour> Tours { get; set; }
        #endregion
    }
}
