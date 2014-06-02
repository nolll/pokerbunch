using Application.Services;
using Web.Models.NavigationModels;

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
            return new AdminNavigationModel(_auth.IsAdmin);
        }
    }
}