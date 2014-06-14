using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameDetailsPageBuilder
    {
        HomegameDetailsPageModel Build(string slug);
    }
}