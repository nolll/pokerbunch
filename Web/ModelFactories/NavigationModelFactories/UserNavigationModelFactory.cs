using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class UserNavigationModelFactory : IUserNavigationModelFactory
    {
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
            if (user == null)
            {
                return GetAnonymousNodes();
            }
            else
            {
                return GetLoggedInNodes(user);
            }
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new AuthLoginUrlModel()),
                    new NavigationNode("Register", new UserAddUrlModel()),
                    new NavigationNode("Forgot password", new ForgotPasswordUrlModel())
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(User user)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(user.DisplayName, new UserDetailsUrlModel(user)),
			        new NavigationNode("Sharing", new SharingSettingsUrlModel()),
			        new NavigationNode("Sign Out", new AuthLogoutUrlModel())
                };
		}
    }
}