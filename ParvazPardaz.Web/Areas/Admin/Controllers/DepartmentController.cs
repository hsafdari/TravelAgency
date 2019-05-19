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
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using Infrastructure;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class DepartmentController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region	Ctor
        public DepartmentController(IUnitOfWork unitOfWork, IDepartmentService departmentService)
        {
            _unitOfWork = unitOfWork;
            _departmentService = departmentService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementDepartment", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        public ActionResult GetDepartment([DataSourceRequest]DataSourceRequest request)
        {
            var query = _departmentService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementDepartment", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddDepartmentViewModel addDepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var newDepartment = await _departmentService.CreateAsync<AddDepartmentViewModel>(addDepartmentViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addDepartmentViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementDepartment", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<ActionResult> Edit(int id)
        {
            EditDepartmentViewModel editDepartmentViewModel = await _departmentService.GetViewModelAsync<EditDepartmentViewModel>(x => x.Id == id);
            return View(editDepartmentViewModel);
        }

        [HttpPost]


        public async Task<ActionResult> Edit(EditDepartmentViewModel editDepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _departmentService.UpdateAsync<EditDepartmentViewModel>(editDepartmentViewModel, t => t.Id == editDepartmentViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editDepartmentViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementDepartment", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _departmentService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
