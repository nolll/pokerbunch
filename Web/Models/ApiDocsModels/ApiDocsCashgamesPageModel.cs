using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels;
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
                    new ParameterModel("added", ParameterTypeModel.Integer, "Amount to add")),
                new ContentBlockModel("Example"),
                new JsonBlockModel(
                    new
                    {
                        playerId = "1234",
                        added = 200
                    })
            )
        };
    }
}