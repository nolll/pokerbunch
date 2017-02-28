using Core.Services;
using Core.UseCases;

namespace Tests.Core.UseCases.ClearCacheTests
{
    public abstract class Arrange : UseCaseTest<ClearCache>
    {
        protected ClearCache.Result Result;

        protected string Message = "message";

        protected override void Setup()
        {
            Mock<IAdminService>().Setup(o => o.ClearCache()).Returns(Message);
        }

        protected override void Execute()
        {
            Result = Sut.Execute();
        }
    }
}