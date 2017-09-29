using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.CodeBlock
{
    public class CodeBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public CodeBlockModel(params string[] content)
        {
            Content = string.Join("\n", content);
        }
    }
}