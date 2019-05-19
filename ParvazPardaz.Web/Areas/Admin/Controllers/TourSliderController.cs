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
using ParvazPardaz.Common.HtmlHelpers.Models;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class TourSliderController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourSliderService _toursliderService;
        private readonly ITourService _tourService;
        #endregion

        #region	Ctor
        public TourSliderController(IUnitOfWork unitOfWork, ITourSliderService toursliderService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _toursliderService = toursliderService;
            _tourService = tourService;
        }
        #endregion

        #region Create      
        public ActionResult Create(int tourId)
        {
            var tour = _unitOfWork.Set<Tour>().FirstOrDefault(x => x.Id == tourId);

            //var tourLinkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tourId && x.linkType == LinkType.Tour && !x.IsDeleted);
            //ViewBag.TourURL = tourLinkTbl != null ? "/Admin" + tourLinkTbl.URL.Insert(6, "TourPreview/") : "";

            var linkTableLandingUrl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.typeId == tour.TourLandingPageUrlId && x.linkType == LinkType.TourLanding);
            ViewBag.TourURL = linkTableLandingUrl != null ? linkTableLandingUrl.URL : "#";

            return View(new AddTourSliderViewModel()
            {
                TourId = tourId,
                EditModeFileUploads = _tourService.GetById(x => x.Id == tourId).TourSliders
                                                  .Where(t => t.IsPrimarySlider == false)
                                                  .Select(s => new EditModeFileUpload()
                                                              {
                                                                  Name = s.ImageFileName,
                                                                  Size = s.ImageSize,
                                                                  Type = s.ImageExtension,
                                                                  Url = s.ImageUrl,
                                                                  Id = s.Id
                                                              }).ToList(),
                EditModeFileUploadsForPrimarySlider = _tourService.GetById(x => x.Id == tourId).TourSliders
                                                                  .Where(t => t.IsPrimarySlider)
                                                                  .Select(s => new EditModeFileUpload()
                                                                          {
                                                                              Name = s.ImageFileName,
                                                                              Size = s.ImageSize,
                                                                              Type = s.ImageExtension,
                                                                              Url = s.ImageUrl,
                                                                              Id = s.Id
                                                                          }).ToList()
            });
        }
        #endregion

        #region Upload
        [HttpPost]
        public JsonResult Upload(AddTourSliderViewModel addTourSliderViewModel)
        {
            TourSlider tourSlider = _toursliderService.Upload(addTourSliderViewModel);
            return Json(tourSlider.Id, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        public JsonResult Delete(int id)
        {
            if (_toursliderService.Remove(id))
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
