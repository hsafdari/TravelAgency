using AutoMapper;
using GSD.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Contract.Link;
using ParvazPardaz.Service.Contract.LinkRedirection;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Z.EntityFramework.Plus;
using ParvazPardaz.Common.Extension;
using System.Data.Entity.Core.Objects;
using ParvazPardaz.ViewModel.Book;
using ParvazPardaz.Service.Contract;
using ParvazPardaz.TravelAgency.UI.Services.Interface.Book;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.ViewModel.Book.LocationTour;
using ParvazPardaz.Service.Contract.Magazine;
using ParvazPardaz.ViewModel.Book.TourReserve;
using ParvazPardaz.Web.SignalR;
using Microsoft.AspNet.SignalR;

namespace ParvazPardaz.Web.Controllers
{
    public class TourController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly ITourService _tourService;
        private readonly IAirportService _airportService;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICompanyTransferService _companyTransferService;
        private readonly IHotelService _hotelService;
        private readonly ILinkService _linkService;
        private readonly ITourLandingPageUrlService _tourLandingPageUrlService;
        private readonly ILinkRedirectionService _linkRedirectionService;
        private readonly IHotelPackageService _hotelPackageService;
        private readonly ITourScheduleService _tourScheduleService;
        private readonly IOrderService _orderService;
        private readonly IHotelRoomService _hotelRoomService;
        private readonly ICouponService _couponService;
        private readonly IPassengerService _passengerService;
        private readonly ICreditService _creditService;
        private readonly ISliderService _sliderService;
        private readonly ISliderGroupService _sliderGroupService;
        private readonly ITourPackageDayService _tourPackageService;
        private readonly IHotelRankService _hotelRankService;
        private readonly ITourSuggestionService _tourSuggestService;
        private List<BreadCrumbsItemViewModel> breadCrumbItems;
        #endregion

