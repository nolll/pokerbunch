using System.Collections.Generic;
using Core.UseCases.AppContext;

namespace Web.Models.NavigationModels
{
    public class UserNavigationModel : NavigationModel
    {
        public UserNavigationModel(AppContextResult appContextResult)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(appContextResult);
        }

        private IList<NavigationNode> GetNodes(AppContextResult appContextResult)
        {
            return appContextResult.IsLoggedIn ? GetLoggedInNodes(appContextResult) : GetAnonymousNodes(appContextResult);
        }

        private IList<NavigationNode> GetAnonymousNodes(AppContextResult appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", appContextResult.LoginUrl.Relative),
                    new NavigationNode("Register", appContextResult.AddUserUrl.Relative),
                    new NavigationNode("Forgot password", appContextResult.ForgotPasswordUrl.Relative)
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(AppContextResult appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(appContextResult.UserDisplayName, appContextResult.UserDetailsUrl.Relative),
                    new NavigationNode("Sign Out", appContextResult.LogoutUrl.Relative)
                };
        }
    }
}