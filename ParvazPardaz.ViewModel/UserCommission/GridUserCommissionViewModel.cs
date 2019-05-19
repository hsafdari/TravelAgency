using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridUserCommissionViewModel : BaseViewModelOfEntity
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
        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string UserName { get; set; }
        [Display(Name = "Organization", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Organization { get; set; }
        [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string NationalCode { get; set; }
        #endregion
    }
}
