using Castle.Core;
using Castle.Windsor;
using Core.UseCases.BunchList;
using Core.UseCases.PlayerList;
using Core.UseCases.UserList;

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
            RegisterComponent<IUserListInteractor, UserListInteractor>();
            RegisterComponent<IBunchListInteractor, BunchListInteractor>();
            RegisterComponent<IPlayerListInteractor, PlayerListInteractor>();
        }
    }
}