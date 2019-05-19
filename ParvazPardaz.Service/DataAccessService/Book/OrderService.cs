using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Entity.Hotel;
using System.Data;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.Contract.Link;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.ViewModel.TourViewModels.Tour.TourClient;
using ParvazPardaz.Common.Utility;
using ParvazPardaz.Model;
using System.Configuration;
using System.Web.Mvc;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Common.Extension;
using System.Globalization;
using ParvazPardaz.Service.Contract;
using ParvazPardaz.Model.Book;
using ParvazPardaz.TravelAgency.UI.Services.Interface.Book;

namespace ParvazPardaz.Service.DataAccessService.Book
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Order> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly ITourService _tourService;
        private readonly ILinkService _linkService;
        private readonly IAirportService _airportService;
        private readonly ICityService _cityService;
        private readonly ICouponService _couponService;
        private readonly ICreditService _creditService;
        private readonly IPaymentLogService _paymentLogService;
        private readonly ICompanyTransferService _companyTransferService;
        private readonly ICountryService _countryService;
        #endregion

        #region Ctor
        public OrderService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, ITourService tourService, ILinkService linkService, IAirportService airportService, ICityService cityService, ICompanyTransferService companyTransferService, ICouponService couponService, IPaymentLogService paymentLogService, ICreditService creditService, ICountryService countryService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Order>();
            _tourService = tourService;
            _linkService = linkService;
            _airportService = airportService;
            _cityService = cityService;
            _couponService = couponService;
            _companyTransferService = companyTransferService;
            _paymentLogService = paymentLogService;
            _creditService = creditService;
            _countryService = countryService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType, DateTime? fromdate, DateTime? todate, string reporttype = "")
        {
            var query = _dbSet.Where(w => !w.IsDeleted && (w.ProfileType == ProfileType || w.ProfileType == null));
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && fromdate != null && todate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(fromdate.Value) <= DbFunctions.TruncateTime(w.ReturnFlightDateTime)) && (DbFunctions.TruncateTime(w.ReturnFlightDateTime) <= (DbFunctions.TruncateTime(todate.Value)))));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridOrderViewModel>(_mappingEngine);
        }

        public IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType, string fromdate = "", string todate = "", string calendertype = "persian", string reporttype = "")
        {
            DateTime? _fromDate = DateTime.Now;
            DateTime? _todateDate = DateTime.Now;
            if (fromdate != "" && todate != "")
            {
                if (calendertype == "persian")
                {
                    string Date = fromdate.ToPersianNumber();
                    _fromDate = System.Convert.ToDateTime(Date);
                    Date = todate.ToPersianNumber();
                    _todateDate = System.Convert.ToDateTime(Date);
                }
                else if (calendertype == "gregorian")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    _fromDate = System.Convert.ToDateTime(fromdate);
                    _todateDate = System.Convert.ToDateTime(todate);
                }
            }

            var query = _dbSet.Where(w => !w.IsDeleted || (w.ProfileType == ProfileType && w.ProfileType == null));
            if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.sales && _fromDate != null && _todateDate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.CreatorDateTime.Value)) && (DbFunctions.TruncateTime(w.CreatorDateTime.Value) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            else if (!string.IsNullOrEmpty(reporttype) && reporttype == ParvazPardaz.Resource.Book.Book.reserve && _fromDate != null && _todateDate != null)
            {
                query = query.Where(w => ((DbFunctions.TruncateTime(_fromDate.Value) <= DbFunctions.TruncateTime(w.ReturnFlightDateTime)) && (DbFunctions.TruncateTime(w.ReturnFlightDateTime) <= (DbFunctions.TruncateTime(_todateDate.Value)))));
            }
            return query.Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ProjectTo<GridOrderViewModel>(_mappingEngine);
        }

        public IQueryable<GridOrderViewModel> GetViewModelForGrid(UserProfileType? ProfileType)
        {
            return _dbSet.Where(w => !w.IsDeleted && (w.ProfileType == ProfileType || w.ProfileType == null) && w.ProfileType == ProfileType).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                            .AsNoTracking().ProjectTo<GridOrderViewModel>(_mappingEngine);
        }
        public IQueryable<GridOrderViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                            .AsNoTracking().ProjectTo<GridOrderViewModel>(_mappingEngine);
        }

        public IQueryable<GridOrderViewModel> GetViewModelForGridByUserId(int creatorUserId)
        {
            return _dbSet.Where(w => !w.IsDeleted && w.CreatorUserId == creatorUserId).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                            .AsNoTracking().ProjectTo<GridOrderViewModel>(_mappingEngine);
        }
        #endregion

        #region 1. PreReserve : After search
        public TourReserveViewModel PreReserve(TourReserveViewModel viewmodel)
        {
            #region واکشی اطلاعات مرتبط با ویوومدل ورودی
            //پروازهای رفت و برگشت انتخاب شده
            var selectedDepartureFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.VehicleTypeClass).FirstOrDefault(x => x.Id == viewmodel.SelectedDepartureFlightId);
            var selectedArrivalFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.VehicleTypeClass).FirstOrDefault(x => x.Id == viewmodel.SelectedArrivalFlightId);

            //پکیج هتل انتخاب شده
            var selectedHotelPackage = _unitOfWork.Set<HotelPackage>().Include(x => x.HotelPackageHotels).FirstOrDefault(x => x.Id == viewmodel.SelectedHotelPackageId && !x.IsDeleted);

            //first OrderType
            var firstOrderType = _unitOfWork.Set<OrderType>().FirstOrDefault();

            //first AgencySetting
            var firstAgencySetting = _unitOfWork.Set<AgencySetting>().FirstOrDefault();
            #endregion

            #region واکشی اطلاعات هتل های موجود با این پکیج هتل انتخاب شده
            //اطلاعات هتل های موجود با این پکیج هتل انتخاب شده
            viewmodel.HotelInfos = selectedHotelPackage.HotelPackageHotels.Select(s => new HotelClientViewModel
            {
                hotel = s.Hotel.Title,
                description = s.Hotel.Summary,
                location = s.Hotel.City.Title,
                stars = s.Hotel.HotelRank.Title,
                service = s.HotelBoard != null ? s.HotelBoard.Name : "",
                ServiceTooltip = s.HotelBoard != null ? s.HotelBoard.Title : "",
                url = JoinGetHotelUrl(s.Hotel.Id),
                images = s.Hotel.HotelGalleries.Where(g => g.IsDeleted == false).Select(i => new ImageViewModel { ImageUrl = i.ImageUrl + i.ImageExtension }),
                facilities = s.Hotel.HotelFacilities.Where(hf => hf.IsDeleted == false).Select(f => f.Title),
                IsSummary = s.Hotel.IsSummary

            }).ToList();
            #endregion

            #region واکشی اطلاعات انواع اتاق موجود در این پکیج هتل انتخاب شده
            viewmodel.HotelRoomInfos = selectedHotelPackage.HotelPackageHotelRooms.Select(x => new HotelPackageHotelRoomViewModel()
            {
                //HotelPackageHotelRoom Properties
                //RialPrice = x.Price,
                AdultRialPrice = x.AdultPrice,
                ChildRialPrice = x.ChildPrice,
                InfantRialPrice = x.InfantPrice,
                BaseRialPrice = (x.Currency != null ? x.Currency.BaseRialPrice : 0),
                //OtherCurrencyPrice = x.OtherCurrencyPrice ?? 0,
                AdultOtherCurrencyPrice = x.AdultOtherCurrencyPrice ?? 0,
                ChildOtherCurrencyPrice = x.ChildOtherCurrencyPrice ?? 0,
                InfantOtherCurrencyPrice = x.InfantOtherCurrencyPrice ?? 0,
                OtherCurrencyTitle = (x.Currency != null ? x.Currency.Title : ""),
                //Capacity = x.Capacity,
                AdultCapacity = x.AdultCapacity,
                ChildCapacity = x.ChildCapacity,
                InfantCapacity = x.InfantCapacity,
                //CapacitySold = x.CapacitySold,
                AdultCapacitySold = x.AdultCapacitySold,
                ChildCapacitySold = x.ChildCapacitySold,
                InfantCapacitySold = x.InfantCapacitySold,
                //HotelRoom properties
                HotelRoomId = x.HotelRoomId,
                Title = x.HotelRoom.Title,
                Priority = x.HotelRoom.Priority,
                HasAdult = x.HotelRoom.HasAdult,
                HasChild = x.HotelRoom.HasChild,
                HasInfant = x.HotelRoom.HasInfant,
                AdultMaxCapacity = x.HotelRoom.AdultMaxCapacity,
                AdultMinCapacity = x.HotelRoom.AdultMinCapacity,
                ChildMaxCapacity = x.HotelRoom.ChildMaxCapacity,
                ChildMinCapacity = x.HotelRoom.ChildMinCapacity,
                InfantMaxCapacity = x.HotelRoom.InfantMaxCapacity,
                InfantMinCapacity = x.HotelRoom.InfantMinCapacity,
                //Count properties
                //ReservedCapacity = 0,
                //AvailableCapacity = (x.Capacity - x.CapacitySold)
                AdultAvailableCapacity = (x.AdultCapacity - x.AdultCapacitySold),
                ChildAvailableCapacity = (x.ChildCapacity - x.ChildCapacitySold),
                InfantAvailableCapacity = (x.InfantCapacity - x.InfantCapacitySold)

            }).ToList();

            #endregion

            #region واکشی اطلاعات پرواز رفت و پرواز برگشت
            //اطلاعات پرواز رفت
            viewmodel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
            {
                name = GetCityTitleOfAirport(selectedDepartureFlight.FromAirportId.Value) + "-" + GetCityTitleOfAirport(selectedDepartureFlight.DestinationAirportId.Value),
                airline = _companyTransferService.GetById(x => x.Id == selectedDepartureFlight.CompanyTransferId).Title,
                logo = _companyTransferService.GetById(x => x.Id == selectedDepartureFlight.CompanyTransferId).ImageUrl,
                from = GetCityTitleOfAirport(selectedDepartureFlight.FromAirportId.Value) + " " + selectedDepartureFlight.StartDateTime.TimeOfDay.ToHHMM(),
                to = GetCityTitleOfAirport(selectedDepartureFlight.DestinationAirportId.Value) + " " + selectedDepartureFlight.EndDateTime.TimeOfDay.ToHHMM(),
                duration = (selectedDepartureFlight.EndDateTime - selectedDepartureFlight.StartDateTime).Hours.ToString(),
                FlightDate = selectedDepartureFlight.StartDateTime.ToString("dddd dd MMMM HH:ss"),
                BaggageAmount = selectedDepartureFlight.BaggageAmount != null ? selectedDepartureFlight.BaggageAmount : "",
                FlightNumber = selectedDepartureFlight.FlightNumber,
                FlightDirection = selectedDepartureFlight.FlightDirection,
                //FLightClass = selectedArrivalFlight.FlightClass
                FLightClass = (selectedDepartureFlight.VehicleTypeClass != null ? selectedDepartureFlight.VehicleTypeClass.Title : "")
            });

            //اطلاعات پرواز برگشت
            viewmodel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
            {
                name = GetCityTitleOfAirport(selectedArrivalFlight.FromAirportId.Value) + "-" + GetCityTitleOfAirport(selectedArrivalFlight.DestinationAirportId.Value),
                airline = _companyTransferService.GetById(x => x.Id == selectedArrivalFlight.CompanyTransferId).Title,
                logo = _companyTransferService.GetById(x => x.Id == selectedArrivalFlight.CompanyTransferId).ImageUrl,
                from = GetCityTitleOfAirport(selectedArrivalFlight.FromAirportId.Value) + " " + selectedArrivalFlight.StartDateTime.TimeOfDay.ToHHMM(),
                to = GetCityTitleOfAirport(selectedArrivalFlight.DestinationAirportId.Value) + " " + selectedArrivalFlight.EndDateTime.TimeOfDay.ToHHMM(),
                duration = (selectedArrivalFlight.EndDateTime - selectedArrivalFlight.StartDateTime).Hours.ToString(),
                FlightDate = selectedArrivalFlight.StartDateTime.ToString("dddd dd MMMM HH:ss"),
                BaggageAmount = selectedArrivalFlight.BaggageAmount != null ? selectedArrivalFlight.BaggageAmount : "",
                FlightNumber = selectedArrivalFlight.FlightNumber,
                FlightDirection = selectedArrivalFlight.FlightDirection,
                //FLightClass = selectedArrivalFlight.FlightClass
                FLightClass = (selectedArrivalFlight.VehicleTypeClass != null ? selectedArrivalFlight.VehicleTypeClass.Title : "")
            });
            #endregion

            #region تور خارجی؟
            var tour = selectedHotelPackage.TourPackage.Tour;
            if (tour != null && tour.TourLandingPageUrlId != null && tour.TourLandingPageUrlId != 0)
            {
                var tourUrl = _unitOfWork.Set<TourLandingPageUrl>().Include(x => x.City).FirstOrDefault(x => x.Id == tour.TourLandingPageUrlId.Value);
                viewmodel.IsForeignTour = !tourUrl.City.State.Country.Title.Contains("ایران");
            }
            else
            {
                viewmodel.IsForeignTour = false;
            }
            #endregion

            return viewmodel;
        }
        #endregion

        #region 2. Reserve : Before bank payment
        /// <summary>
        /// رزرو سفارش قبل رفتن به درگاه بانکی
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <returns></returns>
        public Order Reserve(TourReserveViewModel viewmodel, out bool isAllowedCreditPay)
        {
            //Orders ==> SelectedFlights,
            //       ==> SelectedHotelPackages ==> SelectedHotels, 
            //                                 ==> SelectedRooms  ==> Passengers
            //       ==> OrderInformations

            #region واکشی اطلاعات لازم
            //پروازهای رفت و برگشت انتخاب شده
            var selectedDepartureFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == viewmodel.SelectedDepartureFlightId);
            var selectedArrivalFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == viewmodel.SelectedArrivalFlightId);

            //پکیج هتل انتخاب شده
            var selectedHotelPackage = _unitOfWork.Set<HotelPackage>().FirstOrDefault(x => x.Id == viewmodel.SelectedHotelPackageId);

            //first OrderType
            var firstOrderType = _unitOfWork.Set<OrderType>().FirstOrDefault();

            //first AgencySetting
            var firstAgencySetting = _unitOfWork.Set<AgencySetting>().FirstOrDefault();

            //واکشی مهلت زمانی پرداخت از فایل وب.کانفیگ
            var payDeadLineMinutes = System.Convert.ToInt32(ConfigurationManager.AppSettings["PayDeadLineMinutes"]);

            //واکشی درصد مالیات بر ارزش افزوده
            var taxPercentage = System.Convert.ToInt32(ConfigurationManager.AppSettings["TaxPercentage"]);

            //کاربری که لاگین است
            var loggedInUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.Id == viewmodel.LoggedInUserId);
            #endregion

            #region سفارش جدید
            var newOrder = new Order()
                {
                    TrackingCode = GenerateRandomKey.CallRandom(),
                    TotalPrice = 0, //پایین تر محاسبه می کنیم
                    TotalDiscountPrice = 0, //پایین تر محاسبه می کنیم
                    TotalTaxPrice = 0, //پایین تر محاسبه می کنیم
                    TotalPayPrice = 0, //پایین تر محاسبه می کنیم
                    OrderStep = Model.Enum.EnumOrderStep.ProvisionalBooking,
                    FlightDateTime = selectedDepartureFlight.StartDateTime,
                    ReturnFlightDateTime = selectedArrivalFlight.StartDateTime,
                    TicketReferenceNo = "",
                    AdultCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Adult).Count() : 0),
                    ChildCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Child).Count() : 0),
                    InfantCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Infant).Count() : 0),
                    // آیا این فیلد ظرفیت پرواز هر بار به روز می شه یا اینکه جور دیگه ای باید حساب کنیم؟
                    RemainingFlightCapacity = selectedDepartureFlight.Capacity != null ? selectedDepartureFlight.Capacity.Value : 0,
                    IsSuccessful = false,
                    OrderTypeId = firstOrderType.Id,
                    AgencySettingId = firstAgencySetting.Id,
                    CreatorDateTime = DateTime.Now,
                    PayExpiredDateTime = (DateTime.Now).AddMinutes(payDeadLineMinutes)
                };
            #endregion

            #region در نظر گرفتن پرواز رفت و برگشتی در سفارش
            var airports = _unitOfWork.Set<Airport>();
            newOrder.SelectedFlights = new HashSet<SelectedFlight>();
            newOrder.SelectedFlights.Add(new SelectedFlight()
                {
                    CompanyTransferId = selectedDepartureFlight.CompanyTransfer.Id,
                    AirlineIATACode = selectedDepartureFlight.CompanyTransfer.Title,
                    FlightDateTime = selectedDepartureFlight.StartDateTime,
                    FlightNo = selectedDepartureFlight.FlightNumber,
                    FlightType = Model.Enum.FlightType.Charter,
                    FlightDirection = selectedDepartureFlight.FlightDirection,
                    FromIATACode = selectedDepartureFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.FromAirportId).IataCode : "",
                    ToIATACode = selectedDepartureFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.DestinationAirportId).IataCode : "",
                    TourScheduleId = selectedDepartureFlight.TourScheduleId,
                    VehicleTypeClassId = selectedArrivalFlight.VehicleTypeClassId,
                    BaggageAmount = selectedArrivalFlight.BaggageAmount
                });

            newOrder.SelectedFlights.Add(new SelectedFlight()
            {
                CompanyTransferId = selectedArrivalFlight.CompanyTransfer.Id,
                AirlineIATACode = selectedArrivalFlight.CompanyTransfer.Title,
                FlightDateTime = selectedArrivalFlight.StartDateTime,
                FlightNo = selectedArrivalFlight.FlightNumber,
                FlightType = Model.Enum.FlightType.Charter,
                FlightDirection = selectedArrivalFlight.FlightDirection,
                FromIATACode = selectedArrivalFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).IataCode : "",
                ToIATACode = selectedArrivalFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.DestinationAirportId).IataCode : "",
                TourScheduleId = selectedArrivalFlight.TourScheduleId,
                VehicleTypeClassId = selectedArrivalFlight.VehicleTypeClassId,
                BaggageAmount = selectedArrivalFlight.BaggageAmount
            });
            #endregion

            #region در نظر گرفتن پکیج هتل انتخاب شده + هتل های آن + انواع تخت انتخاب شده ؛ در سفارش
            //در نظر گرفتن پکیج هتل انتخاب شده
            var newSelectedHotelPackage = new SelectedHotelPackage()
                {
                    //SelectedRoomCount = viewmodel.HotelRoomInfos.Where(x => x.ReservedCapacity > 0).Count(),
                    SelectedRoomCount = viewmodel.RoomSections.Count(),
                    AdultCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Adult).Count() : 0),
                    ChildCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Child).Count() : 0),
                    InfantCount = (viewmodel.PassengerList.Any() ? viewmodel.PassengerList.Where(x => x.AgeRange == AgeRange.Infant).Count() : 0),
                    //TotalServicePrice = 0, //مجموع قیمت خدمات اگر انتخاب شده باشد
                    TotalAdultPrice = 0,
                    TotalChildPrice = 0,
                    TotalInfantPrice = 0,
                    HotelPackageId = selectedHotelPackage.Id,
                };

            //در نظر گرفتن هتل های این پکیج هتل انتخاب شده 
            #region SelectedHotels
            newSelectedHotelPackage.SelectedHotels = new HashSet<SelectedHotel>();
            var checkInDateTimeString = selectedDepartureFlight.StartDateTime.ToString("yyyy/MM/dd 14:00 ب.ظ");
            var checkInDateTime = DateTime.Parse(checkInDateTimeString);
            var checkOutDateTimeString = selectedArrivalFlight.StartDateTime.ToString("yyyy/MM/dd 12:00 ق.ظ");
            var checkOutDateTime = DateTime.Parse(checkOutDateTimeString);
            newSelectedHotelPackage.SelectedHotels = selectedHotelPackage.HotelPackageHotels.Select(x => new SelectedHotel()
            {
                CheckInDateTime = checkInDateTime,
                CheckOutDateTime = checkOutDateTime,
                HotelDescription = (x.Hotel.Description != null ? x.Hotel.Description : ""),
                HotelId = x.Hotel.Id,
                IsDeleted = false,
                SelectedHotelPackageId = x.HotelPackageId

            }).ToList();
            #endregion

            //در نظر گرفتن اتاق ها 
            #region SelectedRooms
            newSelectedHotelPackage.SelectedRooms = new HashSet<SelectedRoom>();
            newSelectedHotelPackage.SelectedRooms = selectedHotelPackage.HotelPackageHotelRooms.Join(viewmodel.RoomSections, jhphr => jhphr.HotelRoomId, jvmhr => jvmhr.SelectedRoomTypeId, (jhphr, jvmhr) => new { jhphr, jvmhr }).Select(x => new SelectedRoom()
            {
                //نیاز به بازبینی دارد صفدری
                //CurrentCapacity = (x.jhphr.AdultCapacity + x.jhphr.ChildCapacity + x.jhphr.InfantCapacity),
                CurrentAdultCapacity = x.jhphr.AdultCapacity,
                CurrentChildCapacity = x.jhphr.ChildCapacity,
                CurrentInfantCapacity = x.jhphr.InfantCapacity,
                RialPrice = (x.jvmhr.AdultCount * x.jhphr.AdultPrice) + (x.jvmhr.ChildCount * x.jhphr.ChildPrice) + (x.jvmhr.InfantCount * x.jhphr.InfantPrice),
                //CurrencyPrice = (x.jhphr.OtherCurrencyPrice ?? 0),
                AdultCurrencyPrice = (x.jhphr.AdultOtherCurrencyPrice ?? 0),
                ChildCurrencyPrice = (x.jhphr.ChildOtherCurrencyPrice ?? 0),
                InfantCurrencyPrice = (x.jhphr.InfantOtherCurrencyPrice ?? 0),
                BaseCurrencyToRialPrice = (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0),
                CurrencySign = (x.jhphr.Currency != null ? x.jhphr.Currency.CurrenySign : ""),
                //UnitPrice = (((x.jhphr.OtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.Price),
                AdultUnitPrice = (x.jvmhr.AdultCount * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice)),
                ChildUnitPrice = (x.jvmhr.ChildCount * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice)),
                InfantUnitPrice = (x.jvmhr.InfantCount * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice)),
                IsDeleted = false,
                HotelRoomId = x.jhphr.HotelRoomId,
                SelectedHotelPackageId = x.jhphr.HotelPackageId,
                VAT = taxPercentage,
                VATPrice = (decimal)(taxPercentage * (x.jvmhr.AdultCount * ((((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice))
                + (x.jvmhr.ChildCount * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice))
                + (x.jvmhr.InfantCount * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice)))
                / 100),
                AdultCount = x.jvmhr.AdultCount,
                ChildCount = x.jvmhr.ChildCount,
                InfantCount = x.jvmhr.InfantCount,
                Passengers = new HashSet<Passenger>()

            }).ToList();
            #endregion

            newOrder.SelectedHotelPackages = new HashSet<SelectedHotelPackage>();
            newOrder.SelectedHotelPackages.Add(newSelectedHotelPackage);
            #endregion

            #region محاسبه قیمت در جدول سفارش از روی قیمت های محاسبه شده ی انواع تخت
            //مجموع مبالغ انواع اتاق : Sum(مبلغ نوع اتاق * تعداد درخواستی)
            var SumTotalPrice = selectedHotelPackage.HotelPackageHotelRooms.Join(newSelectedHotelPackage.SelectedRooms.ToList(), jhphr => jhphr.HotelRoomId, jrs => jrs.HotelRoomId, (jhphr, jrs) => new { jhphr, jrs }).Select(x => new
            {
                UnitPrice = (x.jrs.AdultCount * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice))
                + (x.jrs.ChildCount * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice))
                + (x.jrs.InfantCount * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice))

            }).Select(x => x.UnitPrice).Sum();
            newOrder.TotalPrice = SumTotalPrice;
            //مبلغ تخفیف : فعلا صفر
            var coupon = _couponService.Filter(x => x.Code.Equals(viewmodel.CouponCode)).FirstOrDefault();
            if (coupon != null && coupon.ExpireDate >= DateTime.Now && coupon.OrderId == null)
            {
                newOrder.TotalDiscountPrice = (decimal)((coupon.DiscountPercent * SumTotalPrice) / 100);

            }
            else
            {
                newOrder.TotalDiscountPrice = 0;
            }
            //var taxPercentage = Convert.ToInt32(ConfigurationManager.AppSettings["TaxPercentage"]);
            //درصد جاری مالیات بر ارزش افزوده
            newOrder.CurrentTaxPercentage = taxPercentage;
            //مبلغ مالیات بر ارزش افزوده
            newOrder.TotalTaxPrice = (decimal)((taxPercentage * newOrder.TotalPrice) / 100);
            //مبلغ قابل پرداخت : مبلغ مالیات + مبلغ کل
            newOrder.TotalPayPrice = (newOrder.TotalPrice - newOrder.TotalDiscountPrice) + newOrder.TotalTaxPrice;
            #endregion

            #region در نظر گرفتن مسافرین
            foreach (var passenger in viewmodel.PassengerList)
            {
                PersianCalendar pc = new PersianCalendar();
                if (viewmodel.IsForeignTour)
                {
                    var passportExpirationDate = passenger.PassportExpirationDate.Value;
                    passenger.PassportExpirationDate = new DateTime(pc.GetYear(passportExpirationDate), pc.GetMonth(passportExpirationDate), pc.GetDayOfMonth(passportExpirationDate));
                }
                var Birthdate = passenger.Birthdate.Value;
                passenger.Birthdate = new DateTime(pc.GetYear(Birthdate), pc.GetMonth(Birthdate), pc.GetDayOfMonth(Birthdate));
            }
            foreach (var selectedroom in newSelectedHotelPackage.SelectedRooms.ToList())
            {
                selectedroom.Passengers = _mappingEngine.DynamicMap<List<Passenger>>(viewmodel.PassengerList.Where(x => x.RoomIndex == newSelectedHotelPackage.SelectedRooms.ToList().IndexOf(selectedroom)));
            }
            #endregion

            #region ذخیره افرادی که اطلاعات سفارش را خواهند گرفت
            newOrder.VoucherReceivers = _mappingEngine.DynamicMap<List<VoucherReceiverViewModel>, List<VoucherReceiver>>(viewmodel.VoucherReceivers);
            #endregion

            #region ذخیره اطلاعات  کاربر لاگین کرده در جدول OrderInformation
            newOrder.OrderInformation = new OrderInformation()
            {
                Title = string.Format("{0} {1}", loggedInUser.FirstName, loggedInUser.LastName),
                Address = "",
                Age = "",
                Cellphone = loggedInUser.PhoneNumber,
                FirstName = loggedInUser.FirstName,
                LastName = loggedInUser.LastName,
                NationalCode = "",
                Tel = loggedInUser.PhoneNumber,
                Email = loggedInUser.Email,
                UserId = loggedInUser.Id
            };
            #endregion

            #region ذخیره در پایگاه داده
            _dbSet.Add(newOrder);
            var result = _unitOfWork.SaveAllChanges();

            if (coupon != null)
            {
                coupon.OrderId = newOrder.Id;
                _unitOfWork.SaveAllChanges();
            }
            #endregion

            #region بررسی مجاز بودن پرداخت اعتباری
            var remainingCreditPrice = loggedInUser.UserProfile.RemainingCreditValue ?? 0;
            isAllowedCreditPay = (newOrder.TotalPayPrice <= remainingCreditPrice);
            #endregion

            return newOrder;
        }
        #endregion

        #region 3. Voucher Info
        /// <summary>
        /// واکشی اطلاعات واچر
        /// </summary>
        /// <param name="trackingCode">کد پیگیری</param>
        /// <returns></returns>
        public VoucherViewModel VoucherInfo(string trackingCode)
        {
            #region سفارش ثبت شده
            var savedOrder = _unitOfWork.Set<Order>().FirstOrDefault(x => x.TrackingCode == trackingCode);
            #endregion

            if (savedOrder == null) { return null; }

            var viewmodel = new VoucherViewModel();
            /*
             * واکشی اطلاعات خود سفارش
             * واکشی اطلاعات هتل ها
             * واکشی اطلاعات انواع اتاق
             * واکشی اطلاعات پرواز رفت و پرواز برگشت
             * واکشی اطلاعات مسافرین
             */
            viewmodel = _mappingEngine.DynamicMap<VoucherViewModel>(savedOrder);
            foreach (var passenger in viewmodel.PassengerList)
            {
                var country = _countryService.Filter(x => x.Id == passenger.BirthCountryId).FirstOrDefault();
                passenger.BirthCountryTitle = country.Title;
                passenger.EnBirthCountryTitle = country.ENTitle;
            }
            return viewmodel;
        }

        /// <summary>
        /// واکشی اطلاعات واچر سمت مشتری
        /// </summary>
        /// <param name="trackingCode">کد پیگیری</param>
        /// <returns></returns>
        public VoucherViewModel VoucherInfo(string trackingCode, int loggedInUserId)
        {
            #region کاربری که لاگین است
            var loggedInUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.Id == loggedInUserId);
            #endregion

            #region سفارش ثبت شده
            var savedOrder = _unitOfWork.Set<Order>().FirstOrDefault(x => x.TrackingCode == trackingCode && x.CreatorUserId == loggedInUserId);
            #endregion

            if (savedOrder == null) { return null; }

            var viewmodel = new VoucherViewModel();
            /*
             * واکشی اطلاعات خود سفارش
             * واکشی اطلاعات هتل ها
             * واکشی اطلاعات انواع اتاق
             * واکشی اطلاعات پرواز رفت و پرواز برگشت
             * واکشی اطلاعات مسافرین
             */
            viewmodel = _mappingEngine.DynamicMap<VoucherViewModel>(savedOrder);

            return viewmodel;
        }
        #endregion

        #region CreditPayment
        public bool CreditPayment(long orderId)
        {
            //تغییر وضعیت سفارش
            var orderInDb = _dbSet.Find(orderId);
            orderInDb.IsSuccessful = true;
            var savedCount = _unitOfWork.SaveAllChanges();
            if (savedCount > 0)
            {
                //لاگ پرداخت اعتباری
                _paymentLogService.InsertPaymentLog(0, orderId, 0, orderInDb.TotalPayPrice, "1000", "پرداخت موفق اعتباری", orderInDb.TrackingCode, true);
                //رکورد کم کردن اعتبار در جدول Credit
                _creditService.LogCreditAfterPayment(orderInDb);
            }
            return savedCount > 0;
        }
        #endregion

        #region JoinGetHotelUrl
        private string JoinGetHotelUrl(int HotelId)
        {
            var hotelLink = _linkService.Filter(x => x.IsDeleted == false && x.typeId == HotelId && x.linkType == LinkType.Hotel).FirstOrDefault();
            var url = hotelLink != null ? hotelLink.URL : "";
            return url;
        }
        #endregion

        #region GetCityTitleOfAirport
        private string GetCityTitleOfAirport(int AirportId)
        {
            var CityId = _airportService.GetById(x => x.Id == AirportId).CityId;
            var CityTitle = _cityService.GetById(x => x.Id == CityId).Title;
            return CityTitle;
        }
        #endregion

        #region setArrayDay
        private List<int[]> setArrayDay(List<DateTime> date)
        {
            List<int[]> dateArrayList = new List<int[]>();
            foreach (var item in date)
            {
                PersianDateTime pDate = new PersianDateTime(item);
                //با این تابع تاریخ به شمسی تبدیل می شود و میتوان از توابع ماه و روز هفته به شمسی استفاده  کرد. فقط چون روزهای هفته از صفر شروع میشه یکی بهش اضافه می کنیم
                int[] dateArray = new int[] { pDate.Month, pDate.Day, (int)pDate.DayOfWeek + 1 };
                dateArrayList.Add(dateArray);
            }
            return dateArrayList;
        }
        #endregion

        #region GetSuccessfulReserve
        //public IQueryable<ReserveViewModel> GetSuccessfulReserve()
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        #region GeneratePaymentUniqueNumber
        /// <summary>
        /// ساخت شناسه یکتای پرداخت
        /// </summary>
        /// <param name="order">سفارش ثبت شده ی موقت</param>
        /// <returns>شناسه سفارش و شناسه یکتای پرداخت</returns>
        public PaymentUniqueNumber GeneratePaymentUniqueNumber(Order order)
        {
            var newPayUniqueNumber = new PaymentUniqueNumber()
            {
                OrderId = order.Id
            };
            _unitOfWork.Set<PaymentUniqueNumber>().Add(newPayUniqueNumber);
            _unitOfWork.SaveAllChanges();

            return newPayUniqueNumber;
        }
        #endregion
    }
}
