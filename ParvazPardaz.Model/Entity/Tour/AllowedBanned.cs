using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// موارد مجاز و غیر مجاز برای تور  
    /// که میتواند شامل پوشش و متعلقات و ... باشد
    /// </summary>
    public class AllowedBanned : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// فعال یا غیر فعال است ؟
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourAllowBanned> TourAllowBans { get; set; }
        #endregion
    }
}
