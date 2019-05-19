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
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.Service.Contract.Magazine;
using ParvazPardaz.Service.Contract.Location;
using System.ComponentModel.DataAnnotations;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
	[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourSuggestionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourSuggestionService _toursuggestionsService;
        private readonly ILocationService _locationService;
        #endregion

        #region	Ctor
        public TourSuggestionController(IUnitOfWork unitOfWork, ITourSuggestionService toursuggestionsService,ILocationService locationservice)
        {
            _unitOfWork = unitOfWork;
            _toursuggestionsService = toursuggestionsService;
            _locationService = locationservice;
        }
        #endregion
        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourSuggestManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourSuggestion([DataSourceRequest]DataSourceRequest request)
        {
            var query = _toursuggestionsService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetTourSuggestionsTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _toursuggestionsService.GetViewModelForGrid();
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
        [Display(Name = "TourSuggestManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create(string msg)
        {
            AddTourSuggestionViewModel ViewModel = new AddTourSuggestionViewModel();
            ViewModel.LocationDDL = _locationService.GetAllLocationOfSelectListItem();
            ViewBag.success = msg;
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddTourSuggestionViewModel addTourSuggestionViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTourSuggestions = await _toursuggestionsService.CreateAsync(addTourSuggestionViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            addTourSuggestionViewModel.LocationDDL = _locationService.GetAllLocationOfSelectListItem();
            return View(addTourSuggestionViewModel);
        }
        #endregion
        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourSuggestManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditTourSuggestionViewModel editTourSuggestionViewModel = await _toursuggestionsService.GetViewModelAsync<EditTourSuggestionViewModel>(x => x.Id == id);
            editTourSuggestionViewModel.LocationDDL = _locationService.GetAllLocationOfSelectListItem();
            return View(editTourSuggestionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourSuggestionViewModel editTourSuggestionViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _toursuggestionsService.EditAsync(editTourSuggestionViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            editTourSuggestionViewModel.LocationDDL = _locationService.GetAllLocationOfSelectListItem();
            return View(editTourSuggestionViewModel);
        }
        #endregion

        #region Delete
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourSuggestManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _toursuggestionsService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
