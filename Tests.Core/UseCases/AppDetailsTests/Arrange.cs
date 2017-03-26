using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppDetailsTests
{
    public abstract class Arrange : UseCaseTest<AppDetails>
    {
        protected AppDetails.Result Result;

        protected override void Setup()
        {
            var app = AppData.OneApp;

            Mock<IAppService>().Setup(s => s.GetById(AppData.Id1)).Returns(app);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AppDetails.Request(AppData.Id1));
        }
    }
}