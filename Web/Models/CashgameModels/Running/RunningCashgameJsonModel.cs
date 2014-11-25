using System.Collections.Generic;
using System.Linq;
using Core.UseCases.RunningCashgame;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameJsonModel
    {
        [UsedImplicitly]
        public int PlayerId { get; private set; }

        [UsedImplicitly]
        public string PlayerName { get; private set; }

        [UsedImplicitly]
        public string ReportUrl { get; private set; }

        [UsedImplicitly]
        public string BuyinUrl { get; private set; }

        [UsedImplicitly]
        public string CashoutUrl { get; private set; }

        [UsedImplicitly]
        public string EndGameUrl { get; private set; }

        [UsedImplicitly]
        public int DefaultBuyin { get; private set; }
        
        [UsedImplicitly]
	    public List<RunningCashgamePlayerJsonModel> Players { get; private set; }
        
        public RunningCashgameJsonModel(RunningCashgameResult result)
        {
            PlayerId = result.PlayerId;
            PlayerName = result.PlayerName;
            ReportUrl = result.ReportUrl.Relative;
            BuyinUrl = result.BuyinUrl.Relative;
            CashoutUrl = result.CashoutUrl.Relative;
            DefaultBuyin = result.DefaultBuyin;
            EndGameUrl = result.EndGameUrl.Relative;
            Players = result.PlayerItems.Select(o => new RunningCashgamePlayerJsonModel(o)).ToList();
        }
    }
}