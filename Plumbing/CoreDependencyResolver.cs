using Castle.Core;
using Castle.Windsor;
using Core.UseCases;
using Core.UseCases.ShowBunchList;
using Core.UseCases.ShowUserList;

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
            // Use Cases
            RegisterComponent<IShowUserListInteractor, ShowUserListInteractor>();
            RegisterComponent<IShowBunchListInteractor, ShowBunchListInteractor>();
        }
    }
}