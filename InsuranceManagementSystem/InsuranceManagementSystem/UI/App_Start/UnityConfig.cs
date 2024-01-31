using DAL.Data;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace UI.App_Start
{
    public class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IAdminRepository, AdminRepository>();

            container.RegisterType<ICustomerRepository, CustomerRepository>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            Container = container;
            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}