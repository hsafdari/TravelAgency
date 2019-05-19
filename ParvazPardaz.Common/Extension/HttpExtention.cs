using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ParvazPardaz.Common.Extension
{
    public static class HttpExtention
    {
        #region PhysicalToVirtualPathConverter
        public static string PhysicalToVirtualPathConverter(this HttpServerUtilityBase utility, string path, HttpRequestBase context)
        {
            return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "/").Replace(@"\", "/");
        }
        #endregion

        #region Get Ip Address
        public static string GetIp(this HttpRequestBase request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch { ip = null; }

            return ip;

        }

        public static string GetIp(this HttpRequest request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch { ip = null; }

            return ip;

        }
        #endregion

        #region User-Agent
        public static string GetBrowser(this HttpRequest request)
        {
            return "{" + request.Browser.Browser + "} - {" + request.Browser.Version + "}";
        }
        #endregion

        #region GetUserId
        public static int? GetUserId(this HttpRequest request)
        {
            if (HttpContext.Current == null)
                return null;
            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            var userId = int.Parse(HttpContext.Current.User.Identity.GetUserId());
            return userId;
        }

        #endregion
    }
}