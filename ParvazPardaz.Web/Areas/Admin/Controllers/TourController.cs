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
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Post;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourService _tourService;
        private readonly ITourCategoryService _tourCategoryService;
        private readonly ITourLevelService _tourLevelService;
        private readonly ITourTypeService _tourTypeService;
        private readonly IAllowedBannedService _allowedBannedService;
        private readonly ITourPackageService _tourPackageService;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly ITourLandingPageUrlService _tourLandingPageUrlService;
        private readonly IPostGroupService _postGroupService;
        #endregion

        #region	Ctor
        public TourController(IUnitOfWork unitOfWork, ITourService tourService, ITourCategoryService tourCategoryService, ITourLevelService tourLevelService
                            , ITourTypeService tourTypeService, IAllowedBannedService allowedBannedService, ITourPackageService tourPackageService, ICountryService countryService, ITourLandingPageUrlService tourLandingPageUrlService, ICityService cityService, IStateService stateService, IPostGroupService postGroupService)
        {
            _unitOfWork = unitOfWork;
            _tourService = tourService;
            _tourCategoryService = tourCategoryService;
            _tourLevelService = tourLevelService;
            _tourTypeService = tourTypeService;
            _allowedBannedService = allowedBannedService;
            _tourPackageService = tourPackageService;
            _countryService = countryService;
            _tourLandingPageUrlService = tourLandingPageUrlService;
            _cityService = cityService;
            _stateService = stateService;
            _postGroupService = postGroupService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTour([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tourService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Tour's Code validation
        /// <summary>
        /// اعتبارسنجی کد تور
        /// </summary>
        /// <param name="Code">کد تور</param>
        /// <returns>آیا کد تور تکراری است؟</returns>
        public JsonResult IsUniqueTourCode(string Code)
        {
            if (_tourService.GetAll().Any(t => t.Code == Code.Trim()))
            {
                return Json("کد تور تکراری می باشد", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "TourManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create()
        {
            AddTourViewModel addTourViewModel = new AddTourViewModel();
            addTourViewModel.CRUDMode = CRUDMode.Create;
            addTourViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            addTourViewModel._selectedPostGroups = new List<int>();
            addTourViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            addTourViewModel.CityDDL = new List<SelectListItem>();
            //addTourViewModel.TourLandingPageUrlDDL = new List<SelectListItem>();

            ViewBag.TourCategories = _tourCategoryService.GetAllTourCategoriesOfSelectListItem();
            ViewBag.TourLevels = _tourLevelService.GetAllTourLevelsOfSelectListItem();
            ViewBag.TourTypes = _tourTypeService.GetAllTourTypesOfSelectListItem();
            ViewBag.AllowedBans = _allowedBannedService.GetAllAllowBansOfSelectListItem();
            ViewBag.RequiredDocuments = new SelectList(_unitOfWork.Set<RequiredDocument>().Where(t => !t.IsDeleted).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title");

            return View(addTourViewModel);
        }

        [HttpPost]
        public ActionResult Create(AddTourViewModel viewModel, string command)
        {
            if (_tourService.GetAll().Any(t => t.Code == viewModel.Code.Trim()))
            {
                ModelState.AddModelError("Code", "کد تور تکراری می باشد");
            }

            //if (viewModel.Recomended && (viewModel.TourLandingPageUrlId <= 0 || viewModel.TourLandingPageUrlId == null))
            //{
            //    ModelState.AddModelError("Recomended", ParvazPardaz.Resource.Tour.Tours.UrlRequired);
            //}
            //else if (!viewModel.Recomended)
            //{
            //    viewModel.TourLandingPageUrlId = 0;
            //}

            if (ModelState.IsValid)
            {
                var newTour = _tourService.CreateTour(viewModel);
                if (command == ParvazPardaz.Resource.General.Generals.Next || newTour != null)
                {
                    return RedirectToAction("Create", "TourSlider", new { tourId = newTour.Id });
                }
                else if (command == ParvazPardaz.Resource.General.Generals.Finish)
                {
                    return RedirectToAction("Index", "Tour");
                }
            }

            viewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            ViewBag.TourCategories = _tourCategoryService.GetAllTourCategoriesOfSelectListItem();
            ViewBag.TourLevels = _tourLevelService.GetAllTourLevelsOfSelectListItem();
            ViewBag.TourTypes = _tourTypeService.GetAllTourTypesOfSelectListItem();
            ViewBag.AllowedBans = _allowedBannedService.GetAllAllowBansOfSelectListItem();
            ViewBag.RequiredDocuments = new SelectList(_unitOfWork.Set<RequiredDocument>().Where(t => !t.IsDeleted).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title");

            viewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            viewModel.CityDDL = new List<SelectListItem>();
           // viewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
            return View(viewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditTourViewModel viewModel = await _tourService.GetViewModelAsync<EditTourViewModel>(x => x.Id == id);
            ViewBag.TourCategories = _tourCategoryService.GetAllTourCategoriesOfSelectListItem();
            ViewBag.TourLevels = _tourLevelService.GetAllTourLevelsOfSelectListItem();
            ViewBag.TourTypes = _tourTypeService.GetAllTourTypesOfSelectListItem();
            ViewBag.AllowedBans = _allowedBannedService.GetAllAllowBansOfSelectListItem();
            viewModel.CRUDMode = CRUDMode.Update;
            viewModel.Recomended = _tourService.Filter(x => x.Id == id).FirstOrDefault().Recomended;

            //viewModel.LinkTableTitle = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == id && x.linkType == LinkType.Tour).URL;           

            viewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            var model = _tourService.GetById(x => x.Id == id);
            viewModel._selectedPostGroups = model.PostGroups.Select(z => z.Id).ToList();
            ViewBag.RequiredDocuments = new MultiSelectList(_unitOfWork.Set<RequiredDocument>().Where(t => !t.IsDeleted).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title", model.RequiredDocuments.Select(i => i.Id).ToList());

            #region پر کردن دراپ داون ها
           // var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == viewModel.TourLandingPageUrlId);
           // var selectedCityId = (tourLandingPageUrl != null ? tourLandingPageUrl.CityId : 0);
            var selectedCountryId = 0;
            //if (tourLandingPageUrl != null)
            //{
            //    var city = _cityService.GetById(x => x.Id == selectedCityId);
            //    var state = _stateService.GetById(x => x.Id == city.StateId);
            //    selectedCountryId = state.CountryId;

            //    viewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCountryId), Text = c.Title, Value = c.Id.ToString() });
            //    viewModel.CityDDL = _cityService.Filter(c => !c.IsDeleted && c.StateId == state.Id).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCityId), Text = c.Title, Value = c.Id.ToString() });
            //    viewModel.TourLandingPageUrlDDL = _tourLandingPageUrlService.Filter(x => !x.IsDeleted && x.CityId == selectedCityId).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == tourLandingPageUrl.Id), Text = s.URL, Value = s.Id.ToString() });
            //}
            //else
            //{
                viewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
                viewModel.CityDDL = new List<SelectListItem>();
                //viewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
            //}
            #endregion

            //var tourLinkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == id && x.linkType == LinkType.Tour && !x.IsDeleted);
            //ViewBag.TourURL = tourLinkTbl != null ? "/Admin" + tourLinkTbl.URL.Insert(6, "TourPreview/") : ""; //string.Format("/Tour/{0}/", viewModel.Title.Replace(" ", "-"));

            //var linkTableLandingUrl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == viewModel.TourLandingPageUrlId && x.linkType == LinkType.TourLanding);
            //ViewBag.TourURL = linkTableLandingUrl != null ? linkTableLandingUrl.URL : "#";
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditTourViewModel viewModel, string command)
        {
            if (_tourService.GetAll().Any(t => t.Code == viewModel.Code.Trim() && t.Id != viewModel.Id))
            {
                ModelState.AddModelError("Code", "کد تور تکراری است");
            }

            //if (viewModel.Recomended && (viewModel.TourLandingPageUrlId <= 0 || viewModel.TourLandingPageUrlId == null))
            //{
            //    ModelState.AddModelError("Recomended", ParvazPardaz.Resource.Tour.Tours.UrlRequired);
            //}
            //else if (!viewModel.Recomended)
            //{
            //    viewModel.TourLandingPageUrlId = 0;
            //}

            if (ModelState.IsValid)
            {
                var model = _tourService.UpdateTour(viewModel);
                if (command == ParvazPardaz.Resource.General.Generals.Next || command == null)
                {
                    ViewBag.RequiredDocuments = new MultiSelectList(_unitOfWork.Set<RequiredDocument>().Where(t => !t.IsDeleted).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title", model.RequiredDocuments.Select(i => i.Id).ToList());
                    return RedirectToAction("Create", "TourSlider", new { tourId = model.Id });
                }
                else if (command == ParvazPardaz.Resource.General.Generals.Finish)
                {
                    return RedirectToAction("Index", "Tour");
                }
            }

            #region پر کردن دراپ داون ها
            //var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == viewModel.TourLandingPageUrlId);
            //var selectedCityId = (tourLandingPageUrl != null ? tourLandingPageUrl.CityId : 0);
            //var selectedCountryId = 0;
            //if (tourLandingPageUrl != null)
            //{
            //    var city = _cityService.GetById(x => x.Id == selectedCityId);
            //    var state = _stateService.GetById(x => x.Id == city.StateId);
            //    selectedCountryId = state.CountryId;

            //    viewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCountryId), Text = c.Title, Value = c.Id.ToString() });
            //    viewModel.CityDDL = _cityService.Filter(c => !c.IsDeleted && c.StateId == state.Id).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCityId), Text = c.Title, Value = c.Id.ToString() });
            //    viewModel.TourLandingPageUrlDDL = _tourLandingPageUrlService.Filter(x => !x.IsDeleted && x.CityId == selectedCityId).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == tourLandingPageUrl.Id), Text = s.URL, Value = s.Id.ToString() });
            //}
            //else
            //{
                viewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
                viewModel.CityDDL = new List<SelectListItem>();
              //  viewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
           // }
            #endregion

            ViewBag.TourCategories = _tourCategoryService.GetAllTourCategoriesOfSelectListItem();
            ViewBag.TourLevels = _tourLevelService.GetAllTourLevelsOfSelectListItem();
            ViewBag.TourTypes = _tourTypeService.GetAllTourTypesOfSelectListItem();
            ViewBag.AllowedBans = _allowedBannedService.GetAllAllowBansOfSelectListItem();
            ViewBag.RequiredDocuments = new MultiSelectList(_unitOfWork.Set<RequiredDocument>().Where(t => !t.IsDeleted).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title", viewModel.RequiredDocumentIds.ToList());

            return View("Edit", viewModel);
        }
        #endregion

        #region IsUrlSelected
        public JsonResult IsUrlSelected(bool Recomended, Nullable<int> TourLandingPageUrlId)
        {
            if (Recomended && TourLandingPageUrlId != null && TourLandingPageUrlId > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tourService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region CheckURLInLinkTable
        public JsonResult CheckURLInLinkTable(string LinkTableTitle)
        {
            var check = _tourService.CheckURLInLinkTable(LinkTableTitle);

            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("عنوان تکراری است", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region TourPreview
        //
        // GET: /Tour/
        public ActionResult TourPreview(string url)
        {
            if (!Request.Path.EndsWith("/"))
                return RedirectPermanent(Request.Url.ToString() + "/");
            if (url != null)
            {
                string myurl = "/tour/" + url + "/";
                var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == myurl).FirstOrDefault(x => !x.IsDeleted); // && x.linkType == LinkType.Tour && x.IsDeleted == false
                if (linkModel != null)
                {
                    #region اگر در لینک.تیبل بود
                    if (linkModel.linkType == LinkType.TourLanding)
                    {
                        //تورهای مرتبط
                        List<HotelDetailsViewModel> relatedTours = new List<HotelDetailsViewModel>();
                        //تور فعلی
                        var currentTour = _unitOfWork.Set<Tour>().FirstOrDefault(x => x.TourLandingPageUrlId == linkModel.typeId && x.Recomended);
                        if (currentTour != null)
                        {
                            var currentTourGroups = currentTour.PostGroups.ToList();

                            #region واکشی تورهای مرتبط

                            Random rnd = new Random(DateTime.Now.Millisecond);

                            //تمامی تور های مرتبط
                            var allRelatedTours = currentTourGroups.SelectMany(x => x.Tours.ToList()).Distinct().ToList();

                            //واکشی تصویری خاص از هر کدام تور ها
                            foreach (var t in allRelatedTours)
                            {
                                var tour = _unitOfWork.Set<Tour>().Include(x => x.TourSliders).FirstOrDefault(x => x.Id == t.Id);

                                //اولین تصویر تور
                                var img = tour.TourSliders.FirstOrDefault(x => x.IsPrimarySlider);
                                //آدرس تصویر را موقتا در فیلد [کد] نگه داشتیم 
                                t.Code = img != null ? img.ImageUrl : "";
                            }


                            relatedTours = (from p in allRelatedTours
                                            join l in _unitOfWork.Set<LinkTable>().ToList()
                                                on p.Id equals l.typeId
                                            where l.linkType == LinkType.Tour && l.Visible == true && l.IsDeleted == false
                                            select new HotelDetailsViewModel
                                            {
                                                CarouselTitle = "تورهای مرتبط",
                                                AccessLevel = AccessLevel.general,
                                                Id = p.Id,
                                                PostContent = p.Description,
                                                PostSummery = p.ShortDescription,
                                                PublishDatetime = p.CreatorDateTime.Value,
                                                VisitCount = l.VisitCount,
                                                Thumbnail = p.Code,
                                                ThumbnailName = p.Title,
                                                MetaDescription = l.Description,
                                                MetaKeywords = l.Keywords,
                                                Name = l.Name,
                                                Rel = l.Rel,
                                                Target = l.Target,
                                                Title = l.Title,
                                                URL = l.URL,
                                                CommentCount = 0
                                            }

                             ).AsEnumerable().Select(item => new { item, order = rnd.Next() }).OrderByDescending(x => x.item.ExpireDatetime).OrderBy(x => x.order).Select(x => x.item).Take(10).ToList();

                            #endregion

                            ViewBag.Id = currentTour.Id;//linkModel.typeId;
                            ViewBag.PageTitle = linkModel.Title;
                            ViewBag.description = linkModel.Description;
                            ViewBag.keywords = linkModel.Keywords;
                            ViewBag.IsHideDetail = (linkModel.IsDeleted || !linkModel.Visible || !currentTour.Recomended);

                            #region واکشی توضیحات مربوط به آدرس لیندینگ پیج این تور
                            var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == currentTour.TourLandingPageUrlId);
                            ViewBag.TourLandingPageDescription = tourLandingPageUrl != null ? tourLandingPageUrl.Description : "";
                            #endregion


                            return View("TourPreview");
                        }
                        else
                        {
                            //ارور 404
                            return RedirectToAction("NoData", "Blog");
                        }
                    }
                    #endregion
                }
                else
                {
                    //ارور 404
                    return RedirectToAction("NoData", "Blog");
                }
            }
            return Redirect("/");
        }
        #endregion

        #region Update priority of tours
        #region TourPriority
        public ActionResult TourPriority()
        {
            var viewmodel = new TourPriorityViewModel()
            {
                TourGroupMSL = _postGroupService.GetTourGroupMSL()
            };
            return View(viewmodel);
        }
        #endregion

        #region FetchToursByGroup
        public ActionResult FetchToursByGroup(TourPriorityViewModel viewModel)
        {
            viewModel.Tours = _tourService.Filter(x => !x.IsDeleted && x.Recomended && x.PostGroups.Any(y => viewModel.SelectedTourGroupIds.Contains(y.Id))).Distinct().OrderBy(x => x.Priority).ToList();
            return PartialView("_PrvToursPriority", viewModel);
        }
        #endregion

        #region UpdateTourPriority
        public ActionResult UpdateTourPriority(TourPriorityViewModel viewModel)
        {
            bool isUpdate = false;
            ModelState.Remove("SelectedTourGroupIds");
            ModelState.Remove("TourGroupMSL");
            if (ModelState.IsValid)
            {
                if (viewModel.Tours != null && viewModel.Tours.Any())
                {
                    foreach (var t in viewModel.Tours)
                    {
                        var tourInDb = _tourService.GetById(x => x.Id == t.Id);
                        tourInDb.Priority = t.Priority ?? 0;
                    }
                    isUpdate = _unitOfWork.SaveAllChanges() > 0;
                }
            }
            return Json(new { status = isUpdate }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetTourPriorityStatus
        public ActionResult GetTourPriorityStatus()
        {
            return PartialView("_PrvTourPriorityStatus", true);
        }
        #endregion
        #endregion
    }
}
