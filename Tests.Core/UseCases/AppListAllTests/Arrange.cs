using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListAllTests
{
    public abstract class Arrange
    {
        protected AppListAll Sut;

        [SetUp]
        public void Setup()
        {
            var arm = new Mock<IAppRepository>();
            arm.Setup(s => s.ListAll()).Returns(AppData.TwoApps);

            Sut = new AppListAll(arm.Object);
        }
    }
}
