using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourHomeViewModel:BaseViewModelId
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }

        [Display(Name = "Recomended", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public bool Recomended { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ShortDescription { get; set; }

        public string ImageURL { get; set; }

        //[Display(Name = "TourCategory", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //public List<int> SelectedTourCategory { get; set; }
    }
}
