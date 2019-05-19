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
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Model.Entity.Core;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class SliderController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISliderService _sliderService;
        private readonly ICacheService _cacheService;
        #endregion

        #region	Ctor
        public SliderController(IUnitOfWork unitOfWork, ISliderService sliderService, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _sliderService = sliderService;
            _cacheService = cacheService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementSlider", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetSlider([DataSourceRequest]DataSourceRequest request)
        {
            var query = _sliderService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementSlider", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create()
        {
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddSliderViewModel addSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                //addSliderViewModel.SliderGroupID = 1; //Home Slider's ID
                var newSlider = await _sliderService.CreateAsync(addSliderViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(addSliderViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementSlider", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditSliderViewModel editSliderViewModel = await _sliderService.GetViewModelAsync<EditSliderViewModel>(x => x.Id == id);
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(editSliderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditSliderViewModel editSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _sliderService.EditAsync(editSliderViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text"); 
            return View(editSliderViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementSlider", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _sliderService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementSliderHome", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult HomeSliderIndex(string msg)
        {
            ViewBag.success = msg;
            return View();
        }
        public ActionResult GetHomeSlider([DataSourceRequest]DataSourceRequest request)
        {
            var query = _sliderService.GetViewModelForGridHome();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #region SliderCreate
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementSliderHome", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult HomeSliderCreate()
        {
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && (x.Name == "HomeSlider" || x.Name == "HomeSlider2")).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> HomeSliderCreate(AddTourHomeSliderViewModel addSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                //addSliderViewModel.SliderGroupID = 1; //Home Slider's ID
                var newSlider = await _sliderService.CreateAsyncHome(addSliderViewModel);
                return RedirectToAction("HomeSliderIndex", new { msg = "create" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && (x.Name == "HomeSlider" || x.Name == "HomeSlider2")).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(addSliderViewModel);
        }
        #endregion
        
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementSliderHome", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<ActionResult> HomeSliderEdit(int id)
        {
            EditTourHomeSliderViewModel editSliderViewModel = await _sliderService.GetViewModelAsync<EditTourHomeSliderViewModel>(x => x.Id == id);
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && (x.Name == "HomeSlider" || x.Name == "HomeSlider2")).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(editSliderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> HomeSliderEdit(EditTourHomeSliderViewModel tourHomeSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _sliderService.EditAsyncHome(tourHomeSliderViewModel);
                return RedirectToAction("HomeSliderIndex", new { msg = "update" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && (x.Name == "HomeSlider" || x.Name == "HomeSlider2")).Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(tourHomeSliderViewModel);
        }
        
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementSliderTourLanding", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult TourLandingSliderIndex(string msg)
        {
            ViewBag.success = msg;
            return View();
        }
        public ActionResult GetTourLandingSlider([DataSourceRequest]DataSourceRequest request)
        {
            var query = _sliderService.GetViewModelForGridTourLanding();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #region SliderCreate
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementSliderTourLanding", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult TourLandingSliderCreate()
        {
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && x.Name == "TourLanding").Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> TourLandingSliderCreate(AddTourHomeSliderViewModel addSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                //addSliderViewModel.SliderGroupID = 1; //Home Slider's ID
                var newSlider = await _sliderService.CreateAsyncHome(addSliderViewModel);
                return RedirectToAction("TourLandingSliderIndex", new { msg = "create" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && x.Name == "TourLanding").Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(addSliderViewModel);
        }
        #endregion

        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementSliderTourLanding", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> TourLandingSliderEdit(int id)
        {
            EditTourHomeSliderViewModel editSliderViewModel = await _sliderService.GetViewModelAsync<EditTourHomeSliderViewModel>(x => x.Id == id);
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && x.Name == "TourLanding").Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(editSliderViewModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> TourLandingSliderEdit(EditTourHomeSliderViewModel tourLandingSliderViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _sliderService.EditAsyncHome(tourLandingSliderViewModel);
                return RedirectToAction("TourLandingSliderIndex", new { msg = "update" });
            }
            ViewBag.SliderGroupDDL = new SelectList(_unitOfWork.Set<SliderGroup>().Where(x => !x.IsDeleted && x.IsActive && x.Name == "TourLanding").Select(x => new SelectListItem() { Text = x.GroupTitle, Value = x.Id.ToString() }), "Value", "Text");
            return View(tourLandingSliderViewModel);
        }
    }
}
