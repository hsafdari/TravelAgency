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
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CompanyTransferVehicleTypeController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyTransferVehicleTypeService _companytransfervehicletypeService;
        private readonly ICompanyTransferService _companyTransferService;
        private readonly IVehicleTypeService _vehicleTypeService;
        #endregion

        #region	Ctor
        public CompanyTransferVehicleTypeController(IUnitOfWork unitOfWork, ICompanyTransferVehicleTypeService companytransfervehicletypeService, ICompanyTransferService companyTransferService, IVehicleTypeService vehicleTypeService)
        {
            _unitOfWork = unitOfWork;
            _companytransfervehicletypeService = companytransfervehicletypeService;
            _companyTransferService = companyTransferService;
            _vehicleTypeService = vehicleTypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CompanyTransferVehicleTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetCompanyTransferVehicleType([DataSourceRequest]DataSourceRequest request)
        {
            var query = _companytransfervehicletypeService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CompanyTransferVehicleTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create()
        {
            AddCompanyTransferVehicleTypeViewModel addCoTrVeTypeViewModel = new AddCompanyTransferVehicleTypeViewModel();
            addCoTrVeTypeViewModel.CompanyTransferList = _companyTransferService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (addCoTrVeTypeViewModel.CompanyTransferId == c.Id), Text = c.Title, Value = c.Id.ToString() });
            addCoTrVeTypeViewModel.VehicleTypeList = _vehicleTypeService.Filter(v => v.IsDeleted == false).AsEnumerable().Select(v => new SelectListItem() { Selected = (addCoTrVeTypeViewModel.VehicleTypeId == v.Id), Text = v.Title, Value = v.Id.ToString() });
            return View(addCoTrVeTypeViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddCompanyTransferVehicleTypeViewModel addCompanyTransferVehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newCompanyTransferVehicleType = await _companytransfervehicletypeService.CreateAsync<AddCompanyTransferVehicleTypeViewModel>(addCompanyTransferVehicleTypeViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addCompanyTransferVehicleTypeViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "CompanyTransferVehicleTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditCompanyTransferVehicleTypeViewModel editCompanyTransferVehicleTypeViewModel = await _companytransfervehicletypeService.GetViewModelAsync<EditCompanyTransferVehicleTypeViewModel>(x => x.Id == id);
            editCompanyTransferVehicleTypeViewModel.CompanyTransferList = _companyTransferService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = false, Text = c.Title, Value = c.Id.ToString() });
            editCompanyTransferVehicleTypeViewModel.VehicleTypeList = _vehicleTypeService.Filter(v => v.IsDeleted == false).AsEnumerable().Select(v => new SelectListItem() { Selected = false, Text = v.Title, Value = v.Id.ToString() });
            return View(editCompanyTransferVehicleTypeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCompanyTransferVehicleTypeViewModel editCompanyTransferVehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _companytransfervehicletypeService.UpdateAsync<EditCompanyTransferVehicleTypeViewModel>(editCompanyTransferVehicleTypeViewModel, t => t.Id == editCompanyTransferVehicleTypeViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            editCompanyTransferVehicleTypeViewModel.CompanyTransferList = _companyTransferService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(c => new SelectListItem() { Selected = (editCompanyTransferVehicleTypeViewModel.CompanyTransferId == c.Id), Text = c.Title, Value = c.Id.ToString() });
            editCompanyTransferVehicleTypeViewModel.VehicleTypeList = _vehicleTypeService.Filter(v => v.IsDeleted == false).AsEnumerable().Select(v => new SelectListItem() { Selected = (editCompanyTransferVehicleTypeViewModel.VehicleTypeId == v.Id), Text = v.Title, Value = v.Id.ToString() });
            return View(editCompanyTransferVehicleTypeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CompanyTransferVehicleTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _companytransfervehicletypeService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion       
    }
}
