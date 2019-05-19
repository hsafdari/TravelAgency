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
using ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Comment;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class ReviewController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewService _reviewService;
        #endregion

        #region	Ctor
        public ReviewController(IUnitOfWork unitOfWork, IReviewService reviewService)
        {
            _unitOfWork = unitOfWork;
            _reviewService = reviewService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementReviewItems", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetReview([DataSourceRequest]DataSourceRequest request)
        {
            var query = _reviewService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementReviewItems", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddReviewViewModel addReviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var newReview = await _reviewService.CreateAsync<AddReviewViewModel>(addReviewViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addReviewViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementReviewItems", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditReviewViewModel editReviewViewModel = await _reviewService.GetViewModelAsync<EditReviewViewModel>(x => x.Id == id);            
            return View(editReviewViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditReviewViewModel editReviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _reviewService.UpdateAsync<EditReviewViewModel>(editReviewViewModel, t => t.Id == editReviewViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editReviewViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementReviewItems", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _reviewService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
