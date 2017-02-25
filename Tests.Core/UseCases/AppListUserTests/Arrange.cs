using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListUserTests
{
    public abstract class Arrange : UseCaseTest<AppListUser>
    {
        protected AppListUser.Result Result;

        [SetUp]
        public void Setup()
        {
            var apps = AppData.TwoApps;

            Mock<IAppRepository>().Setup(s => s.List()).Returns(apps);

            Result = Sut.Execute();
        }
    }
}
