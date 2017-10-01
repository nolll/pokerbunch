using System;
using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels.CodeBlock;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.JsonBlock;
using Web.Components.ApiDocsModels.ParameterBlock;
using Web.Components.ApiDocsModels.Section;
using Web.Components.ApiDocsModels.SectionHeadingBlock;
using Web.Extensions;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsCashgamesPageModel : ApiDocsPageModel
    {
        private const string BunchId = "cashgame-id";
        private const string CashgameId = "cashgame-id";
        private const string PlayerId = "player-id";
        private const string LocationId = "location-id";
        private const string EventId = "event-id";

        private const string PlayerName = "player-name";
        private const string LocationName = "location-name";

        public override string BrowserTitle => "Api Documentation - Current Cashgames";
        private const string CurrentCashgamesUrl = ApiBunchCashgamesCurrentUrl.Route;
        private const string AddCashgameUrl = ApiBunchCashgamesUrl.Route;
        private const string CashgameDetailsUrl = ApiCashgameUrl.Route;
        private const string BuyinUrl = ApiCashgameBuyinUrl.Route;
        private const string ReportUrl = ApiCashgameReportUrl.Route;
        private const string CashoutUrl = ApiCashgameCashoutUrl.Route;
        private const string EndUrl = ApiCashgameEndUrl.Route;
        private const string CashgamesUrl = ApiBunchCashgamesUrl.Route;
        private const string CashgamesWithYearUrl = ApiBunchCashgamesUrl.RouteWithYear;

        public ApiDocsCashgamesPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new SectionHeadingBlockModel("Current cashgames"),
                new ContentBlockModel("List current cashgames"),
                new CodeBlockModel($"GET {CurrentCashgamesUrl}"),
                new ContentBlockModel("This will return a list of running cashgames"),
                new JsonBlockModel(
                    new[]
                    {
                        new
                        {
                            id = CashgameId,
                            url = new ApiCashgameUrl(CashgameId).Absolute()
                        }
                    }),
                new ContentBlockModel("If there are no games, the list will be empty")),

            new SectionModel(
                new SectionHeadingBlockModel("Start a cashgame"),
                new ContentBlockModel("You can start a new game like this"),
                new CodeBlockModel($"POST {AddCashgameUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("locationId", ParameterTypeModel.String, "A location id"),
                    new ParameterModel("eventId", ParameterTypeModel.String, "An event id (optional)")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        locationId = LocationId,
                        eventId = EventId
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("Cashgame details"),
                new ContentBlockModel("View cashgame details"),
                new CodeBlockModel($"GET {CashgameDetailsUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("locationId", ParameterTypeModel.String, "A location id"),
                    new ParameterModel("eventId", ParameterTypeModel.String, "An event id (optional)")),
                new ContentBlockModel("This will return something like this"),
                new JsonBlockModel(
                    new
                    {
                        id = CashgameId,
                        isRunning =  false,
                        startTime = DateTime.Parse("2017-09-20T23:37:44.217Z"),
                        updatedTime = DateTime.Parse("2017-09-20T23:37:59.467Z"),
                        bunch = new
                        {
                            id = BunchId,
                            timezone = "W. Europe Standard Time",
                            currencyFormat = "{0} kr",
                            currencySymbol = "kr",
                            currencyLayout = "{AMOUNT} {SYMBOL}",
                            thousandSeparator = " ",
                            culture = "sv-SE",
                            role = "manager"
                        },
                        location = new
                        {
                            id = LocationId,
                            name = LocationName
                        },
                        players = new[]
                        {
                            new
                            {
                                id = PlayerId,
                                name = PlayerName,
                                color = "#f44336",
                                startTime = DateTime.Parse("2017-09-20T23:37:44.217Z"),
                                updatedTime = DateTime.Parse("2017-09-20T23:37:59.467Z"),
                                buyin = 200,
                                stack = 125,
                                actions = new[]
                                {
                                    new
                                    {
                                        id = "21463",
                                        type = "buyin",
                                        time = "2017-09-20T23:37:44.217Z",
                                        stack = 200,
                                        added = (int?)200
                                    },
                                    new
                                    {
                                        id = "21464",
                                        type = "report",
                                        time = "2017-09-20T23:37:50.413Z",
                                        stack = 130,
                                        added = (int?)null
                                    },
                                    new
                                    {
                                        id = "21465",
                                        type = "cashout",
                                        time = "2017-09-20T23:37:59.467Z",
                                        stack = 125,
                                        added = (int?)null
                                    }
                                }
                            }
                        }
                    }
                )),
            new SectionModel(
                new SectionHeadingBlockModel("Join a cashgame"),
                new ContentBlockModel("To join a game, just buy in, like this"),
                new CodeBlockModel($"POST {BuyinUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("playerId", ParameterTypeModel.String, "Player Id"),
                    new ParameterModel("added", ParameterTypeModel.Integer, "Amount to bring to table")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        playerId = CashgameId,
                        added = 200
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("Add money"),
                new ContentBlockModel("The buyin endpoint can also be used to add money"),
                new CodeBlockModel($"POST {BuyinUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("playerId", ParameterTypeModel.String, "Player Id"),
                    new ParameterModel("added", ParameterTypeModel.Integer, "Amount to add"),
                    new ParameterModel("stack", ParameterTypeModel.Integer, "Stack size before adding money")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        playerId = CashgameId,
                        added = 200,
                        stack = 0
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("Report stack size"),
                new ContentBlockModel("You can report your stack size like this"),
                new CodeBlockModel($"POST {ReportUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("playerId", ParameterTypeModel.String, "Player Id"),
                    new ParameterModel("stack", ParameterTypeModel.Integer, "Your current stack size")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        playerId = CashgameId,
                        stack = 123
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("Leave a cashgame"),
                new ContentBlockModel("To leave a cashgame, you call cashout"),
                new CodeBlockModel($"POST {CashoutUrl}"),
                new ContentBlockModel("Parameters"),
                new ParametersBlockModel(
                    new ParameterModel("playerId", ParameterTypeModel.String, "Player Id"),
                    new ParameterModel("stack", ParameterTypeModel.Integer, "Your current stack size")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        playerId = CashgameId,
                        stack = 123
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("End cashgame"),
                new ContentBlockModel("When all players have cashed out, the game can be ended"),
                new CodeBlockModel($"POST {EndUrl}")),
            new SectionModel(
                new SectionHeadingBlockModel("Finished cashgames"),
                new ContentBlockModel("List all finished cashgames. They can also be filtered by year"),
                new CodeBlockModel(
                    $"GET {CashgamesUrl}",
                    $"GET {CashgamesWithYearUrl}"),
                new ContentBlockModel("This will return a list of finished cashgames"),
                new JsonBlockModel(
                    new[]
                    {
                        new
                        {
                            id = CashgameId,
                            startTime = DateTime.Parse("2017-09-20T23:37:44.217Z"),
                            updatedTime = DateTime.Parse("2017-09-20T23:37:59.467Z"),
                            location = new
                            {
                                id = LocationId,
                                name = LocationName
                            },
                            players = new[]
                            {
                                new
                                {
                                    id = "1",
                                    startTime = DateTime.Parse("2017-09-20T23:37:44.217Z"),
                                    updatedTime = DateTime.Parse("2017-09-20T23:37:59.467Z"),
                                    buyin = 200,
                                    stack = 125
                                }
                            }
                        }
                    }),
                new ContentBlockModel("If there are no games, the list will be empty"))
        };
    }
}