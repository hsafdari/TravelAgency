using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Enum;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourScheduleController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourScheduleService _tourScheduleService;
        private readonly ITourService _tourService;
        private readonly ICurrencyService _currencyService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly ITourScheduleCompanyTransferService _tourschedulecompanytransferService;
        private readonly ICityService _cityService;
        private readonly ITourPackageService _tourPackageService;
        private readonly ICompanyTransferService _companyTransferService;
        private readonly IAirportService _airportService;

        private const string SectionId = "Section";
        #endregion

        #region	Ctor
        public TourScheduleController(IUnitOfWork unitOfWork, ITourScheduleService tourScheduleService, ITourService tourService, ICurrencyService currencyService, IVehicleTypeService vehicleTypeService, ITourScheduleCompanyTransferService tourschedulecompanytransferService
            , ICityService cityService, ITourPackageService tourPackageService, ICompanyTransferService companyTransferService, IAirportService airportService)
        {
            _unitOfWork = unitOfWork;
            _tourScheduleService = tourScheduleService;
            _tourService = tourService;
            _currencyService = currencyService;
            _vehicleTypeService = vehicleTypeService;
            _tourschedulecompanytransferService = tourschedulecompanytransferService;
            _cityService = cityService;
            _tourPackageService = tourPackageService;
            _companyTransferService = companyTransferService;
            _airportService = airportService;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        //public ActionResult GetTourSchedule([DataSourceRequest]DataSourceRequest request)
        //{
        //    var query = _tourscheduleService.GetViewModelForGrid();
        //    var dataSourceResult = query.ToDataSourceResult(request);
        //    return Json(dataSourceResult);
        //}
        #endregion

        #region Create
        public PartialViewResult Create(int tourPackageId)
        {
            ViewBag.Currencies = _currencyService.GetAllCurrenciesOfSelectListItem();
            //var tour = _tourService.GetById(t => t.Id == tourPackageId);
            var tourPackage = _unitOfWork.Set<TourPackage>().FirstOrDefault(x => x.Id == tourPackageId);// _tourPackageService.GetById(t => t.Id == tourPackageId);
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.VehicleTypeClasses = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.Airport = new SelectList(new List<SelectListItem>(), "Id", "Title");

            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            var list = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return PartialView("Create", new TourScheduleViewModel()
            {
                //TourId = tourPackageId,
                //ListView = tour.TourSchedules.Any() ? tour.TourSchedules.Select(tourSchedule => GetListView(tourSchedule)).ToList() : null
                TourPackageId = tourPackageId,
                ListView = tourPackage.TourSchedules.Where(x => !x.IsDeleted).Any() ? tourPackage.TourSchedules.Where(x => !x.IsDeleted).Select(tourSchedule => GetListView(tourSchedule)).ToList() : null
            });
        }

        [HttpPost]
        public ActionResult Create(TourScheduleViewModel addTourScheduleViewModel)
        {
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            //VM.FromDate >= TDate مجاز
            //VM.ToDate   <= FDate مجاز
            // بازه های زمانی مجاز 1396/04/01 تا 1396/04/04 ••• 1396/04/04 تا 1396/04/08 ••• 1396/04/08 تا 1396/04/16
            var tourSchedules = _tourScheduleService.Filter(t => !t.IsDeleted && t.TourPackageId == addTourScheduleViewModel.TourPackageId);
            if (tourSchedules.Any(t => !((addTourScheduleViewModel.FromDate >= t.ToDate && addTourScheduleViewModel.ToDate > t.ToDate) ||
                                       (addTourScheduleViewModel.FromDate < t.FromDate && addTourScheduleViewModel.ToDate <= t.FromDate))))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var tourSchedule = _tourScheduleService.Create<TourScheduleViewModel>(addTourScheduleViewModel);
                foreach (var item in addTourScheduleViewModel.CompanyTransfer)
                {
                    item.TourScheduleId = tourSchedule.Id;
                    _tourschedulecompanytransferService.Create<TourScheduleCompanyTransferViewModel>(item);
                }
                return PartialView("_ListViewTourSchedule", GetListView(tourSchedule));
            }
            return PartialView("_AddTourSchedule", addTourScheduleViewModel);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var tourSchedule = _tourScheduleService.Filter(x => x.Id == id).Include(x => x.TourPackage).FirstOrDefault();

            //var tourSchedule = _unitOfWork.Set<TourSchedule>().Include(x => x.TourPackage).Include(x => x.Currency).FirstOrDefault(x => x.Id == id);
            var airports = _unitOfWork.Set<Airport>();
            var VehicleTypes = _vehicleTypeService.GetAll();
            var tourSchComTransfer = tourSchedule.TourScheduleCompanyTransfers.Where(x => !x.IsDeleted).Select(x => new TourScheduleCompanyTransferViewModel()
            {
                fromCityId = airports.FirstOrDefault(c => c.Id == x.FromAirportId).CityId,
                FromAirportId = x.FromAirportId.Value,
                destCityId = airports.FirstOrDefault(c => c.Id == x.DestinationAirportId).CityId,
                DestinationAirportId = x.DestinationAirportId.Value,
                DepartureDate = x.StartDateTime.Date,
                DepartureTime = x.StartDateTime.TimeOfDay,
                ArrivalDate = x.EndDateTime.Date,
                ArrivalTime = x.EndDateTime.TimeOfDay,
                //FlightClass = x.FlightClass,
                VehicleTypeClassId = x.VehicleTypeClassId !=null ? x.VehicleTypeClassId.Value : 0,
                FlightNumber = x.FlightNumber,
                Description = x.Description,
                BaggageAmount = x.BaggageAmount,
                FlightDirection = x.FlightDirection,
                //VehicleTypeId = 
                CompanyTransferId = x.CompanyTransferId,
                VehicleTypeId = (x.CompanyTransfer.CompanyTransferVehicleTypes.FirstOrDefault() != null ? x.CompanyTransfer.CompanyTransferVehicleTypes.FirstOrDefault().VehicleTypeId : 0) //x.CompanyTransfer.CompanyTransferVehicleTypes.FirstOrDefault().VehicleTypeId//VehicleTypes.FirstOrDefault(w => w.CompanyTransferVehicleTypes.Any(v => v.Id == x.CompanyTransferId)).Id

            }).ToList();

            var tourPackage = _tourPackageService.GetById(t => t.Id == tourSchedule.TourPackageId);
            ViewBag.Currencies = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(_companyTransferService.GetAllCompanyTransferOfSelectListItem(), "Value", "Text");
            ViewBag.VehicleTypeClasses = new SelectList(_unitOfWork.Set<VehicleTypeClass>().Where(x => !x.IsDeleted).ToList(), "Id", "Title");
            ViewBag.Airport = new SelectList(_airportService.GetAllAirPortOfSelectListItem(), "Value", "Text");

            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            //var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.Filter(x => !x.IsDeleted && x.TourId == tourSchedule.TourPackage.TourId).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");


            var editTourScheduleViewModel = new TourScheduleViewModel()
            {
                FromDate = tourSchedule.FromDate,
                ToDate = tourSchedule.ToDate,
                ExpireDate = tourSchedule.ExpireDate,
                Capacity = tourSchedule.Capacity,
                TourPackageId = tourSchedule.TourPackageId,
                ListView = tourPackage.TourSchedules.Where(x => !x.IsDeleted).Any() ? tourPackage.TourSchedules.Where(x => !x.IsDeleted).Select(tSch => GetListView(tourSchedule)).ToList() : new List<ListViewTourScheduleViewModel>(),
                CompanyTransfer = tourSchedule.TourScheduleCompanyTransfers.Where(x => !x.IsDeleted).Any() ? tourSchComTransfer : new List<TourScheduleCompanyTransferViewModel>(),
                CRUDMode = CRUDMode.Update,
                SectionId = SectionId + tourSchedule.Id.ToString(),
            };

            return PartialView("_AddTourSchedule", editTourScheduleViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TourScheduleViewModel editTourScheduleViewModel)
        {
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            //var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == editTourScheduleViewModel.TourId).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");



            if (ModelState.IsValid)
            {
                var model = _tourScheduleService.UpdateTourSchedule(editTourScheduleViewModel);
                return PartialView("_ListViewTourSchedule", GetListView(model));
            }

            ViewBag.Currencies = _currencyService.GetAllCurrenciesOfSelectListItem();
            return PartialView("_AddTourSchedule", editTourScheduleViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourId)
        {
            bool success = false;
            var model = _tourScheduleService.DeleteLogically(x => x.Id == id);
            if (model.IsDeleted)
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourId = tourId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region RefreshPartialView
        public PartialViewResult RefreshPartialView(int TourPackageId)
        {

            ViewBag.Currencies = _currencyService.GetAllCurrenciesOfSelectListItem();
            //var tourPackage = _tourPackageService.GetById(t => t.Id == TourPackageId);
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");            
            ViewBag.VehicleTypeClasses = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.Airport = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            var tourPackage = _tourPackageService.Filter(x => x.Id == TourPackageId).FirstOrDefault();
            ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == tourPackage.TourId).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return PartialView("_AddTourSchedule", new TourScheduleViewModel()
            {
                CRUDMode = CRUDMode.Create,
                TourPackageId = TourPackageId,
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
                ExpireDate = DateTime.Now
            });
        }
        #endregion

        #region PrivateMethods
        private ListViewTourScheduleViewModel GetListView(TourSchedule tourSchedule)
        {
            var a = tourSchedule.TourScheduleCompanyTransfers.Where(x => x.IsDeleted == false).ToList();
            var q = new ListViewTourScheduleViewModel()
            {
                TourPackageId = tourSchedule.TourPackageId,
                TourPackageTitle = _tourPackageService.GetById(x => x.Id == tourSchedule.TourPackageId).Title,
                FromDate = tourSchedule.FromDate.ConvertToStringLongDate(),
                ToDate = tourSchedule.ToDate.ConvertToStringLongDate(),
                ExpireDate = tourSchedule.ExpireDate.ConvertToStringLongDate(),
                Capacity = tourSchedule.Capacity,
                Id = tourSchedule.Id,
                //Currency = tourSchedule.Currency.Title,
                SectionId = SectionId + tourSchedule.Id,
                //TourId = tourSchedule.TourPackageId,
                NonLimit = tourSchedule.NonLimit,
                CompanyTransfers = tourSchedule.TourScheduleCompanyTransfers.Where(x => x.IsDeleted == false).Select(c => new ListViewTourScheduleCompanyTransferViewModel
                {
                    CompanyTransferTitle = _companyTransferService.Filter(x => x.Id == c.CompanyTransferId).FirstOrDefault().Title,
                    DestinationAirportTitle = c.DestinationAirportId == null ? "" : _airportService.GetById(x => x.Id == c.DestinationAirportId.Value).Title,
                    FromAirportTitle = c.FromAirportId == null ? "" : _airportService.GetById(x => x.Id == c.FromAirportId.Value).Title,
                    EndDateTime = c.EndDateTime.ConvertToStringLongDateTime(),
                    StartDateTime = c.StartDateTime.ConvertToStringLongDateTime(),
                    //FlightClass = c.FlightClass,
                    VehicleTypeClassTitle = (c.VehicleTypeClass != null ? c.VehicleTypeClass.Title : ""),
                    FlightNumber = c.FlightNumber,
                    Description = c.Description,
                    BaggageAmount = c.BaggageAmount,
                    FlightDirection = c.FlightDirection
                    //DurationTime = c.DurationTime.HasValue ? c.DurationTime.Value.ConvertToStringLongDate() : string.Empty,

                }).ToList(),
                //Price = tourSchedule.Price.ConvertToStringCurrency(),
                //Price = tourSchedule.Price.ToString("0,0 ") + tourSchedule.Currency.Title,
                //TourTitle = _tourService.GetById(t => t.Id == tourSchedule.TourPackageId).Title  //tourSchedule.Tour.Title
            };
            return q;
        }
        #endregion

        #region GetCoTransferPartial
        [HttpGet]
        public PartialViewResult GetCoTransferPartial(TourScheduleViewModel coTransfer)
        {
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.VehicleTypeClasses = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.Airport = new SelectList(new List<SelectListItem>(), "Id", "Title");
            coTransfer.CRUDMode = CRUDMode.Create;
            return PartialView("~/Areas/Admin/Views/TourSchedule/_PrvCompanyTransfer.cshtml", coTransfer);
        }
        #endregion

        #region FetchCoTransferPartial
        [HttpPost]
        public PartialViewResult FetchCoTransferPartial(int selectedPackageId) //PartialViewResult
        {
            //پکیج تور انتخاب شده
            var selectedTourPackage = _tourPackageService.Filter(x => x.Id == selectedPackageId);

            //TourScheduleCompanyTransfers : مقاصد
            //TourSchedule : زمانبندی سفر
            //واکشی مقاصد مربوط به زمانبندی های سفر مربوط به پکیج تور انتخاب شده
            var airports = _unitOfWork.Set<Airport>();
            var VehicleTypes = _vehicleTypeService.GetAll();
            var CoTransferList = selectedTourPackage.SelectMany(x => x.TourSchedules.Where(y => !y.IsDeleted).SelectMany(y => y.TourScheduleCompanyTransfers.Where(z => !z.IsDeleted))).AsEnumerable().Select(cp => new TourScheduleCompanyTransferViewModel()
            {
                fromCityId = airports.FirstOrDefault(c => c.Id == cp.FromAirportId).CityId,
                FromAirportId = cp.FromAirportId.Value,
                destCityId = airports.FirstOrDefault(c => c.Id == cp.DestinationAirportId).CityId,
                DestinationAirportId = cp.DestinationAirportId.Value,
                DepartureDate = cp.StartDateTime.Date,
                DepartureTime = cp.StartDateTime.TimeOfDay,
                ArrivalDate = cp.EndDateTime.Date,
                ArrivalTime = cp.EndDateTime.TimeOfDay,
                //FlightClass = cp.FlightClass,
                VehicleTypeClassId = cp.VehicleTypeClassId.Value,
                FlightNumber = cp.FlightNumber,
                Description = cp.Description,
                BaggageAmount = cp.BaggageAmount,
                //VehicleTypeId = 
                CompanyTransferId = cp.CompanyTransferId,
                VehicleTypeId = VehicleTypes.FirstOrDefault(w => w.CompanyTransferVehicleTypes.Any(v => v.Id == cp.CompanyTransferId)).Id,
                FlightDirection = (EnumFlightDirectionType)cp.FlightDirection

            }).ToList();

            //پر کردن پارشال-ویووی لیست مقاصد و نشاندن آن در قسمت بادی تیبل مقاصد
            var tourSch = new TourScheduleViewModel() { CompanyTransfer = CoTransferList };

            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.VehicleTypeClasses = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.Airport = new SelectList(new List<SelectListItem>(), "Id", "Title");
            ViewBag.Currencies = _currencyService.GetAllCurrenciesOfSelectListItem();
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.FromCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.DestinationCity = _cityService.GetAllCityOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(_companyTransferService.GetAllCompanyTransferOfSelectListItem(), "Value", "Text");
            ViewBag.Airport = new SelectList(_airportService.GetAllAirPortOfSelectListItem(), "Value", "Text");

            return PartialView("~/Areas/Admin/Views/TourSchedule/_PrvCompanyTransferList.cshtml", tourSch);
        }
        #endregion
    }
}
