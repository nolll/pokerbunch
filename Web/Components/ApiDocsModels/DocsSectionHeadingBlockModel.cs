namespace Web.Components.ApiDocsModels
{
    public class DocsSectionHeadingBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public DocsSectionHeadingBlockModel(string content)
        {
            Content = content;
        }
    }
}