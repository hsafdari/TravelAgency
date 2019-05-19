using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    /// <summary>
    /// امکانات هتل که میتواند شامل اینترنت و استخر و ... باشد
    /// </summary>
    public class HotelFacility : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان امکانات هتل
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Hotel> Hotels { get; set; }
        #endregion
    }
}
