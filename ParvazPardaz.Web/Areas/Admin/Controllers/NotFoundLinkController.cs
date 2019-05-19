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
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.NotFoundLink;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class NotFoundLinkController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotFoundLinkService _notfoundlinkService;
        #endregion

        #region	Ctor
        public NotFoundLinkController(IUnitOfWork unitOfWork, INotFoundLinkService notfoundlinkService)
        {
            _unitOfWork = unitOfWork;
            _notfoundlinkService = notfoundlinkService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "ManagementNotFountLinks", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetNotFoundLink([DataSourceRequest]DataSourceRequest request)
        {
            var query = _notfoundlinkService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "ManagementNotFountLinks", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _notfoundlinkService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

    }
}
