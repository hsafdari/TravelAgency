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
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CountryController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryService _countryService;
        #endregion

        #region	Ctor
        public CountryController(IUnitOfWork unitOfWork, ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
            _countryService = countryService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CountryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetCountry([DataSourceRequest]DataSourceRequest request)
        {
            var query = _countryService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CountryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddCountryViewModel addCountryViewModel)
        {
            if (!addCountryViewModel.File.HasFile())
            {
                ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
                return View(addCountryViewModel);
            }
            if (ModelState.IsValid)
            {
                var newCountry = await _countryService.CreateAsync(addCountryViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addCountryViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "CountryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditCountryViewModel editCountryViewModel = await _countryService.GetViewModelAsync<EditCountryViewModel>(x => x.Id == id);
            return View(editCountryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCountryViewModel editCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _countryService.EditAsync(editCountryViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editCountryViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CountryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var deletedCount = await _countryService.DeleteAsync(x => x.Id == id);
            if (deletedCount > 0)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region IsUniqueTitle
        public JsonResult IsUniqueTitle(string title)
        {
            title = (title != null ? title.Trim() : title);
            var check = _countryService.CheckIsUniqueTitleInCountry(title);

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
