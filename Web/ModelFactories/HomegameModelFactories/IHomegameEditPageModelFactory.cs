using Core.Classes;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameEditPageModelFactory
    {
        HomegameEditPageModel Create(Homegame homegame, HomegameEditPostModel postModel);
    }
}