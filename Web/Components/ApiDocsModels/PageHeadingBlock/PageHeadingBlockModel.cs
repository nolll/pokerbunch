using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.PageHeadingBlock
{
    public class PageHeadingBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public PageHeadingBlockModel(string content)
        {
            Content = content;
        }
    }
}