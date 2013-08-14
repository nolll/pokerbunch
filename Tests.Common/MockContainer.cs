using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.System;
using Moq;
using NUnit.Framework;

namespace Tests.Common
{
    public class MockContainer
    {
        protected Mock<ICashgameRepository> _cashgameRepositoryMock;

        protected Mock<IHomegameStorage> _homegameStorageMock;
        protected Mock<IUserStorage> _userStorageMock;
        protected Mock<ICashgameStorage> _cashgameStorageMock;
        protected Mock<IPlayerStorage> _playerStorageMock;

        protected Mock<ICashgameFactory> _cashgameFactoryMock;
        protected Mock<ICashgameSuiteFactory> _cashgameSuiteFactoryMock;
        protected Mock<ICashgameResultFactory> _cashgameResultFactoryMock;

        protected Mock<IWebContext> _webContextMock;
        protected Mock<IUserContext> _userContextMock;

        protected Mock<ITimeProvider> _timeProviderMock;

        [SetUp]
        public void SetUpMocks()
        {
            _cashgameRepositoryMock = new Mock<ICashgameRepository>();

            _homegameStorageMock = new Mock<IHomegameStorage>();
            _userStorageMock = new Mock<IUserStorage>();
            _cashgameStorageMock = new Mock<ICashgameStorage>();
            _playerStorageMock = new Mock<IPlayerStorage>();

            _cashgameFactoryMock = new Mock<ICashgameFactory>();
            _cashgameSuiteFactoryMock = new Mock<ICashgameSuiteFactory>();
            _cashgameResultFactoryMock = new Mock<ICashgameResultFactory>();

            _webContextMock = new Mock<IWebContext>();
            _userContextMock = new Mock<IUserContext>();

            _timeProviderMock = new Mock<ITimeProvider>();
        }
    }
}
