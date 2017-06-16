using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public abstract class Arrange : UseCaseTest<PlayerDetails>
    {
        protected PlayerDetails.Result Result;

        protected abstract Role Role { get; }
        protected abstract string PlayerId { get; }
        protected string IdForPlayerThatIsUser = PlayerData.Id1;
        protected string IdForPlayerThatIsNotUser = PlayerData.Id2;
        protected string IdForPlayerThatHasPlayedGames = PlayerData.Id1;
        protected string IdForPlayerThatHasNotPlayedGames = PlayerData.Id2;

        protected override void Setup()
        {
            var bunch = BunchData.Bunch1(Role);
            var playerThatIsUser = new Player(BunchData.Id1, IdForPlayerThatIsUser, UserData.Id1, UserData.UserName1, PlayerData.Name1, Role, PlayerData.Color1);
            var playerThatIsNotUser = new Player(BunchData.Id1, IdForPlayerThatIsNotUser, null, null, PlayerData.Name1, Role, PlayerData.Color1);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var user = new User(UserData.Id1, UserData.UserName1, "user-display-name", "user-real-name", "user1@example.com");

            Mock<IBunchService>().Setup(o => o.Get(BunchData.Id1)).Returns(bunch);
            Mock<IPlayerService>().Setup(o => o.Get(IdForPlayerThatIsUser)).Returns(playerThatIsUser);
            Mock<IPlayerService>().Setup(o => o.Get(IdForPlayerThatIsNotUser)).Returns(playerThatIsNotUser);
            Mock<ICashgameService>().Setup(o => o.PlayerList(IdForPlayerThatHasPlayedGames)).Returns(cashgames);
            Mock<ICashgameService>().Setup(o => o.PlayerList(IdForPlayerThatHasNotPlayedGames)).Returns(new List<ListCashgame>());
            Mock<IUserService>().Setup(o => o.GetById(UserData.Id1)).Returns(user);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new PlayerDetails.Request(PlayerId));
        }
    }
}