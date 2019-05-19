using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class LastSecondTourViewModel
    {
        #region constructor
        public LastSecondTourViewModel()
        {
            OtherLastSecondTours = new List<LastSecondTourViewModel>();
        }
        #endregion

        #region properties
        public string Title { get; set; }
        public string NavigationUrl { get; set; }
        public string Description { get; set; }
        public string Context { get; set; }
        public Nullable<DateTime> ContentDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool CommentIsActive { get; set; }
        public string ImageUrl { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        #endregion

        #region OtherLastSecondTours
        public List<LastSecondTourViewModel> OtherLastSecondTours { get; set; }
        #endregion
    }
}
