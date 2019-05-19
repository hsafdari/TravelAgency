using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridSelectedHotelPackageViewModel : BaseViewModelOfEntity
    {
        public GridSelectedHotelPackageViewModel()
        {
            this.hotelRoomsInPackage = new List<HotelRoomsInPackageViewModel>();
            this.hotelsInPackage = new List<HotelsInPackageViewModel>();
            this.SearchViewModel = new HotelSearchParamViewModel();
        }
        public HotelSearchParamViewModel SearchViewModel { get; set; }
        #region Properties
        public int TourPackageId { get; set; }
        [Display(Name = "TourPackageTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TourPackageTitle { get; set; }
        [Display(Name = "OrderId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public long OrderId { get; set; }
        [Display(Name = "TourId", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int TourId { get; set; }
        [Display(Name = "TourTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourTitle { get; set; }

        public string SectionId { get; set; }
        [Display(Name = "TourCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourCode { get; set; }
        [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackageCode { get; set; }
        [Display(Name = "TourPackage", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TourPackage { get; set; }
        [Display(Name = "TotalPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TotalPrice { get; set; }
        [Display(Name = "TrackingCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TrackingCode { get; set; }
        [Display(Name = "TotalDiscountPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalDiscountPrice { get; set; }
        [Display(Name = "CurrentTaxPercentage", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int CurrentTaxPercentage { get; set; }
        [Display(Name = "TotalTaxPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalTaxPrice { get; set; }
        [Display(Name = "TotalPayPrice", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public decimal TotalPayPrice { get; set; }
        [Display(Name = "FlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime FlightDateTime { get; set; }
        [Display(Name = "ReturnFlightDateTime", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public DateTime? ReturnFlightDateTime { get; set; }
        [Display(Name = "Buyer", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string BuyerTitle { get; set; }
        [Display(Name = "NationalCode", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string NationalCode { get; set; }
        [Display(Name = "NumberRoom", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public int? NumberRoom { get; set; }
        /// <summary>
        /// شناسه پکیج هتل انتخاب شده
        /// </summary>
        public int SelectedHotelPackageId { get; set; }

        public List<HotelRoomsInPackageViewModel> hotelRoomsInPackage { get; set; }
        public List<HotelsInPackageViewModel> hotelsInPackage { get; set; }
        #endregion


    }
}
