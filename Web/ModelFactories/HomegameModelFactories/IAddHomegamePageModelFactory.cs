using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IAddHomegamePageModelFactory
    {
        AddHomegamePageModel Create(User user, AddHomegamePostModel postModel);
    }
}