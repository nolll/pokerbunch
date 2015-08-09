using System.Collections.Generic;
using Core.UseCases;

namespace Web.Models.NavigationModels
{
    public class UserNavigationModel : NavigationModel
    {
        public UserNavigationModel(AppContext.Result appContextResult)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(appContextResult);
        }

        private IList<NavigationNode> GetNodes(AppContext.Result appContextResult)
        {
            return appContextResult.IsLoggedIn ? GetLoggedInNodes(appContextResult) : GetAnonymousNodes(appContextResult);
        }

        private IList<NavigationNode> GetAnonymousNodes(AppContext.Result appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", appContextResult.LoginUrl.Relative),
                    new NavigationNode("Register", appContextResult.AddUserUrl.Relative),
                    new NavigationNode("Forgot password", appContextResult.ForgotPasswordUrl.Relative)
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(AppContext.Result appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(string.Format("Signed in as {0}", appContextResult.UserDisplayName), appContextResult.UserDetailsUrl.Relative),
                    new NavigationNode("Sign Out", appContextResult.LogoutUrl.Relative)
                };
        }
    }
}