        #region	Ctor
        public TourController(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, ITourService tourService, IAirportService airportService,
            ICityService cityService, ICompanyTransferService companyTransferService, IHotelService hotelService, ILinkService linkService,
            ITourLandingPageUrlService tourLandingPageUrlService, ILinkRedirectionService linkRedirectionService, IHotelPackageService hotelPackageService,
            ITourScheduleService tourScheduleService, IOrderService orderService, IHotelRoomService hotelRoomService, ICountryService countryService,
            ICouponService couponService, IPassengerService passengerService, ICreditService creditService, ISliderService sliderService,
            ISliderGroupService sliderGroupService, ITourPackageDayService tourPackageService, IHotelRankService hotelRankService, ITourSuggestionService tourSuggestService)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _tourService = tourService;
            _airportService = airportService;
            _cityService = cityService;
            _countryService = countryService;
            _companyTransferService = companyTransferService;
            _hotelService = hotelService;
            _linkService = linkService;
            _tourLandingPageUrlService = tourLandingPageUrlService;
            _linkRedirectionService = linkRedirectionService;
            _hotelPackageService = hotelPackageService;
            _tourScheduleService = tourScheduleService;
            _orderService = orderService;
            _hotelRoomService = hotelRoomService;
            breadCrumbItems = new List<BreadCrumbsItemViewModel>();
            _couponService = couponService;
            _passengerService = passengerService;
            _creditService = creditService;
            _sliderService = sliderService;
            _sliderGroupService = sliderGroupService;
            _tourPackageService = tourPackageService;
            _hotelRankService = hotelRankService;
            _tourSuggestService = tourSuggestService;
        }
        #endregion

        #region ساخت بردکرامبز createBreadCrumbs
        /// <summary>
        /// ایجاد breadCrumbItems
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="linkType"></param>
        public void createBreadCrumbs(PostGroup treeNode, LinkType linkType)
        {
            var linkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.linkType == linkType && x.typeId == treeNode.Id);
            var item = new BreadCrumbsItemViewModel() { Title = treeNode.Name, URL = linkTbl != null ? linkTbl.URL : "#" };
            breadCrumbItems.Add(item);
            if (treeNode.PostGroupParent != null)
            {
                createBreadCrumbs(treeNode.PostGroupParent, LinkType.PostGroup);
            }
        }
        #endregion

        #region TourDetail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="country">عنوان انگلیسی کشور</param>
        /// <param name="city">عنوان انگلیسی شهر</param>
        /// <param name="id">شناسه پکیج تور</param>
        /// <returns></returns>   
        public TourReserveViewModel TourDetail(string country, string city, int id)
        {
            TourReserveViewModel viewmodel = new TourReserveViewModel();
            #region اطلاعات مورد نیاز
            //شرکت های هواپیمایی
            var airports = _unitOfWork.Set<Airport>().Where(x => !x.IsDeleted);
            #endregion

            //فهرست پروازهای رفت
            #region ViewBag.DepartureFlights
            //پرواز های رفت تورها با توجه به اطلاعات جستجو
            var _departureFlights = _unitOfWork.Set<TourScheduleCompanyTransfer>()
                                    .Where(x =>
                                        DbFunctions.TruncateTime(x.TourSchedule.ExpireDate) >= DbFunctions.TruncateTime(DateTime.Now)
                                        && x.FlightDirection == EnumFlightDirectionType.Go
                                        && !x.IsDeleted && !x.TourSchedule.TourPackage.Tour.IsDeleted
                                        && x.TourSchedule.TourPackage.Tour.Recomended
                                        && x.TourSchedule.TourPackageId == id
                                        )
                                   .Select(x => new FlightViewModel
                                   {
                                       Id = x.Id,
                                       BaggageAmount = x.BaggageAmount,
                                       Capacity = x.Capacity,
                                       CompanyTransfer = x.CompanyTransfer,
                                       CompanyTransferId = x.CompanyTransferId,
                                       DepartureFlightId = 0,
                                       Description = x.Description,
                                       //DestinationAirportIataCode=x.,
                                       DestinationAirportId = x.DestinationAirportId,
                                       EndDateTime = x.EndDateTime,
                                       //FlightClass = x.FlightClass,
                                       FlightClass = x.VehicleTypeClass.Title,
                                       FlightDirection = x.FlightDirection,
                                       FlightNumber = x.FlightNumber,
                                       //FromAirportIataC
                                       FromAirportId = x.FromAirportId,
                                       NonLimit = x.NonLimit,
                                       StartDateTime = x.StartDateTime,
                                       TourScheduleId = x.TourScheduleId
                                   }).ToList();

            var departureFlights = (from x in _departureFlights
                                    group x by new { x.BaggageAmount, x.Capacity, x.CompanyTransferId, x.CompanyTransfer, x.Description, x.DestinationAirportId, x.EndDateTime, x.FlightClass, x.FlightDirection, x.FlightNumber, x.FromAirportId, x.NonLimit, x.StartDateTime } into g
                                    select new FlightViewModel
                                    {
                                        BaggageAmount = g.Key.BaggageAmount,
                                        CompanyTransferId = g.Key.CompanyTransferId,
                                        CompanyTransfer = g.Key.CompanyTransfer,
                                        Description = g.Key.Description,
                                        DestinationAirportId = g.Key.DestinationAirportId,
                                        StartDateTime = g.Key.StartDateTime,
                                        EndDateTime = g.Key.EndDateTime,
                                        FlightClass = g.Key.FlightClass,
                                        FlightNumber = g.Key.FlightNumber,
                                        FromAirportId = g.Key.FromAirportId,
                                        DepartureFlight = string.Join(",", g.Select(kvp => kvp.Id)),
                                    }).ToList();


            var vm = new TourReserveViewModel();
            ViewBag.DepartureFlights = departureFlights;
            //ViewBag.DestinationCityTitle = _unitOfWork.Set<City>().FirstOrDefault(x => x.Id == viewModel.ArrivalCityId).Title;
            //ViewBag.CompanyTransfer = _companyTransferService.GetAllCompanyTransferOfSelectListItem();

            //ViewBag.CitiesDDL = _cityService.GetAllCityOfSelectListItem();
            ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL();
            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL();
            #endregion

            if (departureFlights != null && departureFlights.Any())
            {
                //پرواز رفت انتخاب شده
                ViewBag.SelectedDefaultDepFlightId = departureFlights.FirstOrDefault().DepartureFlight;

                //فهرست پروازهای برگشت
                #region ViewBag.ArrivalFlightList
                var allFlights = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.TourSchedule);
                List<int> DepartureFlightid = departureFlights.FirstOrDefault().DepartureFlight.Split(',').Select(int.Parse).ToList();
                var depFlight = allFlights.Where(x => DepartureFlightid.Any(y => y == x.Id)).AsEnumerable();
                var arrivalFlights = allFlights.Where(x => depFlight.Any(y => y.TourScheduleId == x.TourScheduleId)
                                                           && x.FlightDirection == EnumFlightDirectionType.Back && !x.IsDeleted).ToList();
                Mapper.CreateMap<TourScheduleCompanyTransfer, FlightViewModel>();
                var _ArrivalFlights = Mapper.DynamicMap<List<TourScheduleCompanyTransfer>, List<FlightViewModel>>(arrivalFlights);
                var ArrivalFlightList = new List<FlightViewModel>();
                foreach (var item in _ArrivalFlights)
                {
                    foreach (var item2 in depFlight)
                    {
                        if (item.TourScheduleId == item2.TourScheduleId)
                        {
                            item.DepartureFlightId = item2.Id;
                        }
                    }
                    ArrivalFlightList.Add(item);
                }
                //پرواز های برگشت مربوط به پرواز رفت انتخاب شده
                ViewBag.ArrivalFlightList = ArrivalFlightList;
                //واکشی عناوین پکیج های تور
                //ViewBag.TourPackageTitles = arrivalFlights.Select(x => x.TourSchedule.TourPackage.Title).Distinct().ToList();
                ViewBag.tourPackageDDL = new SelectList(arrivalFlights.Select(x => x.TourSchedule.TourPackage).Distinct().ToList(), "Id", "Title");
                #endregion

                //پرواز برگشت انتخاب شده
                ViewBag.SelectedDefaultArrivalFlightId = ArrivalFlightList.FirstOrDefault().Id;

                var fromAirportId = departureFlights.FirstOrDefault().FromAirportId;
                var toAirportId = departureFlights.FirstOrDefault().DestinationAirportId;
                var fromAirport = airports.FirstOrDefault(x => x.Id == fromAirportId);
                var toAirport = airports.FirstOrDefault(x => x.Id == toAirportId);
                vm.TourSearchParams = new TourSearchViewModel()
                {
                    AdultCount = 1,
                    ChildCount = 0,
                    InfantCount = 0,
                    ArrivalCityId = fromAirport.CityId,
                    DepartureCityId = toAirport.CityId,
                    DurationTime = null,
                    FlightDate = _departureFlights.FirstOrDefault().StartDateTime.ToString("yyyy/MM/dd"),
                    Calendertype = ParvazPardaz.Model.Enum.Calendertype.persian.ToString()
                };

                #region واکشی هتل های پیش فرض بر اساس کمترین قیمت
                // var selectedTourPackageId = ArrivalFlightList.FirstOrDefault().TourSchedule.TourPackageId;
                var hotelPackageForThisTourPackage = _hotelPackageService.Filter(x => x.TourPackageId == id && !x.IsDeleted);

                var hotelRoomList = _hotelRoomService.GetAll().Where(x => !x.IsDeleted && x.HotelPackageHotelRooms.Any(y => y.HotelPackage.TourPackageId == id)).ToList();
                var defaultHotelPackages = hotelPackageForThisTourPackage.Select(x => new ListViewHotelPackageViewModel()
                {
                    Id = x.Id,
                    OrderId = x.OrderId, // ترتیب
                    TourId = x.TourPackage.TourId,
                    TourPackageId = x.TourPackageId,
                    TourPackageTitle = x.TourPackage.Title,
                    TourTitle = x.TourPackage.Tour.Title,

                    hotelsInPackage = x.HotelPackageHotels.Where(y => !y.IsDeleted && y.Hotel.HotelGalleries.Any(z => !z.IsDeleted) && !y.Hotel.IsDeleted).Distinct().Select(y => new HotelsInPackageViewModel()
                    {
                        Id = y.Id,
                        HotelTitle = y.Hotel.Title,
                        CityId = y.Hotel.CityId,
                        CityTitle = y.Hotel.City.Title,
                        Location = y.Hotel.Location,
                        Summary = y.Hotel.Summary,
                        HotelBoardId = y.HotelBoardId,
                        HotelId = y.HotelId,
                        TourPackageId = y.HotelPackageId,
                        HotelBoardTitle = y.HotelBoard.Name,
                        Thumbnail = y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageUrl + "-261X177" + y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageExtension,
                        HotelGalleryImages = y.Hotel.HotelGalleries.Where(z => !z.IsDeleted).Select(z => new { image = z.ImageUrl + "-700x525" + z.ImageExtension }).Select(z => z.image).ToList(),
                        RankLogo = y.Hotel.HotelRank.Icon,
                        HotelBoardLogo = y.HotelBoard.ImageUrl,
                        RankOrderId = y.Hotel.HotelRank.OrderId

                    }).ToList(),

                }).ToList();

                foreach (var hp in defaultHotelPackages)
                {
                    //Left: HotelRoom, Right: HotelPackageHotelRooms ==> Left Outer Join
                    hp.hotelRoomsInPackage = hotelRoomList.GroupJoin(hotelPackageForThisTourPackage.FirstOrDefault(x => x.Id == hp.Id).HotelPackageHotelRooms.OrderBy(y => y.Id), jhr => jhr.Id, jhphr => jhphr.HotelRoomId, (jhr, jhphr) => new { hr = jhr, hphr = jhphr })
                    .SelectMany(left => left.hphr.DefaultIfEmpty(), (left, right) => new { hr = left.hr, hphr = right })
                    .AsEnumerable()
                    .Select(s => new HotelRoomsInPackageViewModel
                    {
                        AdultPrice = (s.hphr != null ? (s.hphr.AdultPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0))) : 0),
                        ChildPrice = (s.hphr != null ? (s.hphr.ChildPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0))) : 0),
                        InfantPrice = (s.hphr != null ? (s.hphr.InfantPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0))) : 0),

                        AdultOtherCurrencyPrice = (s.hphr != null ? (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0) : 0),
                        ChildOtherCurrencyPrice = (s.hphr != null ? (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0) : 0),
                        InfantOtherCurrencyPrice = (s.hphr != null ? (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0) : 0),

                        OtherCurrencyId = (s.hphr != null ? (s.hphr.OtherCurrencyId != null ? s.hphr.OtherCurrencyId.Value : 0) : 0),
                        RoomTypeId = s.hr.Id,
                        Title = s.hr.Title,
                        Id = (s.hphr != null ? s.hphr.Id : 0)

                    }).ToList();
                }
                //فهرست ارزان ترین هتل های پکیج بر پایه ارزان ترین قیمت
                var defaultHotelPackageList = defaultHotelPackages.OrderBy(x => x.hotelRoomsInPackage.Sum(z => z.AdultPrice)).ToList();
                ViewBag.DefaultHotelPackageList = defaultHotelPackageList;
                ViewBag.FromPrice = _departureFlights.FirstOrDefault().TourSchedule != null ? _departureFlights.FirstOrDefault().TourSchedule.TourPackage.FromPrice : "";
                //_unitOfWork.Set<TourPackage>().FirstOrDefault(x => x.Id == id);
                // ViewBag.FromPrice = selectedTourPackage.FromPrice;
                #endregion

                //پکیج هتل پیش فرض
                var defaultHotelPackage = defaultHotelPackageList.FirstOrDefault();
                ViewBag.DefaultHotelPackageId = defaultHotelPackage.Id;

                // پکیج تور پیش فرض
                ViewBag.DefaultTourPackageId = defaultHotelPackage.TourPackageId;

                //هتل انتخاب شده پیش فرض
                var defaultHotelId = defaultHotelPackage.hotelsInPackage.FirstOrDefault().HotelId;
                ViewBag.DefaultHotelId = defaultHotelId;

                //اطلاعات تور پیش فرض
                ViewBag.Tour = _tourService.Filter(x => x.Id == defaultHotelPackage.TourId).First();

                #region انواع اتاق
                var defaultHotelPackageId = (defaultHotelPackage != null ? defaultHotelPackage.Id : 0);
                var selectedHotelPackage = _hotelPackageService.Filter(x => x.Id == defaultHotelPackageId).FirstOrDefault();

                //برای قسمت وارد کردن تعداد رده سنی : 
                //اگر ظرفیت قابل فروش بزرگسال [تعریف شده در پکیج هتل] کمتــر از 
                //حداکثر ظرفیت بزرگسال [تعریف شده در مدیریت اتاق] بود ؛ 
                //مشتری هنگام وارد کردن تعداد بزرگسال ، حداکثر تا ظرفیت قابل فروش بزرگسال بتونه بزنه 
                //وگرنه همون حداکثر ظرفیتی که توی مدیریت اتاق زده. برای رده های سنی دیگه هم همینطور
                //ظرفیت قابل فروش = حداکثر ظرفیت رده سنی در پکیج هتل منـــهای ظرفیت فروخته شده رده سنی
                var roomTypeList = selectedHotelPackage.HotelPackageHotelRooms.Where(x => (x.AdultPrice > 0 || x.ChildPrice > 0 || x.InfantPrice > 0)).OrderBy(x => x.HotelRoomId).Select(x => new HotelRoomDDLViewModel()
                {
                    Title = x.HotelRoom.Title,
                    Value = x.HotelRoomId.ToString(),
                    CurrencyId = (x.OtherCurrencyId != null ? x.OtherCurrencyId.Value.ToString() : "0"),
                    AdultPrice = x.AdultPrice.ToString("#"),
                    TotalAdultCapacity = ((x.AdultCapacity - x.AdultCapacitySold) < x.HotelRoom.AdultMaxCapacity ? (x.AdultCapacity - x.AdultCapacitySold) : x.HotelRoom.AdultMaxCapacity),
                    AdultOtherCurrencyPrice = (x.AdultOtherCurrencyPrice != null ? x.AdultOtherCurrencyPrice.Value.ToString("#") : "0"),
                    //
                    ChildPrice = x.ChildPrice.ToString("#"),
                    TotalChildCapacity = ((x.ChildCapacity - x.ChildCapacitySold) < x.HotelRoom.ChildMaxCapacity ? (x.ChildCapacity - x.ChildCapacitySold) : x.HotelRoom.ChildMaxCapacity),
                    ChildOtherCurrencyPrice = (x.ChildOtherCurrencyPrice != null ? x.ChildOtherCurrencyPrice.Value.ToString("#") : "0"),
                    //
                    InfantPrice = x.InfantPrice.ToString("#"),
                    TotalInfantCapacity = ((x.InfantCapacity - x.InfantCapacitySold) < x.HotelRoom.InfantMaxCapacity ? (x.InfantCapacity - x.InfantCapacitySold) : x.HotelRoom.InfantMaxCapacity),
                    InfantOtherCurrencyPrice = (x.InfantOtherCurrencyPrice != null ? x.InfantOtherCurrencyPrice.Value.ToString("#") : "0"),
                    Priority = x.HotelRoom.Priority
                }).Distinct().OrderBy(x => x.Priority.Value).ToList();
                ViewBag.RoomType = roomTypeList;
                ViewBag.RoomTypeList = _hotelRoomService.Filter(x => !x.IsDeleted).ToList();
                #endregion

            }
            //var _arrivalcity = _unitOfWork.Set<City>().FirstOrDefault(x => x.Id == vm.TourSearchParams.ArrivalCityId);
            //ViewBag.DestinationCityTitle = _arrivalcity.Title;
            var _linklocation = _unitOfWork.Set<Location>().Where(x => x.ENTitle == city && !x.IsDeleted).FirstOrDefault();
            ViewBag.canonical = _linklocation.URL;
            ViewBag.CurrencyList = _unitOfWork.Set<Currency>().Where(x => !x.IsDeleted).ToList();
            return vm;
        }
        #endregion

        #region AddVoucherReceiverSection
        public ActionResult AddVoucherReceiverSection(ConfirmViewModel viewmodel)
        {
            //viewmodel.VoucherReceivers = new List<VoucherReceiverViewModel>();
            return PartialView("_PrvAddVoucherReceiverItem", viewmodel);
        }
        #endregion

        #region AddVoucherReceiverForm
        public ActionResult AddVoucherReceiverForm()
        {
            return PartialView("_PrvAddVoucherReceiverForm");
        }
        #endregion

        #region 1. TourSearch
        [Route("TourSearch")]
        [HttpGet]
        public ActionResult TourSearch()
        {
            TourSearchViewModel model = new TourSearchViewModel();
            TourReserveViewModel viewmodel = new TourReserveViewModel();
            model.FlightDate = DateTime.Now.ToString("yyyy/MM/dd");
            model.Calendertype = "persian";
            ViewBag.Airports = _airportService.GetAllAirPortOfSelectListItem();
            ViewBag.CompanyTransfer = _companyTransferService.GetAllCompanyTransferOfSelectListItem();
            ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL();
            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL();
            viewmodel.TourSearchParams = model;
            return View(viewmodel);
        }

        /// <summary>
        /// ورودی فیلدهای جستجوی تور بوده و خروجی پروازهای رفت تورهای یافت شده
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [Route("TourSearch")]
        [HttpPost]
        public ActionResult TourSearch(TourSearchViewModel viewModel)
        {
            #region محاسبه تاریخ پرواز بر اساس تاریخ فارسی/انگلیسی
            DateTime? _flightDate = DateTime.Now;
            if (String.IsNullOrEmpty(viewModel.FlightDate))
                viewModel.FlightDate = DateTime.Now.ToString("yyyy/MM/dd");
            if (viewModel.FlightDate != null)
            {
                //var FlightDateArray = viewModel.FlightDate.Split(',');
                if (viewModel.Calendertype == "persian")
                {
                    var pc = new PersianCalendar();
                    //var currentYear = pc.GetYear(DateTime.Now);
                    //viewModel.FlightDate = string.Format("{0}/{1}/{2}", currentYear, FlightDateArray[1], FlightDateArray[2]);
                    string Date = viewModel.FlightDate.ToPersianNumber();
                    _flightDate = System.Convert.ToDateTime(Date);
                    viewModel.FlightDate = _flightDate.Value.ToString("yyyy/MM/dd");
                }
                else if (viewModel.Calendertype == "gregorian")
                {
                    //var currentYear = DateTime.Now.Year;
                    //viewModel.FlightDate = string.Format("{0}/{1}/{2}", currentYear, FlightDateArray[1], FlightDateArray[2]);
                    string Date = viewModel.FlightDate.ToPersianNumber();
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    _flightDate = System.Convert.ToDateTime(Date);
                    viewModel.FlightDate = _flightDate.Value.ToString("yyyy/MM/dd");
                }
            }
            #endregion

            #region اطلاعات مورد نیاز
            //شرکت های هواپیمایی
            var airports = _unitOfWork.Set<Airport>().Where(x => !x.IsDeleted);
            //شناسه شرکت های هواپیمایی که در شهر مبدا هستند؟
            var departureAirportIds = airports.Where(x => x.CityId == viewModel.DepartureCityId).Select(x => x.Id).ToList();
            //var departureAirportIds=1;
            //شناسه شرکت های هواپیمایی که در شهر مقصد هستند؟
            var arrivalAirportIds = airports.Where(x => x.CityId == viewModel.ArrivalCityId).Select(x => x.Id).ToList();
            //var arrivalAirportIds = 60;
            #endregion

            //فهرست پروازهای رفت
            #region ViewBag.DepartureFlights
            //پرواز های رفت تورها با توجه به اطلاعات جستجو
            var _departureFlights = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.TourSchedule)
                                    .Where(x => DbFunctions.TruncateTime(x.StartDateTime) == DbFunctions.TruncateTime(_flightDate.Value)
                                        && DbFunctions.TruncateTime(x.StartDateTime) >= DbFunctions.TruncateTime(DateTime.Now)
                                        && DbFunctions.TruncateTime(x.TourSchedule.ExpireDate) >= DbFunctions.TruncateTime(DateTime.Now)
                                        && x.FlightDirection == EnumFlightDirectionType.Go
                                        && departureAirportIds.Contains(x.FromAirportId.Value)
                                        && !x.IsDeleted && !x.TourSchedule.TourPackage.Tour.IsDeleted && !x.TourSchedule.IsDeleted && !x.TourSchedule.TourPackage.IsDeleted
                                        && x.TourSchedule.TourPackage.Tour.Recomended
                                        && arrivalAirportIds.Contains(x.DestinationAirportId.Value))
                                   .Select(x => new FlightViewModel
                                   {
                                       Id = x.Id,
                                       BaggageAmount = x.BaggageAmount,
                                       Capacity = x.Capacity,
                                       CompanyTransfer = x.CompanyTransfer,
                                       CompanyTransferId = x.CompanyTransferId,
                                       DepartureFlightId = 0,
                                       Description = x.Description,
                                       //DestinationAirportIataCode=x.,
                                       DestinationAirportId = x.DestinationAirportId,
                                       EndDateTime = x.EndDateTime,
                                       //FlightClass = x.FlightClass,
                                       FlightClass = x.VehicleTypeClass.Title,
                                       FlightDirection = x.FlightDirection,
                                       FlightNumber = x.FlightNumber,
                                       //FromAirportIataC
                                       FromAirportId = x.FromAirportId,
                                       NonLimit = x.NonLimit,
                                       StartDateTime = x.StartDateTime,
                                       TourScheduleId = x.TourScheduleId
                                   }).ToList();

            var departureFlights = (from x in _departureFlights
                                    group x by new { x.BaggageAmount, x.Capacity, x.CompanyTransferId, x.CompanyTransfer, x.Description, x.DestinationAirportId, x.EndDateTime, x.FlightClass, x.FlightDirection, x.FlightNumber, x.FromAirportId, x.NonLimit, x.StartDateTime } into g
                                    //select new { g.Key, count = g.Count(), Items = string.Join(",", g.Select(kvp => kvp.TourScheduleId)) });                     
                                    select new FlightViewModel
                                    {
                                        BaggageAmount = g.Key.BaggageAmount,
                                        // Capacity = g.Key.Capacity,
                                        CompanyTransferId = g.Key.CompanyTransferId,
                                        CompanyTransfer = g.Key.CompanyTransfer,
                                        //DepartureFlightId = 0,
                                        Description = g.Key.Description,
                                        //DestinationAirportIataCode=g.,
                                        DestinationAirportId = g.Key.DestinationAirportId,
                                        StartDateTime = g.Key.StartDateTime,
                                        EndDateTime = g.Key.EndDateTime,
                                        FlightClass = g.Key.FlightClass,
                                        //FlightDirection = g.Key.FlightDirection,
                                        FlightNumber = g.Key.FlightNumber,
                                        //FromAirportIataC
                                        FromAirportId = g.Key.FromAirportId,
                                        //NonLimit = g.Key.NonLimit,
                                        //TourScheduleId = g.Key.TourScheduleId,
                                        DepartureFlight = string.Join(",", g.Select(kvp => kvp.Id)),
                                    }).ToList();


            var vm = new TourReserveViewModel();
            vm.TourSearchParams = new TourSearchViewModel()
            {
                AdultCount = viewModel.AdultCount,
                ChildCount = viewModel.ChildCount,
                InfantCount = viewModel.InfantCount,
                ArrivalCityId = viewModel.ArrivalCityId,
                DepartureCityId = viewModel.DepartureCityId,
                DurationTime = viewModel.DurationTime,
                FlightDate = viewModel.FlightDate,
                Calendertype = viewModel.Calendertype
            };
            for (int i = 0; i < departureFlights.Count; i++)
            {
                departureFlights[i].Id = i + 1;
            }
            ViewBag.DepartureFlights = departureFlights;
            var _arrivalcity = _unitOfWork.Set<City>().FirstOrDefault(x => x.Id == viewModel.ArrivalCityId);
            ViewBag.DestinationCityTitle = _arrivalcity.Title;
            var _linklocation = _unitOfWork.Set<Location>().Where(x => x.ENTitle == _arrivalcity.ENTitle && !x.IsDeleted).FirstOrDefault();
            ViewBag.canonical = _linklocation.URL;
            ViewBag.Airports = _airportService.GetAllAirPortOfSelectListItem();
            ViewBag.CompanyTransfer = _companyTransferService.GetAllCompanyTransferOfSelectListItem();

            //ViewBag.CitiesDDL = _cityService.GetAllCityOfSelectListItem();
            ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL();
            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL();
            #endregion

            if (departureFlights != null && departureFlights.Any())
            {
                //پرواز رفت انتخاب شده
                ViewBag.SelectedDefaultDepFlightId = departureFlights.FirstOrDefault().DepartureFlight;

                //فهرست پروازهای برگشت
                #region ViewBag.ArrivalFlightList
                var allFlights = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.TourSchedule);
                List<int> DepartureFlightid = departureFlights.FirstOrDefault().DepartureFlight.Split(',').Select(int.Parse).ToList();
                var depFlight = allFlights.Where(x => DepartureFlightid.Any(y => y == x.Id)).AsEnumerable();
                var arrivalFlights = allFlights.Where(x => depFlight.Any(y => y.TourScheduleId == x.TourScheduleId) && x.FlightDirection == EnumFlightDirectionType.Back).ToList();
                Mapper.CreateMap<TourScheduleCompanyTransfer, FlightViewModel>();
                var _ArrivalFlights = Mapper.DynamicMap<List<TourScheduleCompanyTransfer>, List<FlightViewModel>>(arrivalFlights);
                var ArrivalFlightList = new List<FlightViewModel>();
                foreach (var item in _ArrivalFlights)
                {
                    foreach (var item2 in depFlight)
                    {
                        if (item.TourScheduleId == item2.TourScheduleId)
                        {
                            item.DepartureFlightId = item2.Id;
                        }
                    }
                    ArrivalFlightList.Add(item);
                }
                //پرواز های برگشت مربوط به پرواز رفت انتخاب شده
                ViewBag.ArrivalFlightList = ArrivalFlightList;
                //واکشی عناوین پکیج های تور
                //ViewBag.TourPackageTitles = arrivalFlights.Select(x => x.TourSchedule.TourPackage.Title).Distinct().ToList();
                ViewBag.tourPackageDDL = new SelectList(arrivalFlights.Select(x => x.TourSchedule.TourPackage).Distinct().ToList(), "Id", "Title");
                #endregion

                //پرواز برگشت انتخاب شده
                ViewBag.SelectedDefaultArrivalFlightId = ArrivalFlightList.FirstOrDefault().Id;

                #region واکشی هتل های پیش فرض بر اساس کمترین قیمت
                var selectedTourPackageId = ArrivalFlightList.FirstOrDefault().TourSchedule.TourPackageId;
                var hotelPackageForThisTourPackage = _hotelPackageService.Filter(x => x.TourPackageId == selectedTourPackageId && !x.IsDeleted && x.HotelPackageHotels.Where(y => y.Hotel.IsActive && !y.Hotel.IsDeleted).Any());

                var hotelRoomList = _hotelRoomService.GetAll().Where(x => !x.IsDeleted && x.HotelPackageHotelRooms.Any(y => y.HotelPackage.TourPackageId == selectedTourPackageId && !y.IsDeleted)).ToList();
                var defaultHotelPackages = hotelPackageForThisTourPackage.Select(x => new ListViewHotelPackageViewModel()
                {
                    Id = x.Id,
                    OrderId = x.OrderId, // ترتیب
                    TourId = x.TourPackage.TourId,
                    TourPackageId = x.TourPackageId,
                    TourPackageTitle = x.TourPackage.Title,
                    TourTitle = x.TourPackage.Tour.Title,

                    hotelsInPackage = x.HotelPackageHotels.Where(y => !y.IsDeleted && y.Hotel.IsActive && y.Hotel.HotelGalleries.Any(z => !z.IsDeleted) && !y.Hotel.IsDeleted).Distinct().Select(y => new HotelsInPackageViewModel()
                    {
                        Id = y.Id,
                        HotelTitle = y.Hotel.Title,
                        CityId = y.Hotel.CityId,
                        CityTitle = y.Hotel.City.Title,
                        Location = y.Hotel.Location,
                        Summary = y.Hotel.Summary,
                        HotelBoardId = y.HotelBoardId,
                        HotelId = y.HotelId,
                        TourPackageId = y.HotelPackageId,
                        HotelBoardTitle = y.HotelBoard.Name,
                        Thumbnail = y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageUrl + "-261X177" + y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageExtension,
                        HotelGalleryImages = y.Hotel.HotelGalleries.Where(z => !z.IsDeleted).Select(z => new { image = z.ImageUrl + "-700x525" + z.ImageExtension }).Select(z => z.image).ToList(),
                        RankLogo = y.Hotel.HotelRank.Icon,
                        HotelBoardLogo = y.HotelBoard.ImageUrl,
                        RankOrderId = y.Hotel.HotelRank.OrderId

                    }).ToList(),

                }).ToList();


                foreach (var hp in defaultHotelPackages)
                {
                    //Left: HotelRoom, Right: HotelPackageHotelRooms ==> Left Outer Join
                    hp.hotelRoomsInPackage = hotelRoomList.GroupJoin(hotelPackageForThisTourPackage.FirstOrDefault(x => x.Id == hp.Id).HotelPackageHotelRooms.Where(y => !y.IsDeleted).OrderBy(y => y.Id), jhr => jhr.Id, jhphr => jhphr.HotelRoomId, (jhr, jhphr) => new { hr = jhr, hphr = jhphr })
                    .SelectMany(left => left.hphr.DefaultIfEmpty(), (left, right) => new { hr = left.hr, hphr = right })
                    .AsEnumerable()
                    .Select(s => new HotelRoomsInPackageViewModel
                    {
                        AdultPrice = (s.hphr != null ? (s.hphr.AdultPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0))) : 0),
                        ChildPrice = (s.hphr != null ? (s.hphr.ChildPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0))) : 0),
                        InfantPrice = (s.hphr != null ? (s.hphr.InfantPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0))) : 0),

                        AdultOtherCurrencyPrice = (s.hphr != null ? (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0) : 0),
                        ChildOtherCurrencyPrice = (s.hphr != null ? (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0) : 0),
                        InfantOtherCurrencyPrice = (s.hphr != null ? (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0) : 0),

                        OtherCurrencyId = (s.hphr != null ? (s.hphr.OtherCurrencyId != null ? s.hphr.OtherCurrencyId.Value : 0) : 0),
                        RoomTypeId = s.hr.Id,
                        Title = s.hr.Title,
                        Id = (s.hphr != null ? s.hphr.Id : 0)

                    }).ToList();
                }
                //فهرست ارزان ترین هتل های پکیج بر پایه ارزان ترین قیمت
                //صفدری بازبینی شود
                var defaultHotelPackageList = defaultHotelPackages.OrderBy(x => x.hotelRoomsInPackage.Sum(z => z.AdultPrice)).ToList();
                ViewBag.DefaultHotelPackageList = defaultHotelPackageList;
                var selectedTourPackage = _unitOfWork.Set<TourPackage>().FirstOrDefault(x => x.Id == selectedTourPackageId);
                #endregion

                if (defaultHotelPackages != null && defaultHotelPackages.Any())
                {
                    //پکیج هتل پیش فرض
                    var defaultHotelPackage = defaultHotelPackageList.FirstOrDefault();
                    ViewBag.DefaultHotelPackageId = defaultHotelPackage != null ? defaultHotelPackage.Id : 0;

                    // پکیج تور پیش فرض
                    ViewBag.DefaultTourPackageId = defaultHotelPackage != null ? defaultHotelPackage.TourPackageId : 0;

                    //هتل انتخاب شده پیش فرض
                    var defaultHotelId = defaultHotelPackage != null ? defaultHotelPackage.hotelsInPackage.FirstOrDefault().HotelId : 0;
                    ViewBag.DefaultHotelId = defaultHotelId;

                    //اطلاعات تور پیش فرض
                    var tourId = (defaultHotelPackage != null ? defaultHotelPackage.TourId : 0);
                    ViewBag.Tour = _tourService.Filter(x => x.Id == tourId).FirstOrDefault();

                    #region انواع اتاق
                    var defaultHotelPackageId = (defaultHotelPackage != null ? defaultHotelPackage.Id : 0);
                    var selectedHotelPackage = _hotelPackageService.Filter(x => x.Id == defaultHotelPackageId).IncludeFilter(x => x.HotelPackageHotelRooms).FirstOrDefault();

                    //برای قسمت وارد کردن تعداد رده سنی : 
                    //اگر ظرفیت قابل فروش بزرگسال [تعریف شده در پکیج هتل] کمتــر از 
                    //حداکثر ظرفیت بزرگسال [تعریف شده در مدیریت اتاق] بود ؛ 
                    //مشتری هنگام وارد کردن تعداد بزرگسال ، حداکثر تا ظرفیت قابل فروش بزرگسال بتونه بزنه 
                    //وگرنه همون حداکثر ظرفیتی که توی مدیریت اتاق زده. برای رده های سنی دیگه هم همینطور
                    //ظرفیت قابل فروش = حداکثر ظرفیت رده سنی در پکیج هتل منـــهای ظرفیت فروخته شده رده سنی
                    var roomTypeList = selectedHotelPackage.HotelPackageHotelRooms.OrderBy(x => x.HotelRoom.Priority.Value).Where(x => (x.AdultPrice > 0 || x.ChildPrice > 0 || x.InfantPrice > 0)).OrderBy(x => x.HotelRoomId).Select(x => new HotelRoomDDLViewModel()
                    {
                        Title = x.HotelRoom.Title,
                        Value = x.HotelRoomId.ToString(),
                        CurrencyId = (x.OtherCurrencyId != null ? x.OtherCurrencyId.Value.ToString() : "0"),
                        AdultPrice = x.AdultPrice.ToString("#"),
                        TotalAdultCapacity = ((x.AdultCapacity - x.AdultCapacitySold) < x.HotelRoom.AdultMaxCapacity ? (x.AdultCapacity - x.AdultCapacitySold) : x.HotelRoom.AdultMaxCapacity),
                        AdultOtherCurrencyPrice = (x.AdultOtherCurrencyPrice != null ? x.AdultOtherCurrencyPrice.Value.ToString("#") : "0"),
                        ChildPrice = x.ChildPrice.ToString("#"),
                        TotalChildCapacity = ((x.ChildCapacity - x.ChildCapacitySold) < x.HotelRoom.ChildMaxCapacity ? (x.ChildCapacity - x.ChildCapacitySold) : x.HotelRoom.ChildMaxCapacity),
                        ChildOtherCurrencyPrice = (x.ChildOtherCurrencyPrice != null ? x.ChildOtherCurrencyPrice.Value.ToString("#") : "0"),
                        InfantPrice = x.InfantPrice.ToString("#"),
                        TotalInfantCapacity = ((x.InfantCapacity - x.InfantCapacitySold) < x.HotelRoom.InfantMaxCapacity ? (x.InfantCapacity - x.InfantCapacitySold) : x.HotelRoom.InfantMaxCapacity),
                        InfantOtherCurrencyPrice = (x.InfantOtherCurrencyPrice != null ? x.InfantOtherCurrencyPrice.Value.ToString("#") : "0"),
                        Priority = x.HotelRoom.Priority

                    }).Distinct().OrderBy(x => x.Priority.Value).ToList();
                    ViewBag.RoomType = roomTypeList;
                    ViewBag.RoomTypeList = _hotelRoomService.Filter(x => !x.IsDeleted).OrderBy(x => x.Priority).ToList();
                    #endregion
                }
            }

            ViewBag.CurrencyList = _unitOfWork.Set<Currency>().Where(x => !x.IsDeleted).ToList();

            return View(vm);
        }
        #endregion

        #region 2. TourReserve
        [Route("tour/TourReserve")]
        [HttpPost]
        public ActionResult TourReserve(TourReserveViewModel viewModel)
        {
            //کاربر باید لاگین کرده باشه
            if (User.Identity.IsAuthenticated)
            {
                //بررسی اینکه جا داریم یا نداریم؟
                //بعد تصمیم گیری در ترللو انجام می دیم
                //فعلا فرض می گیریم که جا داریم
                //کدها در اینجا . . .

                //واکشی اطلاعات اولیه لازم برای رزرو این تور
                var filledViewModel = _orderService.PreReserve(viewModel);

                //شهر مبدا و شهر مقصد
                ViewBag.DepartureCity = _cityService.Filter(x => x.Id == viewModel.TourSearchParams.DepartureCityId).FirstOrDefault().Title;
                ViewBag.ArrivalCity = _cityService.Filter(x => x.Id == viewModel.TourSearchParams.ArrivalCityId).FirstOrDefault().Title;
                var selectedhotelpackage = _hotelPackageService.Filter(x => x.Id == viewModel.SelectedHotelPackageId).FirstOrDefault();
                var hotelPackageHotelRooms = selectedhotelpackage.HotelPackageHotelRooms.ToList();

                ViewBag.CountryDDL = _countryService.GetAllCountryOfSelectListItem();

                //سن بزرگسال باید حداقل 12 سال باشد
                ViewBag.AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                var AdultStartBirthDate = DateTime.Now.AddYears(-120).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ViewBag.AdultEndBirthDate = DateTime.Now.AddYears(-12).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                //سن کودک بین 2 تا 12 سال باشد 
                ViewBag.ChildEndBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ViewBag.ChildStartBirthDate = DateTime.Now.AddYears(-11).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                //سن نوزاد باید بین 10 روز تا 2 سال باشد
                ViewBag.InfantEndBirthDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ViewBag.InfantStartBirthDate = DateTime.Now.AddYears(-2).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                //اطلاعات تور پیش فرض
                ViewBag.TourTitle = _tourService.Filter(x => x.Id == selectedhotelpackage.TourPackage.TourId).First().Title;

                //تعداد اتاق ها و انواع آن ها
                var selectedRooms = viewModel.RoomSections.Join(hotelPackageHotelRooms, jrs => jrs.SelectedRoomTypeId, jhphr => jhphr.HotelRoomId, (jrs, jhphr) => new { jrs, jhphr })
                    .GroupBy(x => x.jhphr)
                    .Select(s => new { Room = s.Key.HotelRoom.Title, Count = s.Count() })
                    .ToList();
                ViewBag.SelectedRooms = new SelectList(selectedRooms, "Count", "Room");

                //دراپ داون های مسافرین قبلی به تفکیک بزرگسال ، کودک و نوزاد
                var LoggedInUserId = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                ViewBag.AdultPreviousPassengerDDL = _passengerService.GetPreviousPassengerDDL(LoggedInUserId, AgeRange.Adult);
                ViewBag.ChildPreviousPassengerDDL = _passengerService.GetPreviousPassengerDDL(LoggedInUserId, AgeRange.Child);
                ViewBag.InfantPreviousPassengerDDL = _passengerService.GetPreviousPassengerDDL(LoggedInUserId, AgeRange.Infant);

                return View(filledViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { message = "LogIn" });
            }
        }
        #endregion

        #region 3. Confirmation
        [Route("tour/Confirmation")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Confirmation(TourReserveViewModel reserveViewModel)
        {
            #region اگر تور ایرانی است، اجباری بودن شماره پاسپورت و تاریخ انقضای پاسپورت رو در نظر نگیر
            if (!reserveViewModel.IsForeignTour)
            {
                if (reserveViewModel.PassengerList != null && reserveViewModel.PassengerList.Any())
                {
                    for (int i = 0; i < reserveViewModel.PassengerList.Count(); i++)
                    {
                        var PassportExpirationDateStr = "PassengerList[" + i.ToString() + "].PassportExpirationDate";
                        var PassportNoStr = "PassengerList[" + i.ToString() + "].PassportNo";

                        ModelState.Remove(PassportExpirationDateStr);
                        ModelState.Remove(PassportNoStr);
                    }
                }
            }
            #endregion

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    #region اطلاعات رزرو
                    //پکیج هتل انتخاب شده
                    var selectedHotelPackage = _hotelPackageService.Filter(x => x.Id == reserveViewModel.SelectedHotelPackageId).FirstOrDefault();
                    foreach (var item in reserveViewModel.PassengerList)
                    {
                        var country = _countryService.Filter(x => x.Id == item.BirthCountryId.Value).FirstOrDefault();
                        if (country != null)
                        {
                            item.BirthCountryTitle = country.Title;
                        }
                        var hotelRoomIndex = reserveViewModel.RoomSections.ElementAt(item.RoomIndex).SelectedRoomTypeId;
                        var passengerHotelRoomInfo = selectedHotelPackage.HotelPackageHotelRooms.FirstOrDefault(x => x.HotelRoomId == hotelRoomIndex);
                        switch (item.AgeRange)
                        {
                            case AgeRange.Adult:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.AdultOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.AdultPrice) : 0;
                                break;
                            case AgeRange.Child:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.ChildOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.ChildPrice) : 0;
                                break;
                            case AgeRange.Infant:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.InfantOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.InfantPrice) : 0;
                                break;
                            default:
                                item.AgeRangePriceInThisRoom = 0;
                                break;
                        }

                    }
                    ViewBag.TourReserveViewModel = reserveViewModel;
                    //ریختن اطلاعات ویوومدل در سسشن
                    //برای اینکه از هیدن-فور های زیاد اجتناب کنیم
                    Session["ReserveInformation"] = reserveViewModel;
                    #endregion

                    #region اطلاعات صفحه تایید اطلاعات
                    var selectedTour = _tourService.Filter(x => x.Id == selectedHotelPackage.TourPackage.TourId).FirstOrDefault();
                    var loggedInUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name);
                    var viewModel = new ConfirmViewModel()
                    {
                        TourTitle = selectedTour != null ? selectedTour.Title : "",
                        TourImageUrl = selectedTour != null && selectedTour.TourSliders.Any() ? selectedTour.TourSliders.FirstOrDefault().ImageUrl : ""
                    };
                    if (loggedInUser != null)
                    {
                        viewModel.VoucherReceivers = new List<VoucherReceiverViewModel>();
                        var newVoucherReciever = new VoucherReceiverViewModel()
                        {
                            FullName = loggedInUser.FirstName + " " + loggedInUser.LastName,
                            Email = loggedInUser.Email,
                            Mobile = loggedInUser.PhoneNumber
                        };
                        viewModel.VoucherReceivers.Add(newVoucherReciever);
                    }

                    #region پرواز ها
                    var airports = _unitOfWork.Set<Airport>();
                    //پروازهای رفت و برگشت انتخاب شده
                    var selectedDepartureFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == reserveViewModel.SelectedDepartureFlightId);
                    var selectedArrivalFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == reserveViewModel.SelectedArrivalFlightId);
                    viewModel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
                    {
                        airline = selectedDepartureFlight.CompanyTransfer.Title,
                        BaggageAmount = selectedDepartureFlight.BaggageAmount,
                        //FLightClass = selectedDepartureFlight.FlightClass,
                        FLightClass = selectedDepartureFlight.VehicleTypeClass.Title,
                        VehicleTypeClassCode = selectedArrivalFlight.VehicleTypeClass.Code,
                        FlightDate = selectedDepartureFlight.StartDateTime.ToString("HH:ss | dddd dd MMMM yyyy"),
                        FlightDirection = selectedDepartureFlight.FlightDirection,
                        FlightNumber = selectedDepartureFlight.FlightNumber,
                        from = selectedDepartureFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.FromAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.FromAirportId).Title + "</i>" : "",
                        to = selectedDepartureFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.DestinationAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.DestinationAirportId).Title + "</i>" : "",
                        logo = selectedDepartureFlight.CompanyTransfer.ImageUrl
                    });

                    viewModel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
                    {
                        airline = selectedArrivalFlight.CompanyTransfer.Title,
                        BaggageAmount = selectedArrivalFlight.BaggageAmount,
                        //FLightClass = selectedArrivalFlight.FlightClass,
                        FLightClass = selectedArrivalFlight.VehicleTypeClass.Title,
                        VehicleTypeClassCode = selectedArrivalFlight.VehicleTypeClass.Code,
                        FlightDate = selectedArrivalFlight.StartDateTime.ToString("HH:ss | dddd dd MMMM yyyy"),
                        FlightDirection = selectedArrivalFlight.FlightDirection,
                        FlightNumber = selectedArrivalFlight.FlightNumber,
                        from = selectedArrivalFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).Title + "</i>" : "",
                        to = selectedArrivalFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.DestinationAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).Title + "</i>" : "",
                        logo = selectedArrivalFlight.CompanyTransfer.ImageUrl
                    });
                    #endregion

                    #region تاریخ ورود و خروج هتل
                    ViewBag.CheckinDate = selectedDepartureFlight.StartDateTime.ToString("yyyy/MM/dd 14:00");
                    ViewBag.CheckoutDate = selectedArrivalFlight.StartDateTime.ToString("yyyy/MM/dd 12:00");
                    #endregion

                    #region هتل ها
                    //اطلاعات هتل های موجود با این پکیج هتل انتخاب شده
                    viewModel.HotelInfos = selectedHotelPackage.HotelPackageHotels.Select(s => new HotelClientViewModel
                    {
                        hotel = s.Hotel.Title,
                        description = s.Hotel.Summary,
                        Address = s.Hotel.City.Title,
                        location = s.Hotel.Location,
                        stars = s.Hotel.HotelRank.Title,
                        service = s.HotelBoard != null ? s.HotelBoard.Name : "",
                        ServiceTooltip = s.HotelBoard != null ? s.HotelBoard.Title : "",
                        url = JoinGetHotelUrl(s.Hotel.Id),
                        images = s.Hotel.HotelGalleries.Where(g => g.IsDeleted == false).Select(i => new ImageViewModel { ImageUrl = i.ImageUrl + i.ImageExtension }),
                        facilities = s.Hotel.HotelFacilities.Where(hf => hf.IsDeleted == false).Select(f => f.Title),
                        IsSummary = s.Hotel.IsSummary

                    }).ToList();
                    #endregion

                    #region تعداد اتاق ها و انواع آن ها
                    //تعداد اتاق ها و انواع آن ها
                    var selectedRooms = reserveViewModel.RoomSections.Join(selectedHotelPackage.HotelPackageHotelRooms, jrs => jrs.SelectedRoomTypeId, jhphr => jhphr.HotelRoomId, (jrs, jhphr) => new { jrs, jhphr })
                        .GroupBy(x => x.jhphr)
                        .Select(s => new { Room = s.Key.HotelRoom.Title, Count = s.Count() })
                        .ToList();
                    ViewBag.SelectedRooms = new SelectList(selectedRooms, "Count", "Room");
                    #endregion

                    #region تعداد مسافرین و تعداد آن ها به تفکیک رده سنی
                    ViewBag.PassengerTotalCount = reserveViewModel.RoomSections.Select(x => new { Total = x.AdultCount + x.ChildCount + x.InfantCount }).Sum(x => x.Total);
                    ViewBag.PassengerAdultCount = reserveViewModel.RoomSections.Sum(x => x.AdultCount);
                    ViewBag.PassengerChildCount = reserveViewModel.RoomSections.Sum(x => x.ChildCount);
                    ViewBag.PassengerInfantCount = reserveViewModel.RoomSections.Sum(x => x.InfantCount);
                    #endregion

                    #region محاسبه قیمت
                    /// <summary>
                    /// لیستی از اتاق ها و اطلاعات قیمت و تعداد افرادی به تفکیک رده سنی
                    /// </summary>
                    viewModel.RoomTypePriceInfos = reserveViewModel.PassengerList.GroupBy(g => new { AgeRange = g.AgeRange, RoomTypeId = reserveViewModel.RoomSections.ElementAt(g.RoomIndex).SelectedRoomTypeId })
                        .Select(x => new { AgeRangeTitle = x.Key.AgeRange.GetDisplayValue(), EnumAgeRange = x.Key.AgeRange, RoomTypeId = x.Key.RoomTypeId, Count = x.Count() })
                        .Join(selectedHotelPackage.HotelPackageHotelRooms, jg => jg.RoomTypeId, jhphr => jhphr.HotelRoomId, (jg, jhphr) => new { jg, jhphr })
                        .Select(x => new RoomTypePriceInfoViewModel()
                        {
                            RoomTypeTitle = x.jhphr.HotelRoom.Title,
                            AgeRangeTitle = x.jg.AgeRangeTitle,
                            Count = x.jg.Count,
                            Price = (x.jg.EnumAgeRange == AgeRange.Adult ? (x.jg.Count * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice)) :
                            (x.jg.EnumAgeRange == AgeRange.Child ? (x.jg.Count * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice)) :
                            (x.jg.Count * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice)))),

                        }).ToList();

                    //مجموع مبالغ انواع اتاق : Sum(مبلغ نوع اتاق * تعداد درخواستی)
                    //فقط انواع اتاقی که کاربر انتخاب کرده ، و در انواع اتاق این پکیج هتل وجود داره ، در محاسبه قیمت در نظر گرفته می شه
                    var SumTotalPrice = selectedHotelPackage.HotelPackageHotelRooms.Join(reserveViewModel.RoomSections, jhphr => jhphr.HotelRoomId, jrs => jrs.SelectedRoomTypeId, (jhphr, jrs) => new { jhphr, jrs }).Select(x => new
                    {
                        UnitPrice = (x.jrs.AdultCount * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice))
                        + (x.jrs.ChildCount * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice))
                        + (x.jrs.InfantCount * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice))

                    }).Select(x => x.UnitPrice).Sum();
                    viewModel.TotalPrice = SumTotalPrice;
                    //مبلغ تخفیف : فعلا صفر
                    viewModel.TotalDiscountPrice = 0;
                    var taxPercentage = System.Convert.ToInt32(ConfigurationManager.AppSettings["TaxPercentage"]);
                    //مبلغ مالیات بر ارزش افزوده
                    viewModel.TotalTaxPrice = (decimal)((taxPercentage * viewModel.TotalPrice) / 100);
                    //مبلغ قابل پرداخت : مبلغ مالیات + مبلغ کل
                    viewModel.TotalPayPrice = viewModel.TotalPrice + viewModel.TotalTaxPrice;
                    #endregion

                    #region اعتبار
                    var remainingCreditPrice = (loggedInUser.UserProfile != null && loggedInUser.UserProfile.RemainingCreditValue != null) ? loggedInUser.UserProfile.RemainingCreditValue.Value : 0;
                    viewModel.CreditValue = (remainingCreditPrice / 10).ToString("#,0");
                    viewModel.IsAvailableCreditPay = (viewModel.TotalPayPrice <= remainingCreditPrice);
                    #endregion

                    #endregion

                    return View(viewModel);
                }

                return RedirectToAction("Index", "", new { message = "LogIn" });
            }
            return View(reserveViewModel);
        }
        #endregion

        #region 4. TourPayment
        [Route("tour/TourPayment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TourPayment(ConfirmViewModel confirmViewModel)
        {
            //#region اگر تور ایرانی است، اجباری بودن شماره پاسپورت و تاریخ انقضای پاسپورت رو در نظر نگیر
            //if (!confirmViewModel.IsForeignTour)
            //{
            //    if (confirmViewModel.PassengerList != null && confirmViewModel.PassengerList.Any())
            //    {
            //        for (int i = 0; i < confirmViewModel.PassengerList.Count(); i++)
            //        {
            //            var PassportExpirationDateStr = "PassengerList[" + i.ToString() + "].PassportExpirationDate";
            //            var PassportNoStr = "PassengerList[" + i.ToString() + "].PassportNo";

            //            ModelState.Remove(PassportExpirationDateStr);
            //            ModelState.Remove(PassportNoStr);
            //        }
            //    }
            //}
            //#endregion

            //دریافت اطلاعات ویوومدل از سسشن
            var viewModel = Session["ReserveInformation"] as TourReserveViewModel;
            if (User.Identity.IsAuthenticated && viewModel != null && confirmViewModel.VoucherReceivers != null && confirmViewModel.VoucherReceivers.Any())
            {
                //بررسی اینکه جا داریم یا نداریم؟
                //بعد تصمیم گیری در ترللو انجام می دیم
                //فعلا فرض می گیریم که جا داریم
                //کدها در اینجا . . .

                viewModel.LoggedInUserId = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                viewModel.VoucherReceivers = confirmViewModel.VoucherReceivers;
                var couponSession = Session["CouponCode"];

                viewModel.CouponCode = (couponSession != null ? Session["CouponCode"].ToString() : "");

                bool isAllowedCreditPay = false;
                var savedOrder = _orderService.Reserve(viewModel, out isAllowedCreditPay);

                #region اگر پرداخت اعتباری رو انتخاب کرده بود و اعتبار نداشت، باید پیام بدیم
                if (confirmViewModel.IsBeCreditPay && !isAllowedCreditPay && User.Identity.IsAuthenticated)
                {
                    #region اطلاعات رزرو
                    //پکیج هتل انتخاب شده
                    var selectedHotelPackage = _hotelPackageService.Filter(x => x.Id == viewModel.SelectedHotelPackageId).FirstOrDefault();
                    foreach (var item in viewModel.PassengerList)
                    {
                        var country = _countryService.Filter(x => x.Id == item.BirthCountryId.Value).FirstOrDefault();
                        if (country != null)
                        {
                            item.BirthCountryTitle = country.Title;
                        }
                        var hotelRoomIndex = viewModel.RoomSections.ElementAt(item.RoomIndex).SelectedRoomTypeId;
                        var passengerHotelRoomInfo = selectedHotelPackage.HotelPackageHotelRooms.FirstOrDefault(x => x.HotelRoomId == hotelRoomIndex);
                        switch (item.AgeRange)
                        {
                            case AgeRange.Adult:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.AdultOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.AdultPrice) : 0;
                                break;
                            case AgeRange.Child:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.ChildOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.ChildPrice) : 0;
                                break;
                            case AgeRange.Infant:
                                item.AgeRangePriceInThisRoom = passengerHotelRoomInfo != null ? ((passengerHotelRoomInfo.InfantOtherCurrencyPrice ?? 0) * (passengerHotelRoomInfo.Currency != null ? passengerHotelRoomInfo.Currency.BaseRialPrice : 0) + passengerHotelRoomInfo.InfantPrice) : 0;
                                break;
                            default:
                                item.AgeRangePriceInThisRoom = 0;
                                break;
                        }

                    }
                    ViewBag.TourReserveViewModel = viewModel;
                    #endregion

                    #region اطلاعات صفحه تایید اطلاعات
                    var selectedTour = _tourService.Filter(x => x.Id == selectedHotelPackage.TourPackage.TourId).FirstOrDefault();
                    var loggedInUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name);
                    var reConfirmViewModel = new ConfirmViewModel()
                    {
                        TourTitle = selectedTour != null ? selectedTour.Title : "",
                        TourImageUrl = selectedTour != null && selectedTour.TourSliders.Any() ? selectedTour.TourSliders.FirstOrDefault().ImageUrl : ""
                    };
                    if (loggedInUser != null)
                    {
                        reConfirmViewModel.VoucherReceivers = new List<VoucherReceiverViewModel>();
                        var newVoucherReciever = new VoucherReceiverViewModel()
                        {
                            FullName = loggedInUser.FirstName + " " + loggedInUser.LastName,
                            Email = loggedInUser.Email,
                            Mobile = loggedInUser.PhoneNumber
                        };
                        reConfirmViewModel.VoucherReceivers.Add(newVoucherReciever);
                    }

                    #region پرواز ها
                    var airports = _unitOfWork.Set<Airport>();
                    //پروازهای رفت و برگشت انتخاب شده
                    var selectedDepartureFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == viewModel.SelectedDepartureFlightId);
                    var selectedArrivalFlight = _unitOfWork.Set<TourScheduleCompanyTransfer>().FirstOrDefault(x => x.Id == viewModel.SelectedArrivalFlightId);
                    reConfirmViewModel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
                    {
                        airline = selectedDepartureFlight.CompanyTransfer.Title,
                        BaggageAmount = selectedDepartureFlight.BaggageAmount,
                        //FLightClass = selectedDepartureFlight.FlightClass,
                        FLightClass = selectedDepartureFlight.VehicleTypeClass.Title,
                        VehicleTypeClassCode = selectedArrivalFlight.VehicleTypeClass.Code,
                        FlightDate = selectedDepartureFlight.StartDateTime.ToString("HH:ss | dddd dd MMMM yyyy"),
                        FlightDirection = selectedDepartureFlight.FlightDirection,
                        FlightNumber = selectedDepartureFlight.FlightNumber,
                        from = selectedDepartureFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.FromAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.FromAirportId).Title + "</i>" : "",
                        to = selectedDepartureFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.DestinationAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedDepartureFlight.DestinationAirportId).Title + "</i>" : "",
                        logo = selectedDepartureFlight.CompanyTransfer.ImageUrl
                    });

                    reConfirmViewModel.SelectedFlights.Add(new TourPacakgeFlightsViewModel()
                    {
                        airline = selectedArrivalFlight.CompanyTransfer.Title,
                        BaggageAmount = selectedArrivalFlight.BaggageAmount,
                        //FLightClass = selectedArrivalFlight.FlightClass,
                        FLightClass = selectedArrivalFlight.VehicleTypeClass.Title,
                        VehicleTypeClassCode = selectedArrivalFlight.VehicleTypeClass.Code,
                        FlightDate = selectedArrivalFlight.StartDateTime.ToString("HH:ss | dddd dd MMMM yyyy"),
                        FlightDirection = selectedArrivalFlight.FlightDirection,
                        FlightNumber = selectedArrivalFlight.FlightNumber,
                        from = selectedArrivalFlight.FromAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).Title + "</i>" : "",
                        to = selectedArrivalFlight.DestinationAirportId != null ? airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.DestinationAirportId).City.Title + " <i> فرودگاه " + airports.FirstOrDefault(x => x.Id == selectedArrivalFlight.FromAirportId).Title + "</i>" : "",
                        logo = selectedArrivalFlight.CompanyTransfer.ImageUrl
                    });
                    #endregion

                    #region تاریخ ورود و خروج هتل
                    ViewBag.CheckinDate = selectedDepartureFlight.StartDateTime.ToString("yyyy/MM/dd 14:00");
                    ViewBag.CheckoutDate = selectedArrivalFlight.StartDateTime.ToString("yyyy/MM/dd 12:00");
                    #endregion

                    #region هتل ها
                    //اطلاعات هتل های موجود با این پکیج هتل انتخاب شده
                    reConfirmViewModel.HotelInfos = selectedHotelPackage.HotelPackageHotels.Select(s => new HotelClientViewModel
                    {
                        hotel = s.Hotel.Title,
                        description = s.Hotel.Summary,
                        Address = s.Hotel.City.Title,
                        location = s.Hotel.Location,
                        stars = s.Hotel.HotelRank.Title,
                        service = s.HotelBoard != null ? s.HotelBoard.Name : "",
                        ServiceTooltip = s.HotelBoard != null ? s.HotelBoard.Title : "",
                        url = JoinGetHotelUrl(s.Hotel.Id),
                        images = s.Hotel.HotelGalleries.Where(g => g.IsDeleted == false).Select(i => new ImageViewModel { ImageUrl = i.ImageUrl + i.ImageExtension }),
                        facilities = s.Hotel.HotelFacilities.Where(hf => hf.IsDeleted == false).Select(f => f.Title),
                        IsSummary = s.Hotel.IsSummary

                    }).ToList();
                    #endregion

                    #region تعداد اتاق ها و انواع آن ها
                    //تعداد اتاق ها و انواع آن ها
                    var selectedRoomList = viewModel.RoomSections.Join(selectedHotelPackage.HotelPackageHotelRooms, jrs => jrs.SelectedRoomTypeId, jhphr => jhphr.HotelRoomId, (jrs, jhphr) => new { jrs, jhphr })
                        .GroupBy(x => x.jhphr)
                        .Select(s => new { Room = s.Key.HotelRoom.Title, Count = s.Count() })
                        .ToList();
                    ViewBag.SelectedRooms = new SelectList(selectedRoomList, "Count", "Room");
                    #endregion

                    #region تعداد مسافرین و تعداد آن ها به تفکیک رده سنی
                    ViewBag.PassengerTotalCount = viewModel.RoomSections.Select(x => new { Total = x.AdultCount + x.ChildCount + x.InfantCount }).Sum(x => x.Total);
                    ViewBag.PassengerAdultCount = viewModel.RoomSections.Sum(x => x.AdultCount);
                    ViewBag.PassengerChildCount = viewModel.RoomSections.Sum(x => x.ChildCount);
                    ViewBag.PassengerInfantCount = viewModel.RoomSections.Sum(x => x.InfantCount);
                    #endregion

                    #region محاسبه قیمت
                    /// <summary>
                    /// لیستی از اتاق ها و اطلاعات قیمت و تعداد افرادی به تفکیک رده سنی
                    /// </summary>
                    reConfirmViewModel.RoomTypePriceInfos = viewModel.PassengerList.GroupBy(g => new { AgeRange = g.AgeRange, RoomTypeId = viewModel.RoomSections.ElementAt(g.RoomIndex).SelectedRoomTypeId })
                        .Select(x => new { AgeRangeTitle = x.Key.AgeRange.GetDisplayValue(), EnumAgeRange = x.Key.AgeRange, RoomTypeId = x.Key.RoomTypeId, Count = x.Count() })
                        .Join(selectedHotelPackage.HotelPackageHotelRooms, jg => jg.RoomTypeId, jhphr => jhphr.HotelRoomId, (jg, jhphr) => new { jg, jhphr })
                        .Select(x => new RoomTypePriceInfoViewModel()
                        {
                            RoomTypeTitle = x.jhphr.HotelRoom.Title,
                            AgeRangeTitle = x.jg.AgeRangeTitle,
                            Count = x.jg.Count,
                            Price = (x.jg.EnumAgeRange == AgeRange.Adult ? (x.jg.Count * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice)) :
                            (x.jg.EnumAgeRange == AgeRange.Child ? (x.jg.Count * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice)) :
                            (x.jg.Count * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice)))),

                        }).ToList();

                    //مجموع مبالغ انواع اتاق : Sum(مبلغ نوع اتاق * تعداد درخواستی)
                    //فقط انواع اتاقی که کاربر انتخاب کرده ، و در انواع اتاق این پکیج هتل وجود داره ، در محاسبه قیمت در نظر گرفته می شه
                    var SumTotalPrice = selectedHotelPackage.HotelPackageHotelRooms.Join(viewModel.RoomSections, jhphr => jhphr.HotelRoomId, jrs => jrs.SelectedRoomTypeId, (jhphr, jrs) => new { jhphr, jrs }).Select(x => new
                    {
                        UnitPrice = (x.jrs.AdultCount * (((x.jhphr.AdultOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.AdultPrice))
                        + (x.jrs.ChildCount * (((x.jhphr.ChildOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.ChildPrice))
                        + (x.jrs.InfantCount * (((x.jhphr.InfantOtherCurrencyPrice ?? 0) * (x.jhphr.Currency != null ? x.jhphr.Currency.BaseRialPrice : 0)) + x.jhphr.InfantPrice))

                    }).Select(x => x.UnitPrice).Sum();
                    reConfirmViewModel.TotalPrice = SumTotalPrice;
                    //مبلغ تخفیف : فعلا صفر
                    reConfirmViewModel.TotalDiscountPrice = 0;
                    var taxPercentage = System.Convert.ToInt32(ConfigurationManager.AppSettings["TaxPercentage"]);
                    //مبلغ مالیات بر ارزش افزوده
                    reConfirmViewModel.TotalTaxPrice = (decimal)((taxPercentage * reConfirmViewModel.TotalPrice) / 100);
                    //مبلغ قابل پرداخت : مبلغ مالیات + مبلغ کل
                    reConfirmViewModel.TotalPayPrice = reConfirmViewModel.TotalPrice + reConfirmViewModel.TotalTaxPrice;
                    #endregion

                    #region اعتبار
                    var remainingCreditPrice = loggedInUser.UserProfile.RemainingCreditValue ?? 0;
                    reConfirmViewModel.CreditValue = remainingCreditPrice.ToString("#,0");
                    reConfirmViewModel.IsAvailableCreditPay = (reConfirmViewModel.TotalPayPrice <= remainingCreditPrice);
                    #endregion

                    #endregion

                    ViewBag.success = "LackOfCredit";
                    return View("Confirmation", reConfirmViewModel);
                }
                #endregion

                if (confirmViewModel.IsBeCreditPay && isAllowedCreditPay)
                {
                    //پرداخت اعتباری
                    _orderService.CreditPayment(savedOrder.Id);
                }
                else
                {
                    //ToDo : جایی که باید سرویس پرداخت آنلاین رو صدا بزنیم
                }

                #region ارسال ایمیل به دریافت کنندگان اطلاعات تور
                if (savedOrder.VoucherReceivers != null && savedOrder.VoucherReceivers.Any())
                {
                    var websiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"].ToString();
                    foreach (var person in savedOrder.VoucherReceivers)
                    {
                        dynamic emailSend = new Postal.Email("Voucher.Html");
                        emailSend.Subject = string.Format("رسید الکترونیکی {0}", savedOrder.CreatorDateTime.Value.ToString("yyyy/MM/dd"));
                        emailSend.Name = string.Format("{0}/GenerateVoucher/{1}", websiteUrl, savedOrder.TrackingCode);//person.FullName;
                        emailSend.To = person.Email;
                        emailSend.Send();
                    }
                }
                #endregion

                //ToDo: وقتی پرداخت آنلاین رو راه انداختیم کدهای زیر رو توی یه اکشن دیگه می بریم
                #region آماده سازی رسید
                var Footer = _unitOfWork.Set<ParvazPardaz.Model.Entity.Core.Footer>().Where(x => x.FooterType == EnumFooterType.Ticket).ToList();
                VoucherViewModel voucherInfo = new VoucherViewModel();

                voucherInfo = _orderService.VoucherInfo(savedOrder.TrackingCode);
                if (Footer != null)
                {
                    _mappingEngine.DynamicMap<List<ParvazPardaz.Model.Entity.Core.Footer>, List<FooterUIViewModel>>(Footer, voucherInfo.Footer);
                }
                List<VoucherBookRoomList> selectedRooms = voucherInfo.HotelRoomInfos
                    .GroupBy(x => new { Title = x.Title, TitleEn = x.TitleEn })
                    .Select(s => new VoucherBookRoomList { Title = s.Key.Title, TitleEn = s.Key.TitleEn, Count = s.Count() })
                    .ToList();
                ViewBag.SelectedRooms = selectedRooms;
                return View("Voucher", voucherInfo);
                #endregion
            }
            else
            {
                return RedirectToAction("Index", "Home", new { message = "LogIn" });
            }
        }
        #endregion

        #region GetCouponForm
        public ActionResult GetCouponForm(decimal totalPrice, decimal totalTaxPrice)
        {
            return PartialView("_PrvApplyCouponForm", new ApplyCouponViewModel() { TotalPrice = totalPrice, TotalTaxPrice = totalTaxPrice });
        }
        #endregion

        #region GetPassengersSection
        /// <summary>
        /// واکشی پارشال ویووی ورود اطلاعات مسافرین
        /// </summary>
        /// <param name="TotalAdultCount"></param>
        /// <param name="TotalChildCount"></param>
        /// <param name="TotalInfantCount"></param>
        /// <returns></returns>
        [Route("tour/GetPassengersSection")]
        public ActionResult GetPassengersSection(List<HotelRoomPassengerSectionViewModel> viewModel)//TourReserveViewModel viewModel)
        {
            ViewBag.CountryDDL = _countryService.GetAllCountryOfSelectListItem();
            var masterViewModel = new TourReserveViewModel() { hotelRoomPassengerSection = viewModel };
            return PartialView("_PrvPassengersSection", masterViewModel);
        }
        #endregion

        #region GetPassengerFilledInfoSection
        [Route("GetPassengerFilledInfoSection")]
        public ActionResult GetPassengerFilledInfoSection(string userId, AgeRange ageRange, int index, int roomIndex, string isForeignTour)
        {
            if (userId != "")
            {
                ViewBag.CountryDDL = _countryService.GetAllCountryOfSelectListItem();
                ViewBag.AgeRangeTitle = ageRange.GetDisplayValue();
                ViewBag.Index = index;
                ViewBag.RoomIndex = roomIndex;
                ViewBag.IsForeignTour = isForeignTour;
                //دراپ داون های مسافرین قبلی به تفکیک بزرگسال ، کودک و نوزاد
                var LoggedInUserId = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                ViewBag.PreviousPassengerDDL = _passengerService.GetPreviousPassengerDDL(LoggedInUserId, ageRange);
                //واکشی اطلاعات مسافر مورد نظر
                var uId = System.Convert.ToInt32(userId);
                var previousPassenger = _passengerService.Filter(x => x.Id == uId).FirstOrDefault();
                ViewBag.PassengerInfo = new AddPassengerViewModel()
                {
                    AgeRange = previousPassenger.AgeRange,
                    BirthCountryId = previousPassenger.BirthCountryId,
                    Birthdate = previousPassenger.Birthdate,
                    EnFirstName = previousPassenger.EnFirstName,
                    EnLastName = previousPassenger.EnLastName,
                    FirstName = previousPassenger.FirstName,
                    Gender = previousPassenger.Gender,
                    Id = previousPassenger.Id,
                    LastName = previousPassenger.LastName,
                    NationalCode = previousPassenger.NationalCode,
                    PassportExpirationDate = previousPassenger.PassportExpirationDate,
                    PassportExporterCountryId = previousPassenger.PassportExporterCountryId,
                    PassportNo = previousPassenger.PassportNo,
                    RoomIndex = previousPassenger.SelectedRoomId,
                };
                return PartialView("~/Views/Tour/_PrvPassengerFilledSection.cshtml");
            }
            return null;
        }

        #endregion

        #region FetchTourData
        [Route("FetchTourData")]
        public ActionResult FetchTourData(int tourId)
        {
            var tour = _tourService.Filter(x => x.Id == tourId).First();
            return PartialView("_PrvTourData", tour);
        }
        #endregion

        #region GetArrivalFlights
        [Route("tour/GetArrivalFlights")]
        public ActionResult GetArrivalFlights(string DepartureFlight)
        {
            var viewModel = new TourReserveViewModel();
            var allFlights = _unitOfWork.Set<TourScheduleCompanyTransfer>().Include(x => x.TourSchedule);
            List<int> DepartureFlightid = DepartureFlight.Split(',').Select(int.Parse).ToList();
            //واکشی پرواز رفت
            var departureFlight = allFlights.Where(x => DepartureFlightid.Any(y => y == x.Id)).AsEnumerable();
            //واکشی پروازهای برگشت مربوط به پرواز رفت
            var arrivalFlights = allFlights.Where(x => departureFlight.Any(y => y.TourScheduleId == x.TourScheduleId)
                                                       && x.FlightDirection == EnumFlightDirectionType.Back && !x.IsDeleted).ToList();
            Mapper.CreateMap<TourScheduleCompanyTransfer, FlightViewModel>();
            var _ArrivalFlights = Mapper.DynamicMap<List<TourScheduleCompanyTransfer>, List<FlightViewModel>>(arrivalFlights);
            List<FlightViewModel> _ArrivalFlightsNew = new List<FlightViewModel>();
            foreach (var item in _ArrivalFlights)
            {
                foreach (var item2 in departureFlight)
                {
                    if (item.TourScheduleId == item2.TourScheduleId)
                    {
                        item.DepartureFlightId = item2.Id;
                    }
                }
                _ArrivalFlightsNew.Add(item);
            }
            ViewBag.ArrivalFlights = _ArrivalFlightsNew;
            //واکشی عناوین پکیج های تور
            //ViewBag.TourPackageTitles = arrivalFlights.Select(x => x.TourSchedule.TourPackage.Title).Distinct().ToList();
            ViewBag.tourPackageDDL = new SelectList(arrivalFlights.Select(x => x.TourSchedule.TourPackage).Distinct().ToList(), "Id", "Title");

            return PartialView("_PrvArrivalFlights", viewModel);
        }
        #endregion

        #region FetchRoomSection
        // [Route("tour/FetchRoomSection")]
        public ActionResult FetchRoomSection(int index, int sHotelPackageId)
        {
            if (sHotelPackageId > 0)
            {
                var selectedHotelPackage = _unitOfWork.Set<HotelPackage>().FirstOrDefault(x => x.Id == sHotelPackageId);
                //ViewBag.RoomType = _hotelRoomService.GetAllHotelRoomsOfSelectListItem().Where(x => selectedHotelPackage.HotelPackageHotelRooms.Any(y => y.HotelRoomId == System.Convert.ToInt32(x.Value) && y.Price > 0)).AsEnumerable<SelectListItem>();
                //صفدری بازبینی شود
                //ViewBag.RoomType = selectedHotelPackage.HotelPackageHotelRooms.Where(x => x.Price > 0).OrderBy(x => x.HotelRoomId).Select(x => new HotelRoomDDLViewModel() { Title = x.HotelRoom.Title, Value = x.HotelRoomId.ToString(), CurrencyId = (x.OtherCurrencyId != null ? x.OtherCurrencyId.Value.ToString() : "0"), OtherCurrencyPrice = (x.OtherCurrencyPrice != null ? x.OtherCurrencyPrice.Value.ToString("#") : "0"), Price = x.Price.ToString("#") }).Distinct().ToList();

                //موقع وارد کردن تعداد بزرگسال کودک نوزاد :  
                //اگر ظرفیت قابل فروش بزرگسال [تعریف شده در پکیج هتل] کمتــر از 
                //حداکثر ظرفیت بزرگسال [تعریف شده در مدیریت اتاق] بود ؛ 
                //مشتری هنگام وارد کردن تعداد بزرگسال ، حداکثر تا ظرفیت قابل فروش بزرگسال بتونه بزنه 
                //وگرنه همون حداکثر ظرفیتی که توی مدیریت اتاق زده. برای رده های سنی دیگه هم همینطور
                //ظرفیت قابل فروش = حداکثر ظرفیت رده سنی در پکیج هتل منـــهای ظرفیت فروخته شده رده سنی
                var HotelRoomDDLVMList = selectedHotelPackage.HotelPackageHotelRooms.Where(x => (x.AdultPrice > 0 || x.ChildPrice > 0 || x.InfantPrice > 0)).OrderBy(x => x.HotelRoomId).Select(x => new HotelRoomDDLViewModel()
                {
                    Title = x.HotelRoom.Title,
                    Value = x.HotelRoomId.ToString(),
                    CurrencyId = (x.OtherCurrencyId != null ? x.OtherCurrencyId.Value.ToString() : "0"),
                    AdultPrice = x.AdultPrice.ToString("#"),
                    TotalAdultCapacity = ((x.AdultCapacity - x.AdultCapacitySold) < x.HotelRoom.AdultMaxCapacity ? (x.AdultCapacity - x.AdultCapacitySold) : x.HotelRoom.AdultMaxCapacity),
                    AdultOtherCurrencyPrice = (x.AdultOtherCurrencyPrice != null ? x.AdultOtherCurrencyPrice.Value.ToString("#") : "0"),
                    //
                    ChildPrice = x.ChildPrice.ToString("#"),
                    TotalChildCapacity = ((x.ChildCapacity - x.ChildCapacitySold) < x.HotelRoom.ChildMaxCapacity ? (x.ChildCapacity - x.ChildCapacitySold) : x.HotelRoom.ChildMaxCapacity),
                    ChildOtherCurrencyPrice = (x.ChildOtherCurrencyPrice != null ? x.ChildOtherCurrencyPrice.Value.ToString("#") : "0"),
                    //
                    InfantPrice = x.InfantPrice.ToString("#"),
                    TotalInfantCapacity = ((x.InfantCapacity - x.InfantCapacitySold) < x.HotelRoom.InfantMaxCapacity ? (x.InfantCapacity - x.InfantCapacitySold) : x.HotelRoom.InfantMaxCapacity),
                    InfantOtherCurrencyPrice = (x.InfantOtherCurrencyPrice != null ? x.InfantOtherCurrencyPrice.Value.ToString("#") : "0"),
                    Priority = x.HotelRoom.Priority

                }).Distinct().OrderBy(x => x.Priority.Value).ToList();
                ViewBag.RoomType = HotelRoomDDLVMList != null && HotelRoomDDLVMList.Any() ? HotelRoomDDLVMList : Enumerable.Empty<HotelRoomDDLViewModel>();
                ViewBag.RoomTypeList = _hotelRoomService.Filter(x => !x.IsDeleted).ToList();
                var viewmodel = new TourReserveViewModel() { RoomSectionIndex = index };
                return PartialView("_PrvRoomSection", viewmodel);
            }
            return null;
        }
        #endregion

        #region GetHotelPackages
        [Route("tour/GetHotelPackages")]
        public ActionResult GetHotelPackages(int tourPackageId)
        {
            var hotelPackageForThisTourPackage = _hotelPackageService.Filter(x => x.TourPackageId == tourPackageId && !x.IsDeleted && x.HotelPackageHotels.Any(y => y.Hotel.IsActive && !y.Hotel.IsDeleted));
            var SelectedDeparture = _tourScheduleService.Filter(x => x.TourPackageId == tourPackageId && !x.IsDeleted && x.TourScheduleCompanyTransfers.Any(y => y.FlightDirection == EnumFlightDirectionType.Go)).Select(x => new { x.TourScheduleCompanyTransfers.FirstOrDefault().FromAirportId }).FirstOrDefault();
            var SelectedDepartureFlightId = SelectedDeparture.FromAirportId;
            var viewModel = new TourReserveViewModel();

            var hotelRoomList = _hotelRoomService.GetAll().Where(x => !x.IsDeleted && x.HotelPackageHotelRooms.Any(y => y.HotelPackage.TourPackageId == tourPackageId && !y.IsDeleted)).ToList();
            var hotelPackages = hotelPackageForThisTourPackage.Select(x => new ListViewHotelPackageViewModel()
            {
                Id = x.Id,
                OrderId = x.OrderId, // ترتیب
                TourId = x.TourPackage.TourId,
                TourPackageId = x.TourPackageId,
                TourPackageTitle = x.TourPackage.Title,
                TourTitle = x.TourPackage.Tour.Title,

                hotelsInPackage = x.HotelPackageHotels.Where(y => !y.IsDeleted && y.Hotel.IsActive && y.Hotel.HotelGalleries.Any(z => !z.IsDeleted) && !y.Hotel.IsDeleted).Distinct().Select(y => new HotelsInPackageViewModel()
                {
                    Id = y.Id,
                    HotelTitle = y.Hotel.Title,
                    CityId = y.Hotel.CityId,
                    CityTitle = y.Hotel.City.Title,
                    Location = y.Hotel.Location,
                    Summary = y.Hotel.Summary,
                    HotelBoardId = y.HotelBoardId,
                    HotelId = y.HotelId,
                    TourPackageId = y.HotelPackageId,
                    HotelBoardTitle = y.HotelBoard.Name,
                    Thumbnail = y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageUrl + "-261X177" + y.Hotel.HotelGalleries.FirstOrDefault(z => z.IsPrimarySlider).ImageExtension,
                    HotelGalleryImages = y.Hotel.HotelGalleries.Where(z => !z.IsDeleted).Select(z => new { image = z.ImageUrl + "-700x525" + z.ImageExtension }).Select(z => z.image).ToList(),
                    RankLogo = y.Hotel.HotelRank.Icon,
                    HotelBoardLogo = y.HotelBoard.ImageUrl,
                    RankOrderId = y.Hotel.HotelRank.OrderId

                }).ToList(),

            }).ToList();

            foreach (var hp in hotelPackages)
            {
                //Left: HotelRoom, Right: HotelPackageHotelRooms ==> Left Outer Join
                hp.hotelRoomsInPackage = hotelRoomList.GroupJoin(hotelPackageForThisTourPackage.FirstOrDefault(x => x.Id == hp.Id).HotelPackageHotelRooms.OrderBy(y => y.Id), jhr => jhr.Id, jhphr => jhphr.HotelRoomId, (jhr, jhphr) => new { hr = jhr, hphr = jhphr })
                .SelectMany(left => left.hphr.DefaultIfEmpty(), (left, right) => new { hr = left.hr, hphr = right })
                .AsEnumerable()
               .Select(s => new HotelRoomsInPackageViewModel
               {
                   AdultPrice = (s.hphr != null ? (s.hphr.AdultPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0))) : 0),
                   ChildPrice = (s.hphr != null ? (s.hphr.ChildPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0))) : 0),
                   InfantPrice = (s.hphr != null ? (s.hphr.InfantPrice + ((s.hphr.Currency != null ? s.hphr.Currency.BaseRialPrice : 0) * (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0))) : 0),
                   AdultOtherCurrencyPrice = (s.hphr != null ? (s.hphr.AdultOtherCurrencyPrice != null ? s.hphr.AdultOtherCurrencyPrice.Value : 0) : 0),
                   ChildOtherCurrencyPrice = (s.hphr != null ? (s.hphr.ChildOtherCurrencyPrice != null ? s.hphr.ChildOtherCurrencyPrice.Value : 0) : 0),
                   InfantOtherCurrencyPrice = (s.hphr != null ? (s.hphr.InfantOtherCurrencyPrice != null ? s.hphr.InfantOtherCurrencyPrice.Value : 0) : 0),
                   OtherCurrencyId = (s.hphr != null ? (s.hphr.OtherCurrencyId != null ? s.hphr.OtherCurrencyId.Value : 0) : 0),
                   RoomTypeId = s.hr.Id,
                   Title = s.hr.Title,
                   Id = (s.hphr != null ? s.hphr.Id : 0)

               }).ToList();
            }

            //پکیج های هتل رو از طریق ویووبگ اونور می گیریم و می چینیم
            ViewBag.HotelPackages = hotelPackages;
            //کل انواع اتاق 
            ViewBag.AllHotelRooms = hotelRoomList;
            ViewBag.SelectedDepartureFlightId = SelectedDepartureFlightId;
            return PartialView("_PrvListOfHotelPackage");
        }
        #endregion

        #region LastSecondTour
        //[Route("tour/{url}-offer/")]
        public ActionResult LastSecondTour(string url)
        {
            if (!Request.Path.EndsWith("/"))
                return RedirectPermanent(Request.Url.ToString() + "/");
            if (url != null)
            {
                string myurl = string.Format("/tour/{0}-offer/", url);
                var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == myurl).FirstOrDefault(x => !x.IsDeleted);

                // اگر در لینک-تیبل بود و آدرس تور لندینگ پیج بود
                #region اگر در لینک-تیبل بود و آدرس تور لندینگ پیج بود
                if (linkModel != null && linkModel.linkType == LinkType.TourLanding)
                {
                    #region افزایش مشاهده
                    linkModel.VisitCount += 1;
                    try
                    {
                        _unitOfWork.SaveAllChanges();
                    }
                    catch (Exception)
                    {
                    }
                    #endregion

                    #region واکشی توضیحات مربوط به آدرس لیندینگ پیج این تور
                    var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == linkModel.typeId);
                    ViewBag.TourLandingPageDescription = tourLandingPageUrl != null ? tourLandingPageUrl.Description : "";
                    #endregion

                    var lastSecondTour = _unitOfWork.Set<Content>()
                        .Join(_unitOfWork.Set<LinkTable>(), c => c.TourLandingPageUrlId, lt => lt.typeId, (c, lt) => new { c, lt })
                        .FirstOrDefault(x => x.lt.linkType == LinkType.TourLanding && x.c.TourLandingPageUrlId == linkModel.typeId);

                    if (lastSecondTour != null)
                    {
                        var lastSecondTourDetail = new LastSecondTourViewModel()
                        {
                            CommentIsActive = lastSecondTour.c.CommentIsActive,
                            ContentDateTime = lastSecondTour.c.CreatorDateTime,
                            Context = lastSecondTour.c.Context,
                            Description = lastSecondTour.c.Description,
                            ImageUrl = lastSecondTour.c.ImageUrl,
                            IsActive = lastSecondTour.c.IsActive,
                            NavigationUrl = lastSecondTour.c.NavigationUrl,
                            Title = lastSecondTour.lt.Title//lastSecondTour.c.Title,
                        };

                        lastSecondTourDetail.OtherLastSecondTours = _unitOfWork.Set<Content>().Where(x => x.IsActive && !x.IsDeleted && x.Id != lastSecondTour.c.Id).OrderByDescending(x => x.CreatorDateTime).Select(z => new LastSecondTourViewModel()
                        {
                            CommentIsActive = z.CommentIsActive,
                            ContentDateTime = z.CreatorDateTime,
                            Context = z.Context,
                            Description = z.Description,
                            ImageUrl = z.ImageUrl,
                            IsActive = z.IsActive,
                            NavigationUrl = z.NavigationUrl,
                            Title = z.Title

                        }).ToList();
                        ViewBag.Title = lastSecondTour.lt.Title;
                        ViewBag.description = lastSecondTour.lt.Description;
                        ViewBag.keywords = lastSecondTour.lt.Keywords;
                        ViewBag.CustomMetaTags = lastSecondTour.lt.CustomMetaTags;

                        return View(lastSecondTourDetail);
                    }
                }
                #endregion
                #region در غیر این صورت احتمالا باید در فیلد نویگیشن-یوآرال بوده باشد
                else
                {
                    //اگر در لینک-تیبل نباشد باید در جدول لینک-ریدایرکشن نگاه کنیم و به آدرس جدید هدایت کنیم 
                    var linkRedirection301 = _unitOfWork.Set<LinkRedirection>().FirstOrDefault(x => x.OldLink == myurl);
                    if (linkRedirection301 != null)
                    {
                        return RedirectPermanent(linkRedirection301.NewLink);
                    }
                    else
                    {
                        //لاگ لینک 404
                        var notFoundLink = new NotFoundLink() { URL = myurl };
                        _unitOfWork.Set<NotFoundLink>().Add(notFoundLink);
                        _unitOfWork.SaveAllChanges();

                        //ارور 404
                        return RedirectToAction("NoData", "Blog");
                    }
                }
                #endregion
            }
            return Redirect("/");
        }
        #endregion

        #region TourData
        [Route("tour/TourData")]
        public JsonResult TourData(int tourId)
        {
            #region TourData
            var tour = _tourService.GetById(x => x.Id == tourId);

            TourDetailViewModel model = new TourDetailViewModel();

            model.id = tour.Code;
            //model.name = tour.Title;
            var tourlandingpageUrlLinkModel = _linkService.Filter(x => x.typeId == tour.TourLandingPageUrlId.Value && x.linkType == LinkType.TourLanding && !x.IsDeleted).FirstOrDefault();
            model.name = tourlandingpageUrlLinkModel != null ? tourlandingpageUrlLinkModel.Title : tour.Title;
            model.Essentials = tour.RequiredDocuments.Select(x => x.Title).ToList();
            model.description = tour.Description;
            model.images = _mappingEngine.Map(tour.TourSliders.Where(x => x.IsPrimarySlider == false && x.IsDeleted == false), model.images); //tour.TourSliders.ToList();

            model.itinerary = tour.TourPrograms.Where(tp => tp.IsDeleted == false).OrderBy(tp => tp.DayOrder).Select(tp => new TourProgramClientViewModel
            {
                name = string.Format("روز {0} : {1}", tp.DayOrder, tp.Description),// tp.Description,
                slides = tp.TourProgramDetails.Select(d => new ProgramSlideViewModel
                {
                    //عنوان اسلاید از عنوان فعالیت گرفته شود
                    ActivityTitle = d.Activity.Title,
                    description = d.Description,
                    image = d.Activity.ImageUrl,
                })
            });

            model.packages = tour.TourPackages.Where(tp => tp.IsDeleted == false).OrderBy(x => x.Priority).Select(tp => new TourPackageClientViewModel
            {
                DateTitle = tp.DateTitle,
                lowestPrice = tp.FromPrice,//tp.FromPrice != null ? Convert.ToDecimal(tp.FromPrice.Trim()).ToString("#,0") : tp.FromPrice,
                start = setArrayDay(tp.TourSchedules.Where(sc => sc.IsDeleted == false).Select(sc => sc.FromDate).ToList()),
                //start = tp.TourSchedules.Where(sc => sc.IsDeleted == false).Select(sc=>sc.FromDate)
                flights = tp.TourSchedules.Where(sc => sc.IsDeleted == false).Count() == 0 ? null : tp.TourSchedules.Where(sc => sc.IsDeleted == false).FirstOrDefault().TourScheduleCompanyTransfers.Where(c => c.IsDeleted == false).Select(f => new TourPacakgeFlightsViewModel
                {
                    name = GetCityTitleOfAirport(f.FromAirportId.Value) + "-" + GetCityTitleOfAirport(f.DestinationAirportId.Value),
                    airline = _companyTransferService.GetById(x => x.Id == f.CompanyTransferId).Title,
                    logo = _companyTransferService.GetById(x => x.Id == f.CompanyTransferId).ImageUrl,
                    from = GetCityTitleOfAirport(f.FromAirportId.Value) + " " + f.StartDateTime.TimeOfDay.ToHHMM(),
                    to = GetCityTitleOfAirport(f.DestinationAirportId.Value) + " " + f.EndDateTime.TimeOfDay.ToHHMM(),
                    duration = (f.EndDateTime - f.StartDateTime).Hours.ToString(),
                    FlightDate = f.StartDateTime.ToString("yyyy/MM/dd"),
                    BaggageAmount = f.BaggageAmount != null ? f.BaggageAmount : "",
                    FlightNumber = f.FlightNumber
                }),
                hotels = tp.HotelPackages.Where(hp => hp.IsDeleted == false).OrderBy(x => x.OrderId).Select(hip => new HotelPackageClientViewModel
                {
                    hotelInPackage = hip.HotelPackageHotels.Select(x => new { hotel = x.Hotel, hotelBoard = (x.HotelBoard != null ? x.HotelBoard.Name : ""), hotelBoardTooltip = (x.HotelBoard != null ? x.HotelBoard.Title : "") }).Where(hs => hs.hotel.IsDeleted == false && hs.hotel.IsActive && hs.hotel.HotelGalleries.Any()).OrderBy(x => x.hotel.Sort).Select(h => new HotelClientViewModel
                    {
                        hotel = h.hotel.Title,
                        description = h.hotel.Summary,
                        location = h.hotel.City.Title,
                        stars = h.hotel.HotelRank.Icon,
                        service = h.hotelBoard,
                        ServiceTooltip = h.hotelBoardTooltip,
                        url = JoinGetHotelUrl(h.hotel.Id),
                        images = h.hotel.HotelGalleries.Where(g => g.IsDeleted == false).Select(i => new ImageViewModel
                        {
                            ImageUrl = i.ImageUrl + "-700X525" + i.ImageExtension,
                        }),

                        facilities = h.hotel.HotelFacilities.Where(hf => hf.IsDeleted == false).Select(f => f.Title),

                        IsSummary = h.hotel.IsSummary
                        //images = hip.Hotels.Where(hs => hs.IsDeleted == false && hs.IsActive == true).Select(i => new ImageViewModel { 

                        //ImageUrl=i.ImageUrl,
                        //})
                    }),
                    price = hip.HotelPackageHotelRooms.OrderBy(hs => hs.HotelRoom.Priority).Where(hs => !hs.IsDeleted && !hs.HotelRoom.IsDeleted && hs.HotelRoom.IsPrimary).Select(hr => new HotelPriceViewModel
                    {
                        description = hr.HotelRoom.Title,
                        //price = hr.Price.ToString("#,0"),
                        AdultPrice = hr.AdultPrice.ToString("#,0"),
                        ChildPrice = hr.ChildPrice.ToString("#,0"),
                        InfantPrice = hr.InfantPrice.ToString("#,0"),
                        //otherCurrencyPrice = (hr.OtherCurrencyPrice != null && hr.OtherCurrencyPrice.Value > 0) ? " " + hr.OtherCurrencyPrice.Value.ToString("#,0") : "",
                        //otherCurrencyTitle = (hr.OtherCurrencyPrice != null && hr.OtherCurrencyPrice.Value > 0) ? " " + hr.Currency.Title : "",
                        AdultOtherCurrencyPrice = (hr.AdultOtherCurrencyPrice != null && hr.AdultOtherCurrencyPrice.Value > 0) ? " " + hr.AdultOtherCurrencyPrice.Value.ToString("#,0") : "",
                        AdultOtherCurrencyTitle = (hr.AdultOtherCurrencyPrice != null && hr.AdultOtherCurrencyPrice.Value > 0) ? " " + hr.Currency.Title : "",
                        ChildOtherCurrencyPrice = (hr.ChildOtherCurrencyPrice != null && hr.ChildOtherCurrencyPrice.Value > 0) ? " " + hr.ChildOtherCurrencyPrice.Value.ToString("#,0") : "",
                        ChildOtherCurrencyTitle = (hr.ChildOtherCurrencyPrice != null && hr.ChildOtherCurrencyPrice.Value > 0) ? " " + hr.Currency.Title : "",
                        InfantOtherCurrencyPrice = (hr.InfantOtherCurrencyPrice != null && hr.InfantOtherCurrencyPrice.Value > 0) ? " " + hr.InfantOtherCurrencyPrice.Value.ToString("#,0") : "",
                        InfantOtherCurrencyTitle = (hr.InfantOtherCurrencyPrice != null && hr.InfantOtherCurrencyPrice.Value > 0) ? " " + hr.Currency.Title : ""
                    })

                }),
            });
            #endregion
            var json = new JavaScriptSerializer().Serialize(model);
            return Json(json, JsonRequestBehavior.AllowGet);
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

        #region JoinGetHotelUrl
        private string JoinGetHotelUrl(int HotelId)
        {
            var hotelLink = _linkService.Filter(x => x.IsDeleted == false && x.typeId == HotelId && x.linkType == LinkType.Hotel).FirstOrDefault();
            var url = hotelLink != null ? hotelLink.URL : "";
            //var hotel = _hotelService..GetById(x => x.Id == HotelId);
            //var url= (from h in hotel
            //       join l in _unitOfWork.Set<LinkTable>().ToList()
            //           on h.Id equals l.typeId
            //       where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false
            //       )
            //var url = _hotelService.JoinHotelToLink(hotel);
            return url;
        }
        #endregion

        #region IsPassportExpirationDateValid
        public JsonResult IsPassportExpirationDateValid(string dateString = "")//TourReserveViewModel viewmodel)
        {

            ////فقط برای اولین آیتم لیست جواب می دهد
            ////موارد دیگه چون ایندکسشون بزرگتر از 0 هست و ایندکس های قبلش اینجا دریافت نمی شه نمیتونه به مقدار فعلی دسترسی داشته باشه
            //var passportExpirationDate = viewmodel.PassengerList.First().PassportExpirationDate;
            ////اگر تاریخ انقضای پاسپورت بزرگتر یا مساوی 6 ماه بعد بود مجاز محسوب شود
            //if (passportExpirationDate != null)
            //{
            //    var tempDateString = string.Format("{0}/{1}/{2}", passportExpirationDate.Value.Year.ToString(), passportExpirationDate.Value.Month.ToString("00"), passportExpirationDate.Value.Day.ToString("00"));
            //    var tempDate = DateTime.Parse(tempDateString);
            //    if (tempDate.Date >= DateTime.Now.AddMonths(6).Date)
            //    {
            //        return Json(true, JsonRequestBehavior.AllowGet);
            //    }
            //}

            try
            {
                var tempDate = DateTime.Parse(dateString.Trim());
                if (tempDate.Date >= DateTime.Now.AddMonths(6).Date)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ApplyCoupon
        [Route("tour/ApplyCoupon")]
        public ActionResult ApplyCoupon(ApplyCouponViewModel viewmodel)
        {
            if (viewmodel.Coupon != null && viewmodel.Coupon.Trim() != "")
            {
                var coupon = _couponService.Filter(x => x.Code.Equals(viewmodel.Coupon.Trim())).FirstOrDefault();
                if (coupon != null && coupon.ExpireDate >= DateTime.Now && coupon.OrderId == null)
                {
                    Session["CouponCode"] = coupon.Code;
                    var totalPrice = viewmodel.TotalPrice;
                    var totalDiscountPrice = (coupon.DiscountPercent * viewmodel.TotalPrice) / 100;
                    var totalTaxPrice = viewmodel.TotalTaxPrice;
                    var totalPayPrice = (totalPrice - totalDiscountPrice) + totalTaxPrice;
                    return Json(new { status = true, disPrice = totalDiscountPrice.ToString("#,0"), payPrice = totalPayPrice.ToString("#,0") }, JsonRequestBehavior.AllowGet);
                }
            }
            Session.Remove("CouponCode");
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCityProp
        public int GetCityProp(int? tourLandingPageUrlId)
        {
            if (tourLandingPageUrlId != null)
            {
                var tourLandingPageUrl = _tourLandingPageUrlService.Filter(x => x.Id == tourLandingPageUrlId.Value).Include(x => x.City).FirstOrDefault();
                return (tourLandingPageUrl != null ? tourLandingPageUrl.City.Id : 0);
            }
            return 0;
        }
        #endregion

        public JsonResult CreateRequest(CreateRequestViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                Request model = new Model.Entity.Tour.Request()
                {
                    AdultCount = viewModel.AdultCount,
                    ChildCount = viewModel.ChildCount,
                    InfantCount = viewModel.InfantCount,
                    RoomType = viewModel.RoomType,
                    HotelPackageTitle = viewModel.HotelPackageTitle,
                    HotelPackageId = viewModel.HotelPackageId,
                    TourPackageTitle = viewModel.TourPackageTitle,
                    TourPackageId = viewModel.TourPackageId,
                    DepartureFlightId = viewModel.DepartureFlightId,
                    ArrivalFlightId = viewModel.ArrivalFlightId,
                    TotalPrice = viewModel.TotalPrice
                };
                _unitOfWork.Set<Request>().Add(model);
                var HubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
                RequestHub HubObj = new RequestHub();
                //var RequiredId = HubObj.InvokeHubMethod();
                _unitOfWork.SaveAllChanges();
                HubObj.BroadcastData();
                //HubContext.InvokeHubMethod();
                return new JsonResult()
                {
                    Data = new
                    {
                        success = true,
                        TourPackageId = model.TourPackageId,
                        TourPackageTitle = viewModel.TourPackageTitle,
                        HotelPackageId = viewModel.HotelPackageId,
                        HotelPackageTitle = viewModel.HotelPackageTitle,
                        id = model.Id
                        //View = this.RenderPartialViewToString("_Report", viewModel)
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        success = false
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult RequestSuccess(int request)
        {
            var model = _unitOfWork.Set<Request>().FirstOrDefault(x => x.Id == request);
            var arrival = _unitOfWork.Set<TourScheduleCompanyTransfer>().AsNoTracking().FirstOrDefault(x => x.Id == model.ArrivalFlightId);
            var departure = _unitOfWork.Set<TourScheduleCompanyTransfer>().AsNoTracking().FirstOrDefault(x => x.Id == model.DepartureFlightId);

            var viewModel = new SuccessRequestViewModel()
            {
                AdultCount = model.AdultCount,
                ChildCount = model.ChildCount,
                InfantCount = model.InfantCount,
                ArrivalFlight = arrival != null ? string.Format("{0}/از {1} به مقصد {2}", arrival.FlightNumber, _unitOfWork.Set<Airport>().FirstOrDefault(x => x.Id == arrival.FromAirportId).Title, _unitOfWork.Set<Airport>().FirstOrDefault(x => x.Id == arrival.DestinationAirportId).Title) : "",
                DepartureFlight = departure != null ? string.Format("{0}/از {1} به مقصد {2}", departure.FlightNumber, _unitOfWork.Set<Airport>().FirstOrDefault(x => x.Id == departure.FromAirportId).Title, _unitOfWork.Set<Airport>().FirstOrDefault(x => x.Id == departure.DestinationAirportId).Title) : "",
                HotelPackageTitle = model.HotelPackageTitle,
                RoomType = model.RoomType,
                TourPackageTitle = model.TourPackageTitle,
                TotalCount = model.AdultCount + model.ChildCount + model.InfantCount,
                TotalPrice = model.TotalPrice
            };
            return View(viewModel);
        }

        public string GeLinkUrl(int tourPackageId)
        {
            var tourPackage = _unitOfWork.Set<TourPackage>().Include(x => x.Tour).FirstOrDefault(x => x.Id == tourPackageId);
            if (tourPackage.Tour.TourLandingPageUrlId > 0)
            {
                var tourLandUrl = _unitOfWork.Set<TourLandingPageUrl>().Include(x => x.City).FirstOrDefault(x => x.Id == tourPackage.Tour.TourLandingPageUrlId.Value);
                return string.Format("/tour/{0}-{1}?id={2}", tourLandUrl.City.State.Country.ENTitle, tourLandUrl.City.ENTitle, tourPackageId);
            }
            return "";
        }

        #region All
        [Route("tours")]
        public ActionResult All()
        {
            var viewmodel = new TourAllViewModel();

            #region اسلایدر بالای صفحه
            var sliders = _sliderService.Filter(x => !x.IsDeleted && x.ImageIsActive && x.SliderGroup.Name == "TourLandingTopSlider").ToList();
            viewmodel.TopSliders = _mappingEngine.DynamicMap<List<SlidersUIViewModel>>(sliders);
            #endregion

            #region گروه های اسلایدر و تورهای آن ها
            var sliderGroups = _sliderGroupService.Filter(x => x.Name.Equals("TourLanding") && !x.IsDeleted && x.IsActive && x.Sliders.Any(y => !y.IsDeleted && y.ImageIsActive && (y.Expirationdate != null ? EntityFunctions.TruncateTime(y.Expirationdate.Value) >= EntityFunctions.TruncateTime(DateTime.Now) : true))).OrderBy(x => x.Priority)
                .IncludeFilter(x => x.Sliders.Where(w => !w.IsDeleted && w.ImageIsActive && (w.Expirationdate != null ? System.Data.Entity.DbFunctions.TruncateTime(w.Expirationdate.Value) >= System.Data.Entity.DbFunctions.TruncateTime(DateTime.Now) : true)).ToList())
                .ToList();
            viewmodel.SliderGroups = _mappingEngine.DynamicMap<List<TourSliderGroupViewModel>>(sliderGroups);
            #endregion

            #region تورهای کشورها و شهرها
            Random rnd = new Random();
            viewmodel.SliderCountries = _tourService.Filter(x => !x.IsDeleted && x.Recomended && x.TourLandingPageUrlId > 0 && x.TourPackages.Any(y => y.TourSchedules.Any(z => DbFunctions.TruncateTime(z.FromDate) >= DbFunctions.TruncateTime(DateTime.Now)))
                && x.TourSliders.Any(y => !y.IsDeleted) && x.TourLandingPageUrlId != null && x.TourPackages.Any(p => p.TourPackgeDayId > 0))
             .Join(_tourLandingPageUrlService.Filter(x => !x.IsDeleted), jTour => jTour.TourLandingPageUrlId, jTlp => jTlp.Id, (jTour, jTlp) => new { jTour, jTlp })
             .Join(_linkService.Filter(x => !x.IsDeleted && x.Visible), j2 => j2.jTour.Id, jLink => jLink.typeId, (j2, jLink) => new { j2, jLink })
             .Select(s => new
             {
                 CountryItem = s.j2.jTlp.City.State.Country,
                 CityItem = s.j2.jTlp.City,
                 DayItems = s.j2.jTour.TourPackages.Where(p => p.TourPackgeDayId > 0).Select(p => p.TourPackageDay),
                 TourPackageItem = s.j2.jTour.TourPackages.ToList(),
                 TourLinkUrl = s.jLink.URL
             })
             .GroupBy(g => g.CountryItem).AsEnumerable().Select(z => new TourSliderCountriesUIViewModel()
             {
                 BackgroundColor = "",
                 ImageUrl = z.Key.ImageUrl,
                 Name = z.Key.ENTitle,
                 Title = string.Format("تورهای {0}", z.Key.Title),
                 TourCities = z.Select(c => new { Id = c.CityItem.Id, Title = c.CityItem.Title, Name = c.CityItem.ENTitle }).Distinct().Select(c => new TourCitiesUIViewModel() { Id = c.Id, Title = c.Title, Name = c.Name }).ToList(),
                 TourDays = z.SelectMany(d => d.DayItems).Select(d => new { Id = d.Id, Name = d.Name, Title = d.Title }).Distinct().Select(d => new TourDaysViewModel() { Id = d.Id, Name = d.Name, Title = d.Title }).ToList(),
                 TourSliders = z.SelectMany(tp => tp.TourPackageItem).Select(tp => new SlidersUITourHomeViewModel()
                 {
                     SliderGroupTitle = "",
                     Priority = tp.Priority,
                     footerLine1 = tp.Description,
                     footerLine2 = tp.FromPrice,
                     NavigationUrl = GeLinkUrl(tp.Id),
                     NavDescription = GetDepartureArrival(tp.TourSchedules.SelectMany(sch => sch.TourScheduleCompanyTransfers).FirstOrDefault(sch => sch.FlightDirection == EnumFlightDirectionType.Go)),
                     ImageURL = tp.Tour.TourSliders.OrderBy(ts => rnd.Next()).FirstOrDefault().ImageUrl,
                     ImageTitle = tp.Title,
                     HeaderDays = (tp.TourPackageDay != null ? tp.TourPackageDay.Title : ""),
                     dayId = (tp.TourPackageDay != null ? tp.TourPackageDay.Id : 0),
                     cityId = (tp.Tour.TourLandingPageUrlId != null ? GetCityProp(tp.Tour.TourLandingPageUrlId.Value) : 0)

                 }).ToList()

             }).ToList();
            #endregion

            #region Seo parameters
            //Seo parameters
            var linkModel = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.URL.Contains("tour-landing-page"));
            if (linkModel != null)
            {
                ViewBag.Title = linkModel.Title;
                ViewBag.description = linkModel.Description;
                ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                ViewBag.keywords = linkModel.Keywords;
            }
            #endregion

            ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL();
            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL();

            return View(viewmodel);
        }
        #endregion

        #region GetDepartureArrival
        public string GetDepartureArrival(TourScheduleCompanyTransfer flight)
        {
            if (flight != null)
            {
                //شرکت های هواپیمایی
                var airports = _unitOfWork.Set<Airport>().Where(x => !x.IsDeleted);
                var fromAirport = airports.FirstOrDefault(x => x.Id == flight.FromAirportId);
                var toAirport = airports.FirstOrDefault(x => x.Id == flight.DestinationAirportId);
                return string.Format("{0} > {1}", fromAirport.City.Title, toAirport.City.Title);
            }
            return "";
        }
        #endregion

        #region GenerateVoucher
        [Route("GenerateVoucher/{trackingCode}")]
        public ActionResult GenerateVoucher(string trackingCode)
        {
            VoucherViewModel voucherInfo = new VoucherViewModel();
            voucherInfo = _orderService.VoucherInfo(trackingCode);
            var Footer = _unitOfWork.Set<ParvazPardaz.Model.Entity.Core.Footer>().Where(x => x.FooterType == EnumFooterType.Ticket).ToList();
            if (Footer != null)
            {
                _mappingEngine.DynamicMap<List<ParvazPardaz.Model.Entity.Core.Footer>, List<FooterUIViewModel>>(Footer, voucherInfo.Footer);
            }
            List<VoucherBookRoomList> selectedRooms = voucherInfo.HotelRoomInfos
                .GroupBy(x => new { Title = x.Title, TitleEn = x.TitleEn })
                .Select(s => new VoucherBookRoomList { Title = s.Key.Title, TitleEn = s.Key.TitleEn, Count = s.Count() })
                .ToList();
            ViewBag.SelectedRooms = selectedRooms;
            return View(voucherInfo);
        }
        #endregion
        [Route("tour-{enTitle}")]
        public ActionResult LocationTours(string enTitle, int? id)
        {
            try
            {

                var destinationlist = enTitle.Split('-').ToList();
                if (id != null)
                {
                    return View("TourDetail", TourDetail(destinationlist[0], destinationlist[1], id.Value));
                }
                TourSearchViewModel modelSearch = new TourSearchViewModel();
                TourLocationViewModel viewmodel = new TourLocationViewModel();
                List<int> departureAirports;
                List<int> arrivalAirportIds = new List<int>();
                modelSearch.FlightDate = DateTime.Now.ToString("yyyy/MM/dd");
                modelSearch.Calendertype = "persian";
                ViewBag.Airports = _airportService.GetAllAirPortOfSelectListItem();
                ViewBag.CompanyTransfer = _companyTransferService.GetAllCompanyTransferOfSelectListItem();

                var _LandingLocation = _unitOfWork.Set<ParvazPardaz.Model.Entity.Magazine.Location>().Where(x => x.URL.Contains("/tour-" + enTitle)).FirstOrDefault();
                if (_LandingLocation != null)
                {
                    var linkModel = _unitOfWork.Set<ParvazPardaz.Model.Entity.Link.LinkTable>().Where(x => !x.IsDeleted && x.Visible && x.URL.Contains(_LandingLocation.URL)).FirstOrDefault();
                    ViewBag.Title = linkModel.Title;
                    ViewBag.description = linkModel.Description;
                    ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                    ViewBag.keywords = linkModel.Keywords;
                    viewmodel.ImageURL = _LandingLocation.ImageURL;
                }
                //Tehran Code
                modelSearch.DepartureCityId = 2;

                //شرکت های هواپیمایی
                var airports = _unitOfWork.Set<Airport>().Where(x => !x.IsDeleted);
                //شناسه شرکت های هواپیمایی که در شهر مبدا هستند؟
                departureAirports = airports.Where(x => x.CityId == modelSearch.DepartureCityId).Select(x => x.Id).ToList();
                //اگر شهر انتخاب شده است
                viewmodel.TourSearchParams = modelSearch;
                viewmodel.TourSuggestions = new List<TourSuggestionViewModel>();
                if (destinationlist.Count == 2)
                {
                    string _city = destinationlist[1];
                    var city = _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>().Where(x => x.ENTitle == _city).FirstOrDefault();
                    string _Country = destinationlist[0];

                    ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL(city.Id);
                    var cityitem = _mappingEngine.DynamicMap<ParvazPardaz.Model.Entity.Country.City, GridCityViewModel>(city);
                    List<GridCityViewModel> citmodel = new List<GridCityViewModel>();
                    citmodel.Add(cityitem);

                    if (city == null)
                    {
                        //یک شهر پیش فرض نمایش دهد 
                        //مشهد
                        //return RedirectToRoute("tour-iran-mashhad");
                    }
                    #region اطلاعات مورد نیاز
                    viewmodel.FilterType = EnumTourPackageFilterType.city;
                    modelSearch.ArrivalCityId = city.Id;
                    //var departureAirportIds=1;
                    //شناسه شرکت های هواپیمایی که در شهر مقصد هستند؟
                    arrivalAirportIds = airports.Where(x => x.CityId == modelSearch.ArrivalCityId && x.City.IsDddlDestination).Select(x => x.Id).ToList();
                    viewmodel.ToCities = citmodel;
                    departureAirports = (from ts in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                                         join s in _unitOfWork.Set<TourSchedule>()
                                         on ts.TourScheduleId equals s.Id
                                         join tp in _unitOfWork.Set<TourPackage>()
                                         on s.TourPackageId equals tp.Id
                                         join t in _unitOfWork.Set<Tour>()
                                         on tp.TourId equals t.Id
                                         where arrivalAirportIds.Any(x => x == ts.DestinationAirportId) && !ts.IsDeleted && ts.FlightDirection == EnumFlightDirectionType.Go
                                         select ts.FromAirportId.Value).Distinct().ToList();
                    viewmodel.FromCities = (from a in airports
                                            where departureAirports.Any(y => y == a.Id)
                                            select new GridCityViewModel
                                            {
                                                Id = a.CityId,
                                                Title = a.City.Title,
                                            }).Distinct().ToList();
                    ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL(2);
                    #endregion
                }
                //کشور انتخاب شده
                else
                {
                    string _Country = destinationlist[0];
                    var country = _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.Country>().Include(x => x.States).Where(x => x.ENTitle == _Country).FirstOrDefault();
                    if (country == null || destinationlist.Count > 3)
                    {
                        ///زمانی که آدرس زیر مجموعه شهر یا کشوری نباشد و بر اساس گروه تور ها بخواهیم لیندینگ بسازیم
                        ///package
                        if (_LandingLocation != null)
                        {
                            viewmodel.TourPackages = _tourScheduleService.LocationTourPackage(destinationlist[0]);

                            viewmodel.Title = _LandingLocation.Title;
                            viewmodel.SeoText = _LandingLocation.SeoText;
                            var companyTransferList1 = viewmodel.TourPackages.Select(x => new { Title = x.CompanyTransferTitle, IataCode = x.CompanyTransferIata, Id = x.CompanyTransferId }).Distinct().ToList();
                            viewmodel.CompayTransfers = (from x in companyTransferList1 select new GridCompanyTransferViewModel() { Id = x.Id, IataCode = x.IataCode, Title = x.Title }).ToList();
                            var toursDays1 = viewmodel.TourPackages.Where(x => x.PackageDayId != null).Select(x => new { Title = x.PackgeDayTitle, Id = x.PackageDayId.Value }).Distinct().ToList();
                            viewmodel.ToursDays = (from t in toursDays1 select new GridTourPackageDayViewModel { Id = t.Id, Title = t.Title }).ToList();
                            viewmodel.HotelRates = _hotelRankService.GetViewModelForGrid().ToList();
                            viewmodel.FromCities = _cityService.GetViewModelForGrid().Where(x => x.IsDddlFrom).ToList();
                            viewmodel.TourSuggestions = _tourSuggestService.GetSuggestions(_LandingLocation.Id).ToList();
                            viewmodel.FilterType = EnumTourPackageFilterType.group;
                            viewmodel.UrlName = _Country;
                            ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL(2);
                            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL(14);
                            var destinationairport = (from ts in _unitOfWork.Set<TourScheduleCompanyTransfer>()
                                                      join s in _unitOfWork.Set<TourSchedule>()
                                                      on ts.TourScheduleId equals s.Id
                                                      join tp in _unitOfWork.Set<TourPackage>()
                                                      on s.TourPackageId equals tp.Id
                                                      join t in _unitOfWork.Set<Tour>()
                                                      on tp.TourId equals t.Id
                                                      where t.PostGroups.Any(x => x.Title == _Country) && !ts.IsDeleted && ts.FlightDirection == EnumFlightDirectionType.Go
                                                      select ts.DestinationAirportId.Value).Distinct().ToList();

                            viewmodel.ToCities = (from air in _unitOfWork.Set<Airport>()
                                                  where destinationairport.Any(x => x == air.Id) && air.City.IsDddlDestination
                                                  select new GridCityViewModel
                                                  {
                                                      Id = air.CityId,
                                                      ENTitle = air.City.ENTitle,
                                                      Title = air.City.Title,
                                                      IsDddlDestination = false,
                                                      IsDddlFrom = false
                                                  }).Distinct().ToList();
                            return View(viewmodel);
                        }
                        //یک شهر پیش فرض نمایش دهد 
                        //مشهد
                        return RedirectToRoute("tour-iran-mashhad");
                    }
                    else
                    {
                        viewmodel.ToCities = _cityService.GetViewModelForGrid().Where(x => x.Country == country.Title && x.IsDddlDestination).ToList();
                        ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL(2);
                        if (viewmodel.ToCities.Count > 0)
                        {
                            ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL(viewmodel.ToCities.FirstOrDefault().Id);
                        }

                    }
                    viewmodel.FilterType = EnumTourPackageFilterType.country;

                    //_unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>().Where(x => country.States.ToList().Any(y => y.Id == x.StateId)).ToList();
                    //شناسه شرکت های هواپیمایی که در شهر مقصد هستند؟            
                    arrivalAirportIds = airports.Where(x => x.City.State.Country.Id == country.Id).Select(x => x.Id).ToList();

                    modelSearch.ArrivalCityId = 14;
                    var tocity = viewmodel.ToCities.Where(x => x.Id != 2).Select(x => x.Id).FirstOrDefault();
                    ViewBag.FormCitiesDDL = _cityService.GetAvailableFromCitiesDDL(2);
                    ViewBag.DestCitiesDDL = _cityService.GetAvailableDestCitiesDDL(tocity);

                }

                viewmodel.TourPackages = _tourScheduleService.LocationTourPackage(departureAirports, arrivalAirportIds, modelSearch.FlightDate);

                viewmodel.Title = _LandingLocation.Title;
                viewmodel.SeoText = _LandingLocation.SeoText;
                var companyTransferList = viewmodel.TourPackages.Select(x => new { Title = x.CompanyTransferTitle, IataCode = x.CompanyTransferIata, Id = x.CompanyTransferId }).Distinct().ToList();
                viewmodel.CompayTransfers = (from x in companyTransferList select new GridCompanyTransferViewModel() { Id = x.Id, IataCode = x.IataCode, Title = x.Title }).ToList();
                var toursDays = (from x in viewmodel.TourPackages where x.PackageDayId != null select new { Title = x.PackgeDayTitle, Id = x.PackageDayId.Value }).Distinct().ToList();
                viewmodel.ToursDays = (from t in toursDays select new GridTourPackageDayViewModel { Id = t.Id, Title = t.Title }).ToList();
                viewmodel.TourSuggestions = _tourSuggestService.GetSuggestions(_LandingLocation.Id).ToList();
                viewmodel.HotelRates = _hotelRankService.GetViewModelForGrid().ToList();
                if (viewmodel.FromCities == null)
                {
                    viewmodel.FromCities = _cityService.GetViewModelForGrid().Where(x => x.IsDddlFrom).ToList();
                }
                return View(viewmodel);

            }
            catch (Exception ex)
            {

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return HttpNotFound();
            }
        }
        //,int[] day
        [HttpPost]
        [Route("Tour-GetTourPackages")]
        public ActionResult GetTourPackages(TourPackageFilterViewModel viewModel, int pageNumber, int pageSize = 6)
        {
            //var destinationlist = Request.UrlReferrer.LocalPath.Split('-').ToList();
            //var _LandingLocation = _unitOfWork.Set<ParvazPardaz.Model.Entity.Magazine.Location>().Where(x => x.URL.Contains(Request.UrlReferrer.LocalPath)).FirstOrDefault();
            //if (destinationlist.Count ==3)
            //{
            //    string _city = destinationlist[2];
            //    var city = _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.City>().Where(x => x.ENTitle == _city).FirstOrDefault();
            //}
            ////کشور انتخاب شده
            //else
            //{
            //    string _Country = destinationlist[1];
            //    var country = _unitOfWork.Set<ParvazPardaz.Model.Entity.Country.Country>().Include(x => x.States).Where(x => x.ENTitle == _Country).FirstOrDefault();
            //    if (country == null)
            //    {
            //        ///زمانی که آدرس زیر مجموعه شهر یا کشوری نباشد و بر اساس گروه تور ها بخواهیم لیندینگ بسازیم
            //        if (_LandingLocation != null)
            //        {
            //            //var _locationTours = _tourScheduleService.LocationTourPackage(viewModel);
            //            //return PartialView("_PrvLocationTourPackage",_locationTours);
            //        }
            //    }               
            //    List<TourPackageLocationViewModel> viewmodel = new List<TourPackageLocationViewModel>();
            //    return PartialView("_PrvLocationTourPackage", viewmodel);
            //}
            var _locationTours = _tourScheduleService.LocationTourPackage(viewModel, pageNumber, pageSize);
            return PartialView("_PrvLocationTourPackage", _locationTours.ToList());
        }
        public ActionResult GetAllPackage(int ArrivalCityId)
        {
            var city = _cityService.GetCityNameWithCountryEN(ArrivalCityId);
            return RedirectToActionPermanent(string.Empty, "tour-" + city);
        }
    }
}