using System.Collections.Generic;
using Web.Models.NavigationModels;
using Web.Security;
using Web.Services;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class AdminNavigationModelFactory : IAdminNavigationModelFactory
    {
        private readonly IAuth _auth;

        public AdminNavigationModelFactory(
            IAuth auth)
        {
            _auth = auth;
        }

        public NavigationModel Create()
        {
            return new NavigationModel
                {
                    Heading = "Admin",
                    CssClass = "admin-nav",
                    Nodes = GetNodes()
                };
        }

        private IList<NavigationNode> GetNodes()
        {
            return _auth.IsAdmin ? GetAdminNodeList() : GetEmptyNodeList();
        }

        private List<NavigationNode> GetAdminNodeList()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Bunches", new HomegameListUrlModel()),
                    new NavigationNode("Users", new UserListUrlModel())
                };
        }

        private static List<NavigationNode> GetEmptyNodeList()
        {
            return new List<NavigationNode>();
        }
    }
}