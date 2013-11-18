using Web.Commands;

namespace Tests.Common.FakeCommands
{
    public class FakeFailedCommand : Command
    {
        public override bool Execute()
        {
            return false;
        }
    }
}
