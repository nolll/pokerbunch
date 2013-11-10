using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelMappers
{
    public class CashgameModelMapper : ICashgameModelMapper
    {
        public Cashgame GetCashgame(Cashgame cashgame, CashgameEditPostModel postModel)
        {
            return new Cashgame
                (
                    cashgame.Id,
                    postModel.Location,
                    cashgame.Status,
                    cashgame.IsStarted,
                    cashgame.StartTime,
                    cashgame.EndTime,
                    cashgame.Duration,
                    cashgame.Results,
                    cashgame.PlayerCount,
                    cashgame.Diff,
                    cashgame.Turnover,
                    cashgame.HasActivePlayers,
                    cashgame.TotalStacks,
                    cashgame.AverageBuyin
                );
        }
    }
}