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
        protected Mock<IHomegameRepository> HomegameRepositoryMock;
        protected Mock<ICashgameRepository> CashgameRepositoryMock;

        protected Mock<IHomegameStorage> HomegameStorageMock;
        protected Mock<IUserStorage> UserStorageMock;
        protected Mock<ICashgameStorage> CashgameStorageMock;
        protected Mock<ICheckpointStorage> CheckpointStorageMock;
        protected Mock<IPlayerStorage> PlayerStorageMock;

        protected Mock<ICashgameFactory> CashgameFactoryMock;
        protected Mock<ICashgameSuiteFactory> CashgameSuiteFactoryMock;
        protected Mock<ICashgameResultFactory> CashgameResultFactoryMock;
        protected Mock<IHomegameFactory> HomegameFactoryMock;

        protected Mock<IWebContext> WebContextMock;
        protected Mock<IUserContext> UserContextMock;

        protected Mock<ITimeProvider> TimeProviderMock;

        [SetUp]
        public void SetUpMocks()
        {
            HomegameRepositoryMock = new Mock<IHomegameRepository>();
            CashgameRepositoryMock = new Mock<ICashgameRepository>();

            HomegameStorageMock = new Mock<IHomegameStorage>();
            UserStorageMock = new Mock<IUserStorage>();
            CashgameStorageMock = new Mock<ICashgameStorage>();
            CheckpointStorageMock = new Mock<ICheckpointStorage>();
            PlayerStorageMock = new Mock<IPlayerStorage>();

            CashgameFactoryMock = new Mock<ICashgameFactory>();
            CashgameSuiteFactoryMock = new Mock<ICashgameSuiteFactory>();
            CashgameResultFactoryMock = new Mock<ICashgameResultFactory>();
            HomegameFactoryMock = new Mock<IHomegameFactory>();

            WebContextMock = new Mock<IWebContext>();
            UserContextMock = new Mock<IUserContext>();

            TimeProviderMock = new Mock<ITimeProvider>();
        }
    }
}
