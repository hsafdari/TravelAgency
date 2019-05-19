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
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourScheduleAdditionalServiceController : BaseController
    {
        #region	Fields
        private const string SectionId = "Section_TourScheduleAdditionalService_";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourScheduleAdditionalServService _tourscheduleadditionalserviceService;
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly ITourScheduleService _tourScheduleService;
        #endregion

        #region	Ctor
        public TourScheduleAdditionalServiceController(IUnitOfWork unitOfWork, ITourScheduleAdditionalServService tourscheduleadditionalserviceService, IAdditionalServiceService additionalServiceService, ITourScheduleService tourScheduleService)
        {
            _unitOfWork = unitOfWork;
            _tourscheduleadditionalserviceService = tourscheduleadditionalserviceService;
            _additionalServiceService = additionalServiceService;
            _tourScheduleService = tourScheduleService;
        }
        #endregion

        #region Create
        public PartialViewResult Create(int tourScheduleId)
        {
            var tourSchedule = _tourScheduleService.GetById(x => x.Id == tourScheduleId);
            return PartialView("_Create", new TourScheduleAdditionalServiceViewModel()
            {
                TourScheduleId = tourScheduleId,
                AdditionalServiceDDL = _additionalServiceService.GetAllAdditionalServiceOfSelectListItem(),
                ListView = tourSchedule.TourScheduleAdditionalServices.Any() ? tourSchedule.TourScheduleAdditionalServices.Select(AdServ => GetListView(AdServ)).ToList() : null
            });
        }


        [HttpPost]
        public ActionResult Create(TourScheduleAdditionalServiceViewModel addTourScheduleAdditionalServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTourScheduleAdditionalService = _tourscheduleadditionalserviceService.CreateTourScheduleAdditionalService(addTourScheduleAdditionalServiceViewModel);
                return PartialView("_ListViewTourScheduleAdditionalService", GetListView(newTourScheduleAdditionalService));
            }
            addTourScheduleAdditionalServiceViewModel.AdditionalServiceDDL = _additionalServiceService.GetAllAdditionalServiceOfSelectListItem();
            return View(addTourScheduleAdditionalServiceViewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            TourScheduleAdditionalServiceViewModel editTourScheduleAdditionalServiceViewModel = await _tourscheduleadditionalserviceService.GetViewModelAsync<TourScheduleAdditionalServiceViewModel>(x => x.Id == id);
            editTourScheduleAdditionalServiceViewModel.SectionId = SectionId + editTourScheduleAdditionalServiceViewModel.Id;
            editTourScheduleAdditionalServiceViewModel.AdditionalServiceDDL = _additionalServiceService.GetAllAdditionalServiceOfSelectListItem();
            return PartialView("_AddTourScheduleAdditionalService", editTourScheduleAdditionalServiceViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TourScheduleAdditionalServiceViewModel editTourScheduleAdditionalServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                var tourScheduleAdditionalService = _tourscheduleadditionalserviceService.UpdateTourScheduleAdditionalService(editTourScheduleAdditionalServiceViewModel);
                return PartialView("_ListViewTourScheduleAdditionalService", GetListView(tourScheduleAdditionalService));
            }
            editTourScheduleAdditionalServiceViewModel.AdditionalServiceDDL = _additionalServiceService.GetAllAdditionalServiceOfSelectListItem();
            return View(editTourScheduleAdditionalServiceViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourScheduleId)
        {
            bool success = false;
            var model = _tourscheduleadditionalserviceService.Delete(x => x.Id == id);
            if (model > 0)
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourScheduleId = tourScheduleId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region RefreshPartialView
        public PartialViewResult RefreshPartialView(int tourScheduleId)
        {
            return PartialView("_AddTourScheduleAdditionalService", new TourScheduleAdditionalServiceViewModel() { CRUDMode = CRUDMode.Create, TourScheduleId = tourScheduleId, AdditionalServiceDDL = _additionalServiceService.GetAllAdditionalServiceOfSelectListItem() });
        }
        #endregion

        public ListViewTourScheduleAdditionalServiceViewModel GetListView(TourScheduleAdditionalService tourScheduleAdditionalService)
        {
            return new ListViewTourScheduleAdditionalServiceViewModel()
            {
                AdditionalServiceTitle = _additionalServiceService.Filter(x => x.Id == tourScheduleAdditionalService.AdditionalServiceId).FirstOrDefault().Title,
                Capacity = tourScheduleAdditionalService.Capacity,
                Id = tourScheduleAdditionalService.Id,
                NonLimit = tourScheduleAdditionalService.NonLimit,
                Price = tourScheduleAdditionalService.Price,
                SoldQuantity = tourScheduleAdditionalService.SoldQuantity,
                TourScheduleId = tourScheduleAdditionalService.TourScheduleId,
                SectionId = SectionId + tourScheduleAdditionalService.Id,
                Description = tourScheduleAdditionalService.Description
            };
        }
    }
}
