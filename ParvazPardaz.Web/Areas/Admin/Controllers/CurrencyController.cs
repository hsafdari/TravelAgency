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
using ParvazPardaz.Model.Entity.Hotel;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CurrencyController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyService _currencyService;
        #endregion

        #region	Ctor
        public CurrencyController(IUnitOfWork unitOfWork, ICurrencyService currencyService)
        {
            _unitOfWork = unitOfWork;
            _currencyService = currencyService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "CurrencyManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetCurrency([DataSourceRequest]DataSourceRequest request)
        {
            var query = _currencyService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "CurrencyManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddCurrencyViewModel addCurrencyViewModel)
        {
            if (ModelState.IsValid)
            {
                var newCurrency = await _currencyService.CreateAsync<AddCurrencyViewModel>(addCurrencyViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addCurrencyViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "CurrencyManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditCurrencyViewModel editCurrencyViewModel = await _currencyService.GetViewModelAsync<EditCurrencyViewModel>(x => x.Id == id);
            return View(editCurrencyViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCurrencyViewModel editCurrencyViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _currencyService.UpdateAsync<EditCurrencyViewModel>(editCurrencyViewModel, t => t.Id == editCurrencyViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editCurrencyViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "CurrencyManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var isUsed = _unitOfWork.Set<HotelPackageHotelRoom>().Any(x => x.OtherCurrencyId == id);
            isUsed = (isUsed || _unitOfWork.Set<TourSchedule>().Any(x => x.CurrencyId == id));
            if (!isUsed)
            {
                var model = await _currencyService.DeleteLogicallyAsync(x => x.Id == id);
                if (model != null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
