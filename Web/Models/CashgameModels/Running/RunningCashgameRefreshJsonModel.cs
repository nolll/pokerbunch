using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameJsonModel : RunningCashgameRefreshJsonModel
    {
        [UsedImplicitly]
        public int PlayerId { get; private set; }

        [UsedImplicitly]
        public string RefreshUrl { get; private set; }

        [UsedImplicitly]
        public string ReportUrl { get; private set; }

        [UsedImplicitly]
        public string BuyinUrl { get; private set; }

        [UsedImplicitly]
        public string CashoutUrl { get; private set; }

        [UsedImplicitly]
        public string EndGameUrl { get; private set; }

        [UsedImplicitly]
        public string CashgameIndexUrl { get; private set; }

        [UsedImplicitly]
        public int DefaultBuyin { get; private set; }

        [UsedImplicitly]
        public string Location { get; private set; }

        [UsedImplicitly]
        public bool IsManager { get; private set; }

        [UsedImplicitly]
        public List<BunchPlayerJsonModel> BunchPlayers { get; private set; }

        public RunningCashgameJsonModel(RunningCashgame.Result result) : base(result)
        {
            PlayerId = result.PlayerId;
            RefreshUrl = result.PlayersDataUrl.Relative;
            ReportUrl = result.ReportUrl.Relative;
            BuyinUrl = result.BuyinUrl.Relative;
            CashoutUrl = result.CashoutUrl.Relative;
            EndGameUrl = result.EndGameUrl.Relative;
            CashgameIndexUrl = result.CashgameIndexUrl.Relative;
            DefaultBuyin = result.DefaultBuyin;
            Location = result.Location;
            IsManager = result.IsManager;
            BunchPlayers = result.BunchPlayerItems.Select(o => new BunchPlayerJsonModel(o)).ToList();
        }
    }

    public class RunningCashgameRefreshJsonModel
    {
        [UsedImplicitly]
        public List<RunningCashgamePlayerJsonModel> Players { get; private set; }

        public RunningCashgameRefreshJsonModel(RunningCashgame.Result result)
        {
            Players = result.PlayerItems.Select(o => new RunningCashgamePlayerJsonModel(o)).ToList();
        }
    }
}