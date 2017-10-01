using Newtonsoft.Json;
using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.JsonBlock
{
    public class JsonBlockModel : DocsBlockModel
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Content { get; }

        public JsonBlockModel(object obj)
        {

            Content = JsonConvert.SerializeObject(obj, Formatting.Indented, JsonSettings);
        }
    }
}