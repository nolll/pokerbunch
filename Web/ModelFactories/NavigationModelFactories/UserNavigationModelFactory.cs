using Core.Entities;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public class UserNavigationModelFactory : IUserNavigationModelFactory
    {
        public NavigationModel Create(User user)
        {
            return new UserNavigationModel(user);
        }
    }
}