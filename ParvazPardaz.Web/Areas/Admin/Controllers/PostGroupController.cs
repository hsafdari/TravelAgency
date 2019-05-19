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
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class PostGroupController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostGroupService _postgroupService;
        private readonly IPostService _postService;
        #endregion

        #region	Ctor
        public PostGroupController(IUnitOfWork unitOfWork, IPostGroupService postgroupService, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _postgroupService = postgroupService;
            _postService = postService;
        }
        #endregion

        #region GetMenuItems
        public ActionResult GetMenuItems(int? Id)
        {
            var treeListView = _unitOfWork.Set<PostGroup>().Where(mn => Id.HasValue ? mn.ParentId == Id : mn.ParentId == null && mn.IsDeleted == false);
            var TreeList = treeListView.Select(tree => new MenuTreeViewModel
            {
                Haschildren = tree.PostGroupChildren.Where(x => x.IsDeleted == false).Any(),
                Id = tree.Id,
                NodeText = tree.Name,
                Title = tree.Title
            }).ToList();
            return Json(TreeList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "PostGroupManagement", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetPostGroup([DataSourceRequest]DataSourceRequest request)
        {
            var query = _postgroupService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "PostGroupManagement", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            AddPostGroupViewModel addPostGroupViewModel = new AddPostGroupViewModel();
            addPostGroupViewModel.PostGroupList = _postgroupService.GetAllNullParentPostGroupOfSelectListItem();
            return View(addPostGroupViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddPostGroupViewModel addPostGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newPostGroup = await _postgroupService.CreateAsync<AddPostGroupViewModel>(addPostGroupViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addPostGroupViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "PostGroupManagement", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public async Task<ActionResult> Edit(int id)
        {
            EditPostGroupViewModel editPostGroupViewModel = await _postgroupService.GetViewModelAsync<EditPostGroupViewModel>(x => x.Id == id);
            editPostGroupViewModel.PostGroupList = _postgroupService.GetAllNullParentPostGroupOfSelectListItem();
            return View(editPostGroupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditPostGroupViewModel editPostGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _postgroupService.UpdateAsync<EditPostGroupViewModel>(editPostGroupViewModel, t => t.Id == editPostGroupViewModel.Id);
                return RedirectToAction("Create", new { msg = "update" });
            }
            return View(editPostGroupViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "PostGroupManagement", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]

        public async Task<JsonResult> Delete(int id)
        {
            var postGroup = _unitOfWork.Set<PostGroup>().Find(id);
            if (!postGroup.PostGroupChildren.Any())
            {
                var model = await _postgroupService.DeleteLogicallyAsync(x => x.Id == id);
                if (model.IsDeleted == true)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            else
            {
                ViewBag.success = "couldNotCascadeDelete";
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

        #region CheckURL
        public JsonResult CheckURL(string Title)
        {
            var check = _postService.CheckURL(Title);
            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("تکراری است", JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}
