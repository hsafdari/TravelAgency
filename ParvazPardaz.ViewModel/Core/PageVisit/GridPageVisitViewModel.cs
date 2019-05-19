using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPageVisitViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "VisitDateTime", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public DateTime VisitDateTime { get; set; }

        [Display(Name = "IPAddress", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [MaxLength(16)]
        public string IPAddress { get; set; }

        public string PageTitle { get; set; }
    }
}
