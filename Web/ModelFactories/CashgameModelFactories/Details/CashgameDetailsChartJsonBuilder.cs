using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.ChartModelFactories;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsChartJsonBuilder : ICashgameDetailsChartJsonBuilder
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IChartValueModelFactory _chartValueModelFactory;
        private readonly ICashgameService _cashgameService;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameDetailsChartJsonBuilder(
            ITimeProvider timeProvider,
            IChartValueModelFactory chartValueModelFactory,
            ICashgameService cashgameService,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _timeProvider = timeProvider;
            _chartValueModelFactory = chartValueModelFactory;
            _cashgameService = cashgameService;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public ChartModel Build(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            
            var players = _cashgameService.GetPlayers(cashgame).OrderBy(o => o.Id).ToList();

            return new ChartModel
                {
                    Columns = GetActionColumns(players),
                    Rows = GetActionRows(homegame, cashgame, players)
                };
        }

        private IList<ChartRowModel> GetActionRows(Homegame homegame, Cashgame cashgame, IList<Player> players)
        {
            var rowModels = new List<ChartRowModel>();
            var results = cashgame.Results.OrderBy(o => o.PlayerId);
            foreach (var result in results)
            {
                var totalBuyin = 0;
                var checkpoints = result.Checkpoints;
                var playerId = result.PlayerId;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += checkpoint.Amount;
                    }
                    var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone);
                    rowModels.Add(GetActionRow(players, localTime, checkpoint.Stack - totalBuyin, playerId));
                }
            }
            if (cashgame.Status == GameStatus.Running)
            {
                rowModels.Add(GetCurrentStacks(homegame.Timezone, results));
            }
            return rowModels;
        }

        private ChartRowModel GetCurrentStacks(TimeZoneInfo timeZone, IEnumerable<CashgameResult> results)
        {
            var timestamp = TimeZoneInfo.ConvertTime(_timeProvider.GetTime(), timeZone);
            var values = new List<ChartValueModel> {_chartValueModelFactory.Create(timestamp)};
            values.AddRange(results.Select(result => _chartValueModelFactory.Create(result.Winnings)));
            return new ChartRowModel
                {
                    C = values
                };
        }

        private IList<ChartColumnModel> GetActionColumns(IList<Player> players)
        {
            var columnModels = new List<ChartColumnModel> { new ChartDateTimeColumnModel("Time", "HH:mm") };
            columnModels.AddRange(players.Select(player => new ChartNumberColumnModel(player.DisplayName)));
            return columnModels;
        }

        private ChartRowModel GetActionRow(IList<Player> players, DateTime dateTime, int winnings, int currentPlayerId)
        {
            var values = new List<ChartValueModel> {_chartValueModelFactory.Create(dateTime)};
            foreach (var player in players)
            {
                string val = null;
                if (player.Id == currentPlayerId)
                {
                    val = winnings.ToString(CultureInfo.InvariantCulture);
                }
                values.Add(_chartValueModelFactory.Create(val));
            }
            return new ChartRowModel
                {
                    C = values
                };
        }
    }
}