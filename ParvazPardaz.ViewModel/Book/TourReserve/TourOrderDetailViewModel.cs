using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TourOrderDetailViewModel
    {
        #region Properties        
        /// <summary>
        /// کلیه پروازهای ورودی و خروجی
        /// </summary>
        public List<FlightViewModel> Flights { get; set; }
        /// <summary>
        /// پکیج های هتل
        /// </summary>
        public List<ListViewHotelPackageViewModel> HotelPackages { get; set; }

        /// <summary>
        /// شناسه پکیج هتل انتخاب شده
        /// </summary>
        public int SelectedHotelPackageId { get; set; }

        /// <summary>
        /// شناسه هتل انتخاب شده در پکیج انتخاب شده
        /// </summary>
        public int SelectedHotelInThisPackageId { get; set; }      
        public int OrderId { get; set; }
        public int TourId { get; set; }
        public string SectionId { get; set; }
        
        public List<HotelRoomsInPackageViewModel> hotelRoomsInPackage { get; set; }
        public List<HotelsInPackageViewModel> hotelsInPackage { get; set; }
        #endregion
    }
}
