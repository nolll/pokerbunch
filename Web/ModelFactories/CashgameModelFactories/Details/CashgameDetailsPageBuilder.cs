using System;
using System.Web;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsPageBuilder : ICashgameDetailsPageBuilder
    {
        private readonly ICashgameDetailsTableModelFactory _cashgameDetailsTableModelFactory;
        private readonly IGlobalization _globalization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;
        private readonly IBunchContextInteractor _contextInteractor;

        public CashgameDetailsPageBuilder(
            ICashgameDetailsTableModelFactory cashgameDetailsTableModelFactory,
            IGlobalization globalization,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            IBunchContextInteractor contextInteractor)
        {
            _cashgameDetailsTableModelFactory = cashgameDetailsTableModelFactory;
            _globalization = globalization;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
            _contextInteractor = contextInteractor;
        }

        public CashgameDetailsPageModel Build(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            var user = _auth.CurrentUser;
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            
            var date = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new CashgameDetailsPageModel(contextResult)
                {
                    Heading = string.Format("Cashgame {0}", date),
			        Location = cashgame.Location,
                    Duration = _globalization.FormatDuration(cashgame.Duration),
			        DurationEnabled = cashgame.Duration > 0,
                    ShowStartTime = showStartTime,
			        StartTime = showStartTime ? _globalization.FormatTime(TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, homegame.Timezone)) : null,
			        ShowEndTime = showEndTime,
                    EndTime = showEndTime ? _globalization.FormatTime(TimeZoneInfo.ConvertTime(cashgame.EndTime.Value, homegame.Timezone)) : null,
                    Status = GameStatusName.GetName(cashgame.Status),
                    CashgameDetailsTableModel = _cashgameDetailsTableModelFactory.Create(homegame, cashgame),
                    EnableEdit = isManager,
                    EnableCheckpointsButton = cashgame.IsInGame(player.Id),
                    EditUrl = new EditCashgameUrl(homegame.Slug, cashgame.DateString),
                    CheckpointsUrl = new CashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id),
                    ChartDataUrl = new CashgameDetailsChartJsonUrl(homegame.Slug, cashgame.DateString)
                };
        }
    }
}