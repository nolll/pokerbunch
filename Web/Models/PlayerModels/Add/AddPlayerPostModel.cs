using System.ComponentModel.DataAnnotations;
using Web.Annotations;

namespace Web.Models.PlayerModels.Add
{
    public class AddPlayerPostModel
    {
        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; [UsedImplicitly] set; }
	}
}