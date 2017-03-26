using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public abstract class Arrange : UseCaseTest<BunchDetails>
    {
        protected BunchDetails.Result Result;

        protected abstract Role Role { get; }
    
        protected override void Setup()
        {
            var bunch = BunchData.Bunch1(Role);

            Mock<IBunchService>().Setup(s => s.Get(BunchData.Id1)).Returns(bunch);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new BunchDetails.Request(BunchData.Id1));
        }
    }
}
