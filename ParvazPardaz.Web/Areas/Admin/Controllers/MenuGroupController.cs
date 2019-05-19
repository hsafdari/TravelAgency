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
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class MenuGroupController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuGroupService _menugroupService;
        #endregion

        #region	Ctor
        public MenuGroupController(IUnitOfWork unitOfWork, IMenuGroupService menugroupService)
        {
            _unitOfWork = unitOfWork;
            _menugroupService = menugroupService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementMenuGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetMenuGroup([DataSourceRequest]DataSourceRequest request)
        {
            var query = _menugroupService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementMenuGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddMenuGroupViewModel addMenuGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newMenuGroup = await _menugroupService.CreateAsync<AddMenuGroupViewModel>(addMenuGroupViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addMenuGroupViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementMenuGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditMenuGroupViewModel editMenuGroupViewModel = await _menugroupService.GetViewModelAsync<EditMenuGroupViewModel>(x => x.Id == id);
            return View(editMenuGroupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditMenuGroupViewModel editMenuGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _menugroupService.UpdateAsync<EditMenuGroupViewModel>(editMenuGroupViewModel, t => t.Id == editMenuGroupViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editMenuGroupViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementMenuGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _menugroupService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
