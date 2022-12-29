using Autofac;
using Autofac.Integration.Mvc;
using EShop.Implementations.Core;
using EShop.Web.Controllers;
using EShop.Web.Identity;
using System.Web.Mvc;

namespace EShop.Web
{
    public class AutofacConfig
    {
        private static IContainer _container;

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(HomeController).Assembly);

            RegisterServices.Register(builder);

            builder.RegisterType<ShopSignInManager>().InstancePerLifetimeScope();
            
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}