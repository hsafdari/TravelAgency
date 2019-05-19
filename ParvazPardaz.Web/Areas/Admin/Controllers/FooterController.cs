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
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Model.Entity.Core;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class FooterController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFooterService _footerService;
        private readonly ICacheService _cacheService;
        #endregion

        #region	Ctor
        public FooterController(IUnitOfWork unitOfWork, IFooterService footerService,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _footerService = footerService;
            _cacheService = cacheService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetFooter([DataSourceRequest]DataSourceRequest request)
        {
            var query = _footerService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddFooterViewModel addFooterViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFooter = await _footerService.CreateAsync<AddFooterViewModel>(addFooterViewModel);

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addFooterViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<ActionResult> Edit(int id)
        {
            EditFooterViewModel editFooterViewModel = await _footerService.GetViewModelAsync<EditFooterViewModel>(x => x.Id == id);
            return View(editFooterViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditFooterViewModel editFooterViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _footerService.UpdateAsync<EditFooterViewModel>(editFooterViewModel, t => t.Id == editFooterViewModel.Id);
                
                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editFooterViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementFooter", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<JsonResult> Delete(int id)
        {
            //اولین رکورد فعال ، برای متن قبل پانویس ها استفاده می شود ، برای همین نباید امکان حذف برای آن باشد.
            var beforeFooter = _unitOfWork.Set<Footer>().FirstOrDefault(x => !x.IsDeleted && x.IsActive);
            if (beforeFooter != null && beforeFooter.Id == id)
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

            var model = await _footerService.DeleteLogicallyAsync(x => x.Id == id);

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
