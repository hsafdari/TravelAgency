using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Tour;
using ParvazPardaz.ViewModel;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourLevelController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourLevelService _tourLevelService;
        #endregion

        #region	Ctor
        public TourLevelController(IUnitOfWork unitOfWork, ITourLevelService tourLevelService)
        {
            _unitOfWork = unitOfWork;
            _tourLevelService = tourLevelService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourLevelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]        
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourLevel([DataSourceRequest] DataSourceRequest request)
        {
            var query = _tourLevelService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "TourLevelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]  
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddTourLevelViewModel addTourLevelViewModel)
        {
            if (ModelState.IsValid)
            {
                var newSample = await _tourLevelService.CreateAsync<AddTourLevelViewModel>(addTourLevelViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addTourLevelViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourLevelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]  
        public async Task<ActionResult> Edit(int id)
        {
            var viewModel = await _tourLevelService.GetViewModelAsync<EditTourLevelViewModel>(t => t.Id == id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourLevelViewModel editTourLevelViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tourLevelService.UpdateAsync<EditTourLevelViewModel>(editTourLevelViewModel, t => t.Id == editTourLevelViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTourLevelViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourLevelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]  
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tourLevelService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}