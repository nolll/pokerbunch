using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.ApiUrls;
using Web.Urls.SiteUrls;

namespace Web.Models.AppModels.Details
{
    public class ApiDocsPageModel : AppPageModel
    {
        public string RunningGameUrl { get; }
        public string BuyinUrl { get; }
        public string ReportUrl { get; }
        public string CashoutUrl { get; }
        public IList<DocsSectionModel> Sections { get; }

        public ApiDocsPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
            const string slug = "bunch-short-name";
            RunningGameUrl = new ApiRunningGameUrl(slug).Absolute;
            BuyinUrl = new ApiBuyinUrl(slug).Absolute;
            ReportUrl = new ApiReportUrl(slug).Absolute;
            CashoutUrl = new ApiCashoutUrl(slug).Absolute;

            var appListUrl = new UserAppsUrl().Relative;
            var tokenUrl = new TokenUrl().Relative;
            var bunchListUrl = new ApiBunchListUrl().Absolute;
            var bunchDetailsUrl = new BunchDetailsUrl("{id}").Relative;

            Sections = new List<DocsSectionModel>
            {
                new DocsSectionModel(
                    new DocsPageHeadingBlockModel("Api Documentation"),
                    new DocsContentBlockModel("You can build your own applications that interact with Poker Bunch, by using the Poker Bunch API. You'll find everything you need to know right here.")),

                new DocsSectionModel(
                    new DocsSectionHeadingBlockModel("API Key"),
                    new DocsContentBlockModel($"The first thing you need to know is that you will need an <a href=\"{appListUrl}\">API Key</a> to access the API.")),

                new DocsSectionModel(
                    new DocsSectionHeadingBlockModel("Posting Data"),
                    new DocsContentBlockModel("The content type of all POST request has to be application/x-www-form-urlencoded.")),

                new DocsSectionModel(
                    new DocsSectionHeadingBlockModel("Authentication"),
                    new DocsContentBlockModel("To authenticate, your application needs to post a request to"),
                    new DocsCodeBlockModel($"POST {tokenUrl}"),
                    new DocsContentBlockModel("Send the following parameters in the body:"),
                    new DocsCodeBlockModel(
                        "grant_type = password",
                        "client_id = { your API key}",
                        "username = {your user's username}",
                        "password = {your user's password}"),
                    new DocsContentBlockModel("If your credentials are valid, the response will include a token, for example"),
                    new DocsCodeBlockModel("ABCDE"),
                    new DocsContentBlockModel("For subsequent requests, include an Authorization header with the content"),
                    new DocsCodeBlockModel("bearer ABCDE")),

                new DocsSectionModel(
                    new DocsSectionHeadingBlockModel("Bunch List"),
                    new DocsContentBlockModel("To see the bunches you can access, call"),
                    new DocsCodeBlockModel(bunchListUrl),
                    new DocsContentBlockModel("The response will look something like this"),
                    new DocsCodeBlockModel(
                        "[",
                        "    {",
                        "        \"id\": \"mypokergame\",",
                        "        \"name\": \"My Poker Game\",",
                        "        \"description\": \"Description of my poker game\",",
                        "        \"defaultBuyin\": 100",
                        "    }",
                        "]")),

                new DocsSectionModel(
                    new DocsSectionHeadingBlockModel("Bunch Details"),
                    new DocsContentBlockModel("To see a specific bunch, call"),
                    new DocsCodeBlockModel(bunchDetailsUrl),
                    new DocsContentBlockModel("The response will look like this"),
                    new DocsCodeBlockModel(
                        "[",
                        "    {",
                        "        \"id\": \"mypokergame\",",
                        "        \"name\": \"My Poker Game\",",
                        "        \"description\": \"Description of my poker game\",",
                        "        \"houseRules\": \"House Rules\",",
                        "        \"timezone\": \"W. Europe Standard Time\",",
                        "        \"currencySymbol\": \"$\",",
                        "        \"currencyLayout\": \"{SYMBOL}{AMOUNT}\",",
                        "        \"defaultBuyin\": 100,",
                        "        {",
                        "            \"id\": 1,",
                        "            \"name\": \"Your Player Name\"",
                        "        },",
                        "        \"role\": \"player\"",
                        "    }",
                        "]"))
            };
        }

        public override string BrowserTitle => "Api Documentation";

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/ApiDocs.cshtml");
        }
    }

    public class DocsSectionModel : IViewModel
    {
        public IEnumerable<DocsBlockModel> Blocks { get; }

        public DocsSectionModel(params DocsBlockModel[] blocks)
        {
            Blocks = blocks;
        }

        public View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/DocsSection.cshtml");
        }
    }

    public class DocsContentBlockModel : DocsBlockModel
    {
        public override string Content { get; }

        public DocsContentBlockModel(string content)
        {
            Content = content;
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/DocsContentBlock.cshtml");
        }
    }

    public class DocsPageHeadingBlockModel : DocsBlockModel
    {
        public override string Content { get; }

        public DocsPageHeadingBlockModel(string content)
        {
            Content = content;
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/DocsPageHeadingBlock.cshtml");
        }
    }

    public class DocsSectionHeadingBlockModel : DocsBlockModel
    {
        public override string Content { get; }

        public DocsSectionHeadingBlockModel(string content)
        {
            Content = content;
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/DocsSectionHeadingBlock.cshtml");
        }
    }

    public class DocsCodeBlockModel : DocsBlockModel
    {
        public override string Content { get; }

        public DocsCodeBlockModel(params string[] content)
        {
            Content = string.Join("\n", content);
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/DocsCodeBlock.cshtml");
        }
    }

    public abstract class DocsBlockModel : IViewModel
    {
        public abstract string Content { get; }
        
        public abstract View GetView();
    }
}