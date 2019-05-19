using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridSliderGroupViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string GroupTitle { get; set; }
        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }
        [Display(Name = "ColorCode", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ColorCode { get; set; }
        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int Priority { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }
    }
}
