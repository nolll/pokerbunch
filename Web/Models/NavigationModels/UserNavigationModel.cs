using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;

namespace Web.Models.NavigationModels
{
    public class UserNavigationModel : NavigationModel
    {
        public UserNavigationModel(CoreContext.Result appContextResult)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(appContextResult);
        }

        private IList<NavigationNode> GetNodes(CoreContext.Result appContextResult)
        {
            return appContextResult.IsLoggedIn ? GetLoggedInNodes(appContextResult) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new LoginUrl().Relative),
                    new NavigationNode("Register", new AddUserUrl().Relative),
                    new NavigationNode("Reset password", new ResetPasswordUrl().Relative)
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(CoreContext.Result appContextResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode($"Signed in as {appContextResult.UserDisplayName}", new UserDetailsUrl(appContextResult.UserName).Relative),
                    new NavigationNode("Sign Out", new LogoutUrl().Relative)
                };
        }
    }
}