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
using ParvazPardaz.Service.Security;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Extension;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class HotelBoardController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelBoardService _hotelboardService;
        #endregion

        #region	Ctor
        public HotelBoardController(IUnitOfWork unitOfWork, IHotelBoardService hotelboardService)
        {
            _unitOfWork = unitOfWork;
            _hotelboardService = hotelboardService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementHotelBoard", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetHotelBoard([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelboardService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementHotelBoard", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddHotelBoardViewModel addHotelBoardViewModel)
        {
            if (!addHotelBoardViewModel.File.HasFile())
            {
                ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "File"));
                return View(addHotelBoardViewModel);
            }
            if (ModelState.IsValid)
            {             
                var newHotelBoard = await _hotelboardService.CreateAsync(addHotelBoardViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addHotelBoardViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementHotelBoard", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditHotelBoardViewModel editHotelBoardViewModel = await _hotelboardService.GetViewModelAsync<EditHotelBoardViewModel>(x => x.Id == id);            
            return View(editHotelBoardViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelBoardViewModel editHotelBoardViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _hotelboardService.EditAsync(editHotelBoardViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editHotelBoardViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementHotelBoard", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelboardService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
