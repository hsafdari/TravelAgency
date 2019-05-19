using GSD.Globalization;
using ParvazPardaz.Common;
using ParvazPardaz.Web.App_Start;
using StructureMap.Web.Pipeline;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using System.IO.Compression;
using ParvazPardaz.Web.Controllers;
using ParvazPardaz.DataAccess.Infrastructure;

namespace ParvazPardaz.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Application_Start
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ApplicationStart.Config();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        #endregion

        #region Application_BeginRequest
        protected void Application_BeginRequest()
        {
            try
            {
                HttpCookie aCookie = Request.Cookies["ParvazPardazAgencyLang"];
                if (aCookie == null)
                {
                    HttpCookie myCookie = new HttpCookie("ParvazPardazAgencyLang");
                    myCookie.Value = ConfigurationManager.AppSettings["DefaultLanguage"];
                    myCookie.Expires = DateTime.Now.AddDays(1d);
                    Response.Cookies.Add(myCookie);
                }
                string currentlang = Request.Cookies["ParvazPardazAgencyLang"].Value;
                if (currentlang == "fa-IR")
                {
                    var persianCulture = new PersianCulture();
                    //persianCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = persianCulture;
                    Thread.CurrentThread.CurrentUICulture = persianCulture;
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentlang);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentlang);
                }
            }
            catch (Exception ex)
            {
                HttpCookie myCookie = new HttpCookie("ParvazPardazAgencyLang");
                myCookie.Value = ConfigurationManager.AppSettings["DefaultLanguage"];
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
            }

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //#region GZip
            //// Implement HTTP compression
            //HttpApplication app = (HttpApplication)sender;
            //// Retrieve accepted encodings
            //string encodings = app.Request.Headers.Get("Accept-Encoding");
            //if (encodings != null)
            //{
            //    // Check the browser accepts deflate or gzip (deflate takes preference)
            //    encodings = encodings.ToLower();
            //    if (encodings.Contains("deflate"))
            //    {
            //        app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "deflate");
            //    }
            //    else if (encodings.Contains("gzip"))
            //    {
            //        app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "gzip");
            //    }
            //} 
            //#endregion
        }
        #endregion

        #region Application_EndRequest
        void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
        #endregion

        #region Application_Error
        protected void Application_Error(object sender, EventArgs e)
        {
            var r = Request;
        }
        #endregion
    }
}
