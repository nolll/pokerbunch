using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListUserTests
{
    public abstract class Arrange : UseCaseTest<AppListUser>
    {
        protected AppListUser.Result Result;

        protected override void Setup()
        {
            var apps = AppData.TwoApps;

            Mock<IAppService>().Setup(s => s.List()).Returns(apps);
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }
    }
}
