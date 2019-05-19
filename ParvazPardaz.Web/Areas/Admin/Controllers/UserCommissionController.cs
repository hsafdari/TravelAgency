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
    public class UserCommissionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCommissionService _usercommissionService;
        #endregion

        #region	Ctor
        public UserCommissionController(IUnitOfWork unitOfWork, IUserCommissionService usercommissionService)
        {
            _unitOfWork = unitOfWork;
            _usercommissionService = usercommissionService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "UserCommissionsManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetUserCommission([DataSourceRequest]DataSourceRequest request)
        {
            var query = _usercommissionService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetUserCommissionTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _usercommissionService.GetViewModelForGrid();
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
       

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "UserCommissionsManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<ActionResult> Edit(int id)
        {
            EditUserCommissionViewModel editUserCommissionViewModel = await _usercommissionService.GetViewModelAsync<EditUserCommissionViewModel>(x => x.Id == id);            
            return View(editUserCommissionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserCommissionViewModel editUserCommissionViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _usercommissionService.UpdateAsync<EditUserCommissionViewModel>(editUserCommissionViewModel, t => t.Id == editUserCommissionViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editUserCommissionViewModel);
        }
        #endregion        
        
    }
}
