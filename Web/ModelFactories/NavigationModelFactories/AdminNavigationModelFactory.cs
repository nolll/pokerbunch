using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class AdminNavigationModelFactory : IAdminNavigationModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public AdminNavigationModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public NavigationModel Create(User user)
        {
            return new NavigationModel
                {
                    Heading = "Admin",
                    CssClass = "admin-nav",
                    Nodes = GetNodes(user)
                };
        }

        private IList<NavigationNode> GetNodes(User user)
        {
            var isAdmin = user != null && user.IsAdmin;
            if (isAdmin)
            {
                const bool selected = false;
                return new List<NavigationNode>
                    {
                        new NavigationNode("Bunches", _urlProvider.GetHomegameListUrl(), selected),
                        new NavigationNode("Users", _urlProvider.GetUserListUrl(), selected)
                    };
            }

            return new List<NavigationNode>();
        }
    }
}