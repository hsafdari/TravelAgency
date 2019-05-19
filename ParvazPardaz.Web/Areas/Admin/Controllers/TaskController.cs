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
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TaskController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskService _taskService;
        #endregion

        #region	Ctor
        public TaskController(IUnitOfWork unitOfWork, ITaskService taskService)
        {
            _unitOfWork = unitOfWork;
            _taskService = taskService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementTasks", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTask([DataSourceRequest]DataSourceRequest request)
        {
            var query = _taskService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementTasks", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTaskViewModel addTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTask = await _taskService.CreateAsync<AddTaskViewModel>(addTaskViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addTaskViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementTasks", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTaskViewModel editTaskViewModel = await _taskService.GetViewModelAsync<EditTaskViewModel>(x => x.Id == id);            
            return View(editTaskViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTaskViewModel editTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _taskService.UpdateAsync<EditTaskViewModel>(editTaskViewModel, t => t.Id == editTaskViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTaskViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementTasks", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _taskService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
