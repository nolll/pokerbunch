using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.SectionHeadingBlock
{
    public class SectionHeadingBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public SectionHeadingBlockModel(string content)
        {
            Content = content;
        }
    }
}