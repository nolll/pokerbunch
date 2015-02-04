using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Core.UseCases.CashgameDetailsChart
{
    public class CashgameDetailsChartInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameDetailsChartInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public CashgameDetailsChartResult Execute(CashgameDetailsChartRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = GetCashgame(bunch, request.DateStr);
            var playerIds = cashgame.Results.Select(result => result.PlayerId).ToList();
            var players = _playerRepository.GetList(playerIds).OrderBy(o => o.Id).ToList();

            var playerItems = GetPlayerItems(bunch, cashgame, players, request.CurrentTime);

            return new CashgameDetailsChartResult(playerItems);
        }

        private Cashgame GetCashgame(Bunch bunch, string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
                return _cashgameRepository.GetRunning(bunch.Id);
            return _cashgameRepository.GetByDateString(bunch.Id, dateStr);
        }

        private static IList<DetailsChartPlayerItem> GetPlayerItems(Bunch bunch, Cashgame cashgame, IEnumerable<Player> players, DateTime now)
        {
            var playerItems = new List<DetailsChartPlayerItem>();
            foreach (var player in players)
            {
                var result = cashgame.GetResult(player.Id);
                var resultItems = new List<DetailsChartResultItem>();
                var totalBuyin = 0;
                var checkpoints = result.Checkpoints;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += checkpoint.Amount;
                    }
                    var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
                    var winnings = checkpoint.Stack - totalBuyin;
                    resultItems.Add(new DetailsChartResultItem(localTime, winnings));
                }
                if (cashgame.Status == GameStatus.Running)
                {
                    var timestamp = TimeZoneInfo.ConvertTime(now, bunch.Timezone);
                    resultItems.Add(new DetailsChartResultItem(timestamp, result.Winnings));
                }
                playerItems.Add(new DetailsChartPlayerItem(player.Id, player.DisplayName, resultItems));
            }
            return playerItems;
        }
    }
}