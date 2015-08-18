[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Storify.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Storify.App_Start.NinjectWebCommon), "Stop")]

namespace Storify.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using WebApiContrib.IoC.Ninject;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                //GlobalConfiguration.Configuration.Services.Add(typeof(IFilterProvider), new NinjetWebApiFilterProvider(kernel));
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            Storify.Business.NinjectRegistration.RegisterServices(kernel);
            //kernel.Bind<>().To<AuthenticationRepository>();
            //kernel.Bind<UserSecurityEntities>().To<UserSecurityEntities>();
            //kernel.Bind<ISendEmails>().To<SendEmailsToNotificationService>();
        }
    }
}


