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
using ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Product;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
     [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class CommentController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentService _commentService;
        #endregion

        #region	Ctor
        public CommentController(IUnitOfWork unitOfWork, ICommentService commentService)
        {

            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public ActionResult Index(string msg)
        {

            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetComment([DataSourceRequest]DataSourceRequest request)
        {
            var query = _commentService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public async Task<ActionResult> Edit(int id)
        {
            EditCommentViewModel editCommentViewModel = await _commentService.GetViewModelAsync<EditCommentViewModel>(x => x.Id == id);
            return View(editCommentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCommentViewModel editCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _commentService.UpdateAsync<EditCommentViewModel>(editCommentViewModel, t => t.Id == editCommentViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editCommentViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _commentService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
