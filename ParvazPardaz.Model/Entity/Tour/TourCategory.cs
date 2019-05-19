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
    /// دسته بندی تور
    /// </summary>
    public class TourCategory : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان دسته بندی تور
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Tour> Tours { get; set; }
        #endregion
    }
}
