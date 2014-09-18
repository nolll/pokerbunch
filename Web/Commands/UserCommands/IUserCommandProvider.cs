using Web.Models.UserModels.Add;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;

namespace Web.Commands.UserCommands
{
    public interface IUserCommandProvider
    {
        Command GetChangePasswordCommand(ChangePasswordPostModel postModel);
        Command GetEditCommand(string userName, EditUserPostModel postModel);
        Command GetAddCommand(AddUserPostModel postModel);
    }
}