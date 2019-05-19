using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class RequiredDocument : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region Collection Navigation Property
        public virtual ICollection<Tour> Tours { get; set; }
        #endregion
    }
}
