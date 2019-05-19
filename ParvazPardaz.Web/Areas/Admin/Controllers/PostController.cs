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
using ParvazPardaz.Common.Extension;
using Entity = ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Model.Entity.Product;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute()]
    public class PostController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostService _postService;
        #endregion

        #region	Ctor
        public PostController(IUnitOfWork unitOfWork, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _postService = postService;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetPost([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<GridPostViewModel> query;
            if (HttpContext.User.IsInRole("Writer"))
            {
                query = _postService.GetViewModelForGrid(HttpContext.User.Identity.Name);
            }
            else
            {
                query = _postService.GetViewModelForGrid();
            }
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult GetMenuItems(int? Id)
        {
            // int postId = 121;
            var treeListView = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).Where(mn => Id.HasValue ? mn.ParentId == Id : mn.ParentId == null && mn.IsDeleted == false);
            var TreeList = treeListView.Select(tree => new MenuTreeViewModel
            {
                Haschildren = tree.PostGroupChildren.Where(x => !x.IsDeleted && x.IsActive).Any(),
                Id = tree.Id,
                NodeText = tree.Name,
                Checked = true

            }).ToList();
            return Json(TreeList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            AddPostViewModel addPostViewModel = new AddPostViewModel();
            addPostViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            addPostViewModel._selectedPostGroups = new List<int>();
            addPostViewModel.KeywordsDDL = _postService.GetTagsForDDL();
            return View(addPostViewModel);
        }


        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(AddPostViewModel addPostViewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    var newPost = await _postService.CreateAsync<AddPostViewModel>(addPostViewModel);
            //    return RedirectToAction("Create", new { msg = "create" });
            //}
            //return View(addPostViewModel);
            //if (!addPostViewModel.File.HasFile())
            //{
            //    ModelState.AddModelError("File", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, ParvazPardaz.Resource.Tour.Tours.File));
            //    addPostViewModel._postGroups = _unitOfWork.Set<PostGroup>().ToList();
            //    addPostViewModel._selectedPostGroups = addPostViewModel._selectedPostGroups;
            //    addPostViewModel.KeywordsDDL = _postService.GetTagsForDDL();
            //    return View(addPostViewModel);
            //}
            if (ModelState.IsValid)
            {
                //ذخیره کلیدواژه ها بصورت همروند
                #region Save Tags
                if (addPostViewModel.TagTitles != null && addPostViewModel.TagTitles.Any())
                {
                    foreach (var tagTitle in addPostViewModel.TagTitles.ToList())
                    {
                        var tagInDB = _unitOfWork.Set<ProductTag>().FirstOrDefault(x => x.Title.Equals(tagTitle));
                        if (tagInDB == null)
                        {
                            ProductTag newTag = new ProductTag() { Title = tagTitle };
                            _unitOfWork.Set<ProductTag>().Add(newTag);
                        }
                    }

                    _unitOfWork.SaveAllChanges();
                }
                #endregion
                addPostViewModel.LinkTableTitle=addPostViewModel.LinkTableTitle.ToLower();
                if (!addPostViewModel.LinkTableTitle.StartsWith("page-"))
                {
                    addPostViewModel.LinkTableTitle= "/tourism/" + addPostViewModel.LinkTableTitle;
                }
                var newPost = await _postService.CreateAsync(addPostViewModel);
                int groupid = _unitOfWork.Set<PostGroup>().Where(x => x.Title == "introducing-hotels").Select(x => x.Id).FirstOrDefault();
                if (addPostViewModel._selectedPostGroups.Contains(groupid))
                {
                    return RedirectToAction("CreateGallary", "PostImage", new { postId = newPost.Id });
                }
                return RedirectToAction("Create", "PostImage", new { postId = newPost.Id });
            }
            addPostViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            addPostViewModel._selectedPostGroups = addPostViewModel._selectedPostGroups;
            addPostViewModel.KeywordsDDL = _postService.GetTagsForDDL();
            return View(addPostViewModel);
        }
        #endregion

        #region Save tags
        //[HttpPost]
        //public bool SaveTags(List<string> tags)
        //{
        //    if (tags != null && tags.Any())
        //    {
        //        foreach (var tagTitle in tags.ToList())
        //        {
        //            var tagInDB = _unitOfWork.Set<Entity.Tag>().FirstOrDefault(x => x.Name.Equals(tagTitle));
        //            if (tagInDB == null)
        //            {
        //                Entity.Tag newTag = new Entity.Tag() { Name = tagTitle };
        //                _unitOfWork.Set<Entity.Tag>().Add(newTag);
        //            }
        //        }

        //        _unitOfWork.SaveAllChanges();
        //    }
        //    return true;
        //}
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            var model = _postService.GetById(x => x.Id == id);
            EditPostViewModel editPostViewModel = await _postService.GetViewModelAsync<EditPostViewModel>(x => x.Id == id);
            editPostViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            editPostViewModel._selectedPostGroups = model.PostGroups.Select(z => z.Id).ToList();
            editPostViewModel.KeywordsDDL = _postService.GetTagsForDDL();
            editPostViewModel.TagTitles = model.Tags.Select(x => x.Name).ToList();
            return View(editPostViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(EditPostViewModel editPostViewModel)
        {
            if (ModelState.IsValid)
            {

                //ذخیره کلیدواژه ها بصورت همروند
                #region Save Tags
                if (editPostViewModel.TagTitles != null && editPostViewModel.TagTitles.Any())
                {
                    foreach (var tagTitle in editPostViewModel.TagTitles.ToList())
                    {
                        var tagInDB = _unitOfWork.Set<ProductTag>().FirstOrDefault(x => x.Title.Equals(tagTitle));
                        if (tagInDB == null)
                        {
                            ProductTag newTag = new ProductTag() { Title = tagTitle };
                            _unitOfWork.Set<ProductTag>().Add(newTag);
                        }
                    }

                    _unitOfWork.SaveAllChanges();
                }
                #endregion


                var update = await _postService.EditAsync(editPostViewModel);
                return RedirectToAction("Index", "Post", new { message = "update" });
            }
            return View(editPostViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _postService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region CheckURL
        public JsonResult CheckURL(string Name)
        {
            var check = _postService.CheckURL(Name);
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

        #region CheckLinkTableURL
        public JsonResult CheckLinkTableURL(string LinkTableTitle)
        {
            var check = _postService.CheckURL(LinkTableTitle);
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
