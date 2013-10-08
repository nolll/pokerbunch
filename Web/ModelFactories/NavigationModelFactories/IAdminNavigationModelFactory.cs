using Core.Classes;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.NavigationModelFactories
{
    public interface IAdminNavigationModelFactory
    {
        NavigationModel Create(User user);
    }
}