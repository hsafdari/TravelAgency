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
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Content;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class ContentGroupController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContentGroupService _contentgroupService;
        #endregion

        #region	Ctor
        public ContentGroupController(IUnitOfWork unitOfWork, IContentGroupService contentgroupService)
        {
            _unitOfWork = unitOfWork;
            _contentgroupService = contentgroupService;
        }
        #endregion

        #region Index
       
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetContentGroup([DataSourceRequest]DataSourceRequest request)
        {
            var query = _contentgroupService.GetViewModelForGrid();
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
        public async Task<ActionResult> Create(AddContentGroupViewModel addContentGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newContentGroup = await _contentgroupService.CreateAsync<AddContentGroupViewModel>(addContentGroupViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addContentGroupViewModel);
        }
        #endregion

        #region Edit       
        public async Task<ActionResult> Edit(int id)
        {
            EditContentGroupViewModel editContentGroupViewModel = await _contentgroupService.GetViewModelAsync<EditContentGroupViewModel>(x => x.Id == id);            
            return View(editContentGroupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditContentGroupViewModel editContentGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _contentgroupService.UpdateAsync<EditContentGroupViewModel>(editContentGroupViewModel, t => t.Id == editContentGroupViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editContentGroupViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _contentgroupService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
