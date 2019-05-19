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
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.LinkRedirection;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class LinkRedirectionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILinkRedirectionService _linkredirectionService;
        #endregion

        #region	Ctor
        public LinkRedirectionController(IUnitOfWork unitOfWork, ILinkRedirectionService linkredirectionService)
        {
            _unitOfWork = unitOfWork;
            _linkredirectionService = linkredirectionService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementLinkRedirection", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetLinkRedirection([DataSourceRequest]DataSourceRequest request)
        {
            var query = _linkredirectionService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementLinkRedirection", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddLinkRedirectionViewModel addLinkRedirectionViewModel)
        {
            if (ModelState.IsValid)
            {
                var newLinkRedirection = await _linkredirectionService.CreateAsync<AddLinkRedirectionViewModel>(addLinkRedirectionViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addLinkRedirectionViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementLinkRedirection", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<ActionResult> Edit(int id)
        {
            EditLinkRedirectionViewModel editLinkRedirectionViewModel = await _linkredirectionService.GetViewModelAsync<EditLinkRedirectionViewModel>(x => x.Id == id);            
            return View(editLinkRedirectionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditLinkRedirectionViewModel editLinkRedirectionViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _linkredirectionService.UpdateAsync<EditLinkRedirectionViewModel>(editLinkRedirectionViewModel, t => t.Id == editLinkRedirectionViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editLinkRedirectionViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementLinkRedirection", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _linkredirectionService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
