using Autofac;
using EShop.Implementations.Core.Domain;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core
{
    public static class RegisterServices
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MailService).Assembly).Where(x => x.Namespace.Contains("Infrastructure.Repositories")
                                                                                   || x.Namespace.Contains("Domain")).Where(x => !x.IsGenericType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<MainDbContext>().InstancePerLifetimeScope();
        }
    }
}