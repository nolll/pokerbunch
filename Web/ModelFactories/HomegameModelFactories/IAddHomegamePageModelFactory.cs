using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IAddHomegamePageModelFactory
    {
        AddHomegamePageModel Create(AddHomegamePostModel postModel);
    }
}