using Core.Entities;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface IUserNavigationModelFactory
    {
        NavigationModel Create(User user);
    }
}