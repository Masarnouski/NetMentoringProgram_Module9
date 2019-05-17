using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastracture;
using NLog;
using PerformanceCounterHelper;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger logger;

        public MvcApplication()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.Register(f => LogManager.GetLogger("ForControllers")).
                As<ILogger>();

            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(builder.Build()));

            logger.Info("Application started");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project"))
            //{
            //    counterHelper.RawValue(Counters.LogIn, 0);
            //    counterHelper.RawValue(Counters.LogOff, 0);
            //    counterHelper.RawValue(Counters.FailedLogIn, 0);

            //}
        }
    }
}
