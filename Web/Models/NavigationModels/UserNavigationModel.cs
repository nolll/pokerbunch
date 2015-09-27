using System.Collections.Generic;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;

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
            return appContextResult.IsLoggedIn ? GetLoggedInNodes(appContextResult) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new LoginUrl().Relative),
                    new NavigationNode("Register", new AddUserUrl().Relative),
                    new NavigationNode("Forgot password", new ForgotPasswordUrl().Relative)
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(AppContext.Result appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(string.Format("Signed in as {0}", appContextResult.UserDisplayName), new UserDetailsUrl(appContextResult.UserName).Relative),
                    new NavigationNode("Sign Out", new LogoutUrl().Relative)
                };
        }
    }
}