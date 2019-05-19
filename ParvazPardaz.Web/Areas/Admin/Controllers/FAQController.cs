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
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class FAQController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFAQService _faqService;
        private readonly ITourService _tourService;
        #endregion

        #region	Ctor
        public FAQController(IUnitOfWork unitOfWork, IFAQService faqService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _faqService = faqService;
            _tourService = tourService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "FAQManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetFAQ([DataSourceRequest]DataSourceRequest request)
        {
            var query = _faqService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "FAQManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create()
        {
            AddFAQViewModel addVM = new AddFAQViewModel();
            addVM.TourList = _tourService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(s => new SelectListItem() { Selected = false, Value = s.Id.ToString(), Text = s.Title });
            return View(addVM);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddFAQViewModel addFAQViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFAQ = await _faqService.CreateAsync<AddFAQViewModel>(addFAQViewModel);
                return RedirectToAction("Index", new { msg = "create" });
            }
            addFAQViewModel.TourList = _tourService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(s => new SelectListItem() { Selected = false, Value = s.Id.ToString(), Text = s.Title });
            return View(addFAQViewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "FAQManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            EditFAQViewModel editFAQViewModel = await _faqService.GetViewModelAsync<EditFAQViewModel>(x => x.Id == id);
            editFAQViewModel.TourList = _tourService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == editFAQViewModel.TourId), Value = s.Id.ToString(), Text = s.Title });
            return View(editFAQViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditFAQViewModel editFAQViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _faqService.UpdateAsync<EditFAQViewModel>(editFAQViewModel, t => t.Id == editFAQViewModel.Id);
                return RedirectToAction("Index", new { msg = "update" });
            }
            editFAQViewModel.TourList = _tourService.Filter(c => c.IsDeleted == false).AsEnumerable().Select(s => new SelectListItem() { Selected = (s.Id == editFAQViewModel.TourId), Value = s.Id.ToString(), Text = s.Title });
            return View(editFAQViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "FAQManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _faqService.DeleteAsync(x => x.Id == id);
            if (model == 1)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
