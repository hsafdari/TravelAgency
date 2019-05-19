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
    /// <summary>
    /// مدیریت مجازها و غیرز مجازها
    /// </summary>
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class AllowedBannedController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAllowedBannedService _allowedbannedService;
        #endregion

        #region	Ctor
        public AllowedBannedController(IUnitOfWork unitOfWork, IAllowedBannedService allowedbannedService)
        {
            _unitOfWork = unitOfWork;
            _allowedbannedService = allowedbannedService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "AllowedBannedManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetAllowedBanned([DataSourceRequest]DataSourceRequest request)
        {
            var query = _allowedbannedService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "AllowedBannedManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddAllowedBannedViewModel addAllowedBannedViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAllowedBanned = await _allowedbannedService.CreateAsync<AddAllowedBannedViewModel>(addAllowedBannedViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addAllowedBannedViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "AllowedBannedManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditAllowedBannedViewModel editAllowedBannedViewModel = await _allowedbannedService.GetViewModelAsync<EditAllowedBannedViewModel>(x => x.Id == id);
            return View(editAllowedBannedViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditAllowedBannedViewModel editAllowedBannedViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _allowedbannedService.UpdateAsync<EditAllowedBannedViewModel>(editAllowedBannedViewModel, t => t.Id == editAllowedBannedViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editAllowedBannedViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "AllowedBannedManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _allowedbannedService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
