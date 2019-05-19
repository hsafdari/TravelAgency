using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using ParvazPardaz.Web.AutoMapperProfile;
using StructureMap.Web;
using ParvazPardaz.ViewModel;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;


namespace ParvazPardaz.Web.IocConfig
{
    public class AutoMapperRegistery : Registry
    {
        public AutoMapperRegistery()
        {
            // The default operation.  A new instance will be created for each request.
            // Singleton => A single instance will be shared across all requests
            // HttpContextScoped => A single instance will be created for each HttpContext.  Caches the instances in the HttpContext.Items collection.
            // HybridHttpOrThreadLocalScoped => Uses HttpContext storage if it exists, otherwise uses ThreadLocal storage.
            For<ConfigurationStore>().Singleton().Use<ConfigurationStore>().Ctor<IEnumerable<IObjectMapper>>().Is(MapperRegistry.Mappers);

            For<IConfigurationProvider>().Use(ctx => ctx.GetInstance<ConfigurationStore>());

            For<IConfiguration>().Use(ctx => ctx.GetInstance<ConfigurationStore>());

            For<ITypeMapFactory>().Use<TypeMapFactory>();

            For<IMappingEngine>().Singleton().Use<MappingEngine>().SelectConstructor(() => new MappingEngine(null));

            Scan(scanner =>
            {
                //AssemblyContainingType :  این متد تمام کلاسهای اسممبلی فراخوانی شده را اسکن میکند
                scanner.AssemblyContainingType<UserProfile>();
                scanner.AddAllTypesOf<Profile>().NameBy(item => item.Name);

                scanner.ConnectImplementationsToTypesClosing(typeof(ITypeConverter<,>)).OnAddedPluginTypes(t => t.HybridHttpOrThreadLocalScoped());

                scanner.ConnectImplementationsToTypesClosing(typeof(ValueResolver<,>)).OnAddedPluginTypes(t => t.HybridHttpOrThreadLocalScoped());
            });



        }
    }
}