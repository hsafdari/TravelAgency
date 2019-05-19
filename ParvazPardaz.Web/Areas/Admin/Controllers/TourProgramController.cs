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
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using System.Transactions;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourProgramController : BaseController
    {
        #region	Fields
        private const string SectionId = "Section";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourProgramService _tourprogramService;
        private readonly IActivityService _activityService;
        private readonly ICityService _cityService;
        private readonly ITourService _tourService;
        #endregion

        #region	Ctor
        public TourProgramController(IUnitOfWork unitOfWork, ITourProgramService tourprogramService, IActivityService activityService, ICityService cityService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _tourprogramService = tourprogramService;
            _activityService = activityService;
            _cityService = cityService;
            _tourService = tourService;
        }
        #endregion

        #region Create
        public ActionResult Create(int tourId)
        {
            ViewBag.Activities = _activityService.GetAllActivityOfSelectListItem();
            var tour = _tourService.GetById(t => t.Id == tourId);

            //var tourLinkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tourId && x.linkType == LinkType.Tour && !x.IsDeleted);
            //ViewBag.TourURL = tourLinkTbl != null ? "/Admin" + tourLinkTbl.URL.Insert(6, "TourPreview/") : "";

            var linkTableLandingUrl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tour.TourLandingPageUrlId && x.linkType == LinkType.TourLanding);
            ViewBag.TourURL = linkTableLandingUrl != null ? linkTableLandingUrl.URL : "#";

            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return View(new TourProgramViewModel()
            {
                TourId = tourId,
                ListView = tour.TourPrograms.Any() ? tour.TourPrograms.Select(tourProgram => GetListView(tourProgram)).ToList() : null
            });
        }


        [HttpPost]
        public ActionResult Create(TourProgramViewModel addTourProgramViewModel)
        {
            if (ModelState.IsValid)
            {
                var tourProgram = _tourprogramService.CreateTourProgram(addTourProgramViewModel);
                ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
                return PartialView("_ListViewTourProgram", GetListView(tourProgram));
            }
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            return View(addTourProgramViewModel);
        }
        #endregion

        #region CreateFromOtherTour
        [HttpPost]
        public string CreateFromOtherTour(int selectedTourId, int currentTourId) //ActionResult
        {
            var tour = _tourService.GetById(x => x.Id == selectedTourId);
            var tourPrograms = _unitOfWork.Set<TourProgram>().Where(x => x.TourId == selectedTourId).ToList();
            var tourProgramDetails = new List<TourProgramDetail>();

            //ذخیره برنامه های سفر
            foreach (var tp in tourPrograms)
            {
                //وهله سازی و مقداردهی برنامه سفر جدید
                var newTp = new TourProgram();
                newTp = tp;
                newTp.TourId = currentTourId;

                //افزودن جزییات این برنامه سفر به لیست
                var tpDetails = tp.TourProgramDetails.ToList();
                tpDetails.Each(x => x.TourProgramId = newTp.Id);
                tourProgramDetails.AddRange(tpDetails);

                //ذخیره برنامه سفر جدید
                _unitOfWork.Set<TourProgram>().Add(newTp);

            }

            //ذخیره جزییات برنامه های سفر
            foreach (var tpDetail in tourProgramDetails)
            {
                _unitOfWork.Set<TourProgramDetail>().Add(tpDetail);
            }

            _unitOfWork.SaveAllChanges();

            ViewBag.Activities = _activityService.GetAllActivityOfSelectListItem();
            var currentTour = _tourService.GetById(t => t.Id == currentTourId);
            ViewBag.TourURL = string.Format("/Tour/{0}/", currentTour.Title.Replace(" ", "-"));
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return "/Admin/TourProgram/Create?tourId=" + currentTourId.ToString();
            //return RedirectToAction("Create", "TourProgram", new { Area = "Admin", tourId = currentTourId });
            //return View(new TourProgramViewModel()
            //{
            //    TourId = currentTourId,
            //    ListView = tour.TourPrograms.Any() ? tour.TourPrograms.Select(tourProgram => GetListView(tourProgram)).ToList() : null
            //});

            //return selectedTourId;
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            TourProgramViewModel editTourProgramViewModel = await _tourprogramService.GetViewModelAsync<TourProgramViewModel>(x => x.Id == id);
            editTourProgramViewModel.CityTitle = _cityService.ShowCityNameWithCountry(editTourProgramViewModel.CityId);
            editTourProgramViewModel.SectionId = SectionId + editTourProgramViewModel.Id;
            ViewBag.Activities = _activityService.GetAllActivityOfSelectListItem();
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");
            return PartialView("_AddTourProgram", editTourProgramViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TourProgramViewModel editTourProgramViewModel)
        {
            if (ModelState.IsValid)
            {
                var tourProgram = _tourprogramService.UpdateTourProgram(editTourProgramViewModel);
                return PartialView("_ListViewTourProgram", GetListView(tourProgram));
            }
            return View(editTourProgramViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourId)
        {
            bool success = false;
            var model = _tourprogramService.Delete(x => x.Id == id);
            if (model > 0)
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourId = tourId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region RefreshPartialView
        public PartialViewResult RefreshPartialView(int tourId)
        {
            ViewBag.Activities = _activityService.GetAllActivityOfSelectListItem();
            ViewBag.Tours = new SelectList(_tourService.GetAll().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title, Value = x.Id.ToString() }), "Value", "Title");

            return PartialView("_AddTourProgram", new TourProgramViewModel() { CRUDMode = CRUDMode.Create, TourId = tourId });
        }
        #endregion

        #region PrivateMethods
        private ListViewTourProgramViewModel GetListView(TourProgram tourProgram)
        {
            return new ListViewTourProgramViewModel()
            {
                Activities = string.Join(",", tourProgram.TourProgramActivities.Select(tp => tp.Activity.Title)),
                CityTitle = _cityService.ShowCityNameWithCountry(tourProgram.CityId),
                DayOrder = tourProgram.DayOrder,
                DurationDay = tourProgram.DurationDay,
                Id = tourProgram.Id,
                Description = tourProgram.Description,
                SectionId = SectionId + tourProgram.Id,
                TourId = tourProgram.TourId
            };
        }
        #endregion
    }
}
