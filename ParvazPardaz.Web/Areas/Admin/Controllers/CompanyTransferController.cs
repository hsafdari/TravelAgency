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
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت شرکت های حمل ونقل
    /// </summary>
    //[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CompanyTransferController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyTransferService _companytransferService;
        public readonly IVehicleTypeClassService _vehicleTypeClassService;
        #endregion

        #region	Ctor
        public CompanyTransferController(IUnitOfWork unitOfWork, ICompanyTransferService companytransferService, IVehicleTypeClassService vehicleTypeClassService)
        {
            _unitOfWork = unitOfWork;
            _companytransferService = companytransferService;
            _vehicleTypeClassService = vehicleTypeClassService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CompanyTransferManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }
        
        public ActionResult GetCompanyTransfer([DataSourceRequest]DataSourceRequest request)
        {
            var query = _companytransferService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CompanyTransferManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddCompanyTransferViewModel addCompanyTransferViewModel)
        {
            if (!addCompanyTransferViewModel.File.HasFile())
            {
                ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
                return View(addCompanyTransferViewModel);
            }
            if (ModelState.IsValid)
            {
                var newCompanyTransfer = await _companytransferService.CreateAsync(addCompanyTransferViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addCompanyTransferViewModel);
        }
        #endregion

        #region Edit
         [CustomAuthorize(Permissionitem.Edit)]
         [Display(Name = "CompanyTransferManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditCompanyTransferViewModel editCompanyTransferViewModel = await _companytransferService.GetViewModelAsync<EditCompanyTransferViewModel>(x => x.Id == id);
            return View(editCompanyTransferViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCompanyTransferViewModel editCompanyTransferViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _companytransferService.EditAsync(editCompanyTransferViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editCompanyTransferViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CompanyTransferManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _companytransferService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region GetCompanyTransfers
        public JsonResult GetCompanyTransfers(int id)
        {
            return Json(_companytransferService.Filter(c => c.CompanyTransferVehicleTypes.Any(ctv => ctv.VehicleTypeId == id)).Select(s => new FilterList() { Id = s.Id, Title = s.Title + " " + s.IataCode }).AsEnumerable(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetVehicleTypeClass
        public JsonResult GetVehicleTypeClass(int id)
        {
            return Json(_vehicleTypeClassService.Filter(x => x.VehicleTypeId == id && !x.IsDeleted).Select(s => new FilterList() { Id = s.Id, Title = s.Title + " " + s.Code }).AsEnumerable(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
