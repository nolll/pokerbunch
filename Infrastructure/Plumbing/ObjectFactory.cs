using System;
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
            RegisterComponent<T, TK>(container, LifestyleType.Singleton);
        }

        public static void RegisterComponent<T, TK>(IWindsorContainer container, LifestyleType lifestyleType)
            where TK : class
            where T : class
        {
            container.Register(Component.For<T, TK>().LifeStyle.Is(lifestyleType));
        }

        public static bool CanResolve(IWindsorContainer container, Type t)
        {
            try
            {
                container.Resolve(t);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}