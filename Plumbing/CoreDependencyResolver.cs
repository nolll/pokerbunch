using Castle.Core;
using Castle.Windsor;

namespace Plumbing
{
    public class CoreDependencyResolver : DependencyResolver
    {
        protected CoreDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
        }
    }
}