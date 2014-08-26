using Application.Factories;
using Core.Entities;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelMappers
{
    public static class CashgameModelMapper
    {
        public static Cashgame Map(Cashgame cashgame, CashgameEditPostModel postModel)
        {
            return CashgameFactory.Create(
                    cashgame.Id,
                    cashgame.HomegameId,
                    postModel.Location,
                    cashgame.Status,
                    cashgame.IsStarted,
                    cashgame.StartTime,
                    cashgame.EndTime,
                    cashgame.Results,
                    cashgame.PlayerCount,
                    cashgame.Diff,
                    cashgame.Turnover,
                    cashgame.HasActivePlayers,
                    cashgame.TotalStacks,
                    cashgame.AverageBuyin,
                    cashgame.DateString);
        }
    }
}