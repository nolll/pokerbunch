using Core.Classes;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameEditPageModelFactory
    {
        HomegameEditPageModel Create(User user, Homegame homegame);
        HomegameEditPageModel Create(User user, Homegame homegame, HomegameEditPostModel postModel);
    }
}