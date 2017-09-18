using System.Collections.Generic;
using Core.UseCases;
using Web.Components.ApiDocsModels;
 using Web.Urls.ApiUrls;
using Web.Urls.SiteUrls;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsBunchesPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Bunches";
        private static string BunchListUrl => new ApiBunchListUrl().Absolute;
        private static string BunchDetailsUrl => new BunchDetailsUrl("{id}").Relative;

        public ApiDocsBunchesPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<DocsSectionModel> Sections => new List<DocsSectionModel>
        {
            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("Bunch List"),
                new DocsContentBlockModel("To see the bunches you can access, call"),
                new DocsCodeBlockModel(BunchListUrl),
                new DocsContentBlockModel("The response will look something like this"),
                new DocsJsonBlockModel(
                    new { id = 1, name = "test" }),
                new DocsJsonBlockModel(
                    new []
                    {
                        new
                        {
                            id = "mypokergame",
                            name = "My Poker Game",
                            description = "Description of my poker game",
                            defaultBuyin = 100
                        }
                    })),

            new DocsSectionModel(
                new DocsSectionHeadingBlockModel("Bunch Details"),
                new DocsContentBlockModel("To see a specific bunch, call"),
                new DocsCodeBlockModel(BunchDetailsUrl),
                new DocsContentBlockModel("The response will look like this"),
                new DocsJsonBlockModel(
                    new {
                        id = "mypokergame",
                        name = "My Poker Game",
                        description = "Description of my poker game",
                        houseRules = "House Rules",
                        timezone = "W. Europe Standard Time",
                        currencySymbol = "$",
                        currencyLayout = "{SYMBOL}{AMOUNT}",
                        defaultBuyin = 100,
                        player = new {
                            id = 1,
                            name = "Your Player Name",
                        },
                        role = "player"
                    }))
        };
    }
}