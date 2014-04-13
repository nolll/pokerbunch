using Castle.Core;
using Castle.Windsor;
using Core.UseCases.ShowBunchList;
using Core.UseCases.ShowPlayerList;
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
            RegisterComponent<IShowPlayerListInteractor, ShowPlayerListInteractor>();
        }
    }
}