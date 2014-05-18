using System.Collections.Generic;
using Application.Services;
using Core.Entities;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class UserNavigationModelFactory : IUserNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public UserNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public NavigationModel Create(User user)
        {
            return new NavigationModel
                {
                    Heading = "Account",
                    CssClass = "user-nav",
                    Nodes = GetNodes(user)
                };
        }

        private IList<NavigationNode> GetNodes(User user)
        {
            return user != null ? GetLoggedInNodes(user) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", _urlProvider.GetLoginUrl()),
                    new NavigationNode("Register", _urlProvider.GetAddUserUrl()),
                    new NavigationNode("Forgot password", _urlProvider.GetForgotPasswordUrl())
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(User user)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(user.DisplayName, _urlProvider.GetUserDetailsUrl(user.UserName)),
			        //new NavigationNode("Sharing", _urlProvider.GetSharingSettingsUrl()),
			        new NavigationNode("Sign Out", _urlProvider.GetLogoutUrl())
                };
		}
    }
}