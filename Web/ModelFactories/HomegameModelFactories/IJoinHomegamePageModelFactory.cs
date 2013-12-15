using Core.Classes;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegamePageModelFactory
    {
        JoinHomegamePageModel Create(User user, Homegame homegame, JoinHomegamePostModel postModel);
    }
}