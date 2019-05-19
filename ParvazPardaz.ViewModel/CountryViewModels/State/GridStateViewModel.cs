using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridStateViewModel:BaseViewModelOfEntity
    {
        /// <summary>
        /// نام استان
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        /// <summary>
        /// تصویری از استان
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ImageUrl { get; set; }
        /// <summary>
        /// نام کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public String Country { get; set; }
    }
}
