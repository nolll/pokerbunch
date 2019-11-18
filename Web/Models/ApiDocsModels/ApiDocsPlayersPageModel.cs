using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Routes;
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
        private const string PlayersUrl = ApiRoutes.Player.ListByBunch;

        public ApiDocsPlayersPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
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