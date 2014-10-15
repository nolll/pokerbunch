using Castle.Core;
using Castle.Windsor;
using Plumbing;
using Web.Commands.CashgameCommands;
using Web.Commands.HomegameCommands;
using Web.Commands.UserCommands;

namespace Web.Plumbing
{
    public class WebDependencyResolver : ApplicationDependencyResolver
    {
        public WebDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            RegisterComponent<IUserCommandProvider, UserCommandProvider>();
            RegisterComponent<IBunchCommandProvider, BunchCommandProvider>();
            RegisterComponent<ICashgameCommandProvider, CashgameCommandProvider>();
        }
    }
}