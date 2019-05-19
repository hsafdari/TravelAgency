using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Controllers
{
    public class HotelController : Controller
    {
        //
        // GET: /Hotel/
        [Route("hotel-iran-mashhad/sarina")]
        public ActionResult Index()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Uploads/");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            var Price = System.Configuration.ConfigurationManager.AppSettings["HotelPrice"];
            if (objAppsettings != null)
            {
                ViewBag.Price = objAppsettings.Settings["HotelPrice"].Value;
            }
            return View();
        }
	}
}