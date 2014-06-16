using Web.Models.HomegameModels.Edit;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IEditHomegamePageBuilder
    {
        HomegameEditPageModel Build(string slug, HomegameEditPostModel postModel = null);
    }
}