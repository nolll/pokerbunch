using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.Home;

namespace Web.Models.NavigationModels
{
    public class AdminNavigationModel : NavigationModel
    {
        public AdminNavigationModel(bool isAdmin)
        {
            Heading = "Admin";
            CssClass = "admin-nav";
            Nodes = GetNodes(isAdmin);
        }

        public AdminNavigationModel(HomeResult homeResult)
            : this(homeResult.IsAdmin)
        {
        }

        private IList<NavigationNode> GetNodes(bool isAdmin)
        {
            return isAdmin ? GetAdminNodeList() : GetEmptyNodeList();
        }

        private List<NavigationNode> GetAdminNodeList()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Bunches", new HomegameListUrl()),
                    new NavigationNode("Users", new UserListUrl())
                };
        }

        private static List<NavigationNode> GetEmptyNodeList()
        {
            return new List<NavigationNode>();
        }
    }
}