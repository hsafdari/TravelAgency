using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.ViewModel.Book.LocationTour
{
    public class TourLocationViewModel
    {
        /// <summary>
        /// موارد جستجو شده
        /// </summary>
        public TourSearchViewModel TourSearchParams { get; set; }
        public List<GridTourPackageDayViewModel> ToursDays { get; set; }
        public List<GridCompanyTransferViewModel> CompayTransfers { get; set; }
        public List<GridHotelRankViewModel> HotelRates { get; set; }
        public List<GridCityViewModel> FromCities { get; set; }
        public List<GridCityViewModel> ToCities { get; set; }
        public List<TourPackageLocationViewModel> TourPackages { get; set; }
        public List<TourSuggestionViewModel>TourSuggestions { get; set; }
        public string SeoText { get; set; }
        public List<TourPackageRankViewModel> TourPackageRanks{ get; set; }
        public string Title { get; set; }
        public EnumTourPackageFilterType FilterType { get; set; }
        public string ImageURL { get; set; }
        public string UrlName { get; set; }
    }
}
