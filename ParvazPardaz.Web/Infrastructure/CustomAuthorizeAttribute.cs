using ParvazPardaz.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public Permissionitem[] _permissions { get; set; }
        public CustomAuthorizeAttribute(params Permissionitem[] permissions)
        {
            _permissions = permissions;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            //Uri myUri = new Uri(httpContext.Request.Url.ToString());
            //    //((((System.Web.Mvc.ControllerContext)(filterContext)).HttpContext).Request.Url).ToString());
            //string lang = HttpUtility.ParseQueryString(myUri.Query).Get("lang");

            //string UrlRequest = httpContext.Request.Url.PathAndQuery;
            //int urlstring = UrlRequest.LastIndexOf("/?lang");
            string urlstring = httpContext.Request.Url.AbsolutePath;
            //if (urlstring==-1)
            //{
            //    urlstring = UrlRequest.LastIndexOf("?lang");
            //}
            //var s = urlstring.LastIndexOf("/");
            //UrlRequest = urlstring.Substring(0, s);
            //?lang=en-US

            switch (_permissions[0].ToString())
            {
                case "List":
                    {
                        if (!(urlstring.EndsWith("Index") || urlstring.EndsWith("Index/")))
                        {
                            if (httpContext.Request.Url.Segments.Count() > 4)
                            {
                                urlstring = string.Concat(httpContext.Request.Url.Segments.Take(4));
                            }
                            else if (urlstring.EndsWith("/"))
                            {
                                urlstring += "Index";
                            }
                            else
                            {
                                urlstring += "/Index";
                            }
                        }
                        break;
                    }
                case "Edit":
                    {
                        var u = string.Concat(httpContext.Request.Url.Segments.Take(4));
                        int index = u.LastIndexOf("/");
                        if (index > 0)
                            urlstring = u.Substring(0, index);
                        break;
                    }
                case "Create" :
                    {
                        if (httpContext.Request.Url.Segments.Count() > 4)
                        {
                            urlstring = string.Concat(httpContext.Request.Url.Segments.Take(4));
                        }
                        break;
                    }
                default:
                    break;
            }
            bool IsAuthorise = HasPermission.CanAccess(_permissions[0].ToString(), urlstring);
            if (!IsAuthorise)
            {
                return false;
            }
            return IsAuthorise;
        }
    }
}