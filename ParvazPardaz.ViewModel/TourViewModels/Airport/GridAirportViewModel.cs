using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridAirportViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        [Display(Name = "IataCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string IataCode { get; set; }
        [Display(Name = "TitleEn", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string TitleEn { get; set; }
        #endregion

        #region Reference Navigation Properties
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CityTitle { get; set; }
        #endregion
    }
}
