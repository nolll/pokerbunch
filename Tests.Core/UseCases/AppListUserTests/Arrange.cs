using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListUserTests
{
    public abstract class Arrange
    {
        protected AppListUser Sut;

        [SetUp]
        public void Setup()
        {
            var arm = new Mock<IAppRepository>();
            arm.Setup(s => s.List()).Returns(AppData.TwoApps);

            Sut = new AppListUser(arm.Object);
        }
    }
}
