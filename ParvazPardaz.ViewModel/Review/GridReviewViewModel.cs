using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridReviewViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان آیتم نقد و بررسی
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Title { get; set; }
        /// <summary>
        /// آیتم نقد و بررسی برای
        /// </summary>
        [Display(Name = "ReviewType", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ReviewType ReviewType { get; set; }
        /// <summary>
        /// فعال؟
        /// </summary>
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }
    }
}
