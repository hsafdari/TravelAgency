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
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]

    public class HotelFacilityController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelFacilityService _hotelfacilityService;
        #endregion

        #region	Ctor
        public HotelFacilityController(IUnitOfWork unitOfWork, IHotelFacilityService hotelfacilityService)
        {
            _unitOfWork = unitOfWork;
            _hotelfacilityService = hotelfacilityService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "HotelFacilityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetHotelFacility([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelfacilityService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "HotelFacilityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddHotelFacilityViewModel addHotelFacilityViewModel)
        {
            if (ModelState.IsValid)
            {
                var newHotelFacility = await _hotelfacilityService.CreateAsync<AddHotelFacilityViewModel>(addHotelFacilityViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addHotelFacilityViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "HotelFacilityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditHotelFacilityViewModel editHotelFacilityViewModel = await _hotelfacilityService.GetViewModelAsync<EditHotelFacilityViewModel>(x => x.Id == id);
            return View(editHotelFacilityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelFacilityViewModel editHotelFacilityViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _hotelfacilityService.UpdateAsync<EditHotelFacilityViewModel>(editHotelFacilityViewModel, t => t.Id == editHotelFacilityViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editHotelFacilityViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "HotelFacilityManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelfacilityService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
