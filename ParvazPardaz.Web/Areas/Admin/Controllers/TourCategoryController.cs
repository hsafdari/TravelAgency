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
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourCategoryController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourCategoryService _tourcategoryService;
        #endregion

        #region	Ctor
        public TourCategoryController(IUnitOfWork unitOfWork, ITourCategoryService tourcategoryService)
        {
            _unitOfWork = unitOfWork;
            _tourcategoryService = tourcategoryService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourCategoryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourCategory([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tourcategoryService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "TourCategoryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTourCategoryViewModel addTourCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTourCategory = await _tourcategoryService.CreateAsync<AddTourCategoryViewModel>(addTourCategoryViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addTourCategoryViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourCategoryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTourCategoryViewModel editTourCategoryViewModel = await _tourcategoryService.GetViewModelAsync<EditTourCategoryViewModel>(x => x.Id == id);
            return View(editTourCategoryViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourCategoryViewModel editTourCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tourcategoryService.UpdateAsync<EditTourCategoryViewModel>(editTourCategoryViewModel, t => t.Id == editTourCategoryViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTourCategoryViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourCategoryManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tourcategoryService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
