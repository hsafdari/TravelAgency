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
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourScheduleCompanyTransferController : BaseController
    {
        #region	Fields
        private const string SectionId = "Section_TourScheduleCompanyTransfer_";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourScheduleCompanyTransferService _tourschedulecompanytransferService;
        private readonly ICompanyTransferService _companyTransferService;
        private readonly ICityService _cityService;
        private readonly ITourScheduleService _tourScheduleService;
        private readonly IVehicleTypeService _vehicleTypeService;
        #endregion

        #region	Ctor
        public TourScheduleCompanyTransferController(IUnitOfWork unitOfWork, ITourScheduleCompanyTransferService tourschedulecompanytransferService, ICompanyTransferService companyTransferService, ITourScheduleService tourScheduleService
                                                     , ICityService cityService, IVehicleTypeService vehicleTypeService)
        {
            _unitOfWork = unitOfWork;
            _tourschedulecompanytransferService = tourschedulecompanytransferService;
            _companyTransferService = companyTransferService;
            _tourScheduleService = tourScheduleService;
            _cityService = cityService;
            _vehicleTypeService = vehicleTypeService;
        }
        #endregion

        #region Create
        public PartialViewResult Create(int tourScheduleId)
        {
            var tourSchedule = _tourScheduleService.GetById(x => x.Id == tourScheduleId);
            var tourSchCoTransViewModel = new TourScheduleCompanyTransferViewModel()
            {
                TourScheduleId = tourScheduleId,
                ListView = tourSchedule.TourScheduleCompanyTransfers.Any() ? tourSchedule.TourScheduleCompanyTransfers.Select(tourScheduleCompanyTransfer => GetListView(tourScheduleCompanyTransfer)).ToList() : null
            };
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            return PartialView("_Create", tourSchCoTransViewModel);
        }


        [HttpPost]

        public ActionResult Create(TourScheduleCompanyTransferViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _tourschedulecompanytransferService.Create<TourScheduleCompanyTransferViewModel>(viewModel);
                return PartialView("_ListViewTourScheduleCompanyTransfer", GetListView(model));
            }
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            TourScheduleCompanyTransferViewModel viewModel = await _tourschedulecompanytransferService.GetViewModelAsync<TourScheduleCompanyTransferViewModel>(x => x.Id == id);
            viewModel.SectionId = SectionId + viewModel.Id;
            //viewModel.FromAirportId = _cityService.ShowCityNameWithCountry(viewModel.FromAirportId);
            //viewModel.DestinationAirportTitle = _cityService.ShowCityNameWithCountry(viewModel.DestinationAirportId);
            viewModel.DepartureTime = viewModel.DepartureDate.TimeOfDay;
            viewModel.ArrivalTime = viewModel.ArrivalDate.TimeOfDay;
            viewModel.CompanyTransferId = viewModel.CompanyTransferId;
            //ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            //ViewBag.CompanyTransfers = _companyTransferService.GetById(c=>c.Id==viewModel.CompanyTransferId) new SelectList(new List<SelectListItem>(), "Id", "Title");
            return PartialView("_AddTourScheduleCompanyTransfer", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TourScheduleCompanyTransferViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _tourschedulecompanytransferService.Update<TourScheduleCompanyTransferViewModel>(viewModel, t => t.Id == viewModel.Id);
                return PartialView("_ListViewTourScheduleCompanyTransfer", GetListView(model));
            }
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourScheduleId)
        {
            bool success = false;
            var model = _tourschedulecompanytransferService.Delete(x => x.Id == id);
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
            ViewBag.VehicleTypes = _vehicleTypeService.GetAllVehicleTypesOfSelectListItem();
            ViewBag.CompanyTransfers = new SelectList(new List<SelectListItem>(), "Id", "Title");
            return PartialView("_AddTourScheduleCompanyTransfer", new TourScheduleCompanyTransferViewModel() { CRUDMode = CRUDMode.Create, TourScheduleId = tourScheduleId, DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now });
        }
        #endregion

        public ListViewTourScheduleCompanyTransferViewModel GetListView(TourScheduleCompanyTransfer tourScheduleCompanyTransfer)
        {
            return new ListViewTourScheduleCompanyTransferViewModel()
            {
                CompanyTransferTitle = _companyTransferService.Filter(x => x.Id == tourScheduleCompanyTransfer.CompanyTransferId).FirstOrDefault().Title,
                TourScheduleId = tourScheduleCompanyTransfer.TourScheduleId,
                SectionId = SectionId + tourScheduleCompanyTransfer.Id,
                Id = tourScheduleCompanyTransfer.Id,
                //Capacity = tourScheduleCompanyTransfer.Capacity,
                FromAirportTitle = _cityService.GetById(x => x.Id == tourScheduleCompanyTransfer.FromAirportId).Title,
                DestinationAirportTitle = _cityService.GetById(x => x.Id == tourScheduleCompanyTransfer.DestinationAirportId).Title,
                DurationTime = tourScheduleCompanyTransfer.DurationTime.HasValue ? tourScheduleCompanyTransfer.DurationTime.Value.ConvertToStringLongDate() : string.Empty,
                StartDateTime = tourScheduleCompanyTransfer.StartDateTime.ConvertToStringLongDate(),
                EndDateTime = tourScheduleCompanyTransfer.EndDateTime.ConvertToStringLongDate(),
                NonLimit = tourScheduleCompanyTransfer.NonLimit,
            };
        }

    }
}
