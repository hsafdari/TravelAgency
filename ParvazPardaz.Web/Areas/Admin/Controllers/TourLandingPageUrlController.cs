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
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Enum;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourLandingPageUrlController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourLandingPageUrlService _tourlandingpageurlService;
        private readonly ICityService _cityService;
        #endregion

        #region	Ctor
        public TourLandingPageUrlController(IUnitOfWork unitOfWork, ITourLandingPageUrlService tourlandingpageurlService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _tourlandingpageurlService = tourlandingpageurlService;
            _cityService = cityService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementTourLandingPageUrls", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTourLandingPageUrl([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tourlandingpageurlService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region FindUrlsByCityId
        public JsonResult FindUrlsByCityId(int id, EnumLandingPageUrlType landingPageUrlType)
        {
            return Json(_tourlandingpageurlService.FindUrlsByCityId(id, landingPageUrlType), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FindUrlsByCityIdEditMode
        public JsonResult FindUrlsByCityIdEditMode(int id, int previousUrlId, EnumLandingPageUrlType landingPageUrlType)
        {
            return Json(_tourlandingpageurlService.FindUrlsByCityIdEditMode(id, previousUrlId,landingPageUrlType), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementTourLandingPageUrls", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;

            var newUrl = new AddTourLandingPageUrlViewModel();
            newUrl.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View(newUrl);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTourLandingPageUrlViewModel addTourLandingPageUrlViewModel)
        {
            if (ModelState.IsValid)
            {
                #region manipulate url
                addTourLandingPageUrlViewModel.URL = addTourLandingPageUrlViewModel.URL.StartsWith("/") ? addTourLandingPageUrlViewModel.URL : "/" + addTourLandingPageUrlViewModel.URL;
                addTourLandingPageUrlViewModel.URL = addTourLandingPageUrlViewModel.URL.EndsWith("/") ? addTourLandingPageUrlViewModel.URL : addTourLandingPageUrlViewModel.URL + "/";
                addTourLandingPageUrlViewModel.URL = addTourLandingPageUrlViewModel.URL.Replace(" ","-");
                #endregion

                var newTourLandingPageUrl = await _tourlandingpageurlService.CreateAsync<AddTourLandingPageUrlViewModel>(addTourLandingPageUrlViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            addTourLandingPageUrlViewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View(addTourLandingPageUrlViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementTourLandingPageUrls", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditTourLandingPageUrlViewModel editTourLandingPageUrlViewModel = await _tourlandingpageurlService.GetViewModelAsync<EditTourLandingPageUrlViewModel>(x => x.Id == id);
            editTourLandingPageUrlViewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View(editTourLandingPageUrlViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTourLandingPageUrlViewModel editTourLandingPageUrlViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tourlandingpageurlService.UpdateAsync<EditTourLandingPageUrlViewModel>(editTourLandingPageUrlViewModel, t => t.Id == editTourLandingPageUrlViewModel.Id);

                return RedirectToAction("Index", new { msg = "update" });
            }
            editTourLandingPageUrlViewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return View(editTourLandingPageUrlViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementTourLandingPageUrls", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tourlandingpageurlService.DeleteLogicallyAsync(x => x.Id == id);
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
            var check = _tourlandingpageurlService.CheckURLInLinkTable(URL);

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
