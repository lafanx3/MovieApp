using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Http;
using CrudApp.Services;

namespace CrudApp
{
    public static class UnityConfig
    {
        private static UnityContainer _container;

        public static UnityContainer GetContainer()
        {
            return _container;
        }

        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IMovieService, MovieService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            config.DependencyResolver = new UnityResolver(container);

            _container = container;
        }
    }
}