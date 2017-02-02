using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.AppListUserTests
{
    public abstract class Arrange
    {
        protected AppListUser Sut;

        [SetUp]
        public void Setup()
        {
            var appRepoMock = new Mock<IAppRepository>();

            var app1 = new App("app-id-1", "key-1", "name-1", "user-id-1");
            var app2 = new App("app-id-2", "key-2", "name-2", "user-id-2");
            var apps = new List<App> {app1, app2};
            appRepoMock.Setup(s => s.List()).Returns(apps);

            Sut = new AppListUser(appRepoMock.Object);
        }
    }
}
