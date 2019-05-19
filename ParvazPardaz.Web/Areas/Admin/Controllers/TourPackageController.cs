using DataTables.Mvc;
using Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Users;
using ParvazPardaz.Service.Security;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourPackageController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourPackageService _tourPackageService;        
        private readonly ITourService _tourService;
        private readonly ITourPackageDayService _tourPackageDayService;
        private readonly ApplicationUserManager _ApplicationUserManager;
        //private readonly ICurrencyService _currencyService;

        private const string SectionId = "Section";
        #endregion

        #region	Ctor
        public TourPackageController(IUnitOfWork unitOfWork, ITourService tourService, ITourPackageService tourPackageService, ApplicationUserManager ApplicationUserManager, ITourPackageDayService tourPackageDayService) //, ITourScheduleService tourScheduleService, ICurrencyService currencyService
        {
            _unitOfWork = unitOfWork;
            _tourPackageService = tourPackageService;
            _tourService = tourService;
            _ApplicationUserManager = ApplicationUserManager;
            _tourPackageDayService = tourPackageDayService;
            //_currencyService = currencyService;
        }
        #endregion
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "PackageManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTourPackage([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tourPackageService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #region Create 
        public ActionResult Create(int tourId)
        {
            ViewBag.User = _tourPackageService.GetUsersSeller();
            var tour = _tourService.GetById(t => t.Id == tourId);

            //var tourLinkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tourId && x.linkType == LinkType.Tour && !x.IsDeleted);
            //ViewBag.TourURL = tourLinkTbl != null ? "/Admin" + tourLinkTbl.URL.Insert(6, "TourPreview/") : "";

            var linkTableLandingUrl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tour.TourLandingPageUrlId && x.linkType == LinkType.TourLanding);
            ViewBag.TourURL = linkTableLandingUrl != null ? linkTableLandingUrl.URL : "#";

            //ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            //var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            //ViewBag.TourPackages = new SelectList(_tourPackageService.GetAll().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
              var DDLTourPackgeDaysList = _tourPackageDayService.GetAllTourPackageDayOfSelectListItem();
            return View(new TourPackageViewModel()
            {
                TourId = tourId,
                ListView = tour.TourPackages.Where(x => !x.IsDeleted).Any() ? tour.TourPackages.Where(x => !x.IsDeleted).Select(tourPackages => GetListView(tourPackages)).OrderBy(x => x.Priority).ToList() : null,
                DDLTourPackgeDaysList = DDLTourPackgeDaysList
            });
        }

        [HttpPost]
        public ActionResult Create(TourPackageViewModel addTourPackageViewModel)
        {
            var tourSchedules = _tourPackageService.Filter(t => t.TourId == addTourPackageViewModel.TourId);
            //if (tourSchedules.Any(t => !((addTourScheduleViewModel.FromDate > t.ToDate && addTourScheduleViewModel.ToDate > t.ToDate) ||
            //                           (addTourScheduleViewModel.FromDate < t.FromDate && addTourScheduleViewModel.ToDate < t.FromDate))))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            if (ModelState.IsValid)
            {
                var model = _tourPackageService.Create<TourPackageViewModel>(addTourPackageViewModel);
               
                return PartialView("_ListViewTourPackage", GetListView(model));
            }

            //ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            //var firstTour = _tourService.GetAll().Where(x => !x.IsDeleted && x.Recomended).FirstOrDefault();
            //ViewBag.TourPackages = new SelectList(_unitOfWork.Set<TourPackage>().Where(x => !x.IsDeleted && x.TourId == firstTour.Id).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            addTourPackageViewModel.DDLTourPackgeDaysList = _tourPackageDayService.GetAllTourPackageDayOfSelectListItem();
            return PartialView("_AddTourPackage", addTourPackageViewModel);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.User = _tourPackageService.GetUsersSeller();
            var tourPackage = _tourPackageService.GetById(x => x.Id == id);
            var tour = _tourService.GetById(t => t.Id == tourPackage.TourId);
            var TourPackageDays=_tourPackageDayService.GetAllTourPackageDayOfSelectListItem();
            var editTourPackageViewModel = new TourPackageViewModel()
            {
                TourId = tourPackage.TourId,
                Title = tourPackage.Title,
                Description=tourPackage.Description,
                DateTitle = tourPackage.DateTitle,
                Code = tourPackage.Code,
                OwnerId = tourPackage.OwnerId,
                ListView = tour.TourPackages.Where(x => !x.IsDeleted).Any() ? tour.TourPackages.Where(x => !x.IsDeleted).Select(tourPackages => GetListView(tourPackages)).ToList() : null,
                CRUDMode = CRUDMode.Update,
                SectionId = SectionId + tourPackage.Id.ToString(),
                FromPrice = tourPackage.FromPrice,
                OfferPrice=tourPackage.OfferPrice,
                Priority = tourPackage.Priority,
                TourPackgeDayId=tourPackage.TourPackgeDayId,
                DDLTourPackgeDaysList = TourPackageDays
            };
            return PartialView("_AddTourPackage", editTourPackageViewModel);
        }
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "PackageManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> EditImage(int id)
        {
           
            EditTourPackageViewModel editTourPackageViewModel = await _tourPackageService.GetViewModelAsync<EditTourPackageViewModel>(x => x.Id == id);
            ViewBag.TourPackageDay=_tourPackageDayService.GetAllTourPackageDayOfSelectListItem();
            return View(editTourPackageViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> EditImage(EditTourPackageViewModel editTourPackageViewModel)
        {

            if (ModelState.IsValid)
            {
                var update = await _tourPackageService.EditAsync(editTourPackageViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }           
            return View(editTourPackageViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(TourPackageViewModel editTourPackageViewModel)
        {
            if (ModelState.IsValid)
            {
                var tourPackageVM = await _tourPackageService.UpdateAsync<TourPackageViewModel>(editTourPackageViewModel, x => x.Id == editTourPackageViewModel.Id);
                var tourPackageInDb = _tourPackageService.GetById(x => x.Id == tourPackageVM.Id);
                return PartialView("_ListViewTourPackage", GetListView(tourPackageInDb));
            }
            return null;
        }

        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourId)
        {
            bool success = false;
            var model = _tourPackageService.DeleteLogically(x => x.Id == id);
            if (model.IsDeleted)
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourId = tourId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region RefreshPartialView
        public PartialViewResult RefreshPartialView(int tourId)
        {
            ViewBag.User = _tourPackageService.GetUsersSeller();
            return PartialView("_AddTourPackage", new TourPackageViewModel()
            {
                CRUDMode = CRUDMode.Create,
                TourId = tourId,
                DDLTourPackgeDaysList=_tourPackageDayService.GetAllTourPackageDayOfSelectListItem()               
            });
        }
        #endregion

        #region PrivateMethods
        private ListViewTourPackageViewModel GetListView(TourPackage tourPackage)
        {
            //ListViewTourPackageViewModel model = new ListViewTourPackageViewModel();
            //model.Id = tourPackage.Id;
            //model.Code = tourPackage.Code;
            //model.DateTitle = tourPackage.DateTitle;
            //model.OwnerId = tourPackage.OwnerId;
            //model.OwnerTitle = "";//_ApplicationUserManager.FindUserById(tourPackage.OwnerId).FullName;
            //model.Title = tourPackage.Title;
            //model.FromPrice = tourPackage.FromPrice;
            //model.Priority = tourPackage.Priority;
            //model.SectionId = SectionId + tourPackage.Id;
            //model.TourId = tourPackage.TourId;
            //model.TourTitle = _tourService.GetById(t => t.Id == tourPackage.TourId).Title;  //tourSchedule.Tour.Title        
            //model.CountryTitle = tourPackage.HotelPackages != null ? tourPackage.HotelPackages.Select(x => x.HotelPackageHotels.Select(y => y.Hotel.City.State.Country.ENTitle).FirstOrDefault()).FirstOrDefault() : "";
            //model.CityTitle = tourPackage.HotelPackages != null ? tourPackage.HotelPackages.Select(x => x.HotelPackageHotels.Select(y => y.Hotel.City.ENTitle).FirstOrDefault()).FirstOrDefault() : "";
            //return model;
           // var TourPackageDays = _tourPackageDayService.GetById(x => x.Id == tourPackage.TourPackgeDayId).FirstOrDefault();
            return new ListViewTourPackageViewModel()
            {
                Id = tourPackage.Id,
                Code = tourPackage.Code,
                DateTitle = tourPackage.DateTitle,
                OwnerId = tourPackage.OwnerId,
                OwnerTitle = "",//_ApplicationUserManager.FindUserById(tourPackage.OwnerId).FullName,
                Title = tourPackage.Title,
                Description = tourPackage.Description,
                FromPrice = tourPackage.FromPrice,
                OfferPrice=tourPackage.OfferPrice,
                Priority = tourPackage.Priority,
                SectionId = SectionId + tourPackage.Id,
                TourId = tourPackage.TourId,
                TourTitle = _tourService.GetById(t => t.Id == tourPackage.TourId).Title,  //tourSchedule.Tour.Title               
                //CountryTitle=tourPackage.HotelPackages.Where(x=>x.TourScheduleCompanyTransfers.Where(y=>y.FlightDirection==EnumFlightDirectionType.Back).FirstOrDefault()!=null).Select(x=>x.TourScheduleCompanyTransfers.Select(y=>y.FromAirportId.Value))
                CountryTitle = tourPackage.HotelPackages != null ? tourPackage.HotelPackages.Select(x => x.HotelPackageHotels.Select(y => y.Hotel.City.State.Country.ENTitle).FirstOrDefault()).FirstOrDefault() : "",
                CityTitle = tourPackage.HotelPackages != null ? tourPackage.HotelPackages.Select(x => x.HotelPackageHotels.Select(y => y.Hotel.City.ENTitle).FirstOrDefault()).FirstOrDefault() : "",
                TourPackageDaysTitle =tourPackage.TourPackgeDayId!=null? _tourPackageDayService.GetById(x => x.Id == tourPackage.TourPackgeDayId).Title:""

            };
        }
        #endregion

        #region FindTourPackageByTourId
        public JsonResult FindTourPackageByTourId(int id)
        {
            return Json(_tourPackageService.FindTourPackageByTourId(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BatchUpdatePrice
        public ActionResult BatchUpdatePrice()
        {
            ViewBag.TourDDL = _tourService.GetTourDDL();
            return View();
        }
        #endregion

        #region GetTourPackagePricePartial
        public ActionResult GetTourPackagePricePartial(int tourPackageId)
        {
            var tourpackageInDb = _tourPackageService.GetById(x => x.Id == tourPackageId);
            var viewmodel = new TourPackageBatchUpdatePriceViewModel()
            {
                CurrencyUpdatePriceVal = 0,
                IranUpdatePriceVal = 0,
                IsUpdateCurrencyPrice = false,
                TourId = tourpackageInDb.TourId,
                TourPackageId = tourpackageInDb.Id,
                TourPacageFromPrice = tourpackageInDb.FromPrice,
                OfferPrice=tourpackageInDb.OfferPrice
            };
            return PartialView("_PrvTourPackageUpdatePrice", viewmodel);
        }
        #endregion

        #region UpdatePrices
        public ActionResult UpdatePrices(TourPackageBatchUpdatePriceViewModel viewmodel)
        {
            //واکشی پکیج تور
            var tourPackageInDb = _tourPackageService.GetById(x => x.Id == viewmodel.TourPackageId);

            //ویرایش شروع قیمت در پکیج تور
            tourPackageInDb.FromPrice = viewmodel.TourPacageFromPrice;

            //تمامی پکیج های هتل موجود در این پکیج تور
            var allHotelPackageOfThis = tourPackageInDb.HotelPackages.Where(x => !x.IsDeleted).ToList();

            if (allHotelPackageOfThis != null && allHotelPackageOfThis.Any())
            {
                foreach (var hotelpackage in allHotelPackageOfThis)
                {
                    var hotelPackageHotelRooms = hotelpackage.HotelPackageHotelRooms.Where(x => !x.IsDeleted).ToList();
                    foreach (var item in hotelPackageHotelRooms)
                    {
                        #region افزایش/کاهش عددی
                        if (viewmodel.EnumUpdatePriceType == EnumUpdatePriceType.Numeric)
                        {
                            //میزان افزایش/کاهش عددی به تومان
                            //item.Price += viewmodel.IranUpdatePriceVal;
                            item.AdultPrice += viewmodel.IranUpdatePriceVal;
                            item.ChildPrice += viewmodel.IranUpdatePriceVal;
                            item.InfantPrice += viewmodel.IranUpdatePriceVal;


                            //میزان افزایش/کاهش عددی ارزی
                            if (viewmodel.IsUpdateCurrencyPrice && viewmodel.CurrencyUpdatePriceVal != null)
                            {
                                //item.OtherCurrencyPrice += viewmodel.CurrencyUpdatePriceVal;
                                if (item.AdultOtherCurrencyPrice != null)
                                {
                                    item.AdultOtherCurrencyPrice += viewmodel.CurrencyUpdatePriceVal;
                                }
                                if (item.ChildOtherCurrencyPrice != null)
                                {
                                    item.ChildOtherCurrencyPrice += viewmodel.CurrencyUpdatePriceVal;
                                }
                                if (item.InfantOtherCurrencyPrice != null)
                                {
                                    item.InfantOtherCurrencyPrice += viewmodel.CurrencyUpdatePriceVal;
                                }
                            }
                        }
                        #endregion
                        #region افزایش/کاهش درصدی
                        else
                        {
                            //میزان افزایش/کاهش درصدی به تومان
                            //item.Price += ((viewmodel.IranUpdatePriceVal * item.Price) / 100);
                            item.AdultPrice += ((viewmodel.IranUpdatePriceVal * item.AdultPrice) / 100);
                            item.ChildPrice += ((viewmodel.IranUpdatePriceVal * item.ChildPrice) / 100);
                            item.InfantPrice += ((viewmodel.IranUpdatePriceVal * item.InfantPrice) / 100);

                            //میزان افزایش/کاهش درصدی ارزی
                            if (viewmodel.IsUpdateCurrencyPrice && viewmodel.CurrencyUpdatePriceVal != null)
                            {
                                //item.OtherCurrencyPrice += ((viewmodel.CurrencyUpdatePriceVal * item.OtherCurrencyPrice) / 100);
                                if (item.AdultOtherCurrencyPrice != null)
                                {
                                    item.AdultOtherCurrencyPrice += ((viewmodel.CurrencyUpdatePriceVal * item.AdultOtherCurrencyPrice) / 100);
                                }
                                if (item.ChildOtherCurrencyPrice != null)
                                {
                                    item.ChildOtherCurrencyPrice += ((viewmodel.CurrencyUpdatePriceVal * item.ChildOtherCurrencyPrice) / 100);
                                }
                                if (item.InfantOtherCurrencyPrice != null)
                                {
                                    item.InfantOtherCurrencyPrice += ((viewmodel.CurrencyUpdatePriceVal * item.InfantOtherCurrencyPrice) / 100);
                                }
                            }
                        }
                        #endregion
                    }
                }
            }

            var isUpdate = _unitOfWork.SaveAllChanges() > 0;
            return Json(new { status = isUpdate }, JsonRequestBehavior.AllowGet);
        }
        #endregion
      
    }
}