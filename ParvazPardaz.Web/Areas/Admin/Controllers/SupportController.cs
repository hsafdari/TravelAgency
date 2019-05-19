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
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class SupportController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupportService _supportService;
        #endregion

        #region	Ctor
        public SupportController(IUnitOfWork unitOfWork, ISupportService supportService)
        {
            _unitOfWork = unitOfWork;
            _supportService = supportService;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetSupport([DataSourceRequest]DataSourceRequest request)
        {
            var query = _supportService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddSupportViewModel addSupportViewModel)
        {
            if (ModelState.IsValid)
            {
                var newSupport = await _supportService.CreateAsync<AddSupportViewModel>(addSupportViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addSupportViewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            EditSupportViewModel editSupportViewModel = await _supportService.GetViewModelAsync<EditSupportViewModel>(x => x.Id == id);
            return View(editSupportViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditSupportViewModel editSupportViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _supportService.UpdateAsync<EditSupportViewModel>(editSupportViewModel, t => t.Id == editSupportViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editSupportViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _supportService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
