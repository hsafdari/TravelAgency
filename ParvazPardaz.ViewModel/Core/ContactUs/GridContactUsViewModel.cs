using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridContactUsViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Title { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Email { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string CellPhone { get; set; }

        [Display(Name = "Department", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string DepartmentTitle { get; set; }

        [Display(Name = "Content", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Content { get; set; }
    }
}
