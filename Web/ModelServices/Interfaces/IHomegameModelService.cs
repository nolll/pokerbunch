using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.ModelServices
{
    public interface IHomegameModelService
    {
        HomegameDetailsPageModel GetDetailsModel(string slug);
        AddHomegamePageModel GetAddModel(AddHomegamePostModel postModel = null);
        AddHomegameConfirmationPageModel GetAddConfirmationModel();
        HomegameEditPageModel GetEditModel(string slug, HomegameEditPostModel postModel = null);
        JoinHomegamePageModel GetJoinModel(string slug, JoinHomegamePostModel postModel = null);
        JoinHomegameConfirmationPageModel GetJoinConfirmationModel(string slug);
    }
}