using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.NotFoundLink
{
    public class GridNotFoundLinkViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string URL { get; set; }
    }
}
