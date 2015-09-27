using System.Collections.Generic;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.NavigationModels
{
    public class AdminNavigationModel : NavigationModel
    {
        public AdminNavigationModel(AppContext.Result appContext)
        {
            Heading = "Admin";
            Nodes = GetNodes(appContext);
        }

        private IList<NavigationNode> GetNodes(AppContext.Result appContext)
        {
            return appContext.IsAdmin ? GetAdminNodeList() : new List<NavigationNode>();
        }

        private List<NavigationNode> GetAdminNodeList()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Bunches", new BunchListUrl().Relative),
                    new NavigationNode("Users", new UserListUrl().Relative),
                    new NavigationNode("Apps", new AllAppsUrl().Relative),
                    new NavigationNode("Test Email", new TestEmailUrl().Relative),
                    new NavigationNode("Clear Cache", new ClearCacheUrl().Relative)
                };
        }

        public override string ViewName
        {
            get { return "~/Views/Navigation/AdminNavigation.cshtml"; }
        }
    }
}