using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Users
{
    public class UserCommission:BaseEntity
    {
        public decimal CommissionPercent { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? ConditionPercent { get; set; }
        #region One to One navigation property
        public virtual UserProfile UserProfile { get; set; }
        #endregion
    }
}
