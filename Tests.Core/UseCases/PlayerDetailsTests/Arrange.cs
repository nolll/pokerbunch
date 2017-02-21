using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public abstract class Arrange
    {
        protected PlayerDetails Sut;

        protected abstract Role Role { get; }
        protected abstract string PlayerId { get; }
        protected string IdForPlayerThatIsUser = PlayerData.Id1;
        protected string IdForPlayerThatIsNotUser = PlayerData.Id2;
        protected string IdForPlayerThatHasPlayedGames = PlayerData.Id1;
        protected string IdForPlayerThatHasNotPlayedGames = PlayerData.Id2;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchData.Id1))
                .Returns(BunchData.Bunch(Role));

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.Get(IdForPlayerThatIsUser))
                .Returns(new Player(BunchData.Id1, IdForPlayerThatIsUser, UserData.Id1, PlayerData.Name1, Role, PlayerData.Color1));
            prm.Setup(o => o.Get(IdForPlayerThatIsNotUser))
                .Returns(new Player(BunchData.Id1, IdForPlayerThatIsNotUser, null, PlayerData.Name1, Role, PlayerData.Color1));

            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.PlayerList(IdForPlayerThatHasPlayedGames))
                .Returns(cashgames);
            crm.Setup(o => o.PlayerList(IdForPlayerThatHasNotPlayedGames))
                .Returns(CashgameData.EmptyCollection);

            var urm = new Mock<IUserRepository>();
            urm.Setup(o => o.GetById(UserData.Id1))
                .Returns(new User(UserData.Id1, UserData.UserName1, "user-display-name", "user-real-name", "user1@example.com"));
            
            Sut = new PlayerDetails(brm.Object, prm.Object, crm.Object, urm.Object);
        }

        protected PlayerDetails.Request Request => new PlayerDetails.Request(PlayerId);
    }
}