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
        protected Mock<ICashgameRepository> CashgameRepositoryMock;

        protected Mock<IHomegameStorage> HomegameStorageMock;
        protected Mock<IUserStorage> UserStorageMock;
        protected Mock<ICashgameStorage> CashgameStorageMock;
        protected Mock<IPlayerStorage> PlayerStorageMock;

        protected Mock<ICashgameFactory> CashgameFactoryMock;
        protected Mock<ICashgameSuiteFactory> CashgameSuiteFactoryMock;
        protected Mock<ICashgameResultFactory> CashgameResultFactoryMock;

        protected Mock<IWebContext> WebContextMock;
        protected Mock<IUserContext> UserContextMock;

        protected Mock<ITimeProvider> TimeProviderMock;

        [SetUp]
        public void SetUpMocks()
        {
            CashgameRepositoryMock = new Mock<ICashgameRepository>();

            HomegameStorageMock = new Mock<IHomegameStorage>();
            UserStorageMock = new Mock<IUserStorage>();
            CashgameStorageMock = new Mock<ICashgameStorage>();
            PlayerStorageMock = new Mock<IPlayerStorage>();

            CashgameFactoryMock = new Mock<ICashgameFactory>();
            CashgameSuiteFactoryMock = new Mock<ICashgameSuiteFactory>();
            CashgameResultFactoryMock = new Mock<ICashgameResultFactory>();

            WebContextMock = new Mock<IWebContext>();
            UserContextMock = new Mock<IUserContext>();

            TimeProviderMock = new Mock<ITimeProvider>();
        }
    }
}
