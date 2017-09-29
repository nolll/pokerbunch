using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels;
using Web.Components.ApiDocsModels.CodeBlock;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.JsonBlock;
using Web.Components.ApiDocsModels.Section;
using Web.Components.ApiDocsModels.SectionHeadingBlock;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsBunchesPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Bunches";
        private const string BunchListUrl = ApiBunchesUrl.Route;
        private const string BunchDetailsUrl = ApiBunchUrl.Route;

        public ApiDocsBunchesPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new SectionHeadingBlockModel("Bunch List"),
                new ContentBlockModel("To see the bunches you can access, call"),
                new CodeBlockModel($"GET {BunchListUrl}"),
                new ContentBlockModel("The response will look something like this"),
                new JsonBlockModel(
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

            new SectionModel(
                new SectionHeadingBlockModel("Bunch Details"),
                new ContentBlockModel("To see a specific bunch, call"),
                new CodeBlockModel($"GET {BunchDetailsUrl}"),
                new ContentBlockModel("The response will look like this"),
                new JsonBlockModel(
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