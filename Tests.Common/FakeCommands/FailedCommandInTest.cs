using Web.Commands;

namespace Tests.Common.FakeCommands
{
    public class FailedCommandInTest : Command
    {
        public override bool Execute()
        {
            return false;
        }
    }
}
