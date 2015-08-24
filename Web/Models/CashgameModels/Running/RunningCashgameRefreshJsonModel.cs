using System.Collections.Generic;
using System.Linq;
using Core.Urls;
using Core.UseCases;
using Web.Annotations;
using Web.Urls;

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
            RefreshUrl = new RunningCashgamePlayersJsonUrl(result.Slug).Relative;
            ReportUrl = new CashgameReportUrl(result.Slug).Relative;
            BuyinUrl = new CashgameBuyinUrl(result.Slug).Relative;
            CashoutUrl = new CashgameCashoutUrl(result.Slug).Relative;
            EndGameUrl = new EndCashgameUrl(result.Slug).Relative;
            CashgameIndexUrl = new RunningCashgameGameJsonUrl(result.Slug).Relative;
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