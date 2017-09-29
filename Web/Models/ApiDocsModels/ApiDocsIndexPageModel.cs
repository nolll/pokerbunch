using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Components.ApiDocsModels;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.NavigationBlock;
using Web.Components.ApiDocsModels.PageHeadingBlock;
using Web.Components.ApiDocsModels.Section;
using Web.Components.ApiDocsModels.SectionHeadingBlock;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsIndexPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation";
        private const string AppListUrl = UserAppsUrl.Route;

        public ApiDocsIndexPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new PageHeadingBlockModel("Api Documentation"),
                new ContentBlockModel("You can build your own applications that interact with Poker Bunch, by using the Poker Bunch API. You'll find everything you need to know right here.")),

            new SectionModel(
                new SectionHeadingBlockModel("API Key"),
                new ContentBlockModel($"The first thing you need to know is that you will need an <a href=\"{AppListUrl}\">API Key</a> to access the API.")),

            new SectionModel(
                new SectionHeadingBlockModel("Posting Data"),
                new ContentBlockModel("The content type of all POST request has to be application/x-www-form-urlencoded.")),

            new SectionModel(
                new SectionHeadingBlockModel("Detailed documentation"),
                new NavigationBlockModel())
        };
    }
}