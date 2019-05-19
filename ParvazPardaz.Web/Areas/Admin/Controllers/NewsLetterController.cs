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
using ParvazPardaz.ViewModel.Core;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class NewsLetterController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsLetterService _NewsLetterService;
        #endregion

        #region	Ctor
        public NewsLetterController(IUnitOfWork unitOfWork, INewsLetterService NewsLetterService)
        {
            _unitOfWork = unitOfWork;
            _NewsLetterService = NewsLetterService;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetNewsLetter([DataSourceRequest]DataSourceRequest request)
        {
            var query = _NewsLetterService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddNewsLetterViewModel addNewsLetterViewModel)
        {
            if (ModelState.IsValid)
            {
                var newNewsLetter = await _NewsLetterService.CreateAsync<AddNewsLetterViewModel>(addNewsLetterViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addNewsLetterViewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            EditNewsLetterViewModel editNewsLetterViewModel = await _NewsLetterService.GetViewModelAsync<EditNewsLetterViewModel>(x => x.Id == id);
            return View(editNewsLetterViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditNewsLetterViewModel editNewsLetterViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _NewsLetterService.UpdateAsync<EditNewsLetterViewModel>(editNewsLetterViewModel, t => t.Id == editNewsLetterViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editNewsLetterViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _NewsLetterService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
