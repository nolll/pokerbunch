using System.Collections.Generic;
using Web.Extensions;

namespace Web.Components.ApiDocsModels
{
    public class DocsSectionModel : Component
    {
        public IEnumerable<DocsBlockModel> Blocks { get; }

        public DocsSectionModel(params DocsBlockModel[] blocks)
        {
            Blocks = blocks;
        }
    }
}