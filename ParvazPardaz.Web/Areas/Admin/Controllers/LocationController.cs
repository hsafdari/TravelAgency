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
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Location;
using ParvazPardaz.Service.Contract.Tour;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class LocationController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocationService _locationService;
        private readonly ITourService _tourService;
        #endregion

        #region	Ctor
        public LocationController(IUnitOfWork unitOfWork, ILocationService locationService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _locationService = locationService;
            _tourService = tourService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        //ParvazPardaz.Resource.CMS.CMS.ManagementLocations 
        [Display(Name = "ManagementLocations", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetLocation([DataSourceRequest]DataSourceRequest request)
        {
            var query = _locationService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddLocationViewModel addLocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var newLocation = await _locationService.CreateAsync(addLocationViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addLocationViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementLocations", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditLocationViewModel editLocationViewModel = await _locationService.GetViewModelAsync<EditLocationViewModel>(x => x.Id == id);            
            return View(editLocationViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditLocationViewModel editLocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _locationService.EditAsync(editLocationViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editLocationViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementLocations", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _locationService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region CheckURLInLinkTable
        public JsonResult CheckURLInLinkTable(string URL)
        {
            var check = _locationService.CheckURLInLinkTable(URL);

            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("آدرس تکراری است", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
