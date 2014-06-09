using System.Collections.Generic;
using Application.UseCases.ApplicationContext;
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

        public UserNavigationModel(ApplicationContextResult applicationContextResult)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(applicationContextResult);
        }

        private IList<NavigationNode> GetNodes(User user)
        {
            return user != null ? GetLoggedInNodes(user.UserName, user.DisplayName) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetNodes(ApplicationContextResult applicationContextResult)
        {
            return applicationContextResult.IsLoggedIn != null ? GetLoggedInNodes(applicationContextResult.UserName, applicationContextResult.UserDisplayName) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new LoginUrlModel()),
                    new NavigationNode("Register", new AddUserUrlModel()),
                    new NavigationNode("Forgot password", new ForgotPasswordUrlModel())
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(string userName, string userDisplayName)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(userDisplayName, new UserDetailsUrlModel(userName)),
                    //new NavigationNode("Sharing", new SharingSettingsUrlModel()),
                    new NavigationNode("Sign Out", new LogoutUrlModel())
                };
        }
    }
}