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
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class AirportController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAirportService _airportService;
        private readonly ICityService _cityService; 
        #endregion

        #region	Ctor
        public AirportController(IUnitOfWork unitOfWork, IAirportService airportService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _airportService = airportService;
            _cityService = cityService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetAirport([DataSourceRequest]DataSourceRequest request)
        {
            var query = _airportService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddAirportViewModel addAirportViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAirport = await _airportService.CreateAsync<AddAirportViewModel>(addAirportViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addAirportViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditAirportViewModel editAirportViewModel = await _airportService.GetViewModelAsync<EditAirportViewModel>(x => x.Id == id);
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View(editAirportViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditAirportViewModel editAirportViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _airportService.UpdateAsync<EditAirportViewModel>(editAirportViewModel, t => t.Id == editAirportViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editAirportViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementAirport", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _airportService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region GetAirports
        public JsonResult GetAirports(int id)
        {
            return Json(_airportService.Filter(c => c.CityId == id).Select(s => new FilterList() { Id = s.Id, Title = s.Title }).AsEnumerable(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
