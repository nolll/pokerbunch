using Web.Annotations;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPostModel
    {
        public string Name { get; [UsedImplicitly] set; }
	}
}