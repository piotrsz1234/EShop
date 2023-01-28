using System.Net;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using EShop.Implementations.Core;
using EShop.Web.Controllers;
using EShop.Web.Identity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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
            builder.Register(c => ShopUserManager.Create(c.Resolve<IdentityFactoryOptions<ShopUserManager>>(), HttpContext.Current.GetOwinContext())).InstancePerLifetimeScope();
            builder.RegisterType<ShopUserStore>().InstancePerLifetimeScope();
            
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ShopUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application")
            }); 
            
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}