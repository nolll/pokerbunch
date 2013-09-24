using Core.Classes;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegamePageModelFactory
    {
        JoinHomegamePageModel Create(User user);
        JoinHomegamePageModel Create(User user, JoinHomegamePostModel postModel);
    }
}