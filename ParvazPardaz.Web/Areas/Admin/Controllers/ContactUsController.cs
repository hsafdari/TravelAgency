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
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class ContactUsController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactUsService _contactusService;
        #endregion

        #region	Ctor
        public ContactUsController(IUnitOfWork unitOfWork, IContactUsService contactusService)
        {
            _unitOfWork = unitOfWork;
            _contactusService = contactusService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementContactUs", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetContactUs([DataSourceRequest]DataSourceRequest request)
        {
            var query = _contactusService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            ViewBag.DepartmentDDL = new SelectList(_unitOfWork.Set<Department>().Where(x => !x.IsDeleted && x.IsActive), "Id", "DepartmentName");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddContactUsViewModel addContactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                var newContactUs = await _contactusService.CreateAsync<AddContactUsViewModel>(addContactUsViewModel);
                return RedirectToAction("Create", new { msg = "create" });
            }
            return View(addContactUsViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ManagementContactUs", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<ActionResult> Edit(int id)
        {
            EditContactUsViewModel editContactUsViewModel = await _contactusService.GetViewModelAsync<EditContactUsViewModel>(x => x.Id == id);
            return View(editContactUsViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditContactUsViewModel editContactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _contactusService.UpdateAsync<EditContactUsViewModel>(editContactUsViewModel, t => t.Id == editContactUsViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            return View(editContactUsViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementContactUs", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _contactusService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
