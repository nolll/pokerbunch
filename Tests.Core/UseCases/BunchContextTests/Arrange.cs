using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchContextTests
{
    public abstract class Arrange : UseCaseTest<BunchContext>
    {
        protected BunchContext.Result Result;

        protected abstract string BunchId { get; }

        protected override void Setup()
        {
            var singleBunch = new Bunch(BunchId);
            Mock<IBunchService>().Setup(o => o.Get(BunchId)).Returns(singleBunch);
        }

        protected override void Execute()
        {
            var coreContext = new CoreContext.Result(true, false, UserData.UserName1, UserData.DisplayName1);
            Result = Subject.Execute(coreContext, new BunchContext.Request(BunchId));
        }
    }
}