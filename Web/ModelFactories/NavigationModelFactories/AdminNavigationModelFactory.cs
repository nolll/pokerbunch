using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class AdminNavigationModelFactory : IAdminNavigationModelFactory
    {
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
                        new NavigationNode("Bunches", new HomegameListingUrlModel(), selected),
                        new NavigationNode("Users", new UserListingUrlModel(), selected)
                    };
            }

            return new List<NavigationNode>();
        }
    }
}