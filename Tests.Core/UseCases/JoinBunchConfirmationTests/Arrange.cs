using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.JoinBunchConfirmationTests
{
    public abstract class Arrange : UseCaseTest<JoinBunchConfirmation>
    {
        protected JoinBunchConfirmation.Result Result;

        protected const string BunchId = BunchData.Id1;
        protected const string DisplayName = BunchData.DisplayName1;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId, DisplayName);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new JoinBunchConfirmation.Request(BunchId));
        }
    }
}