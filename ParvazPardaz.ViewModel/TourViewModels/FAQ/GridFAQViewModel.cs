using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.ViewModel
{
    public class GridFAQViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        ///  سوال
        /// </summary>
        /// 
        [Display(Name = "Question", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Question { get; set; }
        /// <summary>
        /// جواب
        /// </summary>
        /// 
        [Display(Name = "Answer", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Answer { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن 
        /// </summary>
        /// 
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsActive { get; set; }

        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }
        [ScaffoldColumn(false)]
        public int TourId { get; set; }
    }
}
