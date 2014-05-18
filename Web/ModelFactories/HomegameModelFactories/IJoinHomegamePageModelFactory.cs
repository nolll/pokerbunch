using Core.Entities;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegamePageModelFactory
    {
        JoinHomegamePageModel Create(Homegame homegame, JoinHomegamePostModel postModel);
    }
}