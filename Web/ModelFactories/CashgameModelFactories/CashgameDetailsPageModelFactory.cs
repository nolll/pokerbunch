﻿using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Details;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameDetailsPageModelFactory : ICashgameDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public CashgameDetailsPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public CashgameDetailsPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, List<int> years, bool isManager, Cashgame runningGame = null)
        {
            var dateStr = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
            var numResults = cashgame.Results.Count;
            
            var model = new CashgameDetailsPageModel
                {
                    BrowserTitle = "Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    Heading = string.Format("Cashgame {0}", dateStr),
			        Location = cashgame.Location,
                    Duration = Globalization.FormatDuration(cashgame.Duration),
			        DurationEnabled = cashgame.Duration > 0,
                    ShowStartTime = showStartTime,
			        StartTime = showStartTime ? Globalization.FormatTime(cashgame.StartTime.Value) : null,
			        ShowEndTime = showEndTime,
			        EndTime = showEndTime ? Globalization.FormatTime(cashgame.EndTime.Value) : null,
                    Status = GameStatusName.GetName(cashgame.Status),
                    CashgameDetailsTableModel = new CashgameDetailsTableModel(homegame, cashgame),
                    EnablePublish = PublishButtonVisible(isManager, cashgame.Status, numResults),
                    EnableUnpublish = UnpublishButtonVisible(isManager, cashgame.Status),
                    EnableEdit = isManager,
                    EnableCheckpointsButton = cashgame.IsInGame(player),
                    PublishUrl = new CashgamePublishUrlModel(homegame, cashgame),
                    UnpublishUrl = new CashgameUnpublishUrlModel(homegame, cashgame),
                    EditUrl = new CashgameEditUrlModel(homegame, cashgame),
                    CheckpointsUrl = new CashgameActionUrlModel(homegame, cashgame, player),
                    ChartDataUrl = new CashgameDetailsChartJsonUrlModel(homegame, cashgame)
                };

            return model;
        }

        private bool PublishButtonVisible(bool isManager, GameStatus status, int numResults)
        {
            return isManager && status == GameStatus.Finished && numResults >= 2;
        }

        private bool UnpublishButtonVisible(bool isManager, GameStatus status)
        {
            return isManager && status == GameStatus.Published;
        }


    }
}