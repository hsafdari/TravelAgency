using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Contract.AccessLevel;
using ParvazPardaz.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Core;
using Z.EntityFramework.Plus;
using ParvazPardaz.Model.Entity.AccessLevel;


namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class DashboardController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IPermissionService permissionService, IUnitOfWork unitOfWork)
        {
            _permissionService = permissionService;
            _unitOfWork = unitOfWork;
        }

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region GetMenu
        public ActionResult GetMenu()
        {
            //تعریف سشن
            //سشن چک شود آیا بار اول است؟
            //لینک هایی که در جدول دسترسی  لیست دارند استخراج شود
            //در یک سشن ریخته شود
            //تمام لینک ها در سشن چک شود که آیا دسترسی دارد یا نه
            var menuItems = new List<Menu>();
            if (Session["MenuItems"] == null)
            {
                var UserId = System.Web.HttpContext.Current.Request.GetUserId();
                menuItems = _permissionService.MenuPermissions(UserId.Value);
                Session["MenuItems"] = menuItems;
            }                
            else
            {
                menuItems = Session["MenuItems"] as List<Menu>;
            }

            return PartialView("_PrvAdminMenuLinks", menuItems);
        }
        #endregion
    }
}