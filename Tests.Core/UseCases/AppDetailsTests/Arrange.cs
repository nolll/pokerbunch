using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppDetailsTests
{
    public abstract class Arrange : UseCaseTest<AppDetails>
    {
        protected AppDetails.Result Result;

        [SetUp]
        public void Setup()
        {
            var app = AppData.OneApp;

            Mock<IAppRepository>().Setup(s => s.GetById(AppData.Id1)).Returns(app);

            Result = Sut.Execute(new AppDetails.Request(AppData.Id1));
        }
    }
}