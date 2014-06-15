using Application.Services;
using Core.Repositories;

namespace Application.UseCases.Actions
{
    public class ActionsInteractor : IActionsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGlobalization _globalization;

        public ActionsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IGlobalization globalization)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _globalization = globalization;
        }

        public ActionsResult Execute(ActionsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, request.DateStr);
            var player = _playerRepository.GetById(request.PlayerId);

            var date = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;

            return new ActionsResult
                {
                    Date = date,
                    PlayerName = player.DisplayName
                };
        }
    }
}