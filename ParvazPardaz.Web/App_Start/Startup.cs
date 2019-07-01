using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using ParvazPardaz.Web.IocConfig;
using Microsoft.Owin.Security.DataProtection;
using StructureMap.Web;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using ParvazPardaz.Service.Contract;
using Microsoft.AspNet;
using ParvazPardaz.Service.DataAccessService.Users;
using ParvazPardaz.Service.Contract.Users;

[assembly: OwinStartupAttribute(typeof(ParvazPardaz.Web.Startup))]
namespace ParvazPardaz.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }

        private static void ConfigureAuth(IAppBuilder appBuilder)
        {
            const int twoWeeks = 21600;// 14;

            ProjectObjectFactory.Container.Configure(config => config.For<IDataProtectionProvider>().HybridHttpOrThreadLocalScoped().Use(() => appBuilder.GetDataProtectionProvider()));

            // CreatePerOwinContext : این متد کار ساخت وهله سازی از این کلاس برای هر درخواست را بر عهده دارد 
            // This callback will be called once per request and will store the object/objects in OwinContext so that you will be able to use them throughout the application.
            appBuilder.CreatePerOwinContext(() => ProjectObjectFactory.Container.GetInstance<ApplicationUserManager>());

            //UseCookieAuthentication :  تنظیمات کوکی
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // کلاینت ها در صورت عدم احراز هویت به این آدرس هدایت میشوند
                LoginPath = new PathString("/"),
                //زمان انقضای کوکی
                ExpireTimeSpan = TimeSpan.FromDays(twoWeeks),
                //زمان انقضای کوکی در هر بار لاگین ریست میشود
                SlidingExpiration = true,
                CookieName = "keySafar",
                //Provider = new CookieAuthenticationProvider
                //{
                //    OnValidateIdentity = ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>().OnValidateIdentity()
                //}
            });

            //جهت مقدار دهی اولیه جدول نقش ها
            ProjectObjectFactory.Container.GetInstance<IApplicationRoleManager>()
            .SeedDatabase();

            //جهت مقدار دهی اولیه جدول کاربران
            ProjectObjectFactory.Container.GetInstance<IApplicationUserManager>()
            .SeedDatabase();

        }
    }
}