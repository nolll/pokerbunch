using System.Collections.Generic;
using Core.Entities;
using Web.Services;

namespace Web.Models.NavigationModels
{
	public class NavigationModel
    {
	    public string Heading { get; set; }
	    public IList<NavigationNode> Nodes { get; set; }
	    public string CssClass { get; set; }

	    public NavigationModel()
	    {
            Nodes = new List<NavigationNode>();
	    }
	}

    public class UserNavigationModel : NavigationModel
    {
        public UserNavigationModel(User user)
        {
            Heading = "Account";
            CssClass = "user-nav";
            Nodes = GetNodes(user);
        }

        private IList<NavigationNode> GetNodes(User user)
        {
            return user != null ? GetLoggedInNodes(user) : GetAnonymousNodes();
        }

        private IList<NavigationNode> GetAnonymousNodes()
        {
            return new List<NavigationNode>
                {
                    new NavigationNode("Sign in", new LoginUrlModel()),
                    new NavigationNode("Register", new AddUserUrlModel()),
                    new NavigationNode("Forgot password", new ForgotPasswordUrlModel())
                };
        }

        private IList<NavigationNode> GetLoggedInNodes(User user)
        {
            return new List<NavigationNode>
                {
                    new NavigationNode(user.DisplayName, new UserDetailsUrlModel(user.UserName)),
			        //new NavigationNode("Sharing", new SharingSettingsUrlModel()),
			        new NavigationNode("Sign Out", new LogoutUrlModel())
                };
        }
    }
}