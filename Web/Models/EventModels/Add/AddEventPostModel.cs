using Web.Annotations;

namespace Web.Models.EventModels.Add
{
    public class AddEventPostModel
    {
        public string Name { get; [UsedImplicitly] set; }
    }
}