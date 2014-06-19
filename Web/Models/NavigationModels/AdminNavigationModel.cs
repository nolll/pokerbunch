using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using Web.Models.UrlModels;
using Web.Services;

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

        public AdminNavigationModel(AppContextResult appContextResult)
            : this(appContextResult.IsAdmin)
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