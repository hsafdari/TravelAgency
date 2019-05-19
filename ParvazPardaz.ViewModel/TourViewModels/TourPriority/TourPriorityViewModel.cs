using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// اولویت دهی به تورها بر اساس گروه تور    
    /// </summary>
    public class TourPriorityViewModel
    {
        #region Constructor
        public TourPriorityViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// گروه های نوشته که شامل کلمه tour باشد
        /// </summary>
        [Display(Name = "TourGroup", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "SelectAtLeastOneElement", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public List<int> SelectedTourGroupIds { get; set; }
        public MultiSelectList TourGroupMSL { get; set; }

        /// <summary>
        /// تورها
        /// </summary>
        public List<Tour> Tours { get; set; }
        #endregion
    }
}
