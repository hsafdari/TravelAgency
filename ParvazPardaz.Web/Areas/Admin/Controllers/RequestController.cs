using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Service.Contract.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    public class RequestController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestService _requestService;
        #endregion

        #region	Ctor
        public RequestController(IUnitOfWork unitOfWork, IRequestService requestService)
        {
            _unitOfWork = unitOfWork;
            _requestService = requestService;
        }
        #endregion

        #region Index
        //[CustomAuthorize(Permissionitem.List)]
        //[Display(Name = "ManagementReviewItems", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetRequest([DataSourceRequest]DataSourceRequest request)
        {
            var query = _requestService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        public JsonResult gettour(int tourId)
        {
            var tourPackage = _unitOfWork.Set<TourPackage>().FirstOrDefault(x => x.Id == tourId);
            return Json(new { tour = tourPackage.Title });
        }
    }
}