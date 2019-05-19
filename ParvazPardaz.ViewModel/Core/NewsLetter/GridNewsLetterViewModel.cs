using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.Core
{
    public class GridNewsLetterViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Email { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Mobile { get; set; }
    }
}
