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
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tour;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
	[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourPackageDayController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourPackageDayService _tourpackagedayService;
        #endregion

        #region	Ctor
        public TourPackageDayController(IUnitOfWork unitOfWork, ITourPackageDayService tourpackagedayService)
        {
            _unitOfWork = unitOfWork;
            _tourpackagedayService = tourpackagedayService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourPackageDayManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourPackageDay([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tourpackagedayService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetTourPackageDayTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _tourpackagedayService.GetViewModelForGrid();
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
        [Display(Name = "TourPackageDayManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTourPackageDayViewModel addTourPackageDayViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTourPackageDay = await _tourpackagedayService.CreateAsync<AddTourPackageDayViewModel>(addTourPackageDayViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addTourPackageDayViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourPackageDayManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTourPackageDayViewModel editTourPackageDayViewModel = await _tourpackagedayService.GetViewModelAsync<EditTourPackageDayViewModel>(x => x.Id == id);            
            return View(editTourPackageDayViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourPackageDayViewModel editTourPackageDayViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tourpackagedayService.UpdateAsync<EditTourPackageDayViewModel>(editTourPackageDayViewModel, t => t.Id == editTourPackageDayViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTourPackageDayViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourPackageDayManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tourpackagedayService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
