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
using Infrastructure;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
//using ParvazPardaz.Service.DataAccessService.RequiredDocument;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class RequiredDocumentController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequiredDocumentService _requireddocumentService;
        #endregion

        #region	Ctor
        public RequiredDocumentController(IUnitOfWork unitOfWork, IRequiredDocumentService requireddocumentService)
        {
            _unitOfWork = unitOfWork;
            _requireddocumentService = requireddocumentService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementTourRequiredDocs", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetRequiredDocument([DataSourceRequest]DataSourceRequest request)
        {
            var query = _requireddocumentService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementTourRequiredDocs", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddRequiredDocumentViewModel addRequiredDocumentViewModel)
        {
            if (ModelState.IsValid)
            {
                var newRequiredDocument = await _requireddocumentService.CreateAsync<AddRequiredDocumentViewModel>(addRequiredDocumentViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addRequiredDocumentViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementTourRequiredDocs", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditRequiredDocumentViewModel editRequiredDocumentViewModel = await _requireddocumentService.GetViewModelAsync<EditRequiredDocumentViewModel>(x => x.Id == id);            
            return View(editRequiredDocumentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRequiredDocumentViewModel editRequiredDocumentViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _requireddocumentService.UpdateAsync<EditRequiredDocumentViewModel>(editRequiredDocumentViewModel, t => t.Id == editRequiredDocumentViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editRequiredDocumentViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementTourRequiredDocs", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _requireddocumentService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
