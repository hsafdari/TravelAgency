using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ListViewHotelPackageViewModel : BaseViewModelId
    {
        public ListViewHotelPackageViewModel()
        {
            hotelRoomsInPackage = new List<HotelRoomsInPackageViewModel>();
            hotelsInPackage = new List<HotelsInPackageViewModel>();
        }
       
        public int TourPackageId { get; set; }
        public string TourPackageTitle { get; set; }
        public int OrderId { get; set; }
        public int TourId { get; set; }
        public string TourTitle { get; set; }
        public string SectionId { get; set; }

        public List<HotelRoomsInPackageViewModel> hotelRoomsInPackage  { get; set; }
        public List<HotelsInPackageViewModel> hotelsInPackage { get; set; }

    }
}
