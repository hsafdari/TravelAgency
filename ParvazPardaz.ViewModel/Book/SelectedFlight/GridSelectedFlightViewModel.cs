using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.ViewModel
{
    public class GridSelectedFlightViewModel : BaseViewModelOfEntity
    {
        public GridSelectedFlightViewModel()
        {
            this.SearchViewModel = new FlightSearchParamViewModel();
        }
        public FlightSearchParamViewModel SearchViewModel { get; set; }
        #region Properties
        [Display(Name = "AirlineIATACode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string AirlineIATACode { get; set; }

        [Display(Name = "FlightNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string FlightNo { get; set; }

        [Display(Name = "FlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime FlightDateTime { get; set; }
        [Display(Name = "ReturnFlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime? ReturnFlightDateTime { get; set; }

        [Display(Name = "FromIATACode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string FromIATACode { get; set; }

        [Display(Name = "ToIATACode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string ToIATACode { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        [Display(Name = "FlightType", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public FlightType FlightType { get; set; }
        public string FlightTypeDisplay
        {
            get
            {
                return FlightType.GetDisplayValue();
            }
        }

        [Display(Name = "TourSchedule", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int TourScheduleId { get; set; }

        [Display(Name = "Order", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }
        ///<summary>
        ///کد پیگیری
        ///</summary>
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TrackingCode { get; set; }
        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        [Display(Name = "AdultCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کودک
        /// </summary>
        [Display(Name = "ChildCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        [Display(Name = "InfantCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int InfantCount { get; set; }
        /// <summary>
        /// کد تعریف شده تور
        /// </summary>
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourCode { get; set; }
        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }

        /// <summary>
        /// کد پکیج
        /// </summary>
        [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackageCode { get; set; }

        /// <summary>
        /// عنوان پکیج
        /// </summary>
        [Display(Name = "TourPackage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackage { get; set; }
        [Display(Name = "Buyer", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BuyerTitle { get; set; }
        [Display(Name = "NationalCodePassportNo", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string NationalCode { get; set; }
        #endregion
    }
}
