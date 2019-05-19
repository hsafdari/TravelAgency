using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourDetailViewModel
    {

        #region Constructor
        public TourDetailViewModel()
        {
            packages = new HashSet<TourPackageClientViewModel>();
            itinerary = new HashSet<TourProgramClientViewModel>();
            Essentials = new List<string>();
        }
        #endregion

        //public string TripCode { get; set; }
        //public IEnumerable<TourType> TourTypes { get; set; }
        //public int Capacity { get; set; }
        //public int DurationDay { get; set; }
        //public decimal FromPrice { get; set; }
        //public string Currency { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// مدارک مورد نیاز
        /// </summary>
        public List<string> Essentials { get; set; }
        public string description { get; set; }
        public string TourLandingPageDescription { get; set; }
        public List<ImageViewModel> images { get; set; }
        public IEnumerable<TourPackageClientViewModel> packages { get; set; }
        public IEnumerable<TourProgramClientViewModel> itinerary { get; set; }

    }
    public class TourPackageClientViewModel
    {
        public int Id { get; set; }
        public string lowestPrice { get; set; }
        public string DateTitle { get; set; }
        public List<int[]> start { get; set; }
        public string end { get; set; }
        public IEnumerable<TourPacakgeFlightsViewModel> flights { get; set; }
        public IEnumerable<HotelPackageClientViewModel> hotels { get; set; }


    }
    public class TourPacakgeFlightsViewModel
    {
        public int comTransId { get; set; }
        public string name { get; set; }
        public string airline { get; set; }
        public string logo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string duration { get; set; }
        public string FlightDate { get; set; }
        public string FlightDateEn { get; set; }
        public string FlightNumber { get; set; }
        public EnumFlightDirectionType FlightDirection { get; set; }
        public int cityId { get; set; }
        public string BaggageAmount { get; set; }
        public string FLightClass { get; set; }
        public string VehicleTypeClassCode { get; set; }

        public string airlineEn { get; set; }

        public string FromCity { get; set; }

        public string FromCityEn { get; set; }
        public string FromAirport { get; set; }
        public string FromAirportEn { get; set; }

        public string ToCity { get; set; }

        public string ToCityEn { get; set; }
        public string ToAirport { get; set; }
        public string ToAirportEn { get; set; }

        public string FLightClassTitle { get; set; }

        public string FLightClassTitleEn { get; set; }

        public string FlightDateTitle { get; set; }

        public string airlineIcon { get; set; }

        public string FlightDateTimeEn { get; set; }
    }
    public class HotelPackageClientViewModel
    {
        public IEnumerable<HotelClientViewModel> hotelInPackage { get; set; }
        public IEnumerable<HotelPriceViewModel> price { get; set; }
    }
    public class HotelClientViewModel
    {
        public string location { get; set; }
        public string Address { get; set; }
        public string hotel { get; set; }
        public string url { get; set; }
        public string Tel { get; set; }
        public string Summary { get; set; }
        public string GoogleMapIFrame { get; set; }
        public string stars { get; set; }
        public string service { get; set; }
        public string ServiceTooltip { get; set; }
        public string description { get; set; }
        public bool IsSummary { get; set; }
        public string City { get; set; }
        public IEnumerable<ImageViewModel> images { get; set; }
        public IEnumerable<string> facilities { get; set; }
        public IEnumerable<HotelPriceViewModel> price { get; set; }

        /// فیلدهای img  facility  price اضافه شود

    }
    public class TourProgramClientViewModel
    {
        public string name { get; set; }
        public IEnumerable<ProgramSlideViewModel> slides { get; set; }
    }
    public class ProgramSlideViewModel
    {
        public string ActivityTitle { get; set; }
        public string image { get; set; }
        public string description { get; set; }

    }
    public class HotelPriceViewModel
    {
        public string description { get; set; }
        //public string price { get; set; }
        public string AdultPrice { get; set; }
        public string ChildPrice { get; set; }
        public string InfantPrice { get; set; }
        //public string otherCurrencyPrice { get; set; }
        //public string otherCurrencyTitle { get; set; }
        public string AdultOtherCurrencyPrice { get; set; }
        public string AdultOtherCurrencyTitle { get; set; }
        public string ChildOtherCurrencyPrice { get; set; }
        public string ChildOtherCurrencyTitle { get; set; }
        public string InfantOtherCurrencyPrice { get; set; }
        public string InfantOtherCurrencyTitle { get; set; }
    }
}
