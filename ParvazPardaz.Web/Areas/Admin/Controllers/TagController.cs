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
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tags;
using Infrastructure;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TagController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagService _tagService;
        #endregion

        #region	Ctor
        public TagController(IUnitOfWork unitOfWork, ITagService tagService)
        {
            _unitOfWork = unitOfWork;
            _tagService = tagService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TagManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTag([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tagService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "TagManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTagViewModel addTagViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTag = await _tagService.CreateAsync<AddTagViewModel>(addTagViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addTagViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TagManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTagViewModel editTagViewModel = await _tagService.GetViewModelAsync<EditTagViewModel>(x => x.Id == id);
            return View(editTagViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTagViewModel editTagViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tagService.UpdateAsync<EditTagViewModel>(editTagViewModel, t => t.Id == editTagViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTagViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TagManagement", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tagService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
