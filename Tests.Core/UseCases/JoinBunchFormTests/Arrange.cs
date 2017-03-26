using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.JoinBunchFormTests
{
    public abstract class Arrange : UseCaseTest<JoinBunchForm>
    {
        protected JoinBunchForm.Result Result;

        private const string BunchId = BunchData.Id1;
        protected const string DisplayName = BunchData.DisplayName1;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId, DisplayName);

            Mock<IBunchService>().Setup(o => o.Get(BunchId)).Returns(bunch);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new JoinBunchForm.Request(BunchId));
        }
    }
}