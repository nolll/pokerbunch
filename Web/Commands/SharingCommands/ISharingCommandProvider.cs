namespace Web.Commands.SharingCommands
{
    public interface ISharingCommandProvider
    {
        Command GetTwitterStopCommand();
        Command GetStartCommand(string token, string verifier);
    }
}