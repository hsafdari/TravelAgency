using System;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using StructureMap.Web;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Service.DataAccessService.Users;

namespace ParvazPardaz.Web.IocConfig
{
    public class AspNetIdentityRegistery : Registry
    {
        public AspNetIdentityRegistery()
        {
            // The default operation.  A new instance will be created for each request.
            // Singleton => A single instance will be shared across all requests
            // HttpContextScoped => A single instance will be created for each HttpContext.  Caches the instances in the HttpContext.Items collection.
            // HybridHttpOrThreadLocalScoped => Uses HttpContext storage if it exists, otherwise uses ThreadLocal storage.(uses ThreadLocalStorage in the absence of an active HttpContext)
            For<ApplicationDbContext>().HybridHttpOrThreadLocalScoped().Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

            For<DbContext>().HybridHttpOrThreadLocalScoped().Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

            //جهت استفاده در کلاس ApplicationUserManager
            For<IUserStore<User, int>>().HybridHttpOrThreadLocalScoped().Use<UserStore<User, Role, int, UserLogin, UserRole, UserClaim>>();

            //جهت استفاده در کلاس ApplicationRoleManager
            For<IRoleStore<Role, int>>().HybridHttpOrThreadLocalScoped().Use<RoleStore<Role, int, UserRole>>();

            //تزریق وابستگی بین کلاس دات نت و اینترفیس تعریف شده جهت عملیات لاگین
            For<IApplicationSignInManager>().HybridHttpOrThreadLocalScoped().Use<ApplicationSignInManager>();

            // Get the identity framework authentication manager
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
            
            // Get the identity framework role manager
            For<IApplicationRoleManager>().HybridHttpOrThreadLocalScoped().Use<ApplicationRoleManager>();
            
            // Get the identity framework user manager
            For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped().Use<ApplicationUserManager>();

            For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped().Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());
        }
    }
}