using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IAddHomegamePageBuilder
    {
        AddHomegamePageModel Build(AddHomegamePostModel postModel = null);
    }
}