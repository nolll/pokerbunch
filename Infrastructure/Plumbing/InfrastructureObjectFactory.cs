using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Plumbing;

namespace Web.Plumbing
{
    public static class WebObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            // Storage
            ObjectFactory.RegisterComponent<IHomegameStorage, MySqlHomegameStorage>(container);
        }
    }
}