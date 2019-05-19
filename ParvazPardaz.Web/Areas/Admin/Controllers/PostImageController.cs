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
using ParvazPardaz.Common.HtmlHelpers.Models;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute()]
    public class PostImageController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostImageService _postImageService;
        private readonly IPostService _postService;
        #endregion

        #region	Ctor
        public PostImageController(IUnitOfWork unitOfWork, IPostImageService postImageService, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _postImageService = postImageService;
            _postService = postService;
        }
        #endregion

        #region Create
        public ActionResult Create(int postId)
        {
            return View(new AddPostImageViewModel()
            {
                PostId = postId,
                EditModeFileUploads = _postService.GetById(x => x.Id == postId).PostImages
                                                  .Where(t => t.IsPrimarySlider == false)
                                                  .Select(s => new EditModeFileUpload()
                                                              {
                                                                  Name = s.ImageFileName,
                                                                  Size = s.ImageSize,
                                                                  Type = s.ImageExtension,
                                                                  Url = s.ImageUrl,
                                                                  Id = s.Id
                                                              }).ToList(),
                EditModeFileUploads765 = _postService.GetById(x => x.Id == postId).PostImages
                      .Where(t => t.Width == 765 && t.Height == 535)
                      .Select(s => new EditModeFileUpload()
                      {
                          Name = s.ImageFileName,
                          Size = s.ImageSize,
                          Type = s.ImageExtension,
                          Url = s.ImageUrl + "-765x535" + s.ImageExtension,
                          Id = s.Id
                      }).ToList(),
                EditModeFileUploads575 = _postService.GetById(x => x.Id == postId).PostImages
                     .Where(t => t.Width == 575 && t.Height == 530)
                     .Select(s => new EditModeFileUpload()
                     {
                         Name = s.ImageFileName,
                         Size = s.ImageSize,
                         Type = s.ImageExtension,
                         Url = s.ImageUrl + "-575x530" + s.ImageExtension,
                         Id = s.Id
                     }).ToList(),
                EditModeFileUploads370 = _postService.GetById(x => x.Id == postId).PostImages
                     .Where(t => t.Width == 370 && t.Height == 292)
                     .Select(s => new EditModeFileUpload()
                     {
                         Name = s.ImageFileName,
                         Size = s.ImageSize,
                         Type = s.ImageExtension,
                         Url = s.ImageUrl + "-370x292" + s.ImageExtension,
                         Id = s.Id
                     }).ToList(),
                EditModeFileUploads277 = _postService.GetById(x => x.Id == postId).PostImages
                     .Where(t => t.Width == 277 && t.Height == 186)
                     .Select(s => new EditModeFileUpload()
                     {
                         Name = s.ImageFileName,
                         Size = s.ImageSize,
                         Type = s.ImageExtension,
                         Url = s.ImageUrl + "-277x186" + s.ImageExtension,
                         Id = s.Id
                     }).ToList(),
                EditModeFileUploads98 = _postService.GetById(x => x.Id == postId).PostImages
                     .Where(t => t.Width == 98 && t.Height == 98)
                     .Select(s => new EditModeFileUpload()
                     {
                         Name = s.ImageFileName,
                         Size = s.ImageSize,
                         Type = s.ImageExtension,
                         Url = s.ImageUrl + "-98x98" + s.ImageExtension,
                         Id = s.Id
                     }).ToList(),
                EditModeFileUploadsForPrimarySlider = _postService.GetById(x => x.Id == postId).PostImages
                                                                  .Where(t => t.IsPrimarySlider)
                                                                  .Select(s => new EditModeFileUpload()
                                                                          {
                                                                              Name = s.ImageFileName,
                                                                              Size = s.ImageSize,
                                                                              Type = s.ImageExtension,
                                                                              Url = s.ImageUrl,
                                                                              Id = s.Id
                                                                          }).ToList()
            });
        }
        public ActionResult CreateGallary(int postId)
        {
            ImageSliderViewModel model = new ImageSliderViewModel();
            model.PostId = postId;
            model.EditModeFileUploads = _postService.GetById(x => x.Id == postId).PostImages
                                                  .Where(t => t.IsPrimarySlider == false)
                                                  .Select(s => new EditModeFileUpload()
                                                              {
                                                                  Name = s.ImageFileName,
                                                                  Size = s.ImageSize,
                                                                  Type = s.ImageExtension,
                                                                  Url = s.ImageUrl + "-261x177" + s.ImageExtension,
                                                                  Id = s.Id
                                                              }).ToList();
            return View(model);

        }
        #endregion

        #region Upload
        [HttpPost]
        public JsonResult Upload(AddPostImageViewModel addTourSliderViewModel)
        {
            PostImage postimage = _postImageService.Upload(addTourSliderViewModel);
            return Json(postimage.Id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateGallaryUpload(ImageSliderViewModel addTourSliderViewModel)
        {
            PostImage postimage = _postImageService.UpoloadGallery(addTourSliderViewModel);
            return Json(postimage.Id, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //[HttpPost]
        //public ActionResult CheckForm(AddTourSliderViewModel addTourSliderViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("Create", "TourProgram", new { tourId = addTourSliderViewModel.TourId });
        //    }
        //    var tour = _tourService.GetById(x => x.Id == addTourSliderViewModel.TourId);
        //    addTourSliderViewModel.EditModeFileUploads = tour.TourSliders
        //                                                 .Where(t => t.IsPrimarySlider == false)
        //                                                 .Select(s => new EditModeFileUpload()
        //                                                 {
        //                                                     Name = s.ImageFileName,
        //                                                     Size = s.ImageSize,
        //                                                     Type = s.ImageExtension,
        //                                                     Url = s.ImageUrl,
        //                                                     Id = s.Id
        //                                                 }).ToList();
        //    addTourSliderViewModel.EditModeFileUploadsForPrimarySlider = tour.TourSliders
        //                                                          .Where(t => t.IsPrimarySlider)
        //                                                          .Select(s => new EditModeFileUpload()
        //                                                          {
        //                                                              Name = s.ImageFileName,
        //                                                              Size = s.ImageSize,
        //                                                              Type = s.ImageExtension,
        //                                                              Url = s.ImageUrl,
        //                                                              Id = s.Id
        //                                                          }).ToList();
        //    return View("Create", addTourSliderViewModel);
        //}

        //#region RemoveUpload
        //[HttpPost]
        //public JsonResult RemoveUpload(int id)
        //{
        //    var removeHotelGallery = _toursliderService.RemoveUpload(id);
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        #region Delete
        public JsonResult Delete(int id)
        {
            if (_postImageService.Remove(id))
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        public JsonResult DeleteGallery(int id)
        {
            if (_postImageService.RemoveGallery(id))
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
