﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourSliderGroupViewModel:BaseViewModelId
    {
        #region Constructor
        public TourSliderGroupViewModel()
        {
            Sliders = new List<SlidersUITourHomeViewModel>();
        }
        #endregion

        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }
        public List<SlidersUITourHomeViewModel> Sliders { get; set; } 
        #endregion
       
    }
}
