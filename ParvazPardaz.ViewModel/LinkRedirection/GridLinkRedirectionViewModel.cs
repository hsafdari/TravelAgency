using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridLinkRedirectionViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "OldLink", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string OldLink { get; set; }

        [Display(Name = "NewLink", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string NewLink { get; set; }
    }
}
