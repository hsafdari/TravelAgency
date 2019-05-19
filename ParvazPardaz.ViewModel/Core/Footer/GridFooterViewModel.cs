using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridFooterViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Title { get; set; }

        [Display(Name = "Content", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Content { get; set; }

        [Display(Name = "OrderId", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int OrderID { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }

        /// <summary>
        /// پانویس برای ...
        /// </summary>
        [Display(Name = "FooterType", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public EnumFooterType FooterType { get; set; }
    }
}
