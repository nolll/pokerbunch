using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IJoinHomegamePageBuilder
    {
        JoinHomegamePageModel Build(string slug, JoinHomegamePostModel postModel = null);
    }
}