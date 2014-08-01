using Application.Services;
using Application.UseCases.BaseContext;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class BaseContextTests : MockContainer
    {
        [Test]
        public void BaseContext_VersionIsSet()
        {
            SetupDevServer();

            var result = Sut.Execute();

            Assert.IsNotEmpty(result.Version);
        }

        [Test]
        public void BaseContext_AnyServerName_IsInProductionIsFalse()
        {
            SetupDevServer();

            var result = Sut.Execute();

            Assert.IsFalse(result.IsInProduction);
        }

        [Test]
        public void BaseContext_ProductionServerName_IsInProductionIsTrue()
        {
            SetupProductionServer();

            var result = Sut.Execute();

            Assert.IsTrue(result.IsInProduction);
        }

        private void SetupDevServer()
        {
            SetupServer("pokerbunch.lan");
        }

        private void SetupProductionServer()
        {
            SetupServer("pokerbunch.com");
        }

        private void SetupServer(string serverName)
        {
            GetMock<IWebContext>().Setup(o => o.Host).Returns(serverName);
        }

        private BaseContextInteractor Sut
        {
            get {
                return new BaseContextInteractor(
                    GetMock<IWebContext>().Object);
            }
        }
    }
}