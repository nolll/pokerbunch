using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels.CodeBlock;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.JsonBlock;
using Web.Components.ApiDocsModels.Section;
using Web.Components.ApiDocsModels.SectionHeadingBlock;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsPlayersPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Players";
        private const string PlayersUrl = ApiBunchPlayersUrl.Route;

        public ApiDocsPlayersPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new SectionHeadingBlockModel("Players"),
                new ContentBlockModel("List bunch members"),
                new CodeBlockModel($"GET {PlayersUrl}"),
                new ContentBlockModel("This will return a list of players"),
                new JsonBlockModel(
                    new[]
                    {
                        new
                        {
                            id = "1234",
                            name = "Player Name",
                            color = "#9e9e9e"
                        }
                    }))
        };
    }
}