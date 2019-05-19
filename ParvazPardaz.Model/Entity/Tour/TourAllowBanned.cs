using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class TourAllowBanned : BaseEntity
    {
        #region Properties
        /// <summary>
        /// آیا مجاز است ؟
        /// </summary>
        public bool IsAllowed { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual Tour Tour { get; set; }
        public int TourId { get; set; }
        public virtual AllowedBanned AllowedBanned { get; set; }
        public int AllowedBannedId { get; set; }
        #endregion
    }
}
