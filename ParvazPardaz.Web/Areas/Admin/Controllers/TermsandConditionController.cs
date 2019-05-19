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
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Rule;
using ParvazPardaz.Service.Contract.Rule;
using System.ComponentModel.DataAnnotations;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
	[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TermsandConditionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITermsandConditionService _termsandconditionsService;
        #endregion

        #region	Ctor
        public TermsandConditionController(IUnitOfWork unitOfWork, ITermsandConditionService termsandconditionsService)
        {
            _unitOfWork = unitOfWork;
            _termsandconditionsService = termsandconditionsService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TermsAndConditionsManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTermsandCondition([DataSourceRequest]DataSourceRequest request)
        {
            var query = _termsandconditionsService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetTermsandConditionsTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _termsandconditionsService.GetViewModelForGrid();
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
        [Display(Name = "TermsAndConditionsManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(AddTermsandConditionViewModel addTermsandConditionViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTermsandCondition = await _termsandconditionsService.CreateAsync<AddTermsandConditionViewModel>(addTermsandConditionViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addTermsandConditionViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TermsAndConditionsManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTermsandConditionViewModel editTermsandConditionViewModel = await _termsandconditionsService.GetViewModelAsync<EditTermsandConditionViewModel>(x => x.Id == id);            
            return View(editTermsandConditionViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(EditTermsandConditionViewModel editTermsandConditionViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _termsandconditionsService.UpdateAsync<EditTermsandConditionViewModel>(editTermsandConditionViewModel, t => t.Id == editTermsandConditionViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTermsandConditionViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TermsAndConditionsManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _termsandconditionsService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
