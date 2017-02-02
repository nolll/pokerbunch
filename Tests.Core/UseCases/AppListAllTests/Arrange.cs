using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.AppListAllTests
{
    public abstract class Arrange
    {
        protected AppListAll Sut;

        [SetUp]
        public void Setup()
        {
            var appRepoMock = new Mock<IAppRepository>();

            var app1 = new App("app-id-1", "key-1", "name-1", "user-id-1");
            var app2 = new App("app-id-2", "key-2", "name-2", "user-id-2");
            var apps = new List<App> {app1, app2};
            appRepoMock.Setup(s => s.ListAll()).Returns(apps);

            Sut = new AppListAll(appRepoMock.Object);
        }
    }
}
