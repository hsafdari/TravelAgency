using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourAllViewModel
    {
        #region Constructor
        public TourAllViewModel()
        {
            TopSliders = new List<SlidersUIViewModel>();
            SliderGroups = new List<TourSliderGroupViewModel>();
            SliderCountries = new List<TourSliderCountriesUIViewModel>();
        }
        #endregion

        #region Properties
        public List<SlidersUIViewModel> TopSliders { get; set; }
        public List<TourSliderGroupViewModel> SliderGroups { get; set; }
        public List<TourSliderCountriesUIViewModel> SliderCountries { get; set; } 
        #endregion
    }
}
