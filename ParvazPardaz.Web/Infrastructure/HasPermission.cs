using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.AccessLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ParvazPardaz.Common.Extension;

namespace Infrastructure
{
    public static class HasPermission
    {
        public static int? _userId { get; set; }
        public static bool CanAccess(string permission, string url)
        {
            _userId = HttpContext.Current.Request.GetUserId();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var item = db.Set<ParvazPardaz.Model.Entity.AccessLevel.RolePermissionController>().AsEnumerable().Where(x => x.Role.Users.Any(y => y.UserId == _userId) && x.ControllersList.PageUrl.Contains(url.TrimStart('/').TrimEnd('/')) && x.Permission.PermissionName == permission).FirstOrDefault();
                return item != null;
            }
            //return true;
        }
        public static bool MenuAccess(string url)
        {
            var items = HttpContext.Current.Session["MenuItems"] as List<String>;
            if (items != null)
            {
                var hasItem = (from m in items
                               where m == url
                               select m).FirstOrDefault();
                if (hasItem != null)
                    return true;
            }
            return false;
        }


    }
}