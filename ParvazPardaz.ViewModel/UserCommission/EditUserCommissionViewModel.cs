using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditUserCommissionViewModel : BaseViewModelId
    {
        #region Properties
       [Display(Name = "CommissionPercent", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public decimal CommissionPercent { get; set; }
        [Display(Name = "FromDate", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public DateTime? FromDate { get; set; }
        [Display(Name = "ToDate", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public DateTime? ToDate { get; set; }
        [Display(Name = "ConditionPercent", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public decimal? ConditionPercent { get; set; }
        #endregion
    }
}
