using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class EditUserGroupViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        /// <summary>
        /// میزان درصد ی که به گروه کاربری تعلق میگیرد
        /// </summary>
        [Display(Name = "Percent", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression(@"\d*", ErrorMessageResourceName = "OnlyNumber", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int? Percent { get; set; }

        /// <summary>
        /// حداقل میزان موجودی اعتبار برای گروه کاربری
        /// </summary>
        [Display(Name = "MinCreditValue", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression(@"\d*", ErrorMessageResourceName = "OnlyNumber", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public decimal MinCreditValue { get; set; }

        /// <summary>
        /// حداکثر تعداد فروش در روز
        /// </summary>
        [Display(Name = "SalesCountPerDay", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [RegularExpression(@"\d*", ErrorMessageResourceName = "OnlyNumber", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int SalesCountPerDay { get; set; }
    }
}