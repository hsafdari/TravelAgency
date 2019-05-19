using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Web.IocConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Audit;

namespace ParvazPardaz.Web
{
    public static class ApplicationStart
    {

        public static void Config()
        {
            // disable response header for protection  attak
            MvcHandler.DisableMvcResponseHeader = true;

            //// change captcha provider for using cookie
            //CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
            //CaptchaUtils.ImageGenerator.Height = 50;

            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            //set current Filter factory as StructureMapFitlerProvider
            //var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            //FilterProviders.Providers.Remove(filterProider);
            //FilterProviders.Providers.Add(ProjectObjectFactory.Container.GetInstance<StructureMapFilterProvider>());

            //var defaultJsonFactory = ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault();
            //var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            //ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            //ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());


            // DependencyResolver.SetResolver(new StructureMapDependencyResolver());


            //foreach (var task in ProjectObjectFactory.Container.GetAllInstances< <IRunAtInit>())
            //{
            //    task.Execute();
            //}

            //GlobalHost.DependencyResolver = ProjectObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
            //ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            // DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ConfigEf();
        }


        private static void ConfigEf()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            var auditConfiguration = AuditConfiguration.Default;
            auditConfiguration.IncludeRelationships = false;
            auditConfiguration.LoadRelationships = false;
            auditConfiguration.DefaultAuditable = false;
        }

    }

}