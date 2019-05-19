using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class TourSearchViewModel
    {
        #region Constructor
        public TourSearchViewModel()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// شهر مبدا
        /// </summary>
        public int DepartureCityId { get; set; }
        
        /// <summary>
        /// شهر مقصد
        /// </summary>
        public int ArrivalCityId { get; set; }

        /// <summary>
        /// دراپ داون شهرها
        /// </summary>
        public IEnumerable<SelectListItem> CitiesFromDDL { get; set; }
        public IEnumerable<SelectListItem> CitiesDestinationDDL { get; set; }
        public IEnumerable<SelectListItem> CitiesAllDDL { get; set; }

        /// <summary>
        /// تاریخ پرواز
        /// </summary>
        public string FlightDate { get; set; }
        //public DateTime FlightDate { get; set; }

        public string Calendertype { get; set; }

        /// <summary>
        /// مدت اقامت
        /// </summary>
        public Nullable<int> DurationTime { get; set; }

        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        public Nullable<int> AdultCount { get; set; }
        
        /// <summary>
        /// تعداد کودک
        /// </summary>
        public Nullable<int> ChildCount { get; set; }
        
        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        public Nullable<int> InfantCount { get; set; }
        #endregion
    }
}
