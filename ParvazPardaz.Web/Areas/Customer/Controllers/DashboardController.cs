using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Areas.Customer.Controllers
{
    [Mvc5AuthorizeAttribute()]
    public class DashboardController : Controller
    {
        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            return View();
        }
    }
}