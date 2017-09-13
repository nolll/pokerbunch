using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.NavigationModels
{
    public class AdminNavigationModel : NavigationModel
    {
        public AdminNavigationModel(CoreContext.Result appContext)
        {
            Heading = "Admin";
            Nodes = GetNodes(appContext);
        }

        private static IList<NavigationNode> GetNodes(CoreContext.Result appContext)
        {
            return appContext.IsAdmin ? AdminNodeList : new List<NavigationNode>();
        }

        private static List<NavigationNode> AdminNodeList => new List<NavigationNode>
        {
            new NavigationNode("Bunches", new BunchListAllUrl().Relative),
            new NavigationNode("Users", new UserListUrl().Relative),
            new NavigationNode("Apps", new AllAppsUrl().Relative),
            new NavigationNode("Test Email", new TestEmailUrl().Relative),
            new NavigationNode("Clear Cache", new ClearCacheUrl().Relative)
        };

        public override View GetView()
        {
            return new View("~/Views/Navigation/AdminNavigation.cshtml");
        }
    }
}