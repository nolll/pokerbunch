using System.Collections.Generic;
using Core.UseCases;
using PokerBunch.Common.Urls.ApiUrls;
using Web.Components.ApiDocsModels;

namespace Web.Models.ApiDocsModels
{
    public class ApiDocsAuthPageModel : ApiDocsPageModel
    {
        public override string BrowserTitle => "Api Documentation - Authentication";
        private static string TokenUrl => new ApiTokenUrl().Relative;

        public ApiDocsAuthPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
        }

        public override IList<DocsSectionModel> Sections => new List<DocsSectionModel>
        {
            new DocsSectionModel(
                new DocsPageHeadingBlockModel("Authentication"),
                new DocsContentBlockModel("To authenticate, your application needs to post a request to"),
                new DocsCodeBlockModel($"POST {TokenUrl}"),
                new DocsContentBlockModel("Send the following parameters in the body:"),
                new DocsCodeBlockModel(
                    "grant_type = password",
                    "client_id = { your API key}",
                    "username = {your user's username}",
                    "password = {your user's password}"),
                new DocsContentBlockModel("If your credentials are valid, the response will include a token, for example"),
                new DocsCodeBlockModel("ABCDE"),
                new DocsContentBlockModel("For subsequent requests, include an Authorization header with the content"),
                new DocsCodeBlockModel("bearer ABCDE"))
        };

}
}