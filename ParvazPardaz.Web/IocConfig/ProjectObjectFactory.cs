﻿using ParvazPardaz.DataAccess.Infrastructure;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using StructureMap.Web;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMapper;

namespace ParvazPardaz.Web.IocConfig
{
    public class ProjectObjectFactory
    {
        #region Fields
        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion

        public static IContainer Container { get { return ContainerBuilder.Value; } }

        #region DefaultContainer
        private static Container DefaultContainer()
        {
            var container = new Container(ioc =>
            {
                //ioc.For<Microsoft.AspNet.SignalR.IDependencyResolver>()
                // .Singleton()
                // .Add<StructureMapSignalRDependencyResolver>();

                ioc.For<IIdentity>().Use(() => (HttpContext.Current != null && HttpContext.Current.User != null) ? HttpContext.Current.User.Identity : null);

                ioc.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<ApplicationDbContext>();

                ioc.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
                ioc.For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
                ioc.For<HttpRequestBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Request);

                //ioc.For<ISessionProvider>().Use<SessionProvider>();
                //ioc.For<IRemotingFormatter>().Use(a => new BinaryFormatter());
                //ioc.For<ITempDataProvider>().Use<CookieTempDataProvider>();

                ioc.AddRegistry<AspNetIdentityRegistery>();
                //ioc.AddRegistry<TaskRegistry>();
                ioc.AddRegistry<AutoMapperRegistery>();
                ioc.AddRegistry<ServiceLayerRegistery>();

                ioc.Scan(scanner => scanner.WithDefaultConventions());
                ioc.Policies.SetAllProperties(y => y.OfType<HttpContextBase>());
            });
            ConfigureAutoMapper(container);
            return container;
        }
        #endregion


        #region ConfigureAutoMapper
        private static void ConfigureAutoMapper(IContainer container)
        {
            var configuration = container.TryGetInstance<IConfiguration>();
            if (configuration == null) return;
            //saying AutoMapper how to resolve services
            configuration.ConstructServicesUsing(container.GetInstance);
            foreach (var profile in container.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }
        }
        #endregion
    }
}