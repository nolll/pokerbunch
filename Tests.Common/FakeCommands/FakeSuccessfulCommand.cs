using Web.Commands;

namespace Tests.Common.FakeCommands
{
    public class FakeSuccessfulCommand : Command
    {
        public override bool Execute()
        {
            return true;
        }
    }
}
