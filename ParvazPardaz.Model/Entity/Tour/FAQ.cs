using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class FAQ : BaseEntity
    {
        #region Properties
        /// <summary>
        ///  سوال
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// جواب
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن 
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual Tour Tour { get; set; }
        public int TourId { get; set; }
        #endregion
    }
}
