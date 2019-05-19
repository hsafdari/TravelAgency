using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using DataTables.Mvc;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Users;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
	[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class UserGroupController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserGroupService _usergroupService;
        #endregion

        #region	Ctor
        public UserGroupController(IUnitOfWork unitOfWork, IUserGroupService usergroupService)
        {
            _unitOfWork = unitOfWork;
            _usergroupService = usergroupService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementUserGroups", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetUserGroup([DataSourceRequest]DataSourceRequest request)
        {
            var query = _usergroupService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetUserGroupTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _usergroupService.GetViewModelForGrid();
            #endregion

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
            }
            var filteredCount = query.Count();
            #endregion Filtering

            #region Sorting
            // Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var sortColumn = String.Empty;

            foreach (var column in sortedColumns)
            {
                sortColumn += sortColumn != String.Empty ? "," : "";
                sortColumn += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = System.Linq.Dynamic.DynamicQueryable.OrderBy(query, sortColumn == string.Empty ? "Id asc" : sortColumn);//sortColumn + " " + sortColumnDirection);    
            #endregion

            #region Paging
            // Paging
            var data = query.Skip(requestModel.Start).Take(requestModel.Length).ToList();
            #endregion

            #region DataTablesResponse
            var totalCount = query.Count();
            var dataTablesResponse = new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount);
            #endregion

            return Json(dataTablesResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementUserGroups", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddUserGroupViewModel addUserGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newUserGroup = await _usergroupService.CreateAsync<AddUserGroupViewModel>(addUserGroupViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addUserGroupViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementUserGroups", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public async Task<ActionResult> Edit(int id)
        {
            EditUserGroupViewModel editUserGroupViewModel = await _usergroupService.GetViewModelAsync<EditUserGroupViewModel>(x => x.Id == id);            
            return View(editUserGroupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserGroupViewModel editUserGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _usergroupService.UpdateAsync<EditUserGroupViewModel>(editUserGroupViewModel, t => t.Id == editUserGroupViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editUserGroupViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementUserGroups", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _usergroupService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
