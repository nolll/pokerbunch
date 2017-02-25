using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListAllTests
{
    public abstract class Arrange : UseCaseTest<AppListAll>
    {
        protected AppListAll.Result Result;

        [SetUp]
        public void Setup()
        {
            var apps = AppData.TwoApps;

            Mock<IAppRepository>().Setup(s => s.ListAll()).Returns(apps);

            Result = Sut.Execute();
        }
    }
}
