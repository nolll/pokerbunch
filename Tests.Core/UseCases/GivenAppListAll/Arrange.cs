using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.GivenAppListAll
{
    public abstract class Arrange : ArrangeBase
    {
        private AppListAll _sut;

        [SetUp]
        public void Setup()
        {
            var appRepoMock = new Mock<IAppRepository>();

            var apps = new List<App>
            {
                new App("app-id-1", "key-1", "name-1", "user-id-1"),
                new App("app-id-2", "key-2", "name-2", "user-id-2")
            };
            appRepoMock.Setup(s => s.ListAll()).Returns(apps);

            _sut = new AppListAll(appRepoMock.Object);
        }

        protected AppListAll.Result Execute()
        {
            return _sut.Execute();
        }
    }
}
