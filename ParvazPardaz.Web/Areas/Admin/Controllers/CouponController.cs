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
using ParvazPardaz.Model.Entity;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
	[Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CouponController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICouponService _couponService;
        #endregion

        #region	Ctor
        public CouponController(IUnitOfWork unitOfWork, ICouponService couponService)
        {
            _unitOfWork = unitOfWork;
            _couponService = couponService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CouponManagement", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetCoupon([DataSourceRequest]DataSourceRequest request)
        {
            var query = _couponService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }        
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CouponManagement", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddCouponViewModel addCouponViewModel)
        {
            if (ModelState.IsValid)
            {
                var newCoupon = await _couponService.CreateAsync<AddCouponViewModel>(addCouponViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addCouponViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "CouponManagement", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public async Task<ActionResult> Edit(int id)
        {
            EditCouponViewModel editCouponViewModel = await _couponService.GetViewModelAsync<EditCouponViewModel>(x => x.Id == id);
            if (editCouponViewModel.ValidatedDate!=null)
            {
                 return RedirectToAction("Index", new { msg = "notPermission" });
            }
            return View(editCouponViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCouponViewModel editCouponViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _couponService.UpdateAsync<EditCouponViewModel>(editCouponViewModel, t => t.Id == editCouponViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editCouponViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CouponManagement", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public async Task<JsonResult> Delete(int id)
        {
            var data=_couponService.Filter(x => x.Id == id).FirstOrDefault();
            if (data.ValidatedDate != null)
            {
                ViewBag.success = "couldNotCascadeDelete";
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            var model = await _couponService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                ViewBag.success = "couldNotCascadeDelete";
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            ViewBag.success = "couldNotCascadeDelete";
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
