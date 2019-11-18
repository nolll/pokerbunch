using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Components.ApiDocsModels.CodeBlock;
using Web.Components.ApiDocsModels.ContentBlock;
using Web.Components.ApiDocsModels.PageHeadingBlock;
using Web.Components.ApiDocsModels.Section;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsAuthPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Authentication";
        private const string TokenUrl = ApiRoutes.Token.Get;

        public ApiDocsAuthPageModel(AppSettings appSettings, CoreContext.Result contextResult)
            : base(appSettings, contextResult)
        {
        }

        public override IList<SectionModel> Sections => new List<SectionModel>
        {
            new SectionModel(
                new PageHeadingBlockModel("Authentication"),
                new ContentBlockModel("To authenticate, your application needs to post a request to"),
                new CodeBlockModel($"POST {TokenUrl}"),
                new ContentBlockModel("Send the following parameters in the body:"),
                new CodeBlockModel(
                    "grant_type = password",
                    "client_id = { your API key}",
                    "username = {your user's username}",
                    "password = {your user's password}"),
                new ContentBlockModel("If your credentials are valid, the response will include a token, for example"),
                new CodeBlockModel("ABCDE"),
                new ContentBlockModel("For subsequent requests, include an Authorization header with the content"),
                new CodeBlockModel("bearer ABCDE"))
        };

}
}