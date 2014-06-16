using Web.Models.AuthModels;

namespace Web.Commands.AuthCommands
{
    public interface IAuthCommandProvider
    {
        Command GetLoginCommand(LoginPostModel postModel);
        Command GetLogoutCommand();
    }
}