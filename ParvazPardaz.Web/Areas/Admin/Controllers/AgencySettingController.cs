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
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class AgencySettingController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAgencySettingService _agencysettingService;
        #endregion

        #region	Ctor
        public AgencySettingController(IUnitOfWork unitOfWork, IAgencySettingService agencysettingService)
        {
            _unitOfWork = unitOfWork;
            _agencysettingService = agencysettingService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementAgencySettings", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetAgencySetting([DataSourceRequest]DataSourceRequest request)
        {
            var query = _agencysettingService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementAgencySettings", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddAgencySettingViewModel addAgencySettingViewModel)
        {
            if (!addAgencySettingViewModel.File.HasFile())
            {
                ModelState.AddModelError("File", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, ParvazPardaz.Resource.Tour.Tours.File));
                return View(addAgencySettingViewModel);
            }

            if (ModelState.IsValid)
            {
                var newAgencySetting = await _agencysettingService.CreateAsync(addAgencySettingViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addAgencySettingViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementAgencySettings", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<ActionResult> Edit(int id)
        {
            EditAgencySettingViewModel editAgencySettingViewModel = await _agencysettingService.GetViewModelAsync<EditAgencySettingViewModel>(x => x.Id == id);
            return View(editAgencySettingViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditAgencySettingViewModel editAgencySettingViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _agencysettingService.EditAsync(editAgencySettingViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editAgencySettingViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementAgencySettings", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _agencysettingService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
