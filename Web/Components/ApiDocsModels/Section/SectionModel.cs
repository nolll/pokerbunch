using System.Collections.Generic;
using Web.Components.ApiDocsModels.Block;
using Web.Extensions;

namespace Web.Components.ApiDocsModels.Section
{
    public class SectionModel : Component
    {
        public IEnumerable<DocsBlockModel> Blocks { get; }

        public SectionModel(params DocsBlockModel[] blocks)
        {
            Blocks = blocks;
        }
    }
}