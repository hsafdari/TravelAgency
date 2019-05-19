using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class GridUserGroupViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Title { get; set; }

        /// <summary>
        /// میزان درصد ی که به گروه کاربری تعلق میگیرد
        /// </summary>
        [Display(Name = "Percent", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int? Percent { get; set; }

        /// <summary>
        /// حداقل میزان موجودی اعتبار برای گروه کاربری
        /// </summary>
        [Display(Name = "MinCreditValue", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public decimal MinCreditValue { get; set; }

        /// <summary>
        /// حداکثر تعداد فروش در روز
        /// </summary>
        [Display(Name = "SalesCountPerDay", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int SalesCountPerDay { get; set; }
    }
}