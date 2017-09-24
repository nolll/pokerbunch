using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Components.ApiDocsModels;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsIndexPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation";
        private static string AppListUrl => new UserAppsUrl().Relative;

        public ApiDocsIndexPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<DocsSectionModel> Sections => new List<DocsSectionModel>
        {
            new DocsSectionModel(
                new DocsPageHeadingBlockModel("Api Documentation"),
                new DocsContentBlockModel("You can build your own applications that interact with Poker Bunch, by using the Poker Bunch API. You'll find everything you need to know right here.")),

            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("API Key"),
                new DocsContentBlockModel($"The first thing you need to know is that you will need an <a href=\"{AppListUrl}\">API Key</a> to access the API.")),

            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("Posting Data"),
                new DocsContentBlockModel("The content type of all POST request has to be application/x-www-form-urlencoded.")),

            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("Detailed documentation"),
                new DocsNavigationBlockModel())
        };
    }
}