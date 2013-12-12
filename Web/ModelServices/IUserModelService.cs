using Web.Models.UserModels;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;
using Web.Models.UserModels.List;

namespace Web.ModelServices
{
    public interface IUserModelService
    {
        UserDetailsPageModel GetDetailsModel(string userName);
        UserListPageModel GetListModel();
        AddUserPageModel GetAddModel(AddUserPostModel postModel = null);
        AddUserConfirmationPageModel GetAddConfirmationModel();
        EditUserPageModel GetEditModel(string userName, EditUserPostModel postModel = null);
        ChangePasswordPageModel GetChangePasswordModel();
        ChangePasswordConfirmationPageModel GetChangePasswordConfirmationModel();
        ForgotPasswordPageModel GetForgotPasswordModel(ForgotPasswordPostModel postModel = null);
        ForgotPasswordConfirmationPageModel GetForgotPasswordConfirmationModel();
    }
}