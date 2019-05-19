using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
//using System.Web.Mvc;
using GSD.Globalization;

namespace ParvazPardaz.Common.Controller
{
    public class BaseController : System.Web.Mvc.Controller
    {
        public BaseController()
        {
            //var persianCulture = new PersianCulture();
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;
            string currentlang = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (currentlang == "fa-IR")
            {
                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentlang);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentlang);
            }
        }
    }
}