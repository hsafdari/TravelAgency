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
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class AdditionalServiceController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdditionalServiceService _additionalserviceService;
        #endregion

        #region	Ctor
        public AdditionalServiceController(IUnitOfWork unitOfWork, IAdditionalServiceService additionalserviceService)
        {
            _unitOfWork = unitOfWork;
            _additionalserviceService = additionalserviceService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "AdditionalServiceManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetAdditionalService([DataSourceRequest]DataSourceRequest request)
        {
            var query = _additionalserviceService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "AdditionalServiceManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddAdditionalServiceViewModel addAdditionalServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAdditionalService = await _additionalserviceService.CreateAsync<AddAdditionalServiceViewModel>(addAdditionalServiceViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addAdditionalServiceViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "AdditionalServiceManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditAdditionalServiceViewModel editAdditionalServiceViewModel = await _additionalserviceService.GetViewModelAsync<EditAdditionalServiceViewModel>(x => x.Id == id);
            return View(editAdditionalServiceViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditAdditionalServiceViewModel editAdditionalServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _additionalserviceService.UpdateAsync<EditAdditionalServiceViewModel>(editAdditionalServiceViewModel, t => t.Id == editAdditionalServiceViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editAdditionalServiceViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "AdditionalServiceManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _additionalserviceService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
