using ParvazPardaz.Service.DataAccessService.Users;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParvazPardaz.Web.IocConfig
{
    public class ServiceLayerRegistery : Registry
    {
        public ServiceLayerRegistery()
        {
            Scan(scanner =>
            {
                //Default ISomething/Something Convention
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<ApplicationUserManager>();
            });
        }
    }
}