namespace Web.InlineCode
{
    public class GoogleAnalyticsScript : InlineScriptHtml
    {
        private const string Code = "UA-8453410-7";
        protected override string Content => $"window.ga=window.ga||function(){{(ga.q=ga.q||[]).push(arguments)}};ga.l=+new Date;ga('create', '{Code}', 'auto');ga('send', 'pageview');";
    }
}