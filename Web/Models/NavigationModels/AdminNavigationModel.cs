using System.Collections.Generic;
using Core.UseCases.Home;

namespace Web.Models.NavigationModels
{
    public class AdminNavigationModel : NavigationModel
    {
        public AdminNavigationModel(HomeResult homeResult)
        {
            Heading = "Admin";
            CssClass = "admin-nav";
            Nodes = GetNodes(homeResult);
        }

        private IList<NavigationNode> GetNodes(HomeResult homeResult)
        {
            return homeResult.IsAdmin ? GetAdminNodeList(homeResult) : new List<NavigationNode>();
        }

        private List<NavigationNode> GetAdminNodeList(HomeResult homeResult)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Bunches", homeResult.BunchListUrl.Relative),
                    new NavigationNode("Users", homeResult.UserListUrl.Relative),
                    new NavigationNode("Test Email", homeResult.TestEmailUrl.Relative),
                    new NavigationNode("Clear Cache", homeResult.ClearCacheUrl.Relative)//,
                };
        }
    }
}