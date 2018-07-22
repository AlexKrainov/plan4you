using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using plan2plan.App_Start;
using plan2plan.Filter.Auth;
using plan2plan.Util.Kernel;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace plan2plan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.Error += MvcApplication_Error;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // GlobalConfiguration.Configure(WebApiConfig.Register);

            #region внедрение зависимостей
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            kernel.Unbind<ModelValidatorProvider>();
            #endregion
            //AddBindings();

            #region Serilog

            // Get application base directory
            string basedir = AppDomain.CurrentDomain.BaseDirectory;

            // Setup Serilog for logging
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.AppSettings()
                        .WriteTo.RollingFile(basedir + "/Logs/log-{Date}.txt")
                        .CreateLogger();

            Log.Information("Start aplication!");

            #endregion
        }

        private void MvcApplication_Error(object sender, EventArgs e)
        {
            var error = "1";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //  HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
        }


    }
}
