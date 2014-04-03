using System.Collections.Generic;
using Application.Services;
using Web.Models.NavigationModels;
using Web.Security;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class AdminNavigationModelFactory : IAdminNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IAuth _auth;

        public AdminNavigationModelFactory(
            IUrlProvider urlProvider,
            IAuth auth)
        {
            _urlProvider = urlProvider;
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
            return _auth.IsAdmin() ? GetAdminNodeList() : GetEmptyNodeList();
        }

        private List<NavigationNode> GetAdminNodeList()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Bunches", _urlProvider.GetHomegameListUrl()),
                    new NavigationNode("Users", _urlProvider.GetUserListUrl())
                };
        }

        private static List<NavigationNode> GetEmptyNodeList()
        {
            return new List<NavigationNode>();
        }
    }
}