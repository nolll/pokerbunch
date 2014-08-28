using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IBunchDetailsPageBuilder
    {
        BunchDetailsPageModel Build(string slug);
    }
}