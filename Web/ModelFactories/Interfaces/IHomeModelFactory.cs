using Web.Models;
using Web.Models.HomeModels;

namespace Web.ModelFactories
{
    public interface IHomeModelFactory
    {
        HomePageModel Create();
    }
}