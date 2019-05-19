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
using Infrastructure;
using ParvazPardaz.Model.Book;
using ParvazPardaz.Model;
using AutoMapper;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.TravelAgency.UI.Services.Interface.Book;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Service.Security;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class PaymentLogController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly IPaymentLogService _paymentlogService;
        #endregion

        #region	Ctor
        public PaymentLogController(IUnitOfWork unitOfWork, IPaymentLogService paymentlogService, IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _paymentlogService = paymentlogService;
        }
        #endregion

        #region Index
        /// <summary>
        /// مشاهده لاگ های پرداخت
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="id">لاگ های موفق : 1 ، لاگ های ناموفق : 0</param>
        /// <returns></returns>
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "OservationPaymentLog", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public ActionResult Index(string msg, int id)
        {
            ViewBag.success = msg;
            ViewBag.IsSuccessful = id;
            return View();
        }

        
        public ActionResult GetPaymentLog(int isSuccessful, [DataSourceRequest]DataSourceRequest request)
        {
            var query = _paymentlogService.GetViewModelForGrid(isSuccessful);
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

    }
}
