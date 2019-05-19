using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel.Book.LocationTour;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourScheduleService : BaseService<TourSchedule>, ITourScheduleService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourSchedule> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourScheduleService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourSchedule>();
        }

        public List<TourPackageLocationViewModel> LocationTourPackage(List<int> departureAirports, List<int> arrivalAirportIds, string FlightDate)
        {
            var Traingitems = (from st in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                         join ct in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.CompanyTransfer>()
                         on st.CompanyTransferId equals ct.Id                        
                         join ts in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourSchedule>()
                         on st.TourScheduleId equals ts.Id
                         join p in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourPackage>()
                         on ts.TourPackageId equals p.Id
                         join t in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                         on p.TourId equals t.Id
                         join air in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                         on st.DestinationAirportId equals air.Id
                         join city in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                         on air.CityId equals city.Id
                         where departureAirports.Any(y => y.Equals(st.FromAirportId.Value)) && !p.IsDeleted
                            && arrivalAirportIds.Any(y => y.Equals(st.DestinationAirportId.Value))
                           && !t.IsDeleted && t.Recomended && !ts.IsDeleted && ts.FromDate >= DateTime.Now && st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go && st.VehicleTypeClass.VehicleTypeId==3
                         select new TourPackageLocationViewModel()
                         {
                             Id = p.Id,
                             TransferTypeTitle = st.VehicleTypeClass.VehicleType.Title,
                             CompanyTransferTitle = ct.Title,
                             CompanyTransferIata = ct.IataCode,
                             CompanyTransferId = ct.Id,
                             CompnayTransferLogo = ct.ImageUrl,
                             PackageStartPrice = p.FromPrice,
                             OfferPrice = p.OfferPrice,
                             Title = t.Title,
                             TourType=t.TourTypes.FirstOrDefault().Title,
                             PackageUrl = "/tour-" + city.State.Country.ENTitle + "-" + city.ENTitle + "?id=" + p.Id,
                             PackgeDayTitle = p.TourPackageDay.Title,
                             PackageDayId = p.TourPackgeDayId,
                             TransferDate = p.DateTitle,
                             Priority = p.Priority,
                             ImageURL = p.ImageURL,
                             FromDate = st.StartDateTime
                         }).Distinct().OrderBy(x => x.FromDate).Take(6).ToList();
            var items = (from st in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                         join ct in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.CompanyTransfer>()
                         on st.CompanyTransferId equals ct.Id
                         join ts in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourSchedule>()
                         on st.TourScheduleId equals ts.Id
                         join p in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourPackage>()
                         on ts.TourPackageId equals p.Id
                         join t in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                         on p.TourId equals t.Id
                         join air in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                         on st.DestinationAirportId equals air.Id
                         join city in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                         on air.CityId equals city.Id
                         where departureAirports.Any(y => y.Equals(st.FromAirportId.Value)) && !p.IsDeleted
                            && arrivalAirportIds.Any(y => y.Equals(st.DestinationAirportId.Value))
                           && !t.IsDeleted && t.Recomended && !ts.IsDeleted && ts.FromDate >= DateTime.Now && st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go
                           && st.VehicleTypeClass.VehicleTypeId != 3
                         select new TourPackageLocationViewModel()
                         {
                             Id = p.Id,
                             TransferTypeTitle = st.VehicleTypeClass.VehicleType.Title,
                             CompanyTransferTitle = ct.Title,
                             CompanyTransferIata = ct.IataCode,
                             CompanyTransferId = ct.Id,
                             CompnayTransferLogo = ct.ImageUrl,
                             PackageStartPrice = p.FromPrice,
                             OfferPrice=p.OfferPrice,
                             Title = t.Title,
                             TourType = t.TourTypes.FirstOrDefault().Title,
                             PackageUrl = "/tour-" + city.State.Country.ENTitle + "-" + city.ENTitle + "?id=" + p.Id,
                             PackgeDayTitle = p.TourPackageDay.Title,
                             PackageDayId = p.TourPackgeDayId,
                             TransferDate = p.DateTitle,
                             Priority = p.Priority,
                             ImageURL = p.ImageURL,
                             FromDate = st.StartDateTime
                         }).Distinct().OrderBy(x => x.FromDate).Take(6).ToList();          
            return Traingitems.Union<TourPackageLocationViewModel>(items).ToList();
        }

        public List<TourPackageLocationViewModel> LocationTourPackage(string GroupTitle)
        {
            var items = (from st in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                         join ct in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.CompanyTransfer>()
                         on st.CompanyTransferId equals ct.Id
                         join ts in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourSchedule>()
                         on st.TourScheduleId equals ts.Id
                         join p in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourPackage>()
                         on ts.TourPackageId equals p.Id
                         join t in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                         on p.TourId equals t.Id
                         join air in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                         on st.DestinationAirportId equals air.Id
                         join city in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                         on air.CityId equals city.Id
                         where st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go && t.PostGroups.Any(x => x.Title == GroupTitle)
                         && !p.IsDeleted && !t.IsDeleted && t.Recomended && !ts.IsDeleted && ts.FromDate >= DateTime.Now && st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go
                         select new TourPackageLocationViewModel()
                         {
                             Id = p.Id,
                             TransferTypeTitle = st.VehicleTypeClass.VehicleType.Title,
                             CompanyTransferTitle = ct.Title,
                             CompanyTransferIata = ct.IataCode,
                             CompanyTransferId = ct.Id,
                             CompnayTransferLogo = ct.ImageUrl,
                             PackageStartPrice = p.FromPrice,
                             OfferPrice = p.OfferPrice,
                             Title = t.Title,
                             TourType = t.TourTypes.FirstOrDefault().Title,
                             PackageUrl = "/tour-" + city.State.Country.ENTitle + "-" + city.ENTitle + "?id=" + p.Id,
                             PackgeDayTitle = p.TourPackageDay.Title,
                             PackageDayId = p.TourPackgeDayId,
                             TransferDate = p.DateTitle,
                             Priority = p.Priority,
                             ImageURL = p.ImageURL,
                             FromDate = st.StartDateTime
                         }).Distinct().OrderBy(x => x.TransferTypeTitle).OrderBy(x => x.FromDate).Take(6).ToList();
            return items;
        }

        public IQueryable<TourPackageLocationViewModel> LocationTourPackage(TourPackageFilterViewModel viewModel, int pageNumber, int PageSize)
        {
            if (viewModel.tocities.Count == 0)
            {
                viewModel.tocities.AddRange(viewModel.hdntocities);
            }
            bool TourPackgeDayIds = !viewModel.days.Any();
            bool TourairlinesIds = !viewModel.airlines.Any();
            bool TourhotelratesIds = !viewModel.hotelrates.Any();
            bool TourfromcitiesIds = !viewModel.fromcities.Any();
            bool TourtocitiesIds = !viewModel.tocities.Any();
            bool tourgroup = true;
            if (viewModel.FilterType== "group")
            {
                tourgroup = false;
                
            }
            if (TourairlinesIds==false)
            {
                IQueryable<TourPackageLocationViewModel> result1 = (from st in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                                                                   join ct in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.CompanyTransfer>()
                                                                   on st.CompanyTransferId equals ct.Id
                                                                   join ts in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourSchedule>()
                                                                   on st.TourScheduleId equals ts.Id
                                                                   join p in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourPackage>()
                                                                   on ts.TourPackageId equals p.Id
                                                                   join t in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                                                                   on p.TourId equals t.Id
                                                                   join fromair in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                                                                   on st.FromAirportId equals fromair.Id
                                                                   join fromcity in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                                                                   on fromair.CityId equals fromcity.Id
                                                                   join topair in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                                                                   on st.DestinationAirportId equals topair.Id
                                                                   join tocity in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                                                                   on topair.CityId equals tocity.Id
                                                                   join hp in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.HotelPackage>()
                                                                   on p.Id equals hp.TourPackageId
                                                                   join hph in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.HotelPackageHotel>()
                                                                   on hp.Id equals hph.HotelPackageId
                                                                   join h in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.Hotel>()
                                                                   on hph.HotelId equals h.Id
                                                                   where (!t.IsDeleted && !p.IsDeleted && t.Recomended && !ts.IsDeleted && ts.FromDate >= DateTime.Now)
                                                                   && st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go
                                                                   && (viewModel.days.Any(x => x.Equals(p.TourPackgeDayId.Value)) || TourPackgeDayIds)
                                                                   && (viewModel.airlines.Any(x => x.Equals(st.CompanyTransferId)) || TourairlinesIds)
                                                                   && (viewModel.hotelrates.Any(x => x.Equals(h.HotelRankId)) || TourhotelratesIds)
                                                                   && (viewModel.fromcities.Any(x => x.Equals(fromcity.Id)) || TourfromcitiesIds)
                                                                   && (viewModel.tocities.Any(x => x.Equals(tocity.Id)) || TourtocitiesIds)
                                                                   && (t.PostGroups.Any(x => x.Title == viewModel.enTitle || tourgroup))
                                                                   select new TourPackageLocationViewModel()
                                                                   {
                                                                       Id = p.Id,
                                                                       TransferTypeTitle = st.VehicleTypeClass.VehicleType.Title,
                                                                       CompanyTransferTitle = ct.Title,
                                                                       CompanyTransferIata = ct.IataCode,
                                                                       CompanyTransferId = ct.Id,
                                                                       CompnayTransferLogo = ct.ImageUrl,
                                                                       PackageStartPrice = p.FromPrice,
                                                                       OfferPrice = p.OfferPrice,
                                                                       Title = t.Title,
                                                                       TourType = t.TourTypes.FirstOrDefault().Title,
                                                                       PackageUrl = "/tour-" + tocity.State.Country.ENTitle + "-" + tocity.ENTitle + "?id=" + p.Id,
                                                                       PackgeDayTitle = p.TourPackageDay.Title,
                                                                       PackageDayId = p.TourPackgeDayId,
                                                                       TransferDate = p.DateTitle,
                                                                       Priority = p.Priority,
                                                                       ImageURL = p.ImageURL,
                                                                       FromDate = st.StartDateTime
                                                                   }).Distinct().OrderBy(x => x.TransferTypeTitle).OrderBy(x => x.FromDate).Skip(pageNumber * PageSize).Take(PageSize);

            }
            IQueryable<TourPackageLocationViewModel> result = (from st in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                                                               join ct in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.CompanyTransfer>()
                                                               on st.CompanyTransferId equals ct.Id
                                                               join ts in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourSchedule>()
                                                               on st.TourScheduleId equals ts.Id
                                                               join p in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.TourPackage>()
                                                               on ts.TourPackageId equals p.Id
                                                               join t in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                                                               on p.TourId equals t.Id
                                                               join fromair in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                                                               on st.FromAirportId equals fromair.Id
                                                               join fromcity in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                                                               on fromair.CityId equals fromcity.Id
                                                               join topair in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Airport>()
                                                               on st.DestinationAirportId equals topair.Id
                                                               join tocity in _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>()
                                                               on topair.CityId equals tocity.Id
                                                               join hp in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.HotelPackage>()
                                                               on p.Id equals hp.TourPackageId
                                                               join hph in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.HotelPackageHotel>()
                                                               on hp.Id equals hph.HotelPackageId
                                                               join h in _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.Hotel>()
                                                               on hph.HotelId equals h.Id
                                                               where (!t.IsDeleted && !p.IsDeleted && t.Recomended && !ts.IsDeleted && ts.FromDate >= DateTime.Now)
                                                               && st.FlightDirection == Model.Enum.EnumFlightDirectionType.Go
                                                               && (viewModel.days.Any(x => x.Equals(p.TourPackgeDayId.Value)) || TourPackgeDayIds)
                                                               && (viewModel.airlines.Any(x => x.Equals(st.CompanyTransferId)) || TourairlinesIds)
                                                               && (viewModel.hotelrates.Any(x => x.Equals(h.HotelRankId)) || TourhotelratesIds)
                                                               && (viewModel.fromcities.Any(x => x.Equals(fromcity.Id)) || TourfromcitiesIds)
                                                               && (viewModel.tocities.Any(x => x.Equals(tocity.Id)) || TourtocitiesIds)
                                                               && (t.PostGroups.Any(x => x.Title == viewModel.enTitle || tourgroup))
                                                               select new TourPackageLocationViewModel()
                                                               {
                                                                   Id = p.Id,
                                                                   TransferTypeTitle = st.VehicleTypeClass.VehicleType.Title,
                                                                   CompanyTransferTitle = ct.Title,
                                                                   CompanyTransferIata = ct.IataCode,
                                                                   CompanyTransferId = ct.Id,
                                                                   CompnayTransferLogo = ct.ImageUrl,
                                                                   PackageStartPrice = p.FromPrice,
                                                                   OfferPrice = p.OfferPrice,
                                                                   Title = t.Title,
                                                                   TourType = t.TourTypes.FirstOrDefault().Title,
                                                                   PackageUrl = "/tour-" + tocity.State.Country.ENTitle + "-" + tocity.ENTitle + "?id=" + p.Id,
                                                                   PackgeDayTitle = p.TourPackageDay.Title,
                                                                   PackageDayId = p.TourPackgeDayId,
                                                                   TransferDate = p.DateTitle,
                                                                   Priority = p.Priority,
                                                                   ImageURL = p.ImageURL,
                                                                   FromDate = st.StartDateTime
                                                               }).Distinct().OrderBy(x => x.TransferTypeTitle).OrderBy(x => x.FromDate).Skip(pageNumber * PageSize).Take(PageSize);

            return result;
        }
        #endregion

        #region UpdateTourSchedule
        public TourSchedule UpdateTourSchedule(TourScheduleViewModel editTourScheduleViewModel)
        {
            var tourSchedule = _unitOfWork.Set<TourSchedule>().FirstOrDefault(x => x.Id == editTourScheduleViewModel.Id);

            // به روز رسانی اطلاعات پایه
            tourSchedule.Capacity = editTourScheduleViewModel.Capacity;
            tourSchedule.ExpireDate = editTourScheduleViewModel.ExpireDate;
            tourSchedule.FromDate = editTourScheduleViewModel.FromDate;
            tourSchedule.ToDate = editTourScheduleViewModel.ToDate;

            // حذف مقاصد قبلی
            var previousDestination = tourSchedule.TourScheduleCompanyTransfers.ToList();
            foreach (var item in previousDestination)
            {
                _unitOfWork.Set<TourScheduleCompanyTransfer>().Remove(item);
            }

            //افزودن مقاصد جدید
            foreach (var item in editTourScheduleViewModel.CompanyTransfer.ToList())
            {
                var newCoTr = new TourScheduleCompanyTransfer();
                newCoTr = _mappingEngine.Map(item, newCoTr);
                newCoTr.CompanyTransferId = item.CompanyTransferId;
                newCoTr.TourScheduleId = tourSchedule.Id;
                _unitOfWork.Set<TourScheduleCompanyTransfer>().Add(newCoTr);
            }
            _unitOfWork.SaveAllChanges(false);
            return tourSchedule;
        }
        #endregion

    }
}
