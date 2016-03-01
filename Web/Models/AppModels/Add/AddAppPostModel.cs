using JetBrains.Annotations;

namespace Web.Models.AppModels.Add
{
    public class AddAppPostModel
    {
        public string AppName { get; [UsedImplicitly] set; }
    }
}