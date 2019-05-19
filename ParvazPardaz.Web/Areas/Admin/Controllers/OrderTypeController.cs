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
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class OrderTypeController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderTypeService _ordertypeService;
        #endregion

        #region	Ctor
        public OrderTypeController(IUnitOfWork unitOfWork, IOrderTypeService ordertypeService)
        {
            _unitOfWork = unitOfWork;
            _ordertypeService = ordertypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementOrderTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetOrderType([DataSourceRequest]DataSourceRequest request)
        {
            var query = _ordertypeService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementOrderTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddOrderTypeViewModel addOrderTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newOrderType = await _ordertypeService.CreateAsync<AddOrderTypeViewModel>(addOrderTypeViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addOrderTypeViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementOrderTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<ActionResult> Edit(int id)
        {
            EditOrderTypeViewModel editOrderTypeViewModel = await _ordertypeService.GetViewModelAsync<EditOrderTypeViewModel>(x => x.Id == id);            
            return View(editOrderTypeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditOrderTypeViewModel editOrderTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _ordertypeService.UpdateAsync<EditOrderTypeViewModel>(editOrderTypeViewModel, t => t.Id == editOrderTypeViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editOrderTypeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementOrderTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _ordertypeService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
