using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel.Book.LocationTour;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourScheduleService : IBaseService<TourSchedule>
    {
        TourSchedule UpdateTourSchedule(TourScheduleViewModel editTourScheduleViewModel);
        List<TourPackageLocationViewModel> LocationTourPackage(List<int> departureAirports, List<int> arrivalAirportIds, string FlightDate);
        List<TourPackageLocationViewModel> LocationTourPackage(string GroupTitle);
        IQueryable<TourPackageLocationViewModel> LocationTourPackage(TourPackageFilterViewModel viewModel, int pageNumber, int PageSize);
            //1List<int> days, List<int> airlines, List<int> hotelrates, List<int> fromcities, List<int> tocities, int Pagefrom, int PageTo);
    }
}
