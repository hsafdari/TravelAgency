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
    public class VehicleTypeClassController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleTypeClassService _vehicletypeclassService;
        private readonly IVehicleTypeService _vehicletypeService;
        #endregion

        #region	Ctor
        public VehicleTypeClassController(IUnitOfWork unitOfWork, IVehicleTypeClassService vehicletypeclassService, IVehicleTypeService vehicletypeService)
        {
            _unitOfWork = unitOfWork;
            _vehicletypeclassService = vehicletypeclassService;
            _vehicletypeService = vehicletypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementVehicleTypeClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetVehicleTypeClass([DataSourceRequest]DataSourceRequest request)
        {
            var query = _vehicletypeclassService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }

        public JsonResult GetVehicleTypeClassTable([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            #region Fetch Data
            var query = _vehicletypeclassService.GetViewModelForGrid();
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
        [Display(Name = "ManagementVehicleTypeClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            ViewBag.VehicleTypeDDL = _vehicletypeService.GetAllVehicleTypesOfSelectListItem();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddVehicleTypeClassViewModel addVehicleTypeClassViewModel)
        {
            if (ModelState.IsValid)
            {
                var newVehicleTypeClass = await _vehicletypeclassService.CreateAsync<AddVehicleTypeClassViewModel>(addVehicleTypeClassViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            ViewBag.VehicleTypeDDL = _vehicletypeService.GetAllVehicleTypesOfSelectListItem();
            return View(addVehicleTypeClassViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementVehicleTypeClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditVehicleTypeClassViewModel editVehicleTypeClassViewModel = await _vehicletypeclassService.GetViewModelAsync<EditVehicleTypeClassViewModel>(x => x.Id == id);
            ViewBag.VehicleTypeDDL = _vehicletypeService.GetAllVehicleTypesOfSelectListItem();
            return View(editVehicleTypeClassViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditVehicleTypeClassViewModel editVehicleTypeClassViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _vehicletypeclassService.UpdateAsync<EditVehicleTypeClassViewModel>(editVehicleTypeClassViewModel, t => t.Id == editVehicleTypeClassViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            ViewBag.VehicleTypeDDL = _vehicletypeService.GetAllVehicleTypesOfSelectListItem();
            return View(editVehicleTypeClassViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementVehicleTypeClass", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _vehicletypeclassService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
