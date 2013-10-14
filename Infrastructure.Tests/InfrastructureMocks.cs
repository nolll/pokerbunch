using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Moq;
using Tests.Common;

namespace Infrastructure.Tests
{
    public class InfrastructureMocks : WebMocks
    {
        internal readonly Mock<IHomegameStorage> HomegameStorageMock;
        public readonly Mock<IUserStorage> UserStorageMock;
        public readonly Mock<ICashgameStorage> CashgameStorageMock;
        public readonly Mock<ICheckpointStorage> CheckpointStorageMock;
        public readonly Mock<IPlayerStorage> PlayerStorageMock;
        public readonly Mock<IRawHomegameFactory> RawHomegameFactoryMock;
        public readonly Mock<IRawCashgameFactory> RawCashgameFactoryMock;
        
        public InfrastructureMocks()
        {
            HomegameStorageMock = new Mock<IHomegameStorage>();
            UserStorageMock = new Mock<IUserStorage>();
            CashgameStorageMock = new Mock<ICashgameStorage>();
            CheckpointStorageMock = new Mock<ICheckpointStorage>();
            PlayerStorageMock = new Mock<IPlayerStorage>();
            RawHomegameFactoryMock = new Mock<IRawHomegameFactory>();
            RawCashgameFactoryMock = new Mock<IRawCashgameFactory>();
        }

    }

}
