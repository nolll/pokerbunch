namespace Web.Components.ApiDocsModels
{
    public class DocsPageHeadingBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public DocsPageHeadingBlockModel(string content)
        {
            Content = content;
        }
    }
}