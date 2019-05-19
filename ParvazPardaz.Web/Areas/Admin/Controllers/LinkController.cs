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
using ParvazPardaz.Service.Contract.Link;
using ParvazPardaz.Common.Filters;
using Infrastructure;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class LinkController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILinkService _linkService;
        #endregion

        #region	Ctor
        public LinkController(IUnitOfWork unitOfWork, ILinkService linkService)
        {
            _unitOfWork = unitOfWork;
            _linkService = linkService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "LinkManagement", ResourceType = typeof(ParvazPardaz.Resource.Link.LinkResource))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetLink([DataSourceRequest]DataSourceRequest request)
        {
            var query = _linkService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "LinkManagement", ResourceType = typeof(ParvazPardaz.Resource.Link.LinkResource))]

        public async Task<ActionResult> Edit(long id)
        {
            EditLinkViewModel editLinkTableViewModel = await _linkService.GetViewModelAsync<EditLinkViewModel>(x => x.Id == id);

            List<SelectListItem> TargetDDl = new List<SelectListItem>();
            TargetDDl.Add(new SelectListItem() { Text = "_blank", Value = "_blank" });
            TargetDDl.Add(new SelectListItem() { Text = "_self", Value = "_self" });
            TargetDDl.Add(new SelectListItem() { Text = "_parent", Value = "_parent" });
            TargetDDl.Add(new SelectListItem() { Text = "_top", Value = "_top" });


            List<SelectListItem> RelDDl = new List<SelectListItem>();
            RelDDl.Add(new SelectListItem() { Text = "alternate", Value = "alternate" });
            RelDDl.Add(new SelectListItem() { Text = "author", Value = "author" });
            RelDDl.Add(new SelectListItem() { Text = "bookmark", Value = "bookmark" });
            RelDDl.Add(new SelectListItem() { Text = "external", Value = "external" });
            RelDDl.Add(new SelectListItem() { Text = "help", Value = "help" });
            RelDDl.Add(new SelectListItem() { Text = "license", Value = "license" });
            RelDDl.Add(new SelectListItem() { Text = "next", Value = "next" });
            RelDDl.Add(new SelectListItem() { Text = "nofollow", Value = "nofollow" });
            RelDDl.Add(new SelectListItem() { Text = "noreferrer", Value = "noreferrer" });
            RelDDl.Add(new SelectListItem() { Text = "noopener", Value = "noopener" });
            RelDDl.Add(new SelectListItem() { Text = "prev", Value = "prev" });
            RelDDl.Add(new SelectListItem() { Text = "search", Value = "search" });
            RelDDl.Add(new SelectListItem() { Text = "tag", Value = "tag" });

            editLinkTableViewModel.RelDDL = RelDDl;
            editLinkTableViewModel.TargetDDL = TargetDDl;
            return View(editLinkTableViewModel);
        }

        [HttpPost]
        public JsonResult Edit(EditLinkViewModel editLinkTableViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = _linkService.UniqEdit(editLinkTableViewModel);
                //return RedirectToAction("Index", new { msg = "update" });
                return Json(update);
            }
            //return View(editLinkTableViewModel);
            return Json("error");
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "LinkManagement", ResourceType = typeof(ParvazPardaz.Resource.Link.LinkResource))]

        public async Task<JsonResult> Delete(long id)
        {
            var model = await _linkService.DeleteLogicallyBigEntityAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
