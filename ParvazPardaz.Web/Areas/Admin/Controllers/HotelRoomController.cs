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
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class HotelRoomController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRoomService _hotelroomService;
        #endregion

        #region	Ctor
        public HotelRoomController(IUnitOfWork unitOfWork, IHotelRoomService hotelroomService)
        {
            _unitOfWork = unitOfWork;
            _hotelroomService = hotelroomService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "HotelRoomManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetHotelRoom([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelroomService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "HotelRoomManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddHotelRoomViewModel addHotelRoomViewModel)
        {
            if (ModelState.IsValid)
            {
                var newHotelRoom = await _hotelroomService.CreateAsync<AddHotelRoomViewModel>(addHotelRoomViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addHotelRoomViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "HotelRoomManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditHotelRoomViewModel editHotelRoomViewModel = await _hotelroomService.GetViewModelAsync<EditHotelRoomViewModel>(x => x.Id == id);
            return View(editHotelRoomViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelRoomViewModel editHotelRoomViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _hotelroomService.UpdateAsync<EditHotelRoomViewModel>(editHotelRoomViewModel, t => t.Id == editHotelRoomViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editHotelRoomViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "HotelRoomManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelroomService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
