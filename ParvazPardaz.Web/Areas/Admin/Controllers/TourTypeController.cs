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
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourTypeController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourTypeService _tourtypeService;
        #endregion

        #region	Ctor
        public TourTypeController(IUnitOfWork unitOfWork, ITourTypeService tourtypeService)
        {
            _unitOfWork = unitOfWork;
            _tourtypeService = tourtypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "TourTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourType([DataSourceRequest]DataSourceRequest Request)
        {
            var query = _tourtypeService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(Request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "TourTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTourTypeViewModel addTourTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTourType = await _tourtypeService.CreateAsync<AddTourTypeViewModel>(addTourTypeViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }

            return View(addTourTypeViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "TourTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTourTypeViewModel editTourTypeViewModel = await _tourtypeService.GetViewModelAsync<EditTourTypeViewModel>(x => x.Id == id);
            return View(editTourTypeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourTypeViewModel editTourTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tourtypeService.UpdateAsync<EditTourTypeViewModel>(editTourTypeViewModel, t => t.Id == editTourTypeViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editTourTypeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "TourTypeManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var isUsed = _unitOfWork.Set<Tour>().Any(x => x.TourTypes.Any(y => y.Id == id));
            if (!isUsed)
            {
                var model = await _tourtypeService.DeleteLogicallyAsync(x => x.Id == id);
                if (model != null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
