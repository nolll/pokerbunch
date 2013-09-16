using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelMappers
{
    public class CashgameModelMapper : ICashgameModelMapper
    {
        public Cashgame GetCashgame(Cashgame cashgame, CashgameEditPostModel postModel)
        {
            return new Cashgame
                {
                    Location = postModel.Location,
                    Status = cashgame.Status,
                    Id = cashgame.Id,
                    Results = cashgame.Results,
                    PlayerCount = cashgame.PlayerCount,
                    StartTime = cashgame.StartTime,
                    EndTime = cashgame.EndTime,
                    Duration = cashgame.Duration,
                    IsStarted = cashgame.IsStarted,
                    Turnover = cashgame.Turnover,
                    HasActivePlayers = cashgame.HasActivePlayers,
                    TotalStacks = cashgame.TotalStacks,
                    AverageBuyin = cashgame.AverageBuyin
                };
        }
    }
}