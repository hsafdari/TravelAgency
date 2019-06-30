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
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    //[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CityController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        #endregion

        #region	Ctor
        public CityController(IUnitOfWork unitOfWork, ICityService cityService, IStateService stateService, ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _stateService = stateService;
            _countryService = countryService;
        }
        #endregion

        #region FindStatesByCountryId
        public JsonResult FindStatesByCountryId(int id)
        {
            return Json(_stateService.FindStatesByCountryId(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FindCitiesByCountryId
        public JsonResult FindCitiesByCountryId(int id)
        {
            return Json(_stateService.FindCitiesByCountryId(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetCity([DataSourceRequest]DataSourceRequest request)
        {
            var query = _cityService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            AddCityViewModel addCityViewModel = new AddCityViewModel();
            addCityViewModel.CountryList = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            addCityViewModel.StateList = new List<SelectListItem>();
            return View(addCityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddCityViewModel addCityViewModel)
        {
            if (addCityViewModel.StateId <= 0 || !addCityViewModel.File.HasFile())
            {
                addCityViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
                addCityViewModel.StateList = _stateService.FindStatesByCountryId(addCityViewModel.CountryId);
                if (addCityViewModel.StateId <= 0)
                {
                    ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "State"));
                }
                if (!addCityViewModel.File.HasFile())
                {
                    ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
                }
                return View(addCityViewModel);
            }

            if (ModelState.IsValid)
            {
                var newCity = await _cityService.CreateAsync(addCityViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            addCityViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
            addCityViewModel.StateList = _stateService.FindStatesByCountryId(addCityViewModel.CountryId);
            return View(addCityViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "CityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditCityViewModel editCityViewModel = await _cityService.GetViewModelAsync<EditCityViewModel>(x => x.Id == id);
            editCityViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
            editCityViewModel.StateList = _stateService.FindStatesByCountryId(editCityViewModel.CountryId);
            return View(editCityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCityViewModel editCityViewModel)
        {
            if (editCityViewModel.StateId <= 0)
            {
                editCityViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
                editCityViewModel.StateList = _stateService.FindStatesByCountryId(editCityViewModel.CountryId);
                if (editCityViewModel.StateId <= 0)
                {
                    ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "State"));
                }
                return View(editCityViewModel);
            }
            if (ModelState.IsValid)
            {
                var update = await _cityService.EditAsync(editCityViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            editCityViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
            editCityViewModel.StateList = _stateService.FindStatesByCountryId(editCityViewModel.CountryId);
            return View(editCityViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var deletedCount = await _cityService.DeleteAsync(x => x.Id == id);
            if (deletedCount > 0)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region GetCities
        public JsonResult GetCities(string term)
        {
            var findExcept = _cityService.Filter(c => c.Title.Contains(term) || c.State.Title.Contains(term) || c.State.Country.Title.Contains(term))
                                         .Select(s => new { Id = s.Id, Title = s.Title.ToUpper() + " ( " + s.State.Country.Title.ToUpper() + " ) " }).ToList();

            return Json(findExcept, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region IsUniqueTitle
        public JsonResult IsUniqueTitle(string title)
        {
            title = (title != null ? title.Trim() : title);
            var check = _cityService.CheckIsUniqueTitleInCity(title);

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

        #region IsUniqueENTitle
        public JsonResult IsUniqueENTitle(string enTitle)
        {
            enTitle = (enTitle != null ? enTitle.Trim() : enTitle);
            var check = _cityService.CheckIsUniqueENTitleInCity(enTitle);

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
    }
}
