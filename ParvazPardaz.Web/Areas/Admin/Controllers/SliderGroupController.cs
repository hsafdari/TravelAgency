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
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class SliderGroupController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISliderGroupService _slidergroupService;
        #endregion

        #region	Ctor
        public SliderGroupController(IUnitOfWork unitOfWork, ISliderGroupService slidergroupService)
        {
            _unitOfWork = unitOfWork;
            _slidergroupService = slidergroupService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementSliderGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetSliderGroup([DataSourceRequest]DataSourceRequest request)
        {
            var query = _slidergroupService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementSliderGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddSliderGroupViewModel addSliderGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newSliderGroup = await _slidergroupService.CreateAsync<AddSliderGroupViewModel>(addSliderGroupViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addSliderGroupViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementSliderGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<ActionResult> Edit(int id)
        {
            EditSliderGroupViewModel editSliderGroupViewModel = await _slidergroupService.GetViewModelAsync<EditSliderGroupViewModel>(x => x.Id == id);
            return View(editSliderGroupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditSliderGroupViewModel editSliderGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _slidergroupService.UpdateAsync<EditSliderGroupViewModel>(editSliderGroupViewModel, t => t.Id == editSliderGroupViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editSliderGroupViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementSliderGroup", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _slidergroupService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
