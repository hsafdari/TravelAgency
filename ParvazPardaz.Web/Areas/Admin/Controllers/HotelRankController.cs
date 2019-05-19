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
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]

    public class HotelRankController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRankService _hotelrankService;
        #endregion

        #region	Ctor
        public HotelRankController(IUnitOfWork unitOfWork, IHotelRankService hotelrankService)
        {
            _unitOfWork = unitOfWork;
            _hotelrankService = hotelrankService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "HotelRankManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetHotelRank([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelrankService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "HotelRankManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddHotelRankViewModel addHotelRankViewModel)
        {
            if (ModelState.IsValid)
            {
                var newHotelRank = await _hotelrankService.CreateAsync(addHotelRankViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addHotelRankViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "HotelRankManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<ActionResult> Edit(int id)
        {
            EditHotelRankViewModel editHotelRankViewModel = await _hotelrankService.GetViewModelAsync<EditHotelRankViewModel>(x => x.Id == id);
            return View(editHotelRankViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelRankViewModel editHotelRankViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _hotelrankService.EditAsync(editHotelRankViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editHotelRankViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "HotelRankManagment", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelrankService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
