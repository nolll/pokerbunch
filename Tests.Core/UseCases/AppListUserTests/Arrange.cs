using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.AppListUserTests
{
    public abstract class Arrange : ArrangeBase
    {
        private AppListUser _sut;

        [SetUp]
        public void Setup()
        {
            var appRepoMock = new Mock<IAppRepository>();

            var apps = new List<App>
            {
                new App("app-id-1", "key-1", "name-1", "user-id-1"),
                new App("app-id-2", "key-2", "name-2", "user-id-2")
            };
            appRepoMock.Setup(s => s.List()).Returns(apps);

            _sut = new AppListUser(appRepoMock.Object);
        }

        protected AppListUser.Result Execute()
        {
            return _sut.Execute();
        }
    }
}
