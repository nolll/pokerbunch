using Core.Services;
using Core.UseCases;

namespace Tests.Core.UseCases.TestEmailTests
{
    public abstract class Arrange : UseCaseTest<TestEmail>
    {
        protected TestEmail.Result Result;

        protected string Message = "message";

        protected override void Setup()
        {
            Mock<IAdminService>().Setup(o => o.SendEmail()).Returns(Message);
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }
    }
}
