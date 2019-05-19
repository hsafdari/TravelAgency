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
    public class StateController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        #endregion

        #region	Ctor
        public StateController(IUnitOfWork unitOfWork, IStateService stateService, ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
            _stateService = stateService;
            _countryService = countryService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "StateManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetState([DataSourceRequest]DataSourceRequest request)
        {
            var query = _stateService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "StateManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {

            AddStateViewModel addVM = new AddStateViewModel();
            addVM.CountryList = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(s => new SelectListItem() { Selected = false, Value = s.Id.ToString(), Text = s.Title });    //(System.Threading.CancellationToken.None).ConfigureAwait(false).Result;
            return View(addVM);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddStateViewModel addStateViewModel)
        {
            if (!addStateViewModel.File.HasFile())
            {
                addStateViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
                ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
                return View(addStateViewModel);
            }
            if (ModelState.IsValid)
            {
                var newState = await _stateService.CreateAsync(addStateViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            addStateViewModel.CountryList = _countryService.GetAllCountryOfSelectListItem();
            return View(addStateViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "StateManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditStateViewModel editStateViewModel = await _stateService.GetViewModelAsync<EditStateViewModel>(x => x.Id == id);
            editStateViewModel.CountryList = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == editStateViewModel.CountryId), Value = c.Id.ToString(), Text = c.Title });
            return View(editStateViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditStateViewModel editStateViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _stateService.EditAsync(editStateViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            editStateViewModel.CountryList = _countryService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (c.Id == editStateViewModel.CountryId), Value = c.Id.ToString(), Text = c.Title });
            return View(editStateViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "StateManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var deletedCount = await _stateService.DeleteAsync(x => x.Id == id);
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
            var check = _stateService.CheckIsUniqueTitleInState(title);

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
