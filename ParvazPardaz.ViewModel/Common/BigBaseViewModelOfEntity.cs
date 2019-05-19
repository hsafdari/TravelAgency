using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class BigBaseViewModelOfEntity : BaseViewModelBigId
    {
        /// <summary>
        /// نام کاربری ایجاد کننده
        /// </summary>
        [Display(Name = "CreatorUserName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string CreatorUserName { get; set; }
        /// <summary>
        /// تاریج ایجاد
        /// </summary>
        [Display(Name = "CreatorDateTime", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public DateTime? CreatorDateTime { get; set; }
        /// <summary>
        /// تاریج ایجاد
        /// </summary>
        //public string CreatorDate { get; set; }
        /// <summary>
        /// نام کاربری آخرین ویرایش
        /// </summary>
        [Display(Name = "LastModifierUserName", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public string LastModifierUserName { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        [Display(Name = "LastModifierDate", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public DateTime? LastModifierDate { get; set; }

        ///// <summary>
        ///// تاریج ایجاد
        ///// </summary>
        //public string CreatorDate
        //{
        //    get
        //    {
        //        if (CreatorDateTime.HasValue && CreatorDateTime.Value != DateTime.MinValue)
        //        {
        //            return String.Format("{0:dddd, MMMM d, yyyy}", CreatorDateTime.Value);
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    set { }
        //}
    }
}
