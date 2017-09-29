using Newtonsoft.Json;
using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.JsonBlock
{
    public class JsonBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public JsonBlockModel(object obj)
        {
            Content = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}