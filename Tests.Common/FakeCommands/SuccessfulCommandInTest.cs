using Web.Commands;

namespace Tests.Common.FakeCommands
{
    public class SuccessfulCommandInTest : Command
    {
        public override bool Execute()
        {
            return true;
        }
    }
}
