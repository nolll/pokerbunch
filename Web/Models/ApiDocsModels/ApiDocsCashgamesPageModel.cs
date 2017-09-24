using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsCashgamesPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Cashgames";
        private static string CurrentCashgamesUrl => new ApiCurrentCashgamesUrl().Relative;

        public ApiDocsCashgamesPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<DocsSectionModel> Sections => new List<DocsSectionModel>
        {
            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("Current cashgames"),
                new DocsContentBlockModel("List current cashgames"),
                new DocsCodeBlockModel($"GET {CurrentCashgamesUrl}"),
                new DocsContentBlockModel("This will return a list of running cashgames"),
                new DocsJsonBlockModel(
                    new []
                    {
                        new
                        {
                            id = "1234",
                            url = "https://api.pokerbunch.lan/cashgames/1234"
                        }
                    }),
                new DocsContentBlockModel("If there are no games, the list will be empty"))
        };
    }
}