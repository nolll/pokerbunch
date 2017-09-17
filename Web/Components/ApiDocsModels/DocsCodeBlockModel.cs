namespace Web.Components.ApiDocsModels
{
    public class DocsCodeBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public DocsCodeBlockModel(params string[] content)
        {
            Content = string.Join("\n", content);
        }
    }
}