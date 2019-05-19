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
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Content;
using System.IO;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Model.Enum;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class ContentController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContentService _contentService;
        private readonly IContentGroupService _contentGroupService;
        private readonly ICacheService _cacheService;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly ITourLandingPageUrlService _tourLandingPageUrlService;
        #endregion

        #region	Ctor
        public ContentController(IUnitOfWork unitOfWork, IContentService contentService, IContentGroupService contentGroupService, ICacheService cacheService, ICountryService countryService, IStateService stateService, ICityService cityService, ITourLandingPageUrlService tourLandingPageUrlService)
        {
            _unitOfWork = unitOfWork;
            _contentService = contentService;
            _contentGroupService = contentGroupService;
            _cacheService = cacheService;
            _countryService = countryService;
            _stateService = stateService;
            _cityService = cityService;
            _tourLandingPageUrlService = tourLandingPageUrlService;
        }
        #endregion

        #region Index
     
        public ActionResult Index(string msg)
        {

            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetContent([DataSourceRequest]DataSourceRequest request)
        {
            var query = _contentService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
       
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
           
            AddContentViewModel newContent = new AddContentViewModel();
            newContent.ContentGroupDDL = _contentGroupService.GetContentGroupDDL();
            newContent.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            newContent.CityDDL = new List<SelectListItem>();
            newContent.TourLandingPageUrlDDL = new List<SelectListItem>();
            return View(newContent);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddContentViewModel addContentViewModel)
        {
            if (ModelState.IsValid)
            {
                //addContentViewModel.NavigationUrl = addContentViewModel._NavigationUrl;
                var newContent = await _contentService.CreateAsync(addContentViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            addContentViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            addContentViewModel.CityDDL = new List<SelectListItem>();
            addContentViewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
            addContentViewModel.ContentGroupDDL = _contentGroupService.GetContentGroupDDL();
            return View(addContentViewModel);
        }
        #endregion

        #region Edit
       public async Task<ActionResult> Edit(int id)
        {
            EditContentViewModel editContentViewModel = await _contentService.GetViewModelAsync<EditContentViewModel>(x => x.Id == id);
            editContentViewModel.ContentGroupDDL = _contentGroupService.GetContentGroupDDL(editContentViewModel.ContentGroupId);
            //editContentViewModel.NavigationUrl = editContentViewModel.NavigationUrl.Substring(6, editContentViewModel.NavigationUrl.Length - 13);

            #region پر کردن دراپ داون ها
            var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == editContentViewModel.TourLandingPageUrlId);
            var selectedCityId = (tourLandingPageUrl != null ? tourLandingPageUrl.CityId : 0);
            var selectedCountryId = 0;
            if (tourLandingPageUrl != null)
            {
                var city = _cityService.GetById(x => x.Id == selectedCityId);
                var state = _stateService.GetById(x => x.Id == city.StateId);
                selectedCountryId = state.CountryId;

                editContentViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCountryId), Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.CityDDL = _cityService.Filter(c => !c.IsDeleted && c.StateId == state.Id).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCityId), Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.TourLandingPageUrlDDL = _tourLandingPageUrlService.Filter(x => !x.IsDeleted && x.CityId == selectedCityId && x.LandingPageUrlType == EnumLandingPageUrlType.DiscountedTour).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == tourLandingPageUrl.Id), Text = s.URL, Value = s.Id.ToString() });
            }
            else
            {
                editContentViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.CityDDL = new List<SelectListItem>();
                editContentViewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
            }
            #endregion

            return View(editContentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditContentViewModel editContentViewModel)
        {
            if (ModelState.IsValid)
            {
                //editContentViewModel.NavigationUrl = editContentViewModel._NavigationUrl;
                var update = await _contentService.EditAsync(editContentViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }

            #region پر کردن دراپ داون ها
            var tourLandingPageUrl = _tourLandingPageUrlService.GetById(x => x.Id == editContentViewModel.TourLandingPageUrlId);
            var selectedCityId = (tourLandingPageUrl != null ? tourLandingPageUrl.CityId : 0);
            var selectedCountryId = 0;
            if (tourLandingPageUrl != null)
            {
                var city = _cityService.GetById(x => x.Id == selectedCityId);
                var state = _stateService.GetById(x => x.Id == city.StateId);
                selectedCountryId = state.CountryId;

                editContentViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCountryId), Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.CityDDL = _cityService.Filter(c => !c.IsDeleted && c.StateId == state.Id).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == selectedCityId), Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.TourLandingPageUrlDDL = _tourLandingPageUrlService.Filter(x => !x.IsDeleted && x.CityId == selectedCityId && x.LandingPageUrlType == EnumLandingPageUrlType.DiscountedTour).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == tourLandingPageUrl.Id), Text = s.URL, Value = s.Id.ToString() });
            }
            else
            {
                editContentViewModel.CountryDDL = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
                editContentViewModel.CityDDL = new List<SelectListItem>();
                editContentViewModel.TourLandingPageUrlDDL = new List<SelectListItem>();
            }
            #endregion

            editContentViewModel.ContentGroupDDL = _contentGroupService.GetContentGroupDDL(editContentViewModel.ContentGroupId);
            return View(editContentViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _contentService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                //try
                //{
                //    if (System.IO.File.Exists(Server.MapPath(model.ImageUrl)))
                //    {
                //        System.IO.File.Delete(Server.MapPath(model.ImageUrl));
                //        var splittedUrl = model.ImageUrl.Split('.');
                //        var thumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                //        System.IO.File.Delete(Server.MapPath(thumbUrl));
                //    }
                //}
                //catch (Exception)
                //{
                //}

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region IsUniqueEmail
        /// <summary>
        /// اگر شناسه=0 : افزودن محتوا ==> شرط: آیا لینک.تیبلی این لینک را دارد؟
        /// اگر شناسه!=0 : ویرایش محتوا ==> شرط: آیا لینک.تیبلی به غیر از شناسه مزبور این لینک را دارد؟
        /// </summary>
        /// <param name="user"></param>
        /// <returns>لینک منحصر به فرد است؟</returns>
        public ActionResult IsUniqueUrl(AddContentViewModel newContent)
        {
            var linkInDb = new LinkTable();
            if (newContent.Id > 0)
            {
                //در حالت ویرایش هستیم
                //linkInDb = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.Id != newContent.Id && x.URL.Equals(newContent._NavigationUrl));
            }
            else
            {
                //در حالت افزودن هستیم
                //linkInDb = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.URL.Equals(newContent._NavigationUrl));
            }

            if (linkInDb != null)
            {
                //خیر، قبلا استفاده شده است
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //بله، منحصر به فرد است
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueUrlInEdit(EditContentViewModel newContent)
        {
            var linkInDb = new LinkTable();
            if (newContent.Id > 0)
            {
                //در حالت ویرایش هستیم
                //linkInDb = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.Id != newContent.Id && x.URL.Equals(newContent._NavigationUrl));
            }
            else
            {
                //در حالت افزودن هستیم
                //linkInDb = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.URL.Equals(newContent._NavigationUrl));
            }

            if (linkInDb != null)
            {
                //خیر، قبلا استفاده شده است
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //بله، منحصر به فرد است
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region IsUrlSelected
        public JsonResult IsUrlSelected(bool IsActive, Nullable<int> TourLandingPageUrlId)
        {
            if (IsActive && TourLandingPageUrlId != null && TourLandingPageUrlId > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
