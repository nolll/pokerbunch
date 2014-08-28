using Web.Models.HomegameModels.Edit;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IEditBunchPageBuilder
    {
        BunchEditPageModel Build(string slug, BunchEditPostModel postModel = null);
    }
}