using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListAllTests
{
    public abstract class Arrange : UseCaseTest<AppListAll>
    {
        protected AppListAll.Result Result;

        protected override void Setup()
        {
            var apps = AppData.TwoApps;

            Mock<IAppService>().Setup(s => s.ListAll()).Returns(apps);
        }

        protected override void Execute()
        {
            Result = Subject.Execute();
        }
    }
}
