using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridDepartmentViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string DepartmentName { get; set; }

        [Display(Name = "ManagerName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ManagerName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string DepartmentEmail { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; } 
    }
}
