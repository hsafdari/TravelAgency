using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// صفحه اول وبسایت
    /// </summary>
    public class HomeViewModel
    {
        #region Properties
        public IEnumerable<SlidersUIViewModel> sliders { get; set; }

        //public IEnumerable<TourHomeViewModel> tours { get; set; }
        public IEnumerable<SlidersUIViewModel> sliderbellow{ get; set; }

        public ParvazPardaz.ViewModel.Core.AddNewsLetterViewModel AddNewsLetter { get; set; }
        public string BeforeFooterText { get; set; }
        public string BigPhotoUrl { get; set; }
        public List<CorporatesViewModel> CorporateLists { get; set; }
        public List<ParvazPardaz.ViewModel.SlidersUITourHomeViewModel> Slider1 { get; set; }
        public List<ParvazPardaz.ViewModel.SlidersUITourHomeViewModel> Slider2{ get; set; }
        public List<ParvazPardaz.ViewModel.SlidersUITourHomeViewModel> TopHomeSlider { get; set; }

        #endregion
    }
}
