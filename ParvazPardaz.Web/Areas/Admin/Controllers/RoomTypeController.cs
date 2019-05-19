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
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Book;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class RoomTypeController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomTypeService _roomtypeService;
        #endregion

        #region	Ctor
        public RoomTypeController(IUnitOfWork unitOfWork, IRoomTypeService roomtypeService)
        {
            _unitOfWork = unitOfWork;
            _roomtypeService = roomtypeService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementRoomTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetRoomType([DataSourceRequest]DataSourceRequest request)
        {
            var query = _roomtypeService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "ManagementRoomTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddRoomTypeViewModel addRoomTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newRoomType = await _roomtypeService.CreateAsync<AddRoomTypeViewModel>(addRoomTypeViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addRoomTypeViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementRoomTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<ActionResult> Edit(int id)
        {
            EditRoomTypeViewModel editRoomTypeViewModel = await _roomtypeService.GetViewModelAsync<EditRoomTypeViewModel>(x => x.Id == id);            
            return View(editRoomTypeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRoomTypeViewModel editRoomTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _roomtypeService.UpdateAsync<EditRoomTypeViewModel>(editRoomTypeViewModel, t => t.Id == editRoomTypeViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editRoomTypeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementRoomTypes", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _roomtypeService.DeleteLogicallyAsync(x => x.Id == id);            
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
        
    }
}
