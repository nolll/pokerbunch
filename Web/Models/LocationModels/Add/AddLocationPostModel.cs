using Web.Annotations;

namespace Web.Models.LocationModels.Add
{
    public class AddLocationPostModel
    {
        public string Name { get; [UsedImplicitly] set; }
    }
}