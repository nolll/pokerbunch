using Newtonsoft.Json;

namespace Web.Components.ApiDocsModels
{
    public class DocsJsonBlockModel : DocsBlockModel
    {
        public string Content { get; }

        public DocsJsonBlockModel(object obj)
        {
            Content = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}