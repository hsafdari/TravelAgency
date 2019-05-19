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
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
     [Mvc5AuthorizeAttribute(StandardRoles.SystemAdministrator)]
    public class RoleController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationRoleManager _roleManager;
        #endregion

        #region	Ctor
        public RoleController(IUnitOfWork unitOfWork, IApplicationRoleManager roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "RoleManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetRole([DataSourceRequest]DataSourceRequest request)
        {
            var query = _roleManager.GetRolesViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "RoleManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newRole = await _roleManager.CreateRoleAsync(addRoleViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addRoleViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "RoleManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<ActionResult> Edit(int id)
        {
            EditRoleViewModel editRoleViewModel = await _roleManager.GetRoleViewModelAsync(id);
            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.EditRoleAsync(editRoleViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editRoleViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "RoleManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _roleManager.DeleteRoleAsync(id);
            if (model > 0 == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
