using Web.Urls;

namespace Web.Components.ApiDocsModels
{
    public class DocsContentBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public DocsContentBlockModel(string content)
        {
            Content = content;
        }
    }
}