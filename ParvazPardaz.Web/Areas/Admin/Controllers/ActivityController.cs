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
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class ActivityController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityService _activityService;
        #endregion

        #region	Ctor
        public ActivityController(IUnitOfWork unitOfWork, IActivityService activityService)
        {
            _unitOfWork = unitOfWork;
            _activityService = activityService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ActivityManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetActivity([DataSourceRequest]DataSourceRequest request)
        {
            var query = _activityService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ActivityManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddActivityViewModel addActivityViewModel)
        {
            //درخواست شد که اجباری نباشد
            //if (!addActivityViewModel.File.HasFile())
            //{
            //    ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
            //    return View(addActivityViewModel);
            //}
            if (ModelState.IsValid)
            {
                var newActivity = await _activityService.CreateAsync(addActivityViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addActivityViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ActivityManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditActivityViewModel editActivityViewModel = await _activityService.GetViewModelAsync<EditActivityViewModel>(x => x.Id == id);
            return View(editActivityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditActivityViewModel editActivityViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _activityService.EditAsync(editActivityViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editActivityViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ActivityManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _activityService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
