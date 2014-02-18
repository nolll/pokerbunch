namespace Web.Commands.AdminCommands
{
    public interface IAdminCommandProvider
    {
        EmailTestCommand GetEmailTestCommand(string to);
    }
}