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
    public class VehicleTypeController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleTypeService _vehicletypeService;
        #endregion

        #region	Ctor
        public VehicleTypeController(IUnitOfWork unitOfWork, IVehicleTypeService vehicletypeService)
        {
            _unitOfWork = unitOfWork;
            _vehicletypeService = vehicletypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "VehicleTypeMangement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetVehicleType([DataSourceRequest]DataSourceRequest request)
        {
            var query = _vehicletypeService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion
        
        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "VehicleTypeMangement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddVehicleTypeViewModel addVehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newVehicleType = await _vehicletypeService.CreateAsync<AddVehicleTypeViewModel>(addVehicleTypeViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addVehicleTypeViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "VehicleTypeMangement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditVehicleTypeViewModel editVehicleTypeViewModel = await _vehicletypeService.GetViewModelAsync<EditVehicleTypeViewModel>(x => x.Id == id);
            return View(editVehicleTypeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditVehicleTypeViewModel editVehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _vehicletypeService.UpdateAsync<EditVehicleTypeViewModel>(editVehicleTypeViewModel, t => t.Id == editVehicleTypeViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editVehicleTypeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "VehicleTypeMangement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _vehicletypeService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
