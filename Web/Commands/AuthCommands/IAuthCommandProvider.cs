namespace Web.Commands.AuthCommands
{
    public interface IAuthCommandProvider
    {
        Command GetLoginCommand(string loginName, string password, bool rememberMe);
        Command GetLogoutCommand();
    }
}