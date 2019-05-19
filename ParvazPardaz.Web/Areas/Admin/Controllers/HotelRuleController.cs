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
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class HotelRuleController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRuleService _hotelruleService;
        #endregion

        #region	Ctor
        public HotelRuleController(IUnitOfWork unitOfWork, IHotelRuleService hotelruleService)
        {
            _unitOfWork = unitOfWork;
            _hotelruleService = hotelruleService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementHotelRules", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetHotelRule([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelruleService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementHotelRules", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddHotelRuleViewModel addHotelRuleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newHotelRule = await _hotelruleService.CreateAsync<AddHotelRuleViewModel>(addHotelRuleViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addHotelRuleViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementHotelRules", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditHotelRuleViewModel editHotelRuleViewModel = await _hotelruleService.GetViewModelAsync<EditHotelRuleViewModel>(x => x.Id == id);            
            return View(editHotelRuleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelRuleViewModel editHotelRuleViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _hotelruleService.UpdateAsync<EditHotelRuleViewModel>(editHotelRuleViewModel, t => t.Id == editHotelRuleViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editHotelRuleViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementHotelRules", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelruleService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
