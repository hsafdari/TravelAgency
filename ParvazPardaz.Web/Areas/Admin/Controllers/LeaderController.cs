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
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class LeaderController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaderService _leaderService;
        #endregion

        #region	Ctor
        public LeaderController(IUnitOfWork unitOfWork, ILeaderService leaderService)
        {
            _unitOfWork = unitOfWork;
            _leaderService = leaderService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "LeaderManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetLeader([DataSourceRequest]DataSourceRequest request)
        {
            var query = _leaderService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "LeaderManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddLeaderViewModel addLeaderViewModel)
        {
            if (ModelState.IsValid)
            {
                var newLeader = await _leaderService.CreateAsync<AddLeaderViewModel>(addLeaderViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            return View(addLeaderViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "LeaderManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditLeaderViewModel editLeaderViewModel = await _leaderService.GetViewModelAsync<EditLeaderViewModel>(x => x.Id == id);
            return View(editLeaderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditLeaderViewModel editLeaderViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _leaderService.UpdateAsync<EditLeaderViewModel>(editLeaderViewModel, t => t.Id == editLeaderViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editLeaderViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "LeaderManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _leaderService.DeleteLogicallyAsync(x => x.Id == id);
            if (model != null)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
