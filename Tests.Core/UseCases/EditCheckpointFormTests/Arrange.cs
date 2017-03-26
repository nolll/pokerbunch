using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCheckpointFormTests
{
    public abstract class Arrange : UseCaseTest<EditCheckpointForm>
    {
        protected EditCheckpointForm.Result Result;

        protected const string CashgameId = CashgameData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected abstract string ActionId { get; }
        private readonly TimeZoneInfo _timezone = TimezoneData.Swedish;
        private readonly Currency _currency = CurrencyData.Sek;
        protected readonly string BuyinActionId = "action-id-1";
        protected readonly string ReportActionId = "action-id-2";
        protected DateTime BuyinTime = TimeData.Swedish("2001-01-01 12:00:00");
        private readonly DateTime _reportTime = TimeData.Swedish("2001-01-01 13:00:00");
        protected const int Stack = 200;
        protected const int Added = 200;

        protected override void Setup()
        {
            Mock<ICashgameService>().Setup(o => o.GetDetailedById(CashgameId)).Returns(GameWithTwoPlayers);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditCheckpointForm.Request(CashgameId, ActionId));
        }

        private DetailedCashgame GameWithTwoPlayers => new DetailedCashgame(
            CashgameId,
            BuyinTime,
            _reportTime,
            false,
            new CashgameBunch(BunchData.Id1, _timezone, _currency),
            Role.Admin,
            new CashgameLocation(LocationData.Id1, LocationData.Name1),
            null,
            new List<DetailedCashgame.CashgamePlayer>
            {
                new DetailedCashgame.CashgamePlayer(
                    PlayerData.Id1,
                    PlayerData.Name1,
                    PlayerData.Color1,
                    200,
                    200,
                    BuyinTime,
                    _reportTime,
                    new List<DetailedCashgame.CashgameAction>
                    {
                        new DetailedCashgame.CashgameAction(
                            BuyinActionId,
                            PlayerId,
                            CheckpointType.Buyin,
                            BuyinTime,
                            Stack,
                            Added),
                        new DetailedCashgame.CashgameAction(
                            ReportActionId,
                            PlayerId,
                            CheckpointType.Cashout,
                            _reportTime,
                            Stack,
                            0)
                    })
            });
    }
}