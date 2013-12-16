using Web.Models.HomeModels;

namespace Web.ModelServices
{
    public interface IHomeModelService
    {
        HomePageModel GetIndexModel();
    }
}