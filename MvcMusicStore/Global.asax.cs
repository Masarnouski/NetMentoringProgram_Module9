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
using MvcMusicStore.Infrastracture.Logger;
using PerformanceCounterHelper;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.Register(f => new Logger()).
                As<ILogger>();

            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project"))
            {
                counterHelper.RawValue(Counters.LogIn, 0);
                counterHelper.RawValue(Counters.LogOff, 0);
                counterHelper.RawValue(Counters.FailedLogIn, 0);
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            ILogger logger = DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger;
            logger?.Error(exception.Message, exception);
        }
    }
}
