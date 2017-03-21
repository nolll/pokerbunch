using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.JoinBunchTests
{
    public abstract class Arrange : UseCaseTest<JoinBunch>
    {
        protected JoinBunch.Result Result;

        protected const string BunchId = BunchData.Id1;
        protected const string Code = "abc123";

        protected string PostedBunchId;
        protected string PostedCode;

        protected override void Setup()
        {
            PostedBunchId = null;
            PostedCode = null;

            Mock<IBunchRepository>().Setup(o => o.Join(It.IsAny<string>(), It.IsAny<string>())).Callback((string bunchId, string code) => { PostedBunchId = bunchId; PostedCode = code; });
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new JoinBunch.Request(BunchId, Code));
        }
    }
}