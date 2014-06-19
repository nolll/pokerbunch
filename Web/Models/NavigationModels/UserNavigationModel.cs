using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.AppContext;
using Application.UseCases.CashgameContext;
using Core.Entities;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.NavigationModels
{
    public class UserNavigationModel : NavigationModel
    {
        public UserNavigationModel(User user)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(user);
        }

        public UserNavigationModel(AppContextResult appContextResult)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(appContextResult);
        }

        private IList<NavigationNode> GetNodes(User user)
        {
            return user != null ? GetLoggedInNodes(user.UserName, user.DisplayName) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetNodes(AppContextResult appContextResult)
        {
            return appContextResult.IsLoggedIn ? GetLoggedInNodes(appContextResult.UserName, appContextResult.UserDisplayName) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new LoginUrl()),
                    new NavigationNode("Register", new AddUserUrl()),
                    new NavigationNode("Forgot password", new ForgotPasswordUrl())
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(string userName, string userDisplayName)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(userDisplayName, new UserDetailsUrl(userName)),
                    //new NavigationNode("Sharing", new SharingSettingsUrlModel()),
                    new NavigationNode("Sign Out", new LogoutUrl())
                };
        }
    }
}