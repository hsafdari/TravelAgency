using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridSupportViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// فعال؟
        /// </summary>
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [Display(Name = "UpdateDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string UpdateDescription { get; set; }
        /// <summary>
        /// توضیحات فنی مدیریت
        /// </summary>
        [Display(Name = "UpdateAdminDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string UpdateAdminDescription { get; set; }
        /// <summary>
        /// نویسنده
        /// </summary>
        [Display(Name = "Author", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Author { get; set; }
        /// <summary>
        /// تاریخ پشتیبانی
        /// </summary>
        [Display(Name = "SupportDateTime", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public DateTime SupportDateTime { get; set; }
    }
}
