using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourSliderCountriesUIViewModel
    {
        #region پراپرتی های کشور / شهر
        /// <summary>
        /// countryTitle in Persian
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// country name in English
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// for background
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// set a color for background
        /// </summary>
        public string BackgroundColor { get; set; } 
        #endregion

        #region کجا می روید؟
        public List<TourCitiesUIViewModel> TourCities { get; set; } 
        #endregion
        
        #region چند روز؟
        public List<TourDaysViewModel> TourDays { get; set; } 
        #endregion

        #region اسلایدر تورها
        public List<SlidersUITourHomeViewModel> TourSliders { get; set; } 
        #endregion
    }

    #region TourCitiesUIViewModel
    public class TourCitiesUIViewModel
    {
        /// <summary>
        /// Title of City in Persian
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Name Of City In En
        /// </summary>
        public string Name { get; set; }
        //public List<TourDaysViewModel> TourDays { get; set; }
        public int Id { get; set; }
    } 
    #endregion

    #region TourDaysViewModel
    public class TourDaysViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        //public List<SlidersUITourHomeViewModel> TourSliders { get; set; }
    } 
    #endregion
}
