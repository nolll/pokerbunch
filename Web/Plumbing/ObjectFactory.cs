using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Repositories;
using Infrastructure.Repositories;

namespace Web.Plumbing
{
    public static class ObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            container.Register(Component.For<IHomegameRepository>().ImplementedBy<HomegameRepository>());
        }
    }
}