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
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.DataAccessService.Magazine;
using ParvazPardaz.ViewModel.Magazine;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.Contract.Location;
using Infrastructure;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TabMagazineController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITabMagazineService _tabmagazineService;
        private readonly ICountryService _countryService;
        private readonly ILocationService _locationService;
        private readonly ICacheService _cacheService;
        #endregion

        #region	Ctor
        public TabMagazineController(IUnitOfWork unitOfWork, ITabMagazineService tabmagazineService, ICountryService countryService, ICacheService cacheService, ILocationService locationService)
        {
            _unitOfWork = unitOfWork;
            _tabmagazineService = tabmagazineService;
            _countryService = countryService;
            _cacheService = cacheService;
            _locationService = locationService;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetTabMagazine([DataSourceRequest]DataSourceRequest request)
        {
            var query = _tabmagazineService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult Create(string msg)
        {
            AddTabMagazineViewModel newTabMag = new AddTabMagazineViewModel();
            newTabMag.CountryDDL = _locationService.GetAllLocationOfSelectListItem();
            newTabMag.GroupMSL = new SelectList(_unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }), "Value", "Text");
            ViewBag.success = msg;
            return View(newTabMag);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddTabMagazineViewModel addTabMagazineViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTabMagazine = await _tabmagazineService.CreateTabMagAsync(addTabMagazineViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            addTabMagazineViewModel.CountryDDL = _locationService.GetAllLocationOfSelectListItem();
            addTabMagazineViewModel.GroupMSL = new SelectList(_unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }), "Value", "Text");
            return View(addTabMagazineViewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            EditTabMagazineViewModel editTabMagazineViewModel = await _tabmagazineService.GetViewModelAsync<EditTabMagazineViewModel>(x => x.Id == id);
            editTabMagazineViewModel.CountryDDL = _locationService.GetAllLocationOfSelectListItem();
            editTabMagazineViewModel.GroupMSL = new SelectList(_unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }), "Value", "Text");
            return View(editTabMagazineViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditTabMagazineViewModel editTabMagazineViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _tabmagazineService.UpdateTabMagAsync(editTabMagazineViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }

            editTabMagazineViewModel.CountryDDL = _locationService.GetAllLocationOfSelectListItem();
            editTabMagazineViewModel.GroupMSL = new SelectList(_unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }), "Value", "Text");
            return View(editTabMagazineViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _tabmagazineService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
