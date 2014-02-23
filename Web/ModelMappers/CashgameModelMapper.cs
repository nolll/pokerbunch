using Application.Factories;
using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelMappers
{
    public class CashgameModelMapper : ICashgameModelMapper
    {
        private readonly ICashgameFactory _cashgameFactory;

        public CashgameModelMapper(ICashgameFactory cashgameFactory)
        {
            _cashgameFactory = cashgameFactory;
        }

        public Cashgame Map(Cashgame cashgame, CashgameEditPostModel postModel)
        {
            return _cashgameFactory.Create(
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
                    cashgame.AverageBuyin,
                    cashgame.DateString);
        }
    }
}