using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public static class UserTransactions
    {
        public static string GetIpdAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static Guid GetUserLoginedId()
        {
            return Guid.Parse(System.Web.HttpContext.Current.User.Identity.Name);
        }

        public static string GetCurrentUserLanguage()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        }
        public static HttpCookie ChangeLanguage(string lang)
        {
            HttpCookie myCookie = new HttpCookie("ParvazPardazAgencyLang");
            myCookie.Value = lang;
            myCookie.Expires = DateTime.Now.AddDays(1d);
            return myCookie;
        }


        public static object CurrentUserTitle { get; set; }

        public static string MakeUrlString(string lang, string controller, string id, string title)
        {
            return "/" + controller + "/" + id + "/" + lang + "/" + title.Replace(" ", "-");
        }
    }
}