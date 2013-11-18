using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Commands.UserCommands
{
    public interface IUserCommandProvider
    {
        Command GetForgotPasswordCommand(ForgotPasswordPostModel postModel);
        Command GetChangePasswordCommand(User user, ChangePasswordPostModel postModel);
        Command GetEditCommand(User user, EditUserPostModel postModel);
        Command GetAddCommand(AddUserPostModel postModel);
    }
}