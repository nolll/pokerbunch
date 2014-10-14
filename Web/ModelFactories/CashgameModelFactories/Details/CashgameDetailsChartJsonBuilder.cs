using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Details;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsChartJsonBuilder : ICashgameDetailsChartJsonBuilder
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ICashgameService _cashgameService;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameDetailsChartJsonBuilder(
            ITimeProvider timeProvider,
            ICashgameService cashgameService,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _timeProvider = timeProvider;
            _cashgameService = cashgameService;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public ChartModel Build(string slug, string dateStr)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch, dateStr);
            if (cashgame == null)
                throw new HttpException(404, "Cashgame not found");
            
            var players = _cashgameService.GetPlayers(cashgame).OrderBy(o => o.Id).ToList();

            var columns = GetActionColumns(players);
            var rows = GetActionRows(bunch, cashgame, players);

            return new DetailsChartModel(columns, rows);
        }

        private IList<ChartRowModel> GetActionRows(Bunch bunch, Cashgame cashgame, IList<Player> players)
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
                    var localTime = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
                    rowModels.Add(GetActionRow(players, localTime, checkpoint.Stack - totalBuyin, playerId));
                }
            }
            if (cashgame.Status == GameStatus.Running)
            {
                rowModels.Add(GetCurrentStacks(bunch.Timezone, results));
            }
            return rowModels;
        }

        private ChartRowModel GetCurrentStacks(TimeZoneInfo timeZone, IEnumerable<CashgameResult> results)
        {
            var timestamp = TimeZoneInfo.ConvertTime(_timeProvider.GetTime(), timeZone);
            var values = new List<ChartValueModel> { new ChartDateTimeValueModel(timestamp) };
            values.AddRange(results.Select(result => new ChartIntValueModel(result.Winnings)));
            return new ChartRowModel(values);
        }

        private IList<ChartColumnModel> GetActionColumns(IList<Player> players)
        {
            var columnModels = new List<ChartColumnModel> { new ChartDateTimeColumnModel("Time", "HH:mm") };
            columnModels.AddRange(players.Select(player => new ChartNumberColumnModel(player.DisplayName)));
            return columnModels;
        }

        private ChartRowModel GetActionRow(IList<Player> players, DateTime dateTime, int winnings, int currentPlayerId)
        {
            var values = new List<ChartValueModel> { new ChartDateTimeValueModel(dateTime) };
            foreach (var player in players)
            {
                string val = null;
                if (player.Id == currentPlayerId)
                {
                    val = winnings.ToString(CultureInfo.InvariantCulture);
                }
                values.Add(new ChartValueModel(val));
            }
            return new ChartRowModel(values);
        }
    }
}