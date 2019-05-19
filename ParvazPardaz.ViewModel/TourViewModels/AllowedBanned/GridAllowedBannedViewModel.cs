using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridAllowedBannedViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public bool IsActive { get; set; }
    }
}
