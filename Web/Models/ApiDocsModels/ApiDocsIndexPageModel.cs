using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
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

        public ApiDocsIndexPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new PageHeadingBlockModel("Api Documentation"),
                new ContentBlockModel("You can build your own applications that interact with Poker Bunch, by using the Poker Bunch API. You'll find everything you need to know right here.")),

            new SectionModel(
                new SectionHeadingBlockModel("Posting Data"),
                new ContentBlockModel("The content type of all POST request has to be application/x-www-form-urlencoded.")),

            new SectionModel(
                new SectionHeadingBlockModel("Detailed documentation"),
                new NavigationBlockModel())
        };
    }
}