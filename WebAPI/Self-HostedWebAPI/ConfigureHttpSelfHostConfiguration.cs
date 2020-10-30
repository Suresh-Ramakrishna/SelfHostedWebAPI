using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Web.Http.SelfHost;

namespace SelfHostedWebAPI
{
    class ConfigureHttpSelfHostConfiguration
    {
        public static void Configure(HttpSelfHostConfiguration config)
        {
            config.DependencyResolver = new CustomNinjectResolver();
        }
    }
    public class CustomNinjectResolver : IDependencyResolver
    {
        private IKernel kernel;
        public CustomNinjectResolver()
        {
            kernel = new StandardKernel();
            kernel.Bind<IRepository>().To<NumbersRepository>().InSingletonScope();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }
    }
}
