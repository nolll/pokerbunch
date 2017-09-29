using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.ContentBlock
{
    public class ContentBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public ContentBlockModel(string content)
        {
            Content = content;
        }
    }
}