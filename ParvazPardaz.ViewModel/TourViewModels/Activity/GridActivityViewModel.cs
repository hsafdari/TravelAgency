using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridActivityViewModel:BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        [Display(Name = "Place", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Place { get; set; }
         [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageUrl { get; set; }
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }
    }
}
