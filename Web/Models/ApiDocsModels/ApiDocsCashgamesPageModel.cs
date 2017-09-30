using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels.CodeBlock;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.JsonBlock;
using Web.Components.ApiDocsModels.ParameterBlock;
using Web.Components.ApiDocsModels.Section;
using Web.Components.ApiDocsModels.SectionHeadingBlock;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsCashgamesPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Cashgames";
        private const string CurrentCashgamesUrl = ApiBunchCashgamesCurrentUrl.Route;
        private const string BuyinUrl = ApiCashgameBuyinUrl.Route;
        private const string ReportUrl = ApiCashgameReportUrl.Route;
        private const string CashoutUrl = ApiCashgameCashoutUrl.Route;
        private const string EndUrl = ApiCashgameEndUrl.Route;

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
                            id = "1234",
                            url = "https://api.pokerbunch.lan/cashgames/1234"
                        }
                    }),
                new ContentBlockModel("If there are no games, the list will be empty")),

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
                        playerId = "1234",
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
                        playerId = "1234",
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
                        playerId = "1234",
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
                        playerId = "1234",
                        stack = 123
                    })),
            new SectionModel(
                new SectionHeadingBlockModel("End cashgame"),
                new ContentBlockModel("When all players have cashed out, the game can be ended"),
                new CodeBlockModel($"POST {EndUrl}"))
        };
    }
}