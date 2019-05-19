using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class AddMenuGroupViewModel : BaseViewModelId
    {
        [Display(Name = "GroupName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string GroupName { get; set; }

        [Display(Name = "IsRoot", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsRoot { get; set; }
    }
}
