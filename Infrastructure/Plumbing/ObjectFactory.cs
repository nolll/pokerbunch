using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Infrastructure.Plumbing
{
    public abstract class ObjectFactory
    {
        public static void RegisterComponent<T, TK>(IWindsorContainer container)
            where TK : class
            where T : class
        {
            container.Register(Component.For<T, TK>().LifeStyle.Is(LifestyleType.Singleton));
        }

        public static void RegisterComponent<T, TK>(IWindsorContainer container, LifestyleType lifestyleType)
            where TK : class
            where T : class
        {
            container.Register(Component.For<T, TK>().LifeStyle.Is(lifestyleType));
        }
    }
}