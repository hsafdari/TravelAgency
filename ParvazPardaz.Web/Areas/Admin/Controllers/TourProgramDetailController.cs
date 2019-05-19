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
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourProgramDetailController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourProgramDetailService _tourProgramDetailService;
        private readonly ITourProgramService _tourProgramService;
        private readonly ICityService _cityService;

        private const string SectionId = "Section_TourProgramDetail_";
        #endregion

        #region	Ctor
        public TourProgramDetailController(IUnitOfWork unitOfWork, ITourProgramDetailService tourprogramdetailService,
                                           ITourProgramService tourProgramService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _tourProgramDetailService = tourprogramdetailService;
            _tourProgramService = tourProgramService;
            _cityService = cityService;
        }
        #endregion

        #region Index
        //public ActionResult Index(string msg)
        //{
        //    ViewBag.success = msg;
        //    return View();
        //}

        //public ActionResult GetTourProgramDetail([DataSourceRequest]DataSourceRequest request)
        //{
        //    var query = _tourprogramdetailService.GetViewModelForGrid();
        //    var dataSourceResult = query.ToDataSourceResult(request);
        //    return Json(dataSourceResult);
        //}
        #endregion

        #region Create
        public ActionResult Create(int tourProgramId)
        {
            var model = _tourProgramService.GetById(t => t.Id == tourProgramId);
            return PartialView("_Create", new TourProgramDetailViewModel()
            {
                TourProgramId = tourProgramId,
                ActivitySelectList = new SelectList(_unitOfWork.Set<Activity>().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title + "(مکان : " + x.Place + ")", Value = x.Id.ToString() }), "Value", "Title"),
                ListView = model.TourProgramDetails.Any() ? model.TourProgramDetails.Select(tourProgramDetail => GetListView(tourProgramDetail)).ToList() : null
            });
        }


        [HttpPost]
        public ActionResult Create(TourProgramDetailViewModel viewModel)
        {
            //if (!viewModel.File.HasFile() || string.IsNullOrWhiteSpace(viewModel.Title) || string.IsNullOrWhiteSpace(viewModel.Description))
            //{
            //    return Json(false, JsonRequestBehavior.DenyGet);
            //}

            if (viewModel.TourActivityId < 1 || string.IsNullOrWhiteSpace(viewModel.DetailDescription))
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

            var activity = _unitOfWork.Set<Activity>().FirstOrDefault(x => x.Id == viewModel.TourActivityId);
            viewModel.Title = activity.Title;
            viewModel.ImageUrl = activity.ImageUrl;
            viewModel.ImageFileName = activity.ImageFileName;
            viewModel.ImageSize = activity.ImageSize;
            viewModel.ImageExtension = activity.ImageExtension;
            //توضیحات فعالیت + توضیحات جدید
            viewModel.DetailDescription = activity.Description + "<br />" + viewModel.DetailDescription;
            viewModel.TourActivityId = activity.Id;

            var model = _tourProgramDetailService.CreateTourProgramDetail(viewModel);
            return PartialView("_ListViewTourProgramDetail", GetListView(model));
        }
        #endregion

        #region Edit
        //public async Task<ActionResult> Edit(int id)
        //{
        //    EditTourProgramDetailViewModel editTourProgramDetailViewModel = await _tourProgramDetailService.GetViewModelAsync<EditTourProgramDetailViewModel>(x => x.Id == id);
        //    return View(editTourProgramDetailViewModel);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Edit(EditTourProgramDetailViewModel editTourProgramDetailViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var update = await _tourProgramDetailService.UpdateAsync<EditTourProgramDetailViewModel>(editTourProgramDetailViewModel, t => t.Id == editTourProgramDetailViewModel.Id);
        //        return RedirectToAction("Index", new { msg = "update" });
        //    }
        //    return View(editTourProgramDetailViewModel);
        //}
        #endregion


        #region Delete
        [HttpPost]
        public JsonResult Delete(int id, string sectionId, int tourProgramId)
        {
            bool success = false;
            if (_tourProgramDetailService.Remove(id))
            {
                success = true;
                return Json(new { Success = success, SectionId = sectionId, TourProgramId = tourProgramId }, JsonRequestBehavior.DenyGet);
            }
            return Json(success, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region LoadAddTourProgramPatialView
        public PartialViewResult RefreshPartialView(int tourProgramId)
        {
            return PartialView("_AddTourProgramDetail", new TourProgramDetailViewModel() { CRUDMode = CRUDMode.Create, TourProgramId = tourProgramId, ActivitySelectList = new SelectList(_unitOfWork.Set<Activity>().Where(x => !x.IsDeleted).Select(x => new { Title = x.Title + "(مکان : " + x.Place + ")", Value = x.Id.ToString() }), "Value", "Title") });
        }
        #endregion

        #region PrivateMethods
        private ListViewTourProgramDetailViewModel GetListView(TourProgramDetail tourProgramDetail)
        {
            return new ListViewTourProgramDetailViewModel()
            {
                CityTitle = _cityService.ShowCityNameWithCountry(tourProgramDetail.TourProgram.CityId),
                Id = tourProgramDetail.Id,
                Description = tourProgramDetail.Description,
                SectionId = SectionId + tourProgramDetail.Id,
                TourProgramId = tourProgramDetail.TourProgramId,
                Title = tourProgramDetail.Title,
            };
        }
        #endregion

    }
}
